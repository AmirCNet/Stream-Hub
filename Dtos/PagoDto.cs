namespace StreamHub.Dtos
{
    public class PagoDto
    {
        public string FacturaId { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string? CallbackUrl { get; set; }
    }
}