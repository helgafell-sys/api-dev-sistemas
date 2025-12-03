using System.ComponentModel.DataAnnotations;

namespace Consultorio.Models;

public record PatientReadDto(int Id, string Name, string Email);

public record PatientCreateDto(
    [property: Required, StringLength(100)] string Name,
    [property: Required, EmailAddress, StringLength(150)] string Email
);

public record PatientUpdateDto(
    [property: StringLength(100)] string? Name,
    [property: EmailAddress, StringLength(150)] string? Email
);