var input = File.ReadAllLines("test-input.txt")
    .First()
    .Split(",")
    .Select(x => int.Parse(x))
    .ToList();

var endOfTime = 256;

for(int i = 1; i <= endOfTime; ++i)
{
    input = input.Select(x => x - 1).ToList();
    var newGenerationCount = input.Count(x => x == -1);
    input = input.Select(x => x == -1 ? 6 : x).ToList();
    input.AddRange(Enumerable.Repeat(8, newGenerationCount));

    Console.WriteLine($"After {i} days:\t{input.Count()}");
}

Console.WriteLine($"Count: {input.Count()}");