using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WalletApp.Domain.CardAggregate;
using WalletApp.Domain.TransactionAggregate;

namespace WalletApp.Domain.UserAggregate;

[Index(nameof(Id))]
public class User : IdentityUser<Guid>
{
    public virtual ICollection<Card> Cards { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; }
}