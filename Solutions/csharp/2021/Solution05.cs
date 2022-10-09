using System.Security.AccessControl;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Y2021;

[Solution(2021, 5)]
public class Solution05
{
    [Part1]
    public void Part1(string filename)
    {
        var lines = File.ReadAllLines(filename)
            .Select(inputLine => 
            {
                var parsedLine = inputLine.Split(" -> ");
                return new Line
                {
                    start = new Point(parsedLine[0]),
                    end = new Point(parsedLine[1])
                };
            })
            //.Where(line => line.start.x == line.end.x || line.start.y == line.end.y)
            .ToArray();

        var groupedLines = lines.SelectMany(line => line.Range()).GroupBy(x => x);

        Console.WriteLine($"Dangerous spots: {groupedLines.Count(x => x.Count() > 1)}");
    }

    [Part2]
    public void Part2(string filename)
    {
        Console.WriteLine("No answer");
    }
}
public struct Point
{
    public Point(int x, int y) => (this.x, this.y) = (x, y);
    public Point(string point)
    {
        var split = point.Split(",");
        x = int.Parse(split[0]);
        y = int.Parse(split[1]);
    }

    public int x;
    public int y;

    public override string ToString()
    {
        return $"{x},{y}";
    }
}

public class Line
{
    public Point start;
    public Point end;

    public List<Point> Range()
    {
        var (xDirection, yDirection) = GetDirection(start, end);
        var distance = GetDistance(start, end);

        var result = new List<Point>();
        for(int i = 0; i <= distance; ++i)
        {
            result.Add(new Point(start.x + i * xDirection, start.y + i * yDirection));
        }
        
        return result;
    }

    private int GetDistance(Point start, Point end)
    {
        var xDistance = Math.Abs(end.x - start.x);
        var yDistance = Math.Abs(end.y - start.y);

        if(xDistance == 0 || yDistance == 0)
        {
            return Math.Max(xDistance, yDistance);
        }

        if(xDistance != yDistance)
            throw new NotImplementedException();

        return xDistance;
    }

    private (int xDirection, int yDirection) GetDirection(Point start, Point end)
    {
        return (GetDirection(start.x, end.x), GetDirection(start.y, end.y));
    }

    private int GetDirection(int start, int end)
    {
        return (end - start) switch
        {
            <0 => -1,
            0 => 0,
            >0 => 1
        };
    }

    private (int min, int max) Swap(int a, int b)
    {
        if(a > b) return (b, a);

        return (a, b);
    }
}