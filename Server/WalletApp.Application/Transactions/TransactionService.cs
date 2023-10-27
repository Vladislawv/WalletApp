using WalletApp.Domain.TransactionAggregate;

namespace WalletApp.Application.Transactions;

public class TransactionService : ITransactionService
{
    private const int DEFAULT_TRANSACTIONS_COUNT_TO_GENERATE = 10;
    private const int MAX_TOTAL = 10000;

    private static readonly Dictionary<string, string> TRANSACTIONS = new()
    {
        { "Apple", "Transaction from Apple" },
        { "IKEA", "Transaction from IKEA" },
        { "Target", "Transaction from Target" }
    };


    public IEnumerable<Transaction> Generate(Guid userId, int transactionsCount = 0)
    {
        transactionsCount = transactionsCount != 0 ? transactionsCount : DEFAULT_TRANSACTIONS_COUNT_TO_GENERATE;

        for (var count = 0; count <= transactionsCount; count++)
        {
            var transactionType = GenerateTransactionType();
            var transactionName = GetName(transactionType);
            var creationDate = DateTime.UtcNow;
            var isPending = GenerateIsPending();
            
            yield return new Transaction
            {
                UserId = userId,
                // TODO IconId = null,
                TransactionType = transactionType,
                Total = GenerateTotal(),
                Name = transactionName,
                Description = GenerateDescription(isPending, transactionName),
                CreatedOn = creationDate,
                Date = GetDate(creationDate),
                IsPending = isPending,
            };
        }
    }

    private static TransactionType GenerateTransactionType()
    {
        var randomType = new Random().Next(0, 2);
        return (TransactionType)randomType;
    }

    private static double GenerateTotal()
    {
        return new Random().NextDouble() * MAX_TOTAL;
    }

    private static string GenerateDescription(bool isPending, string transactionName)
    {
        var description = TRANSACTIONS[transactionName]; 
        return isPending ? $"Pending - {description}" : description;
    }

    private static string GetName(TransactionType transactionType)
    {
        return transactionType is TransactionType.Payment ? "Payment" : GenerateName();
    }

    private static string GenerateName()
    {
        var transactionNames = TRANSACTIONS.Keys.ToList();
        
        var randomIndex = new Random().Next(0, transactionNames.Count);
        return transactionNames[randomIndex];
    }

    private static string GetDate(DateTime creationDate)
    {
        var isTransactionMadeInTheCurrentWeek = (DateTime.UtcNow - creationDate).Days < 7;
        return isTransactionMadeInTheCurrentWeek ? creationDate.DayOfWeek.ToString() : creationDate.ToString("M/d/yy");
    }

    private static bool GenerateIsPending()
    {
        var randomIndex = new Random().Next(0, 2);
        return randomIndex is not 0;
    }
}