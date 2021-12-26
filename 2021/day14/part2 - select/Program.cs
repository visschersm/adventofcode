var input = File.ReadLines("test-input.txt");
var template = input.First();

var insertions = input.Skip(2).Select(i => (pair: i.Split(" -> ")[0], element: i.Split(" -> ")[1])).ToDictionary(x => x.pair, x => x.element);

Console.WriteLine($"Template\t{template}");

string previousResult = template;
int numberOfSteps = 40;

string result = "";
for (int step = 0; step < numberOfSteps; ++step)
{
    Console.WriteLine($"Computing: {step + 1}");

    result = new string(previousResult.Skip(1).Select((x, index) =>
    {
        return $"{previousResult[index]}{insertions[($"{previousResult[index]}{x}")]}";
    }).SelectMany(x => x).ToArray()) + previousResult.Last();
    previousResult = result;

    Console.WriteLine($"Done computing: {step + 1}\tlength: {result.Length}");
}

var grouped = result.GroupBy(c => c);

var maxValue = grouped.Max(x => x.Count());
var minValue = grouped.Min(x => x.Count());

Console.WriteLine($"Total: {maxValue - minValue}");