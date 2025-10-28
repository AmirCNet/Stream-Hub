namespace StreamHub.Dtos;

// DTO para la respuesta del endpoint GET /content/{id}/play
public class AccessResultDto
{
    public string Message { get; set; } = string.Empty;
    
    public string? StreamUrl { get; set; }
    
    public string? SubscriptionStatus { get; set; }
}
