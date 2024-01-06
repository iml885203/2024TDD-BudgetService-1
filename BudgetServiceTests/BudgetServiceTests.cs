using FluentAssertions;
using NSubstitute;

namespace BudgetServiceTests;

public class Tests
{
    private BudgetService _budgetService = null!;
    private IBudgetRepo _budgetRepo = null!;

    [SetUp]

    public void Setup()
    {
        _budgetRepo = Substitute.For<IBudgetRepo>();
        _budgetService = new BudgetService(_budgetRepo);
    }

    [Test]
    public void No_Budget_In_Period()
    {
        _budgetRepo.GetAll().Returns(new List<Budget>()
        {
            new Budget()
            {
                YearMonth = "202401",
                Amount = 310
            }
        });
        
        var startDate = new DateTime(2023, 12, 01);
        var endDate = new DateTime(2023, 12, 01);
        var actual = _budgetService.Query(startDate, endDate);
        
        actual.Should().Be(0m);
    }
}