namespace WalletApp.Domain.TransactionAggregate;

public interface ITransactionDataSource
{
    public IQueryable<Transaction> Transactions { get; }
}