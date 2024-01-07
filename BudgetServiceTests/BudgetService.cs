namespace BudgetServiceTests;

public class BudgetService(IBudgetRepo budgetRepo)
{
    public decimal Query(DateTime start, DateTime end)
    {
        if (end < start)
        {
            return 0;
        }

        var period = new Period(start, end);

        return budgetRepo.GetAll().Sum(budget => budget.GetOverlappingAmount(period));
    }
}