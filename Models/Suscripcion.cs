namespace StreamHub.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Suscripcion
{
    public int Id { get; set; }
    public string? IdUsuario { get; set; }
    public string? TipoSuscripcion { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public String? Estado { get; set; }

    [ForeignKey("IdUsuario")]
    public Usuario? User { get; set; }
}