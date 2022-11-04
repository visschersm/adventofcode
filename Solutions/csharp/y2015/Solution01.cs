using AdventOfCode;

namespace Solutions.y2015d01;

[Solution(2015, 1)]
public class Solution01
{
    [Part1]
    public void Part1(string filename)
    {
        var data = File.ReadAllText(filename).Select(c => c == '(' ? 1 : -1);

        Console.WriteLine("Santa is on the {0}th floor", data.Sum());
    }

    [Part2]
    public void Part2(string filename)
    {
        var floor = 0;
        var tryCounter = 0;

        var text = File.ReadAllText(filename).ToList();

        foreach (var c in text)
        {
            tryCounter++;
            floor += c == '(' ? 1 : -1;
            if (floor == -1) break;
        };

        Console.WriteLine("Santa found the basement after {0} tries", tryCounter);
    }
}
