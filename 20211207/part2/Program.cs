using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt")
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

    // foreach (var consumption in fuelConsumption)
    //     Console.WriteLine($"Consumption to from {consumption.Distance} - {min}\t| {consumption.Consumption}");

    lowestFuelConsumption = Math.Min(lowestFuelConsumption, fuelConsumption.Sum(x => x.Consumption));
}

Console.WriteLine($"LowestFuelConsumption: {lowestFuelConsumption}");