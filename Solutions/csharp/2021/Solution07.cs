using System.IO;
using System.Linq;

namespace AdventOfCode.Y2021;

[Solution(2021, 7)]
public class Solution07
{
    [Part1]
    public void Part1(string filename)
    {
        var input = File.ReadAllLines(filename)
            .First()
            .Split(",")
            .Select(x => int.Parse(x));

        var min = input.Min();
        var max = input.Max();

        int lowestFuelConsumption = int.MaxValue;
        for (int i = min; i < max; ++i)
        {
            lowestFuelConsumption = Math.Min(lowestFuelConsumption, input.Select(x => Math.Abs(x - i)).Sum());
        }

        Console.WriteLine($"LowestFuelConsumption: {lowestFuelConsumption}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var input = File.ReadAllLines(filename)
            .First()
            .Split(",")
            .Select(x => int.Parse(x));

        var min = input.Min();
        var max = input.Max();

        int lowestFuelConsumption = int.MaxValue;
        for (int i = min; i < max; ++i)
        {
            var fuelConsumption = input.Select(x => new
            {
                Distance = Math.Abs(x - i),
                Consumption = Math.Abs(x - i) * (Math.Abs(x - i) + 1) / 2
            });

            lowestFuelConsumption = Math.Min(lowestFuelConsumption, fuelConsumption.Sum(x => x.Consumption));
        }

        Console.WriteLine($"LowestFuelConsumption: {lowestFuelConsumption}");
    }
}