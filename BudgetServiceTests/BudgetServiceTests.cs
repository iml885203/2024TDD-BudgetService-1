using FluentAssertions;
using NSubstitute;

namespace BudgetServiceTests;

public class Tests
{
    private IBudgetRepo _budgetRepo = null!;
    private BudgetService _budgetService = null!;

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

    [TestCase("2024-01-01", "2024-01-31", 310)]
    [TestCase("2024-02-01", "2024-02-29", 2900)]
    public void Whole_Month(string start, string end, decimal expected)
    {
        GivenBudgets(new List<Budget>()
        {
            new Budget()
            {
                YearMonth = "202401",
                Amount = 310
            },
            new Budget()
            {
                YearMonth = "202402",
                Amount = 2900
            }
        });

        var startDate = DateTime.Parse(start);
        var endDate = DateTime.Parse(end);
        var actual = _budgetService.Query(startDate, endDate);

        actual.Should().Be(expected);
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

    [Test]
    public void Query_Two_Months()
    {
        GivenBudgets(new List<Budget>()
        {
            new Budget()
            {
                YearMonth = "202401",
                Amount = 310
            },
            new Budget()
            {
                YearMonth = "202402",
                Amount = 2900
            }
        });

        var startDate = new DateTime(2024, 01, 01);
        var endDate = new DateTime(2024, 02, 29);
        var actual = _budgetService.Query(startDate, endDate);

        actual.Should().Be(3210m);
    }

    [Test]
    public void Partial_Month()
    {
        GivenBudgets(new List<Budget>()
        {
            new Budget()
            {
                YearMonth = "202401",
                Amount = 310
            },
            new Budget()
            {
                YearMonth = "202402",
                Amount = 2900
            }
        });

        var startDate = new DateTime(2024, 01, 30);
        var endDate = new DateTime(2024, 02, 05);
        var actual = _budgetService.Query(startDate, endDate);

        actual.Should().Be(520m);
    }

    [Test]
    public void Invalid_Period()
    {
        GivenBudgets(new List<Budget>()
        {
            new Budget()
            {
                YearMonth = "202401",
                Amount = 310
            },
            new Budget()
            {
                YearMonth = "202402",
                Amount = 2900
            }
        });

        var startDate = new DateTime(2024, 02, 05);
        var endDate = new DateTime(2024, 01, 30);
        var actual = _budgetService.Query(startDate, endDate);

        actual.Should().Be(0m);
    }

    private void GivenBudgets(List<Budget> budgets)
    {
        _budgetRepo.GetAll().Returns(budgets);
    }
}