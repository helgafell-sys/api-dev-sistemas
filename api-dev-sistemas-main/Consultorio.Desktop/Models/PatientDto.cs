namespace Consultorio.Desktop.Models;

public class PatientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class PatientCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class PatientUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}