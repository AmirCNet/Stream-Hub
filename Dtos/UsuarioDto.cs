using System.ComponentModel.DataAnnotations;

namespace StreamHub.Dtos;

public class UserRegisterDto
{
    public string Name { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
    
    public string InitialPlanType { get; set; } = string.Empty;
}
