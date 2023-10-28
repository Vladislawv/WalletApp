using WalletApp.Api.Controllers.Card.Dto;
using UserDomain = WalletApp.Domain.Aggregates.UserAggregate.User;
using CardDomain = WalletApp.Domain.Aggregates.CardAggregate.Card;

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
            Cards = user.Cards.ToDto()
        };
    }

    private static List<CardDto> ToDto(this ICollection<CardDomain> cards)
    {
        return cards.Select(x => x.ToDto()).ToList();
    }
}