using gizmogeo.Application.Auth.Commands.LoginUser;
using gizmogeo.Application.Auth.Commands.LogoutUser;
using gizmogeo.Application.Auth.Commands.RegisterUser;
using gizmogeo.Application.Auth.Commands.ResetPhone;
using gizmogeo.Application.Auth.Commands.TokenCommands.RefreshToken;
using gizmogeo.Application.Auth.Commands.VerifyPhone;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace gizmogeo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var tokens = await _mediator.Send(command);
        return Ok(tokens);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("Verify-code")]
    public async Task<IActionResult> VerifyCode([FromBody] VerifyPhoneCommand command)
    {
        var success = await _mediator.Send(command);
        return Ok(success);
    }

    [AllowAnonymous]
    [HttpPost("reset-phone")]
    public async Task<IActionResult> Logout(ResetPhoneCommand command)
    {
        await _mediator.Send(command);
        return Ok("Succesfully Logged Out");
    }



    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var newToken = await _mediator.Send(command);
        return Ok(newToken);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _mediator.Send(new LogoutUserCommand());
        return Ok("Succesfully Logged Out");
    }
}
