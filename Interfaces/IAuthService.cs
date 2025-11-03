using StreamHub.Dtos;
namespace StreamHub.Interfaces;

public interface IAuthService
{
    string CreateToken(CreateTokenDto createTokenDto);
    string? Login(LoginDto loginDto);
}