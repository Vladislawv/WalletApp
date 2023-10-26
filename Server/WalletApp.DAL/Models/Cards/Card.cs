using Microsoft.EntityFrameworkCore;
using WalletApp.DAL.Models.Transactions;

namespace WalletApp.DAL.Models.Cards;

[Index(nameof(Id))]
[Index(nameof(UserId))]
public class Card
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public double Total { get; set; }
    public double Available { get; set; }
    public bool IsPaymentRequired { get; set; }
    public string Description { get; set; }
    public string DailyPoints { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; }
}