var measurements = File.ReadAllLines("input.txt").Select(x => int.Parse(x));
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