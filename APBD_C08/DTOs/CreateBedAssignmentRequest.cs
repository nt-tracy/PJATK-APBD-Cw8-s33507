namespace APBD_C08.DTOs;
using System;
using System.ComponentModel.DataAnnotations;


public class CreateBedAssignmentRequest
{
    [Required(ErrorMessage = "WardId is required.")]
    public int WardId { get; set; }

    [Required(ErrorMessage = "BedTypeId is required.")]
    public int BedTypeId { get; set; }

    [Required(ErrorMessage = "Start date (From) is required.")]
    public DateTime From { get; set; }

    public DateTime? To { get; set; }
}