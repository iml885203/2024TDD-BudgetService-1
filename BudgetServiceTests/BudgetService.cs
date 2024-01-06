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
        var diffDays = (end - start).Days + 1;

        return budgets.Sum((x) => x.Amount) / 31m * diffDays;
    }
}