using AdventOfCode.Lib;
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

        for (int y = 1; y < data.Length - 1; ++y)
        {
            for (int x = 1; x < data[y].Length - 1; ++x)
            {
                var currentTree = data[y][x];

                bool notVisible = data[y][0..x].Any(tree => tree >= currentTree)
                    && data[y][(x + 1)..].Any(tree => tree >= currentTree)
                    && data[0..y].Select(line => line[x]).Any(tree => tree >= currentTree)
                    && data[(y + 1)..].Select(line => line[x]).Any(tree => tree >= currentTree);

                if (!notVisible)
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

        for (int y = 1; y < data.Length - 1; ++y)
        {
            for (int x = 1; x < data[y].Length - 1; ++x)
            {
                var currentTree = data[y][x];
                int scenicScore = GetScore(data[y][0..x].Reverse().ToArray(), currentTree);
                scenicScore *= GetScore(data[y][(x + 1)..].ToArray(), currentTree);
                scenicScore *= GetScore(data[0..y].Reverse().Select(line => line[x]).ToArray(), currentTree);
                scenicScore *= GetScore(data[(y + 1)..].Select(line => line[x]).ToArray(), currentTree);

                maxScore = Math.Max(scenicScore, maxScore);
            }
        }

        Console.WriteLine($"Highest scenic score: {maxScore}");
    }

    public int GetScore(ICollection<int> data, int currentTree)
    {
        bool blocked = false;
        return data.TakeWhile(tree =>
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
    }
}