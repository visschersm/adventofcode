var input = File.ReadAllLines("input.txt")
    .Select(x =>
    {
        var startIndex = x.IndexOf("|");
        return x.Substring(startIndex + 2);
    })
    .Select(x => x.Split(" "))
    .ToList();

// 7 3seg
// 4 4seg
// 1 2seg
// 8 7seg

var segments = input.SelectMany(line => line.GroupBy(x => x.Count()));

var result = segments.Where(segment => segment.Key == 3 || segment.Key == 4 || segment.Key == 2 || segment.Key == 7)
    .Sum(x => x.Count());

Console.WriteLine($"Result: {result}");
