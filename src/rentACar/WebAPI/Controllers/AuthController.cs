using Application.Features.Auths.Commands.Login;
using Application.Features.Auths.Commands.Register;
using Core.Security.Dtos;
using Core.Security.JWT;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
    {
        LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto };
        AccessToken result = await Mediator.Send(loginCommand);
        return Ok(result);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
    {
        RegisterCommand registerCommand = new() { UserForRegisterDto = userForRegisterDto };
        AccessToken result = await Mediator.Send(registerCommand);
        return Created("", result);
    }
}