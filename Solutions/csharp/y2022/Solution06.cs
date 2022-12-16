using System;
using System.Linq;
using System.Collections.Generic;
using AdventOfCode;
using AdventOfCode.Lib;

namespace Solutions.y2022d06;

[Solution(2022, 6)]
public class Solution06
{
    [Part1]
    public void Part1(string filename)
    {
        var buffer = new Queue<char>();
        int counter = 0;
        foreach (var c in FileHelper.ReadByCharacter(filename))
        {
            counter++;

            buffer.Enqueue(c);
            if (buffer.Distinct().Count() == 4)
                break;

            if (buffer.Count >= 4)
                buffer.Dequeue();
        }

        Console.WriteLine($"Start of packet found at index: {counter}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var buffer = new Queue<char>();
        int counter = 0;
        foreach (var c in FileHelper.ReadByCharacter(filename))
        {
            counter++;

            buffer.Enqueue(c);
            if (buffer.Distinct().Count() == 14)
                break;

            if (buffer.Count >= 14)
                buffer.Dequeue();
        }

        Console.WriteLine($"Start of first message found at index: {counter}");
    }
}