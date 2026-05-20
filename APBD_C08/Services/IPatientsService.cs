using APBD_C08.DTOs;

namespace APBD_C08.Services;

public interface IPatientsService
{
    Task<IEnumerable<PatientDetailsResponse>> GetPatientsAsync(string? search, CancellationToken cancellationToken);
    Task<BedAssignmentResponse> AssignBedAsync(string pesel, CreateBedAssignmentRequest request, CancellationToken cancellationToken);

}