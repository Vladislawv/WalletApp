namespace WalletApp.Domain.TransactionAggregate;

public interface ITransactionService
{
    public IEnumerable<Transaction> Generate(Guid userId, int transactionsCount = 0);
}