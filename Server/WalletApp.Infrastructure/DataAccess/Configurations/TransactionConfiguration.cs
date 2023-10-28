using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletApp.Domain.Aggregates.CardAggregate;
using WalletApp.Domain.Aggregates.TransactionAggregate;
using WalletApp.Domain.Aggregates.UserAggregate;

namespace WalletApp.Infrastructure.DataAccess.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasOne<Card>(transaction => transaction.Card)
            .WithMany(card => card.Transactions)
            .HasForeignKey(transaction => transaction.CardId);
    }
}