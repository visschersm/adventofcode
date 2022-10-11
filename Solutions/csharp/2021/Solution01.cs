using AdventOfCode;

namespace Solutions.Y2021;

[Solution(2021, 1)]
public class Solution01
{
    [Part1]
    public void Part1(string filename)
    {
        var measurements = File.ReadAllLines(filename).Select(x => int.Parse(x));//.ToArray();

        var current = measurements.First(); 
        var result = measurements.Skip(1).Aggregate(0, (result, next) => 
        {
            result += next > current ? 1 : 0;
            current = next;
            return result;
        });
        
        Console.WriteLine($"The amount of meassurments larger: {result}");
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