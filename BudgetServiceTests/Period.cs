namespace BudgetServiceTests;

public class Period(DateTime start, DateTime end)
{
    private readonly DateTime _endDate = end;
    private readonly DateTime _startDate = start;

    public int GetOverLappingDays(Period period)
    {
        var overlapStartDate = _startDate > period._startDate ? _startDate : period._startDate;
        var overlapEndDate = _endDate < period._endDate ? _endDate : period._endDate;
        var overLappingDays = (overlapEndDate-overlapStartDate).Days + 1;
        return overLappingDays > 0 ? overLappingDays : 0;
    }
}