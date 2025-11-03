namespace StreamHub.Dtos;

public class SuscripcionDto
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public string? TipoSuscripcion { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string? Estado { get; set; }
}

public class SuscripcionCreateDto
{
    public int IdUsuario { get; set; }
    public string TipoSuscripcion { get; set; } = string.Empty;
    public DateTime? FechaFin { get; set; }
    public string? Estado { get; set; }
}
