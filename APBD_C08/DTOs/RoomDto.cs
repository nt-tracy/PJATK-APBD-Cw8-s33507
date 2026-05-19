namespace APBD_C08.DTOs;

public class RoomDto
{
    public string Id { get; set; } = null!; // Zgodnie z diagramem ERD varchar(4)
    public bool HasTv { get; set; }
    public WardDto Ward { get; set; } = null!;
}