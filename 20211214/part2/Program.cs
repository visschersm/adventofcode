var input = File.ReadLines("test-input.txt");
var template = input.First();

var insertions = input.Skip(2).Select(i => (pair: i.Split(" -> ")[0], element: i.Split(" -> ")[1])).ToDictionary(x => x.pair, x => x.element);

Console.WriteLine($"Template\t{template}");

string previousResult = template;
int numberOfSteps = 1;

string finalResult = "";
for(int step = 0; step < numberOfSteps; ++step)
{
    Console.WriteLine($"Computing: {step + 1}");
    var result = previousResult.Select(x => x.ToString()).Aggregate((result, next) => 
    {
        var pair = $"{result.Last()}{next}";

        return result + insertions[pair] + next;
    });

    Console.WriteLine($"After step {step + 1}\t{result}");
}

var grouped = finalResult.GroupBy(c => c);

var maxValue = grouped.Max(x => x.Count());
var minValue = grouped.Min(x => x.Count());

Console.WriteLine($"Total: {maxValue - minValue}");