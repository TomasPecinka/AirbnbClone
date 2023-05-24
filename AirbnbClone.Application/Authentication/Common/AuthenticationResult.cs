using AirbnbClone.Domain.Entities;

namespace AirbnbClone.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);