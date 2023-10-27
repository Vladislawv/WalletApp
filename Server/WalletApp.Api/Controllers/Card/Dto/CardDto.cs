using WalletApp.Api.Controllers.Transaction.Dto;

namespace WalletApp.Api.Controllers.Card.Dto;

public class CardDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Total { get; set; }
    public string Available { get; set; }
    public bool IsPaymentRequired { get; set; }
    public string Description { get; set; }

    public virtual ICollection<TransactionDto> Transactions { get; set; }
}