using System.Security.AccessControl;
using System.Linq;
using System.Collections.Generic;

var lines = File.ReadAllLines("input2.txt")
    .Select(inputLine => 
    {
        var parsedLine = inputLine.Split(" -> ");
        return new Line
        {
            start = new Point(parsedLine[0]),
            end = new Point(parsedLine[1])
        };
    })
    .ToArray();

// foreach(var line in lines)
// {
//     Console.WriteLine($"{line.start} -> {line.end} | {string.Join("|", line.Range())}");
// }

var groupedLines = lines.SelectMany(line => line.Range()).GroupBy(x => x);

// foreach(var group in groupedLines)
// {
//     Console.WriteLine($"Group: {group.Key}, {group.Count()}");
// }

Console.WriteLine($"Dangerous spots: {groupedLines.Count(x => x.Count() > 1)}");
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
        int x = 0;
        var a = (start.x - end.x) switch
        {
            <0 => -1,
            0 => 0,
            >0 => 1,
            _ => throw new NotImplementedException()
        };
        var b = start.y - end.y;

        
        Console.WriteLine($"a,b: {a},{b}");

        if(start.x == end.x)
        {
            var (min, max) = Swap(start.y, end.y);
            return Enumerable.Range(min, max - min + 1).Select(y => new Point(start.x, y)).ToList();
        }

        if(start.y == end.y)
        {
            var (min, max) = Swap(start.x, end.x);
            return Enumerable.Range(min, max - min + 1).Select(x => new Point(x, start.y)).ToList();
        }
        
        var result = new List<Point>();

        if(start.x < end.x && start.y < end.y)
        {
            for(var i = 0; i <= (end.x - start.x); ++i)
            {
                result.Add(new Point(start.x + i, start.y + i));
            }

            return result;
        }

        if(start.x > end.x && start.y < end.y)
        {
            for(var x = end.x; x <= start.x; ++x)
            {
                result.Add(new Point(start.x - x, start.y + x));
            }

            return result;
        }

        if(start.x > end.x && start.y > end.y)
        {
            for(var i = 0; i <= (start.x - end.x); ++i)
            {
                result.Add(new Point(end.x + i, end.y + i));
            }

            return result;
        }

        if(start.x < end.x && start.y > end.y)
        {
            for(var i = 0; i <= (end.x - start.x); ++i)
            {
                result.Add(new Point(start.x + i, start.y - i));
            }

            return result;
        }

        return new List<Point>();
    }

    private (int min, int max) Swap(int a, int b)
    {
        if(a > b) return (b, a);

        return (a, b);
    }
}