using System.Reflection;
using System.Drawing;

namespace AdventOfCode.Y2021;

[Solution(2021, 11)]
public class Solution11
{
    [Part1]
    public void Part1(string filename)
    {
        var input = File.ReadAllLines(filename)
            .Select((line, y) => line.Select((c, x) => new Point
                {
                    X = x,
                    Y = y,
                    Value = int.Parse(c.ToString())
                }).ToArray())
            .ToArray();

        int amountOfSteps = 100;
        int flashCount = 0;
        Console.WriteLine("Before any steps");
        DrawGrid(input);

        for(int i = 0; i < amountOfSteps; ++i)
        {
            IncrementValues(input);
            CheckForFlash(input);

            flashCount += input.SelectMany(line => line.Select(point => point)).Count(point => point.Flashed);
            ResetFlash(input);

            if(i < 10 || (i + 1) % 10 == 0)
            {
                Console.WriteLine();
                Console.WriteLine($"After step {i + 1}");
                DrawGrid(input);
            }
        }

        Console.WriteLine($"After step {amountOfSteps} there have been a total of {flashCount} flashes.");
    }
    
    [Part2]
    public void Part2(string filename)
    {
        var input = File.ReadAllLines("input.txt")
            .Select((line, y) => line.Select((c, x) => new Point
                {
                    X = x,
                    Y = y,
                    Value = int.Parse(c.ToString())
                }).ToArray())
            .ToArray();

        int amountOfSteps = 3000;
        Console.WriteLine("Before any steps");
        DrawGrid(input);

        for(int i = 0; i < amountOfSteps; ++i)
        {
            IncrementValues(input);
            CheckForFlash(input);
            ResetFlash(input);

            if(input.SelectMany(line => line.Select(point => point)).All(point => point.Value == 0))
            {
                Console.WriteLine($"Flashing synchronized at step {i + 1}");
                break;
            }

            if(i < 10 || (i + 1) % 10 == 0)
            {
                Console.WriteLine();
                Console.WriteLine($"After step {i + 1}");
                DrawGrid(input);
            }
        }
    }
    
    void IncrementValues(Point[][] input)
    {
        for(int y = 0; y < input.Length; ++y)
        {
            for(int x = 0; x < input[y].Length; ++x)
            {
                input[y][x].Value++;
            }
        }
    }

    void CheckForFlash(Point[][] input)
    {
        for(int y = 0; y < input.Length; ++y)
        {
            for(int x = 0; x < input[y].Length; ++x)
            {
                CheckPointForFlash(input[y][x], input);
            }
        }
    }

    void CheckPointForFlash(Point point, Point[][] input)
    {
        if(point.Value > 9 && !point.Flashed)
        {
            point.Flashed = true;
            var x = point.X;
            var y = point.Y;

            if(y - 1 >= 0) Flashed(input[y - 1][x], input);
            if(y + 1 < input.Length) Flashed(input[y + 1][x], input);
            if(x - 1 >= 0) Flashed(input[y][x - 1], input);
            if(x + 1 < input[y].Length) Flashed(input[y][x + 1], input);

            if(y - 1 >= 0 && x - 1 >= 0) Flashed(input[y - 1][x - 1], input);
            if(y + 1 < input.Length && x - 1 >= 0) Flashed(input[y + 1][x - 1], input);
            if(y - 1 >= 0 && x + 1 < input[y].Length) Flashed(input[y - 1][x + 1], input);
            if(y + 1 < input.Length && x + 1 < input[y].Length) Flashed(input[y + 1][x + 1], input);
            
        }
    }

    void Flashed(Point point, Point[][] input)
    {
        point.Value += 1;
        CheckPointForFlash(point, input);
    }

    void ResetFlash(Point[][] input)
    {
        for(int y = 0; y < input.Length; ++y)
        {
            for(int x = 0; x < input[y].Length; ++x)
            {
                var point = input[y][x];
                if(point.Flashed)
                {
                    point.Flashed = false;
                    point.Value = 0;
                }
            }
        }
    }

    void DrawGrid(Point[][] input)
    {
        for(int y = 0; y < input.Length; ++y)
        {
            for(int x = 0; x < input[y].Length; ++x)
            {
                Console.Write(input[y][x]);
            }
            Console.WriteLine();
        }
    }

    class Point
    {
        public int X = 0;
        public int Y = 0;
        public int Value = 0;
        public bool Flashed = false;

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}