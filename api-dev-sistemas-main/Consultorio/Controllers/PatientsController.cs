using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Consultorio.Data;
using Consultorio.Models;

namespace Consultorio.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PatientsController(AppDbContext context)
    {
        _context = context;
    }

    // GET /api/v1/patients
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientReadDto>>> GetAll()
    {
        var patients = await _context.Patients.ToListAsync();
        return Ok(patients.Select(p => new PatientReadDto(p.Id, p.Name, p.Email)));
    }

    // GET /api/v1/patients/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PatientReadDto>> GetById(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
            return NotFound(new { message = "Paciente não encontrado" });

        return Ok(new PatientReadDto(patient.Id, patient.Name, patient.Email));
    }

    // POST /api/v1/patients
    [HttpPost]
    public async Task<ActionResult<PatientReadDto>> Create(PatientCreateDto dto)
    {
        var patient = new Patient { Name = dto.Name, Email = dto.Email };
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = patient.Id },
            new PatientReadDto(patient.Id, patient.Name, patient.Email));
    }

    // PUT /api/v1/patients/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PatientUpdateDto dto)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
            return NotFound(new { message = "Paciente não encontrado" });

        patient.Name = dto.Name ?? patient.Name;
        patient.Email = dto.Email ?? patient.Email;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE /api/v1/patients/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
            return NotFound(new { message = "Paciente não encontrado" });

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}