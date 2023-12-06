using System.ComponentModel.DataAnnotations;
using AdventOfCode;
using AdventOfCode.Lib;

namespace Solutions.y2023d01;

[Solution(2023, 1)]
public class Solution01
{
    [Part1]
    public void Part1(string filename)
    {
        Console.WriteLine("Part1 not yet implemented");
    }

    [Part2]
    public void Part2(string filename)
    {
        var result = 0;

        foreach (var line in FileHelper.ReadByLine(filename))
        {
            var tokenizedLine = line.Replace("one", "one1one");
            tokenizedLine = tokenizedLine.Replace("two", "two2two");
            tokenizedLine = tokenizedLine.Replace("three", "three3three");
            tokenizedLine = tokenizedLine.Replace("four", "four4four");
            tokenizedLine = tokenizedLine.Replace("five", "five5five");
            tokenizedLine = tokenizedLine.Replace("six", "six6six");
            tokenizedLine = tokenizedLine.Replace("seven", "seven7seven");
            tokenizedLine = tokenizedLine.Replace("eight", "eight8eight");
            tokenizedLine = tokenizedLine.Replace("nine", "nine9nine");

            var firstDigit = 0;
            var lastDigit = 0;

            for (int i = 0; i < tokenizedLine.Length; ++i)
            {
                var c = tokenizedLine[i];
                if (char.IsDigit(c))
                {
                    firstDigit = int.Parse(c.ToString());
                    break;
                }
            }

            for (int i = tokenizedLine.Length - 1; i >= 0; --i)
            {
                var c = tokenizedLine[i];
                if (char.IsDigit(c))
                {
                    lastDigit = int.Parse(c.ToString());
                    break;
                }
            }

            var concat = int.Parse($"{firstDigit}{lastDigit}");
            result += concat;
        }

        Console.WriteLine($"Result: {result}");
    }
}
