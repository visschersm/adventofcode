namespace AdventOfCode.Y2021;

[Solution(2021, 9)]
public class Solution09
{
    [Part1]
    public void Part1(string filename)
    {
        var input = File.ReadAllLines(filename)
            .Select(line => line.Select(c => int.Parse(c.ToString())).ToArray())
            .ToArray();

        List<int> lowPoints = new List<int>();

        for(int x = 0; x < input.Length; ++x)
        {
            for(int y = 0; y < input[x].Length; ++y)
            {
                if(LowPoint(x, y, input))
                {
                    lowPoints.Add(input[x][y]);
                }
            }
        }

        Console.WriteLine($"Total: {lowPoints.Sum(x => x + 1)}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var input = File.ReadAllLines(filename)
            .Select(line => line.Select(c => int.Parse(c.ToString())).ToArray())
            .ToArray();

        List<Point> lowPoints = new List<Point>();

        for(int y = 0; y < input.Length; ++y)
        {
            for(int x = 0; x < input[y].Length; ++x)
            {
                if(LowPoint(x, y, input))
                {
                    //Console.WriteLine($"Add lowPoint: {x},{y}");
                    lowPoints.Add(new Point(x, y));
                }
            }
        }

        List<List<Point>> Basins = new List<List<Point>>();
        foreach(var point in lowPoints)
        {
            //var point = lowPoints.Skip(3).First();
            var results = Basin(point, input);
            results.Add(point);
            Basins.Add(results.Distinct().ToList());
        }

        foreach(var basin in Basins.OrderByDescending(x => x.Count()))
        {
            Console.WriteLine($"Basin of size: {basin.Count()}");
        }

        int total = 1;

        foreach(var basin in Basins.OrderByDescending(x => x.Count()).Take(3))
        {
            total *= basin.Count();
        }

        Console.WriteLine($"Total: {total}");
    }

    bool LowPoint(int x, int y, int[][] input)
    {
        var point = input[x][y];

        if((x - 1) >= 0 && point >= input[x - 1][y]) return false;
        if((x + 1) < input.Length && point >= input[x + 1][y]) return false;
        if((y - 1) >= 0 && point >= input[x][y - 1]) return false;
        if((y + 1) < input[x].Length && point >= input[x][y + 1]) return false;

        return true;
    }

    List<Point> Basin(Point point, int[][] input)
    {
        List<Point> result = new();

        var(x, y) = point;

        //Console.WriteLine($"Point: {x},{y} value: {PointValue(point)}");
        if(x - 1 >= 0)
        {
            var pointLeft = new Point(x - 1, y);

            //Console.WriteLine($"PointLeft: {PointValue(pointLeft)}");
            if(PointValue(pointLeft, input) > PointValue(point, input) && PointValue(pointLeft, input) != 9)
            {
                result.Add(pointLeft);
                result.AddRange(Basin(pointLeft, input));
            }
        }
        
        if(x + 1 < input[y].Length)
        {
            var pointRight = new Point(x + 1, y);
            //Console.WriteLine($"PointRight: {PointValue(pointRight)}");
            if(PointValue(pointRight, input) > PointValue(point, input) && PointValue(pointRight, input) != 9)
            {
                result.Add(pointRight);
                result.AddRange(Basin(pointRight, input));
            }
        }
        
        if(y - 1 > 0)
        {
            var pointUp = new Point(x, y - 1);
            //Console.WriteLine($"PointUp: {PointValue(pointUp)}");

            if(PointValue(pointUp, input) > PointValue(point, input) && PointValue(pointUp, input) != 9)
            {
                result.Add(pointUp);
                result.AddRange(Basin(pointUp, input));
            }
        }

        if(y + 1 < input.Length)
        {
            var pointDown = new Point(x, y + 1);
            //Console.WriteLine($"PointDown: {PointValue(pointDown)}");

            if(PointValue(pointDown, input) > PointValue(point, input) && PointValue(pointDown, input) != 9)
            {
                result.Add(pointDown);
                result.AddRange(Basin(pointDown, input));
            }
        }
        
        return result;
    }

    int PointValue(Point point, int[][] input)
    {
        return input[point.y][point.x];
    }

    record Point(int x, int y);
}
