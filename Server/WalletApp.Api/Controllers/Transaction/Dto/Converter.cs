using System.Globalization;
using WalletApp.Domain.Aggregates.TransactionAggregate;
using TransactionDomain = WalletApp.Domain.Aggregates.TransactionAggregate.Transaction;

namespace WalletApp.Api.Controllers.Transaction.Dto;

public static class Converter
{
    private static readonly CultureInfo US_CULTURE = new("en-US");

    public static TransactionDto ToDto(this TransactionDomain transaction)
    {
        var transactionTotal = transaction.Total.ToString("C", US_CULTURE);

        return new TransactionDto
        {
            Id = transaction.Id,
            CardId = transaction.CardId,
            IconId = transaction.IconId,
            TransactionType = transaction.TransactionType.ToString(),
            Total = transaction.TransactionType is TransactionType.Payment ? $"+{transactionTotal}" : transactionTotal,
            Name = transaction.Name,
            Description = transaction.Description,
            Date = transaction.Date,
            CreatedOn = transaction.CreatedOn,
            IsPending = transaction.IsPending
        };
    }
}