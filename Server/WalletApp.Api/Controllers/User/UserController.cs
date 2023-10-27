using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletApp.Api.Controllers.User.Dto;
using WalletApp.Application.Queries;

namespace WalletApp.Api.Controllers.User;

[ApiController]
[Authorize]
[Route("api/users")]
[Produces(MediaTypeNames.Application.Json)]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get user by Id.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <response code="200">If operation was successful.</response>
    /// <response code="404">If user was not found.</response>
    /// <returns>User.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute, Required] Guid id)
    {
        var user = await _mediator.Send(new GetUserQuery(id));
        return Ok(user.ToDto());
    }
}