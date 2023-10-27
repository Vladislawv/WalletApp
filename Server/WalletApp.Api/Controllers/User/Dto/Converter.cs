using WalletApp.Api.Controllers.Card.Dto;
using WalletApp.Api.Controllers.Transaction.Dto;
using UserDomain = WalletApp.Domain.Aggregates.UserAggregate.User;
using CardDomain = WalletApp.Domain.Aggregates.CardAggregate.Card;
using TransactionDomain = WalletApp.Domain.Aggregates.TransactionAggregate.Transaction;

namespace WalletApp.Api.Controllers.User.Dto;

public static class Converter
{
    public static UserDto ToDto(this UserDomain user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email,
            DailyPoints = user.DailyPoints,
            CreatedOn = user.CreatedOn,
            Cards = user.Cards.ToDto(),
            Transactions = user.Transactions.ToDto()
        };
    }

    private static List<CardDto> ToDto(this ICollection<CardDomain> cards)
    {
        return cards.Select(x => x.ToDto()).ToList();
    }

    private static List<TransactionDto> ToDto(this ICollection<TransactionDomain> transactions)
    {
        return transactions.Select(x => x.ToDto()).ToList();
    }
}