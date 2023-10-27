using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WalletApp.Domain.Aggregates.CardAggregate;
using WalletApp.Domain.Aggregates.IconAggregate;
using WalletApp.Domain.Aggregates.TransactionAggregate;
using WalletApp.Domain.Aggregates.UserAggregate;

namespace WalletApp.Infrastructure.DataAccess.Database;

public class WalletAppContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public WalletAppContext(DbContextOptions<WalletAppContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Icon> Icons { get; set; }
}