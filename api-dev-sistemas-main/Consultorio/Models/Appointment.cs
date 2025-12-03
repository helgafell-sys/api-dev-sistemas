using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultorio.Models;

public class Appointment
{
    public int Id { get; set; }

    [Required]
    public DateTime AppointmentDate { get; set; }

    [Required]
    [StringLength(500)]
    public string Reason { get; set; } = string.Empty;

    [Required]
    public int PatientId { get; set; }

    [ForeignKey(nameof(PatientId))]
    public Patient? Patient { get; set; }
}