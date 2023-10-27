using WalletApp.Domain.DailyPointAggregate;

namespace WalletApp.Application.DailyPoints;

public class DailyPointCalculationService : IDailyPointCalculationService
{
    private const string INITIAL_POINTS = "10";

    public string GetInitialPoints()
    {
        return INITIAL_POINTS;
    }
}