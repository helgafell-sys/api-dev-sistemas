using System.ComponentModel.DataAnnotations;

namespace Consultorio.Models;

public class Patient
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Relacionamento 1:N
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}