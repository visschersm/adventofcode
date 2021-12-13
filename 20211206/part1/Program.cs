var lines = File.ReadAllLines("test-input.txt")
    .First()
    .Split(",")
    .Select(x => int.Parse(x));

var endOfTime = 80;

Console.WriteLine($"Amount: {foo(lines, 0, endOfTime)}");


int foo(IEnumerable<int> input, int day, int endOfTime)
{
    if(day >= endOfTime)
        return input.Count();
    
    input = input.Select(x => x - 1);
    var newGenerationCount = input.Count(x => x == -1);
    input = input.Select(x => x == -1 ? 6 : x);
    input = input.Concat(Enumerable.Repeat(8, newGenerationCount));

    return foo(input, ++day, endOfTime);
}