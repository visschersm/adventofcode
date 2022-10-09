using AdventOfCode;

namespace Solutions.Y2021;

[Solution(2021, 1)]
public class Solution01
{
    [Part1]
    public void Part1(string filename)
    {
        var measurements = File.ReadAllLines(filename).Select(x => int.Parse(x));
        var increasedCounter = 0;

        var previousMeasurement = measurements.First();
        Console.WriteLine($"{previousMeasurement} (N/A - no previous measurement)");

        foreach (var meassurement in measurements.Skip(1))
        {
            var increased = meassurement > previousMeasurement;
            Console.WriteLine($"{meassurement} ({(increased ? "increased" : "decreased")})");
            if (increased) increasedCounter++;
            previousMeasurement = meassurement;
        }

        Console.WriteLine($"increasedCounter: {increasedCounter}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var measurements = File.ReadAllLines(filename).Select(x => int.Parse(x));
        var increasedCounter = 0;

        var previousMeasurement = measurements.Skip(0).Take(3).Sum();
        Console.WriteLine($"{previousMeasurement} (N/A - no previous measurement)");

        for (int i = 1; i < measurements.Count() - 2; ++i)
        {
            var meassurementWindow = measurements.Skip(i).Take(3);
            var meassurement = meassurementWindow.Sum();
            var increased = meassurement > previousMeasurement;
            Console.WriteLine($"{string.Join(", ", meassurementWindow)} - {meassurement} ({(increased ? "increased" : "decreased")})");
            if (increased) increasedCounter++;
            previousMeasurement = meassurement;
        }

        Console.WriteLine($"increasedCounter: {increasedCounter}");
    }
}