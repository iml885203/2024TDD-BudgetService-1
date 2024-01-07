namespace BudgetServiceTests;

public class Period(DateTime start, DateTime end)
{
    private readonly DateTime _endDate = end;
    private readonly DateTime _startDate = start;

    public int GetOverlappingDays(Period period)
    {
        var overlapStartDate = _startDate > period._startDate ? _startDate : period._startDate;
        var overlapEndDate = _endDate < period._endDate ? _endDate : period._endDate;
        var overlappingDays = (overlapEndDate - overlapStartDate).Days + 1;
        return overlappingDays > 0 ? overlappingDays : 0;
    }
}