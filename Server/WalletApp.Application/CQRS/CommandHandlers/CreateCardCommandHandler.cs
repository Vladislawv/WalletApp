﻿using System.Globalization;
using WalletApp.Application.CQRS.Abstractions;
using WalletApp.Application.CQRS.Commands;
using WalletApp.Domain.Aggregates.CardAggregate;
using WalletApp.Domain.Aggregates.TransactionAggregate;

namespace WalletApp.Application.CQRS.CommandHandlers;

public class CreateCardCommandHandler : ICommandHandler<CreateCardCommand>
{
    private const int MAX_TOTAL = 1500;
    private const bool IS_PAYMENT_REQUIRED = false;
    
    private readonly ICardRepository _cardRepository;
    private readonly ITransactionGenerator _transactionGenerator;

    public CreateCardCommandHandler(ICardRepository cardRepository, ITransactionGenerator transactionGenerator)
    {
        _cardRepository = cardRepository;
        _transactionGenerator = transactionGenerator;
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
            Transactions = await _transactionGenerator.GenerateAsync(request.UserId, request.RequestedUserId)
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
        var currentMonth = DateTime.UtcNow.ToString("MMMM", CultureInfo.InvariantCulture);
        
        return !IS_PAYMENT_REQUIRED
            ? $"You've paid your {currentMonth} balance." 
            : $"You need to pay your {currentMonth} balance.";
    }
}