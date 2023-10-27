namespace WalletApp.Domain.DailyPointAggregate;

public interface IDailyPointCalculationService
{
    public string Calculate(DateTime userCreatedOn);
    public string GetInitialPoints();
}