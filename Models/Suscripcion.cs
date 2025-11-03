namespace StreamHub.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Modelo de Suscripción con relación 1:1 con Usuario
public class Suscripcion
{
    public int Id { get; set; }

    // Clave foránea hacia Usuario (relación 1:1)
    public int UsuarioId { get; set; }

    // Tipo de plan (p.ej., Básico, Premium, etc.)
    public string? Plan { get; set; }

    public DateTime FechaInicio { get; set; }

    // Puede ser nula si la suscripción está activa sin fecha de expiración fijada
    public DateTime? FechaExpiracion { get; set; }

    // Estado: Activa, Cancelada, Pendiente
    public string? Estado { get; set; }

    // Propiedad de navegación 1:1
    [ForeignKey("UsuarioId")]
    public Usuario? Usuario { get; set; }
}