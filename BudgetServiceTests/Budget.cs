namespace BudgetServiceTests;

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }

    public DateTime BudgetDate()
    {
        return DateTime.Parse(YearMonth.Insert(4, "-"));
    }

    public int AmountPerDay()
    {
        return Amount / DateTime.DaysInMonth(BudgetDate().Year, BudgetDate().Month);
    }
}