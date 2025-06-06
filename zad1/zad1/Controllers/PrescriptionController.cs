namespace zad1.Controllers;

using zad1.DTOs;
using zad1.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PrescriptionsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionRequestDto dto)
    {
        try
        {
            await _dbService.AddPrescriptionAsync(dto);
            return Ok("Recepta została dodana.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("patient/{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        var result = await _dbService.GetPatientDetailsAsync(id);
        if (result == null)
            return NotFound("Pacjent nie istnieje");

        return Ok(result);
    }
}
