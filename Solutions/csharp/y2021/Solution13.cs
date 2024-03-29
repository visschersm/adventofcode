﻿namespace AdventOfCode.Y2021;

[Solution(2021, 13)]
public class Solution13
{
    [Part1]
    public void Part1(string filename)
    {
    var input = File.ReadAllLines(filename);

    var dotsCordinates = input.Where(x => !x.Contains("fold") && !string.IsNullOrWhiteSpace(x));
    var foldInstructions = input.Where(x => x.Contains("fold along"));

    var cordinates = dotsCordinates.Select(dot => (x: int.Parse(dot.Split(",")[0]), y: int.Parse(dot.Split(",")[1])));

    Console.WriteLine("Get ready to do some folding");

    var firstFold = foldInstructions.First().Replace("fold along ", "");
    var index = firstFold.IndexOf('=');
    var axis = firstFold.Substring(0, index);
    var value = int.Parse(firstFold.Substring(index + 1));

    Console.WriteLine($"Fold: axis: {axis}, value: {value}");

    var foldedCordinates = cordinates.Select(cord => axis == "x" 
        ? cord.x > value ? (x: Math.Abs(cord.x - value * 2), y: cord.y) : (x: cord.x, y: cord.y) 
        : cord.y > value ? (x: cord.x, y: Math.Abs(cord.y -value * 2)) : (x: cord.x, y: cord.y));

    var remainingCords = foldedCordinates.Distinct().Count();
    Console.WriteLine($"Remaining cords: {remainingCords}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var input = File.ReadAllLines(filename);

        var dotsCordinates = input.Where(x => !x.Contains("fold") && !string.IsNullOrWhiteSpace(x));
        var foldInstructions = input.Where(x => x.Contains("fold along"));

        var cordinates = dotsCordinates.Select(dot => (x: int.Parse(dot.Split(",")[0]), y: int.Parse(dot.Split(",")[1])));

        Console.WriteLine("Get ready to do some folding");

        var foldedCordinates = cordinates;
        foreach(var foldInstruction in foldInstructions)
        {
            var fold = foldInstruction.Replace("fold along ", "");
            var index = fold.IndexOf('=');
            var axis = fold.Substring(0, index);
            var value = int.Parse(fold.Substring(index + 1));

            Console.WriteLine($"Fold: axis: {axis}, value: {value}");

            foldedCordinates = foldedCordinates.Select(cord => axis == "x" 
                ? cord.x > value ? (x: Math.Abs(cord.x - value * 2), y: cord.y) : (x: cord.x, y: cord.y) 
                : cord.y > value ? (x: cord.x, y: Math.Abs(cord.y -value * 2)) : (x: cord.x, y: cord.y));
        }

        var remainingCords = foldedCordinates.Distinct().Count();
        Console.WriteLine($"Remaining cords: {remainingCords}");

        for(int y = 0; y <= foldedCordinates.Max(cord => cord.y); ++y)
        {
            for(int x = 0; x <= foldedCordinates.Max(cord => cord.x); ++x)
            {
                Console.Write(foldedCordinates.Any(cord => cord.x == x && cord.y == y) ? "#" : ".");
            }
            Console.WriteLine();
        }
    }
}