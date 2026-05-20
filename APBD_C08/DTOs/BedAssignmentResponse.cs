namespace APBD_C08.DTOs;

public class BedAssignmentResponse
{
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public string BedType { get; set; } = null!;
    public string Ward { get; set; } = null!;
}