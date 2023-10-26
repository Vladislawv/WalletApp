using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletApp.DAL.Models.Cards;
using WalletApp.DAL.Models.Users;

namespace WalletApp.Infrastructure.DataAccess.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasOne<User>(card => card.User)
            .WithMany(user => user.Cards)
            .HasForeignKey(card => card.UserId);
    }
}