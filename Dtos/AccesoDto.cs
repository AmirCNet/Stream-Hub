namespace StreamHub.Dtos;

public class AccessResultDto
{
    public string Message { get; set; } = string.Empty;
    
    public string? StreamUrl { get; set; }
    
    public string? SubscriptionStatus { get; set; }
}