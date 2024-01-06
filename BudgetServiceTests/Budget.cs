namespace BudgetServiceTests;

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }

    public DateTime BudgetDate()
    {
        return DateTime.ParseExact(YearMonth, "yyyyMM", System.Globalization.CultureInfo.InvariantCulture);
    }

    public int AmountPerDay()
    {
        var budgetDate = BudgetDate();
        return Amount / DateTime.DaysInMonth(budgetDate.Year, budgetDate.Month);
    }
}