namespace APBD_C08.DTOs;

public class PatientDetailsResponse
{
    public string Pesel { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int Age { get; set; }
    public string Sex { get; set; } = null!; // "Male" / "Female"
    public IEnumerable<AdmissionDto> Admissions { get; set; } = new List<AdmissionDto>();
    public IEnumerable<BedAssignmentDto> BedAssignments { get; set; } = new List<BedAssignmentDto>();
}