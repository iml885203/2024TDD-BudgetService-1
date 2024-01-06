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

        var sum = 0;
        for (var date = start; date <= end; date = date.AddMonths(1))
        {
            var budget = budgets.FirstOrDefault(x =>
            {
                var budgetDate = DateTime.Parse(x.YearMonth.Insert(4, "-"));
                return budgetDate.Year == start.Year && budgetDate.Month == start.Month;
            });
            if (budget == null)
            {
                continue;
            }
            var budgetDate = DateTime.Parse(budget.YearMonth.Insert(4, "-"));
            sum += budget.Amount / DateTime.DaysInMonth(budgetDate.Year, budgetDate.Month) * diffDays;
        }

        return sum;
    }
}