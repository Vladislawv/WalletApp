using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletApp.Domain.CardAggregate;
using WalletApp.Domain.UserAggregate;

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