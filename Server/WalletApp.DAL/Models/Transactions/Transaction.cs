using Microsoft.EntityFrameworkCore;
using WalletApp.DAL.Models.Cards;
using WalletApp.DAL.Models.Users;

namespace WalletApp.DAL.Models.Transactions;

[Index(nameof(Id))]
[Index(nameof(CardId))]
public class Transaction
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CardId { get; set; }
    public Guid IconId { get; set; }
    public TransactionType TransactionType { get; set; }
    public double Total { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsPending { get; set; }

    public virtual User User { get; set; }
    public virtual Card Card { get; set; }
}