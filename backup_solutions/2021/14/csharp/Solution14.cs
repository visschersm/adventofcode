var input = File.ReadLines("input.txt");
var template = input.First();

var insertions = input.Skip(2).Select(i => (pair: i.Split(" -> ")[0], element: i.Split(" -> ")[1])).ToList();

Console.WriteLine($"Template\t{template}");

string previousResult = template;
int numberOfSteps = 10;

string finalResult = "";
for(int step = 0; step < numberOfSteps; ++step)
{
    string result = previousResult;
    
    for(int i = 0; i < previousResult.Length - 1; ++i)
    {
        var pair = $"{previousResult[i]}{previousResult[i + 1]}";
        //Console.WriteLine($"Pair to validate: {pair}");

        (string? pair, string? element) matchingInsertion = insertions.SingleOrDefault(i => i.pair == pair);

        if(matchingInsertion == default)
        {
            Console.WriteLine("No Matching pairs found");
            result += pair;
            continue;
        }
        
        // Console.WriteLine($"Matches rule: {matchingInsertion.pair} -> {matchingInsertion.element}");
        
        // Console.WriteLine($"Insert index: {i * 2 + 1}");
        // Console.WriteLine($"Intermediate result:\t{result}\tlength: {result.Length}");
        result = result.Insert(i * 2 + 1, matchingInsertion.element);
        // Console.WriteLine($"Intermediate result:\t{result}\tlength: {result.Length}");
        // Console.WriteLine();
    }

    previousResult = result;
    finalResult = result;
}

var grouped = finalResult.GroupBy(c => c);

var maxValue = grouped.Max(x => x.Count());
var minValue = grouped.Min(x => x.Count());

Console.WriteLine($"Total: {maxValue - minValue}");