namespace WalletApp.Domain.TransactionAggregate;

public interface ITransactionService
{
    public Task<List<Transaction>> GenerateAsync(Guid userId, Guid requestedUserId, int transactionsCount = 0);
}