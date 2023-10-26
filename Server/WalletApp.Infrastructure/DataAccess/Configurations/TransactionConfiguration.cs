using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletApp.DAL.Models.Cards;
using WalletApp.DAL.Models.Transactions;

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