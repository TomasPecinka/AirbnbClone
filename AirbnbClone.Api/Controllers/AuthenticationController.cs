using Microsoft.AspNetCore.Mvc;
using AirbnbClone.Contracts.Authentication;
using AirbnbClone.Domain.Common.Errors;
using AirbnbClone.Application.Authentication.Queries.Login;
using AirbnbClone.Application.Authentication.Commands.Register;
using MediatR;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;

namespace AirbnbClone.Api.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

     [Route("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var authenticationResult = await _mediator.Send(command);

        return authenticationResult.Match(
            authenticationResult => Ok(_mapper.Map<AuthenticationResponse>(authenticationResult)),
            errors => Problem(errors));
    }

    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var authenticationResult = await _mediator.Send(query);

        if(authenticationResult.IsError && authenticationResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authenticationResult.FirstError.Description);
        }

        return authenticationResult.Match(
            authenticationResult => Ok(_mapper.Map<AuthenticationResponse>(authenticationResult)),
            errors => Problem(errors));
    }
}