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

    [HttpPost]
    public async Task<IActionResult> AddBedd()
    {
        return Ok();
    }


}