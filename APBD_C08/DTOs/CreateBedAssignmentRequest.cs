namespace APBD_C08.DTOs;
using System;
using System.ComponentModel.DataAnnotations;


public class CreateBedAssignmentRequest
{
    [Required]
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    [Required]
    public string BedType { get; set; } = null!;

    [Required]
    public string Ward { get; set; } = null!;
}