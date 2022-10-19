namespace AdventOfCode.Y2021;

[Solution(2021, 6)]
public class Solution6
{
    [Part1]
    public void Part1(string filename)
    {
        var lines = File.ReadAllLines(filename)
            .First()
            .Split(",")
            .Select(x => int.Parse(x));

        var endOfTime = 80;

        Console.WriteLine($"Amount: {foo(lines, 0, endOfTime)}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var input = File.ReadAllLines(filename)
            .First()
            .Split(",")
            .Select(x => int.Parse(x))
            .GroupBy(x => x)
            .Select(x => new Counted { Key = x.Key, Count = x.Count()})
            .ToList();

        var endOfTime = 256;


        Console.WriteLine($"Initial State:\t{string.Join(",", input)}");
        for(int i = 1; i <= endOfTime; ++i)
        {
            input = input.Select(x => new Counted
            {
                Key = x.Key - 1,
                Count = x.Count
            }).ToList();

            var newGenerationCount = input.SingleOrDefault(x => x.Key == -1)?.Count ?? 0;

            var foo = input.SingleOrDefault(x => x.Key == 6);
            if(foo == null)
                input.Add(new Counted { Key = 6, Count = 0 });

            input.Single(x => x.Key == 6).Count += input.SingleOrDefault(x => x.Key == -1)?.Count ?? 0;
            input = input.Where(x => x.Key != -1).ToList();

            input.Add(new Counted { Key = 8, Count = newGenerationCount });

            //Console.WriteLine($"After {i} days:\t{string.Join(",", input)}");
            //Console.WriteLine($"After {i} days:\t{input.Sum(x => x.Count)}");
        }


        Console.WriteLine($"Count: {input.Sum(x => x.Count)}");
    }

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
}

public class Counted
{
    public int Key;
    public long Count;
    public override string ToString()
    {
        return $"{Key}|{Count}";
    }
}