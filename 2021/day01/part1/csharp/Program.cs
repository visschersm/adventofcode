var measurements = File.ReadAllLines("input.txt").Select(x => int.Parse(x));
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