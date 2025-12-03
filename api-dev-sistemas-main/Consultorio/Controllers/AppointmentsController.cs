using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Consultorio.Data;
using Consultorio.Models;

namespace Consultorio.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AppointmentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentReadDto>>> GetAll()
    {
        var appointments = await _context.Appointments
            .Include(a => a.Patient)
            .ToListAsync();

        return Ok(appointments.Select(a => new AppointmentReadDto(
            a.Id, a.AppointmentDate, a.Reason, a.PatientId, a.Patient!.Name)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentReadDto>> GetById(int id)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appointment == null)
            return NotFound(new { message = "Consulta não encontrada" });

        return Ok(new AppointmentReadDto(
            appointment.Id, appointment.AppointmentDate, appointment.Reason,
            appointment.PatientId, appointment.Patient!.Name));
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentReadDto>> Create(AppointmentCreateDto dto)
    {
        var patient = await _context.Patients.FindAsync(dto.PatientId);
        if (patient == null)
            return BadRequest(new { message = "Paciente não encontrado" });

        var appointment = new Appointment
        {
            AppointmentDate = dto.AppointmentDate,
            Reason = dto.Reason,
            PatientId = dto.PatientId
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = appointment.Id },
            new AppointmentReadDto(appointment.Id, appointment.AppointmentDate,
                appointment.Reason, appointment.PatientId, patient.Name));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AppointmentUpdateDto dto)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
            return NotFound(new { message = "Consulta não encontrada" });

        appointment.AppointmentDate = dto.AppointmentDate;
        appointment.Reason = dto.Reason;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
            return NotFound(new { message = "Consulta não encontrada" });

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}