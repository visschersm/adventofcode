using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt")
    .First()
    .Split(",")
    .Select(x => int.Parse(x));

var min = input.Min();
var max = input.Max();

int lowestFuelConsumption = int.MaxValue;
for(int i = min; i < max; ++i)
{
    lowestFuelConsumption = Math.Min(lowestFuelConsumption, input.Select(x => Math.Abs(x - i)).Sum());
}

Console.WriteLine($"LowestFuelConsumption: {lowestFuelConsumption}");