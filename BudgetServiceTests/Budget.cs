namespace BudgetServiceTests;

public class Budget
{
    public string YearMonth { get; set; } = null!;
    public int Amount { get; set; }

    public decimal GetOverlappingAmount(Period period)
    {
        return AmountPerDay() * period.GetOverlappingDays(GetPeriod());
    }

    private DateTime StartDate()
    {
        return DateTime.ParseExact(YearMonth, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture);
    }

    private int AmountPerDay()
    {
        return Amount / DaysInMonth();
    }

    private Period GetPeriod()
    {
        return new Period(StartDate(), EndDate());
    }

    private DateTime EndDate()
    {
        return DateTime.ParseExact($"{YearMonth}{DaysInMonth()}", $"yyyyMMdd", null);
    }

    private int DaysInMonth()
    {
        return DateTime.DaysInMonth(StartDate().Year, StartDate().Month);
    }
}