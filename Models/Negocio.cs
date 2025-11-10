namespace StreamHub.Models
{
    public class Negocio
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Cuit { get; set; } = string.Empty;
        public string ApiUrl { get; set; } = string.Empty; 
        public string ApiKey { get; set; } = string.Empty; 
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}