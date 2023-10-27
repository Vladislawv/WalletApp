using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletApp.Application.Commands;
using WalletApp.Application.Contracts;

namespace WalletApp.Api.Controllers.Card;

[ApiController]
[Authorize]
[Route("api/cards")]
[Produces(MediaTypeNames.Application.Json)]
public class CardController : ControllerBase
{
    private const string TOKEN_NAME = "access_token";
    
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthTokenManager _authTokenManager;

    public CardController(IMediator mediator, IHttpContextAccessor httpContextAccessor, IAuthTokenManager authTokenManager)
    {
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
        _authTokenManager = authTokenManager;
    }

    /// <summary>
    /// Create card with random data for specific user with given userId.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <response code="200">If operation was successful.</response>
    /// <response code="404">If user was not found.</response>
    /// <returns>Status code of the operation.</returns>
    [HttpPost("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromRoute, Required] Guid userId)
    {
        var token = await _httpContextAccessor.HttpContext.GetTokenAsync(TOKEN_NAME);
        var requestedUserId = _authTokenManager.ParseUserIdFromToken(token);
        
        var createCardCommand = new CreateCardCommand(userId, requestedUserId);
        await _mediator.Send(createCardCommand);
        
        return Ok();
    }
}