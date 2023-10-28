using WalletApp.Domain.Aggregates.DailyPointAggregate;

namespace WalletApp.Application.DailyPoints;

public class DailyPointCalculationService : IDailyPointCalculationService
{
    private static readonly List<int> FirstDaysOfSeason = new()
    {
        new DateTime(DateTime.Now.Year, 12, 1).Day,
        new DateTime(DateTime.Now.Year, 3, 1).Day,
        new DateTime(DateTime.Now.Year, 6, 1).Day,
        new DateTime(DateTime.Now.Year, 9, 1).Day
    };

    private static readonly List<int> SecondDaysOfSeason = new()
    {
        new DateTime(DateTime.Now.Year, 12, 2).Day,
        new DateTime(DateTime.Now.Year, 3, 2).Day,
        new DateTime(DateTime.Now.Year, 6, 2).Day,
        new DateTime(DateTime.Now.Year, 9, 2).Day
    };

    public string Calculate(DateTime userCreatedOn)
    {
        var totalPoints = 0;

        var dayBeforeYesterdayPoints = 0;
        var yesterdayPoints = 0;

        for (var date = userCreatedOn; date <= DateTime.UtcNow; date = date.AddDays(1))
        {
            if (FirstDaysOfSeason.Contains(date.Day))
            {
                totalPoints += 2;
            }
            else if (SecondDaysOfSeason.Contains(date.Day))
            {
                totalPoints += 3;
            }
            else
            {
                var dailyPoints = dayBeforeYesterdayPoints + (yesterdayPoints * 0.6);
                totalPoints += RoundPoints(dailyPoints);
            }

            dayBeforeYesterdayPoints = yesterdayPoints;
            yesterdayPoints = totalPoints;
        }

        return ToString(totalPoints);
    }

    private static int RoundPoints(double dailyPoints)
    {
        var divisor = dailyPoints < 1000 ? 1 : 1000;
        
        var roundedPoints = Math.Round(dailyPoints / divisor) * divisor;
        return (int)roundedPoints;
    }
    
    private static string ToString(int totalPoints)
    {
        var totalPointsString = totalPoints.ToString();

        return totalPoints switch
        {
            >= 1000000 => $"{totalPointsString[..^6]}KK",
            >= 1000 => $"{totalPointsString[..^3]}K",
            _ => totalPointsString
        };
    }
}