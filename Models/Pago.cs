namespace StreamHub.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public string FacturaId { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string Estado { get; set; } = "Pendiente"; 
        public string? ApiPagoId { get; set; }   
        public string? CallbackUrl { get; set; }  
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}