using Microsoft.EntityFrameworkCore;
using WalletApp.DAL.Models.Cards;
using WalletApp.DAL.Models.Transactions;

namespace WalletApp.DAL.Models.Users;

[Index(nameof(Id))]
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Card> Cards { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; }
}