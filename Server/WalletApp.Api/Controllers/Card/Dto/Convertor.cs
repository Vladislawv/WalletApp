using System.Globalization;
using WalletApp.Api.Controllers.Transaction.Dto;
using CardDomain = WalletApp.Domain.CardAggregate.Card;
using TransactionDomain = WalletApp.Domain.TransactionAggregate.Transaction;

namespace WalletApp.Api.Controllers.Card.Dto;

public static class Convertor
{
    private static readonly CultureInfo US_CULTURE = new("en-US");
    
    public static CardDto ToDto(this CardDomain card)
    {
        return new CardDto
        {
            Id = card.Id,
            UserId = card.UserId,
            Total = card.Total.ToString("C", US_CULTURE),
            Available = card.Available.ToString("C", US_CULTURE),
            IsPaymentRequired = card.IsPaymentRequired,
            Description = card.Description,
            Transactions = card.Transactions.ToDto()
        };
    }

    private static List<TransactionDto> ToDto(this ICollection<TransactionDomain> transactions)
    {
        return transactions.Select(x => x.ToDto()).ToList();
    }
}