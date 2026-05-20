using APBD_C08.DTOs;
using APBD_C08.Excpetions;
using APBD_C08.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD_C08.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController(IPatientsService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(string? s, CancellationToken cancellationToken)
    {
        return Ok(await service.GetPatientsAsync(s, cancellationToken));
    }

    [HttpPost("{pesel}/bedassignments")]
    public async Task<IActionResult> AssignBed(string pesel, [FromBody] CreateBedAssignmentRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await service.AssignBedAsync(pesel, request, cancellationToken));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}