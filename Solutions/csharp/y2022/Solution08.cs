using AdventOfCode;

namespace Solutions.y2022d08;

[Solution(2022, 8)]
public class Solution08
{
    [Part1]
    public void Part1(string filename)
    {
        var data = FileHelper.ReadByLine(filename)
            .Select(line => line.Select(c => int.Parse(c.ToString())).ToArray())
            .ToArray();

        var visibleCount = data[0].Length * 2 + data.Length * 2 - 4;

        for(int y = 1; y < data.Length - 1; ++y)
        {
            for(int x = 1; x < data[y].Length - 1; ++x)
            {
                var currentTree = data[y][x];
                
                bool notVisible = data[y][0..x].Any(tree => tree >= currentTree)
                    && data[y][(x + 1)..].Any(tree => tree >= currentTree)
                    && data[0..y].Select(line => line[x]).Any(tree => tree >= currentTree)
                    && data[(y + 1)..].Select(line => line[x]).Any(tree => tree >= currentTree);

                if(!notVisible)
                {
                    visibleCount++;
                }

            }
        }

        Console.WriteLine($"Visible trees: {visibleCount}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var data = FileHelper.ReadByLine(filename)
            .Select(line => line.Select(c => int.Parse(c.ToString())).ToArray())
            .ToArray();

        var visibleCount = data[0].Length * 2 + data.Length * 2 - 4;

        int maxScore = int.MinValue;

        for(int y = 1; y < data.Length - 1; ++y)
        {
            for(int x = 1; x < data[y].Length - 1; ++x)
            {
                var currentTree = data[y][x];
                bool blocked = false;
                int scenicScore = data[y][0..x].Reverse().TakeWhile(tree =>
                {
                    if (blocked) return false;

                    if (tree < currentTree)
                        return true;

                    if(tree >= currentTree)
                    {
                        blocked = true;
                        return true;
                    }

                    return false;

                }).Count();
                blocked = false;
                scenicScore *= data[y][(x + 1)..].TakeWhile(tree =>
                {
                    if (blocked) return false;

                    if (tree < currentTree)
                        return true;

                    if (tree >= currentTree)
                    {
                        blocked = true;
                        return true;
                    }

                    return false;

                }).Count();
                blocked = false;
                scenicScore *= data[0..y].Reverse().Select(line => line[x]).TakeWhile(tree =>
                {
                    if (blocked) return false;

                    if (tree < currentTree)
                        return true;

                    if(tree >= currentTree)
                    {
                        blocked = true;
                        return true;
                    }

                    return false;

                }).Count();
                blocked = false;
                scenicScore *= data[(y + 1)..].Select(line => line[x]).TakeWhile(tree =>
                {
                    if (blocked) return false;

                    if (tree < currentTree)
                        return true;

                    if (tree >= currentTree)
                    {
                        blocked = true;
                        return true;
                    }

                    return false;

                }).Count();

                maxScore = Math.Max(scenicScore, maxScore);
            }
        }

        Console.WriteLine($"Highest scenic score: {maxScore}");
    }
}
