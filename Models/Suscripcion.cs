namespace StreamHub.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Suscripcion
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string? Plan { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public string? Estado { get; set; }

    [ForeignKey("UsuarioId")]
    public Usuario? Usuario { get; set; }
}