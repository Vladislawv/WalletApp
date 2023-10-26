using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WalletApp.DAL.Models.Cards;
using WalletApp.DAL.Models.Icons;
using WalletApp.DAL.Models.Transactions;
using WalletApp.DAL.Models.Users;

namespace WalletApp.Infrastructure.DataAccess.Database;

public class WalletAppContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public WalletAppContext(DbContextOptions<WalletAppContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Icon> Icons { get; set; }
}