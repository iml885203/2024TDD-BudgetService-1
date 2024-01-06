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

        var sum = 0;
        for (var current = start; current <= end; current = current.AddDays(1))
        {
            var budget = budgets.FirstOrDefault(x =>
            {
                var budgetDate = x.BudgetDate();
                return budgetDate.Year == current.Year && budgetDate.Month == current.Month;
            });
            if (budget == null)
            {
                continue;
            }

            sum += budget.AmountPerDay();
        }

        return sum;
    }
}