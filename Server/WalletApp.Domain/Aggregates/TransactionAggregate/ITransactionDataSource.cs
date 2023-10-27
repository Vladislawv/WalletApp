namespace WalletApp.Domain.Aggregates.TransactionAggregate;

public interface ITransactionDataSource
{
    public IQueryable<Transaction> Transactions { get; }
}