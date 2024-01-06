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
        for (var date = start; date <= end; date = date.AddDays(1))
        {
            var budget = budgets.FirstOrDefault(x =>
            {
                var budgetDate = x.BudgetDate();
                return budgetDate.Year == date.Year && budgetDate.Month == date.Month;
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