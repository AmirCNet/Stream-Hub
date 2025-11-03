using System.ComponentModel.DataAnnotations;

namespace StreamHub.Dtos;

public class SubscriptionCreateDto
{
    public Guid UserId { get; set; }
    
    public string PlanType { get; set; } = string.Empty;
}
