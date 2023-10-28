using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WalletApp.Domain.Aggregates.CardAggregate;

namespace WalletApp.Domain.Aggregates.UserAggregate;

[Index(nameof(Id))]
public class User : IdentityUser<Guid>
{
    public string DailyPoints { get; set; }
    public DateTime CreatedOn { get; set; }
    
    public virtual ICollection<Card> Cards { get; set; }
}