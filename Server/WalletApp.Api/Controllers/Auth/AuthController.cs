using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletApp.Api.Controllers.Auth.Requests;

namespace WalletApp.Api.Controllers.Auth;

[ApiController]
[Route("api/auth")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Register the user in the system.
    /// </summary>
    /// <param name="registerRequest">Request that includes username, email and password.</param>
    /// <response code="200">If registration was successful.</response>
    /// <response code="400">If registration was fail.</response>
    /// <returns>Response status code.</returns>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest registerRequest)
    {
        await _mediator.Send(registerRequest.ToCommand());
        return Ok();
    }

    /// <summary>
    /// Login user into the system.
    /// </summary>
    /// <param name="authUserInfoRequest">Request that includes username or email and password.</param>
    /// <response code="200">If login was successful.</response>
    /// <response code="400">If login was fail.</response>
    /// <returns>Authentication info for the given user.</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginAsync([FromBody] GetAuthUserInfoRequest authUserInfoRequest)
    {
        var authUserInfo = await _mediator.Send(authUserInfoRequest.ToQuery());
        return Ok(authUserInfo.ToDto());
    }
}