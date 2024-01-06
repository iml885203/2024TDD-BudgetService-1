namespace BudgetServiceTests;

public class BudgetService
{
    private readonly IBudgetRepo _budgetRepo;

    public BudgetService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public decimal Query(DateTime start, DateTime end)
    {
        var budgets = _budgetRepo.GetAll();
        var isBudgetExists = budgets.Any((x) =>
        {
            var budgetDate = DateTime.Parse(x.YearMonth.Insert(4, "-"));
            return budgetDate.Year == start.Year && budgetDate.Month == start.Month;
        });
        if (!isBudgetExists)
        {
            return 0;
        }
        var diffDays = (end - start).Days + 1;
        var daysInMonth = DateTime.DaysInMonth(start.Year, start.Month);
        
        return (decimal)budgets.Sum((x) => x.Amount) / daysInMonth * diffDays;
    }
}