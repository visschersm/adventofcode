using AdventOfCode;
using System.Collections;

namespace Solutions.y2022d05;

[Solution(2022, 5)]
public class Solution05
{
    // TODO: Also get the amount of stacks and start row of the commands from the input file.
    private const int stackAmount = 9;
    private const int rows = 8;
    private const int commandIndex = 10;

    public Stack[] InitStacks(string filename)
    {
        // Initial stacks
        var stacks = Enumerable.Range(0, stackAmount)
            .Select(x => new Stack())
            .ToArray();

        var lines = FileHelper.ReadByLine(filename).ToArray();

        // Set up current stacks
        for(int rowIndex = rows - 1; rowIndex >= 0; --rowIndex)
        {
            var line = lines[rowIndex];
            
            for(int s = 0; s < stacks.Length; ++s)
            {
                var begin = s * 3 + s * 1;
                var crate = line[begin..(begin + 3)];
                
                if(string.IsNullOrWhiteSpace(crate)) 
                    continue;

                //Console.WriteLine($"Add crate: {crate}");
                stacks[s].Push(crate);
            }
        }

        return stacks;
    }

    public void ParseCommand(string line, out int amount, out int from, out int to)
    {
        var fromIndex = line.IndexOf("from");
        var toIndex = line.IndexOf("to");

        amount = int.Parse(line[5..(fromIndex)]);
        from = int.Parse(line[(fromIndex + 4)..toIndex]);
        to = int.Parse(line[(toIndex + 2)..]);
    }


    [Part1]
    public void Part1(string filename)
    {
        var lines = FileHelper.ReadByLine(filename).ToArray();
        var stacks = InitStacks(filename);

        // Start moving
        foreach(var line in lines.Skip(commandIndex))
        {
            ParseCommand(line, out int amount, out int from, out int to);
            
            for(int a = 0; a < amount; ++a)
            {
                stacks[to - 1].Push(stacks[from - 1].Pop());
            }
        }

        var topCrates = string.Join("", stacks.Select(s => s.Pop()));
        Console.WriteLine($"To crates: {topCrates}");
    }

    [Part2]
    public void Part2(string filename)
    {
        // Initial stacks
        var stacks = InitStacks(filename);

        var lines = FileHelper.ReadByLine(filename).ToArray();

        // Start moving
        foreach(var line in lines.Skip(commandIndex))
        {
            ParseCommand(line, out int amount, out int from, out int to);
            
            var temp = new Stack();
            for(int a = 0; a < amount; ++a)
            {
                temp.Push(stacks[from - 1].Pop());
            }

            while(temp.Count > 0)
            {
                stacks[to - 1].Push(temp.Pop());
            }
        }

        var topCrates = string.Join("", stacks.Select(s => s.Pop()));
        Console.WriteLine($"To crates: {topCrates}");
    }
}
