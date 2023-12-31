﻿using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletApp.Api.Controllers.Card.Dto;
using WalletApp.Application.Contracts;
using WalletApp.Application.CQRS.Commands;
using WalletApp.Application.CQRS.Queries;

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
    /// Returns the card for given card id.
    /// </summary>
    /// <param name="id">The card id.</param>
    /// <response code="200">If operation was successful.</response>
    /// <response code="404">If card was not found.</response>
    /// <returns>Card.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute, Required] Guid id)
    {
        var card = await _mediator.Send(new GetCardQuery(id));
        return Ok(card.ToDto());
    }

    /// <summary>
    /// Create card with random data including 10 random transactions for specific user with given userId.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <response code="200">If operation was successful.</response>
    /// <response code="400">If operation was wrong.</response>
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