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
        GivenBudgets(new List<Budget>()
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

    [Test]
    public void Whole_Month()
    {
        GivenBudgets(new List<Budget>()
        {
            new Budget()
            {
                YearMonth = "202401",
                Amount = 310
            }
        });

        var startDate = new DateTime(2024, 01, 01);
        var endDate = new DateTime(2024, 01, 31);
        var actual = _budgetService.Query(startDate, endDate);

        actual.Should().Be(310m);
    }

    [Test]
    public void Single_Day()
    {
        GivenBudgets(new List<Budget>()
        {
            new Budget()
            {
                YearMonth = "202401",
                Amount = 310
            }
        });

        var startDate = new DateTime(2024, 01, 01);
        var endDate = new DateTime(2024, 01, 01);
        var actual = _budgetService.Query(startDate, endDate);

        actual.Should().Be(10m);
    }

    [Test]
    public void Few_Days_In_Month()
    {
        GivenBudgets(new List<Budget>()
        {
            new Budget()
            {
                YearMonth = "202401",
                Amount = 310
            }
        });

        var startDate = new DateTime(2024, 01, 01);
        var endDate = new DateTime(2024, 01, 05);
        var actual = _budgetService.Query(startDate, endDate);

        actual.Should().Be(50m);
    }

    private void GivenBudgets(List<Budget> budgets)
    {
        _budgetRepo.GetAll().Returns(budgets);
    }
}