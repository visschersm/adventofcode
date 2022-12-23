using AdventOfCode.Lib;
using AdventOfCode;

namespace Solutions.y2022d01;

[Solution(2022, 1)]
public class Solution01
{
    [Part1]
    public static void Part1(string filename)
    {
        var result = HighestCalories(filename, 1);

        // Find the Elf carrying the most Calories. How many total Calories is that Elf carrying?
        Console.WriteLine($"The elf carrying the most calories, carries {result} calories!");
    }

    [Part2]
    public static void Part2(string filename)
    {
        var result = HighestCalories(filename, 3);

        // Find the top three Elves carrying the most Calories. How many Calories are those Elves carrying in total?
        Console.WriteLine($"The top three elves are carrying {result} calories!");
    }

    public static int HighestCalories(string filename, int take)
    {
        return FileHelper.ReadByLine(filename)
            .ChunkBy(line => !string.IsNullOrWhiteSpace(line))
            .Select(elf => elf.Sum(int.Parse))
            .OrderDescending()
            .Take(take)
            .Sum();
    }
}

public static class IEnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> self, Func<T, bool> func)
    {
        List<T> set = new();

        foreach (var element in self)
        {
            if (func(element))
            {
                set.Add(element);
            }
            else
            {
                yield return set;
                set = new List<T>();
            }
        }
    }
}