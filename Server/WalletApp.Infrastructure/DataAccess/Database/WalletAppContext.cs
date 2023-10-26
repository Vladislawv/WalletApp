using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WalletApp.Domain.IconAggregate;
using WalletApp.Domain.CardAggregate;
using WalletApp.Domain.TransactionAggregate;
using WalletApp.Domain.UserAggregate;

namespace WalletApp.Infrastructure.DataAccess.Database;

public class WalletAppContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public WalletAppContext(DbContextOptions<WalletAppContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Icon> Icons { get; set; }
}