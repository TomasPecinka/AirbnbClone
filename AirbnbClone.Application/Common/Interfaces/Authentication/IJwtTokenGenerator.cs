using AirbnbClone.Domain.Entities;

namespace AirbnbClone.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}