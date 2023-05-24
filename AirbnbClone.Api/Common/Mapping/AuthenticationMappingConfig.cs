using AirbnbClone.Application.Authentication.Commands.Register;
using AirbnbClone.Application.Authentication.Common;
using AirbnbClone.Application.Authentication.Queries.Login;
using AirbnbClone.Contracts.Authentication;
using Mapster;

namespace AirbnbClone.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);
    }
}
