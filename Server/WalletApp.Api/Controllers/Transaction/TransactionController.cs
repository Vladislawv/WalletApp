using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletApp.Api.Controllers.Transaction.Dto;
using WalletApp.Application.Queries;

namespace WalletApp.Api.Controllers.Transaction;

[ApiController]
[Authorize]
[Route("api/transactions")]
[Produces(MediaTypeNames.Application.Json)]
public class TransactionController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Returns transaction with specified id.
    /// </summary>
    /// <param name="id">The transaction id.</param>
    /// <response code="200">If operation was successful.</response>
    /// <response code="404">If transaction was not found.</response>
    /// <returns>Transaction.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute, Required] Guid id)
    {
        var transaction = await _mediator.Send(new GetTransactionQuery(id));
        return Ok(transaction.ToDto());
    }
}