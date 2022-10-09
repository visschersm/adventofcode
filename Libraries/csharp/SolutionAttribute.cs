namespace AdventOfCode;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class SolutionAttribute : Attribute
{
    public SolutionAttribute(int year, int day)
    {
        Year = year;
        Day = day;
    }

    public int Year { get; }

    public int Day { get; }
}