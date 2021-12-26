var input = File.ReadLines("input.txt");
var template = input.First();

var insertions = input.Skip(2).Select(i => (pair: i.Split(" -> ")[0], element: i.Split(" -> ")[1])).ToDictionary(x => x.pair, x => x.element);

Console.WriteLine($"Template\t{template}");

string previousResult = template;
int numberOfSteps = 40;

string result = "";
var pairs = SelectPairs(previousResult);
var groupedPairs = pairs.GroupBy(x => x).Select(x => (key: x.Key, count: (long)x.Count()));

for (int step = 0; step < numberOfSteps; ++step)
{
    groupedPairs = groupedPairs.Select(x => (key: x.key, count: (long)x.count))
        .GroupBy(x => x.key)
        .Select(x => (key: x.Key, count: x.Sum(g => (long)g.count)));

    var newGroups = groupedPairs.Select(x => (key: $"{x.key[0]}{insertions[x.key]}{x.key[1]}", count: (long)x.count));

    groupedPairs = newGroups.SelectMany(x => SelectPairs(x.key)
        .Select(p => (key: p, count: (long)x.count)));

    Console.WriteLine($"Done computing: {step + 1}");
}

groupedPairs = groupedPairs.Select(x => (key: x.key, count: (long)x.count))
        .GroupBy(x => x.key)
        .Select(x => (key: x.Key, count: x.Sum(g => (long)g.count)))
        .OrderBy(x => x.count);

Dictionary<char, long> counter = new();
foreach (var group in groupedPairs)
{
    Console.WriteLine($"{group.key} {group.count}");

    if (!counter.ContainsKey(group.key[0]))
        counter.Add(group.key[0], 0);

    counter[group.key[0]] += group.count;
}

counter[template.Last()]++;

foreach (var c in counter.OrderBy(x => x.Value))
{
    Console.WriteLine($"{c.Key} {c.Value}");
}

Console.WriteLine($"Total: {(long)counter.Max(x => x.Value) - (long)counter.Min(x => x.Value)}");

string[] SelectPairs(string str)
{
    return str.Skip(1).Select((c, index) =>
    {
        return $"{str[index]}{c}";
    }).ToArray();
}