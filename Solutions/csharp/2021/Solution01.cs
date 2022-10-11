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

        var result = measurements.Aggregate((previous, current) =>
        {
            increasedCounter += previous > current ? 1 : 0;
            return increasedCounter;
        });
        
        foreach (var meassurement in measurements.Skip(1))
        {
            var increased = meassurement > previousMeasurement;
            if (increased) increasedCounter++;
            previousMeasurement = meassurement;
        }

        Console.WriteLine($"Aggregate result: {result}");
        Console.WriteLine($"increasedCounter: {increasedCounter}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var measurements = File.ReadAllLines(filename).Select(x => int.Parse(x));
        var increasedCounter = 0;

        var previousMeasurement = measurements.Skip(0).Take(3).Sum();

        for (int i = 1; i < measurements.Count() - 2; ++i)
        {
            var meassurementWindow = measurements.Skip(i).Take(3);
            var meassurement = meassurementWindow.Sum();
            var increased = meassurement > previousMeasurement;
            if (increased) increasedCounter++;
            previousMeasurement = meassurement;
        }

        Console.WriteLine($"increasedCounter: {increasedCounter}");
    }
}