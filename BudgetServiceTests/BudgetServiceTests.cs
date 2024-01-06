using FluentAssertions;

namespace BudgetServiceTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void No_Budget_In_Period()
    {
        var startDate = new DateTime(2023, 12, 01);
        var endDate = new DateTime(2023, 12, 01);
        var budgetService = new BudgetService();
        var actual = budgetService.Query(startDate, endDate);
        actual.Should().Be(0m);
    }
}