using AdventOfCode.Lib;
using AdventOfCode;

namespace Solutions.y2022d02;

[Solution(2022, 2)]
public class Solution02
{
    [Part1]
    public void Part1(string filename)
    {
        // A Rock, B Paper, C Scissors
        // X Rock, Y Paper, Z Scissors
        var totalScore = 0;
        foreach(var line in FileHelper.ReadByLine(filename))
        {
            var match = line.Split(" ");

            totalScore += match switch
            {
                ["A", "X"]  => 3 + 1,
                ["B", "X"]  => 0 + 1,
                ["C", "X"]  => 6 + 1,
                ["A", "Y"]  => 6 + 2,
                ["B", "Y"]  => 3 + 2,
                ["C", "Y"]  => 0 + 2,
                ["A", "Z"]  => 0 + 3,
                ["B", "Z"]  => 6 + 3,
                ["C", "Z"]  => 3 + 3,
                _ => throw new NotImplementedException(),
            };
        }

        Console.WriteLine($"Total score if following the guide: {totalScore}");
    }

    [Part2]
    public void Part2(string filename)
    {
        // A Rock, B Paper, C Scissors
        // X Lose, Y Draw, Z Win
        var totalScore = 0;
        foreach(var line in FileHelper.ReadByLine(filename))
        {
            var match = line.Split(" ");

            totalScore += match switch
            {
                ["A", "X"]  => 0 + 3,
                ["B", "X"]  => 0 + 1,
                ["C", "X"]  => 0 + 2,
                ["A", "Y"]  => 3 + 1,
                ["B", "Y"]  => 3 + 2,
                ["C", "Y"]  => 3 + 3,
                ["A", "Z"]  => 6 + 2,
                ["B", "Z"]  => 6 + 3,
                ["C", "Z"]  => 6 + 1,
                _ => throw new NotImplementedException(),
            };
        }

        Console.WriteLine($"Total score if following the guide: {totalScore}");
    }
}
