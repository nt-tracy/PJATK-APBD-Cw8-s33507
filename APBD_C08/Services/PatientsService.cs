using APBD_C08.Context;
using APBD_C08.DTOs;
using APBD_C08.Excpetions;
using APBD_C08.Excpetions;
using APBD_C08.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_C08.Services;

public class PatientsService(MyDbContext ctx) : IPatientsService
{
    public async Task<IEnumerable<PatientDetailsResponse>> GetPatientsAsync(string? search,
        CancellationToken cancellationToken)
    {
        var query = ctx.Patients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            string likePattern = $"%{search}%";
            query = query.Where(p =>
                EF.Functions.Like(p.FirstName, likePattern) ||
                EF.Functions.Like(p.LastName, likePattern));
        }

        return await ctx.Patients.Select(p => new PatientDetailsResponse
        {
            Pesel = p.Pesel,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Age = p.Age,
            Sex = p.Sex ? "Male" : "Female",
            Admissions = p.Admissions.Select(a => new AdmissionDto
            {
                Id = a.Id,
                AdmissionDate = a.AdmissionDate,
                DischargeDate = a.DischargeDate,
                Ward = new WardDto
                {
                    Id = a.Ward.Id,
                    Name = a.Ward.Name,
                    Description = a.Ward.Description
                }
            }).ToList(),
            BedAssignments = p.BedAssignments.Select(ba => new BedAssignmentDto
            {
                Id = ba.Id,
                From = ba.From,
                To = ba.To,
                Bed = new BedDto
                {
                    Id = ba.Bed.Id,
                    BedType = new BedTypeDto
                    {
                        Id = ba.Bed.BedType.Id,
                        Name = ba.Bed.BedType.Name,
                        Description = ba.Bed.BedType.Description
                    },
                    Room = new RoomDto
                    {
                        Id = ba.Bed.Room.Id,
                        HasTv = ba.Bed.Room.HasTv,
                        Ward = new WardDto
                        {
                            Id = ba.Bed.Room.Ward.Id,
                            Name = ba.Bed.Room.Ward.Name,
                            Description = ba.Bed.Room.Ward.Description
                        }
                    }
                }
            }).ToList()
        }).ToListAsync(cancellationToken);
    }


    public async Task<BedAssignmentResponse> AssignBedAsync(string pesel, CreateBedAssignmentRequest request,
        CancellationToken cancellationToken)
    {
        var patientExists = await ctx.Patients.AnyAsync(p => p.Pesel == pesel, cancellationToken);
        if (!patientExists)
        {
            throw new NotFoundException($"Patient with PESEL {pesel} does not exist.");
        }

        var ward = await ctx.Wards.FirstOrDefaultAsync(w => w.Name == request.Ward, cancellationToken);
        if (ward == null)
        {
            throw new NotFoundException($"Ward '{request.Ward}' does not exist.");
        }

        var bedType = await ctx.BedTypes.FirstOrDefaultAsync(bt => bt.Name == request.BedType, cancellationToken);
        if (bedType == null)
        {
            throw new NotFoundException($"Bed type '{request.BedType}' does not exist.");
        }

        var candidateBeds = await ctx.Beds
            .Where(b => b.BedTypeId == bedType.Id && b.Room.WardId == ward.Id)
            .ToListAsync(cancellationToken);

        if (!candidateBeds.Any())
        {
            throw new NotFoundException($"No beds found for type '{request.BedType}' in ward '{request.Ward}'.");
        }

        Bed? availableBed = null;
        DateTime requestTo = request.To ?? DateTime.MaxValue;

        foreach (var bed in candidateBeds)
        {
            var isOccupied = await ctx.BedAssignments
                .AnyAsync(ba => ba.BedId == bed.Id &&
                                ba.From < requestTo &&
                                (ba.To ?? DateTime.MaxValue) > request.From,
                    cancellationToken);

            if (!isOccupied)
            {
                availableBed = bed;
                break;
            }
        }

        if (availableBed == null)
        {
            throw new NotFoundException(
                $"All beds of type '{request.BedType}' in ward '{request.Ward}' are occupied during the requested period.");
        }

        var newAssignment = new BedAssignment
        {
            PatientPesel = pesel,
            BedId = availableBed.Id,
            From = request.From,
            To = request.To
        };

        await ctx.BedAssignments.AddAsync(newAssignment, cancellationToken);
        await ctx.SaveChangesAsync(cancellationToken);

        return new BedAssignmentResponse
        {
            From = newAssignment.From,
            To = newAssignment.To,
            BedType = bedType.Name,
            Ward = ward.Name
        };
    }
}