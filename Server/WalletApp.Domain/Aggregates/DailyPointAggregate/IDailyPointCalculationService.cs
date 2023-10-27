namespace WalletApp.Domain.Aggregates.DailyPointAggregate;

public interface IDailyPointCalculationService
{
    public string Calculate(DateTime userCreatedOn);
}