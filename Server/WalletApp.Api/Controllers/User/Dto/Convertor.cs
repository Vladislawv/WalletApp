using UserDomain = WalletApp.Domain.UserAggregate.User;

namespace WalletApp.Api.Controllers.User.Dto;

public static class Convertor
{
    public static UserDto ToDto(this UserDomain user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.UserName,
            DailyPoints = user.DailyPoints
        };
    }
}