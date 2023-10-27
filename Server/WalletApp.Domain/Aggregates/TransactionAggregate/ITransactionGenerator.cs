namespace WalletApp.Domain.Aggregates.TransactionAggregate;

public interface ITransactionGenerator
{
    public Task<List<Transaction>> GenerateAsync(Guid userId, Guid requestedUserId, int transactionsCount = 0);
}