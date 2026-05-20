namespace APBD_C08.DTOs;

public class BedAssignmentResponse
{
    public int Id { get; set; }
    public string PatientPesel { get; set; } = string.Empty;
    
    public int BedId { get; set; }
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    
    public BedDto Bed { get; set; } = null!;
}