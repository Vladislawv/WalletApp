﻿using MediatR;
using WalletApp.Application.CQRS.Queries;
using WalletApp.Domain.Aggregates.TransactionAggregate;

namespace WalletApp.Application.Transactions;

public class TransactionGenerator : ITransactionGenerator
{
    private const int DEFAULT_TRANSACTIONS_COUNT_TO_GENERATE = 10;
    private const int MAX_TOTAL = 10000;

    private static readonly Dictionary<string, string> TRANSACTIONS = new()
    {
        { "Payment", "From JPMorgan Chase Bank National" },
        { "Apple", "Transaction from Apple" },
        { "IKEA", "Transaction from IKEA" },
        { "Target", "Transaction from Target" }
    };

    private readonly IMediator _mediator;

    public TransactionGenerator(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<Transaction>> GenerateAsync(Guid userId, Guid requestedUserId, int transactionsCount = 0)
    {
        var transactions = new List<Transaction>();
        transactionsCount = transactionsCount != 0 ? transactionsCount : DEFAULT_TRANSACTIONS_COUNT_TO_GENERATE;

        for (var count = 0; count < transactionsCount; count++)
        {
            var transactionType = GenerateTransactionType();
            var transactionName = GetName(transactionType);
            var creationDate = DateTime.UtcNow;
            var isPending = GenerateIsPending();
            
            transactions.Add(new Transaction
            {
                TransactionType = transactionType,
                Total = GenerateTotal(),
                Name = transactionName,
                Description = GenerateDescription(isPending, transactionName),
                CreatedOn = creationDate,
                Date = await GetDateAsync(creationDate, userId, requestedUserId),
                IsPending = isPending
            });
        }

        return transactions;
    }

    private static TransactionType GenerateTransactionType()
    {
        var randomType = new Random().Next(0, 5);
        return randomType <= 2 ? TransactionType.Credit : TransactionType.Payment;
    }

    private static double GenerateTotal()
    {
        return new Random().NextDouble() * MAX_TOTAL;
    }

    private static string GenerateDescription(bool isPending, string transactionName)
    {
        var description = TRANSACTIONS[transactionName]; 
        return isPending ? $"Pending - {description}" : description;
    }

    private static string GetName(TransactionType transactionType)
    {
        return transactionType is TransactionType.Payment ? "Payment" : GenerateName();
    }

    private static string GenerateName()
    {
        var transactionNames = TRANSACTIONS.Keys.ToList();
        
        var randomIndex = new Random().Next(0, transactionNames.Count);
        return transactionNames[randomIndex];
    }

    private async Task<string> GetDateAsync(DateTime creationDate, Guid userId, Guid requestedUserId)
    {
        var isTransactionMadeInTheCurrentWeek = (DateTime.UtcNow - creationDate).Days < 7;
        var date = isTransactionMadeInTheCurrentWeek ? creationDate.DayOfWeek.ToString() : creationDate.ToString("M/d/yy");

        var isDifferentUsers = requestedUserId != userId;
        if (isDifferentUsers)
        {
            var requestedUser = await _mediator.Send(new GetUserQuery(requestedUserId));
            date = $"{requestedUser.UserName} - {date}";
        }
        
        return date;
    }

    private static bool GenerateIsPending()
    {
        var randomIndex = new Random().Next(0, 5);
        return randomIndex > 2;
    }
}