namespace StreamHub.Models;

public class Suscripcion
{
    public int Id { get; set; }
    public string? IdUsuario { get; set; }
    public string? TipoSuscripcion { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public String? Estado { get; set; }
}