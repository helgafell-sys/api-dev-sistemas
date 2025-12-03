using System.ComponentModel.DataAnnotations;

namespace Consultorio.Models;

public record AppointmentReadDto(
    int Id, 
    DateTime AppointmentDate, 
    string Reason, 
    int PatientId, 
    string PatientName
);

public record AppointmentCreateDto(
    [property: Required] DateTime AppointmentDate,
    [property: Required, StringLength(500)] string Reason,
    [property: Required] int PatientId
);

public record AppointmentUpdateDto(
    [property: Required] DateTime AppointmentDate,
    [property: Required, StringLength(500)] string Reason
);