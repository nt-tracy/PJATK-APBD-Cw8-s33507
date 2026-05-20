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


    public async Task<BedAssignmentResponse> AssignBedAsync(string pesel, CreateBedAssignmentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
}