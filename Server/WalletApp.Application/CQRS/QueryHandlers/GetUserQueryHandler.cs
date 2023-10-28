using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WalletApp.Application.CQRS.Abstractions;
using WalletApp.Application.CQRS.Commands;
using WalletApp.Application.CQRS.Queries;
using WalletApp.Domain.Aggregates.DailyPointAggregate;
using WalletApp.Domain.Aggregates.UserAggregate;
using WalletApp.Domain.Exceptions;

namespace WalletApp.Application.CQRS.QueryHandlers;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, User>
{
    private readonly UserManager<User> _userManager;
    private readonly IDailyPointCalculationService _dailyPointCalculationService;
    private readonly IMediator _mediator;

    public GetUserQueryHandler(
        IUserDataSource userDataSource, IDailyPointCalculationService dailyPointCalculationService, IMediator mediator)
    {
        _userManager = userDataSource.UserManager;
        _dailyPointCalculationService = dailyPointCalculationService;
        _mediator = mediator;
    }

    public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
                       .Include(x => x.Cards)
                       .ThenInclude(x => x.Transactions)
                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) 
                   ?? throw new NotFoundException($"User with given Id: {request.Id} is not found in the system.");

        user.DailyPoints = _dailyPointCalculationService.Calculate(user.CreatedOn);
        await _mediator.Send(new UserUpdateCommand(user), cancellationToken);
        return user;
    }
}