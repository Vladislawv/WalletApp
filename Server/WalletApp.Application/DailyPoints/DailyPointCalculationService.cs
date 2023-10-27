using WalletApp.Domain.Aggregates.DailyPointAggregate;

namespace WalletApp.Application.DailyPoints;

public class DailyPointCalculationService : IDailyPointCalculationService
{
    private static readonly List<DateTime> FirstDaysOfSeason = new()
    {
        new DateTime(DateTime.Now.Year, 12, 1),
        new DateTime(DateTime.Now.Year, 3, 1),
        new DateTime(DateTime.Now.Year, 6, 1),
        new DateTime(DateTime.Now.Year, 9, 1)
    };
    
    private static readonly List<DateTime> SecondDaysOfSeason = new()
    {
        new DateTime(DateTime.Now.Year, 12, 2),
        new DateTime(DateTime.Now.Year, 3, 2),
        new DateTime(DateTime.Now.Year, 6, 2),
        new DateTime(DateTime.Now.Year, 9, 2)
    };

    public string Calculate(DateTime userCreatedOn)
    {
        var totalPoints = 0;

        var dayBeforeYesterdayPoints = 0;
        var yesterdayPoints = 0;
        
        for (var date = userCreatedOn; date <= DateTime.UtcNow; date = date.AddDays(1))
        {
            if (FirstDaysOfSeason.Contains(date))
            {
                totalPoints += 2;
                continue;
            }
            if (SecondDaysOfSeason.Contains(date))
            {
                totalPoints += 3;
                continue;
            }

            var dailyPoints = dayBeforeYesterdayPoints + (yesterdayPoints * 0.6);
            var roundedDailyPoints = Math.Round(dailyPoints / 1000) * 1000;
            totalPoints += (int)roundedDailyPoints;

            dayBeforeYesterdayPoints = yesterdayPoints;
            yesterdayPoints = totalPoints;
        }

        return ToString(totalPoints);
    }

    private static string ToString(int totalPoints)
    {
        var totalPointsString = totalPoints.ToString();
        var firstZeroIndex = totalPointsString.IndexOf('0');
        var zeroCount = totalPointsString.Length - firstZeroIndex;
        
        return zeroCount switch
        {
            6 => $"{totalPointsString[..firstZeroIndex]}KK",
            5 => $"{totalPointsString[..(firstZeroIndex + 2)]}K",
            4 => $"{totalPointsString[..(firstZeroIndex + 1)]}K",
            3 => $"{totalPointsString[..firstZeroIndex]}K",
            _ => totalPointsString
        };
    }
}