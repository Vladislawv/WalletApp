using WalletApp.Api.Controllers.Card.Dto;
using WalletApp.Api.Controllers.Transaction.Dto;

namespace WalletApp.Api.Controllers.User.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string DailyPoints { get; set; }
    public DateTime CreatedOn { get; set; }
    public ICollection<CardDto> Cards { get; set; }
    public ICollection<TransactionDto> Transactions { get; set; }
}