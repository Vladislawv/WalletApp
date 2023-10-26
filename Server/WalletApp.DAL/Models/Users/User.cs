using Microsoft.EntityFrameworkCore;
using WalletApp.DAL.Models.Cards;

namespace WalletApp.DAL.Models.Users;

[Index(nameof(Id))]
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Card> Cards { get; set; }
}