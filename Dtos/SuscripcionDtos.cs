namespace StreamHub.Dtos;

public class SuscripcionDto
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string? Plan { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaExpiracion { get; set; }
    public string? Estado { get; set; }
}

public class SuscripcionCreateDto
{
    public int UsuarioId { get; set; }
    public string Plan { get; set; } = string.Empty;
    public DateTime? FechaExpiracion { get; set; }
    public string? Estado { get; set; }
}
