using Shared.InternalMessaging.CQRS.Abstractions;
using WalletApp.Application.Commands;
using WalletApp.Domain.CardAggregate;
using WalletApp.Domain.TransactionAggregate;

namespace WalletApp.Application.CommandHandlers;

public class CreateCardCommandHandler : ICommandHandler<CreateCardCommand>
{
    private const int MAX_TOTAL = 1500;
    private const bool IS_PAYMENT_REQUIRED = false;
    
    private readonly ICardRepository _cardRepository;
    private readonly ITransactionService _transactionService;

    public CreateCardCommandHandler(ICardRepository cardRepository, ITransactionService transactionService)
    {
        _cardRepository = cardRepository;
        _transactionService = transactionService;
    }

    public async Task Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var total = GenerateCardTotal();
        
        var card = new Card
        {
            UserId = request.UserId,
            Total = total,
            Available = MAX_TOTAL - total,
            IsPaymentRequired = IS_PAYMENT_REQUIRED,
            Description = GenerateDescription(),
            Transactions = _transactionService.Generate(request.UserId).ToList()
        };
        
        await _cardRepository.Cards.AddAsync(card, cancellationToken);
        await _cardRepository.DbContext.SaveChangesAsync(cancellationToken);
    }

    private static double GenerateCardTotal()
    {
        return new Random().NextDouble() * MAX_TOTAL;
    }

    private static string GenerateDescription()
    {
        var currentMonth = DateTime.UtcNow.ToString("MMM");
        
        return !IS_PAYMENT_REQUIRED
            ? string.Format("You've paid your {month} balance.", currentMonth)
            : string.Format("You need to pay your {month} balance.", currentMonth);
    }
}