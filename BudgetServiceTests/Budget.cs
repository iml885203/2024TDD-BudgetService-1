namespace BudgetServiceTests;

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }

    public DateTime BudgetDate()
    {
        var budgetDate = DateTime.Parse(YearMonth.Insert(4, "-"));
        return budgetDate;
    }
}