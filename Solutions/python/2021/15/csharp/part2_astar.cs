using System.Diagnostics;
using System.Linq;

Point[][] originalInput = File.ReadAllLines("input.txt")
    .Select((line, y) =>
        line.Select((value, x) =>
            new Point
            {
                x = x,
                y = y,
                risk = int.Parse(value.ToString())
            }).ToArray()
    ).ToArray();

var length = originalInput.Length;
var scale = 5;

var grid = CreateGrid(originalInput, scale);
var points = grid.SelectMany(pointArray => pointArray.Select(point => point))
    .ToArray();

Console.WriteLine($"Number of points: {points.Length}");

Dictionary<(int x, int y), RiskDistance> open = new();
Dictionary<(int x, int y), RiskDistance> closed = new();

var startPoint = grid[0][0];
var endPoint = grid[grid.Length - 1][grid[0].Length - 1];

var distances = points.Select(point => new RiskDistance
{
    Point = point,
    PreviousPoint = null,
    HValue = GetDistance(point, endPoint),
    GValue = int.MaxValue - GetDistance(point, endPoint)
}).ToDictionary(distance => (distance.Point.x, distance.Point.y));

var startDistance = distances[(startPoint.x, startPoint.y)];
startDistance.GValue = 0;
open.Add((startPoint.x, startPoint.y), startDistance);

int counter = 0;

Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();

do
{
    if (counter++ % 100 == 0)
        Console.WriteLine($"Closed: {closed.Count}, Open: {open.Count}");

    var currentDistance = open.Select(x => x.Value).OrderBy(x => x.FValue).First();
    var currentPoint = currentDistance.Point;

    var unvisitedNeigbours = NextStep(currentDistance.Point)
        .ToList();
  
    foreach (var neighbour in unvisitedNeigbours)
    {
        RiskDistance? neighbourDistance;
        if(closed.ContainsKey((neighbour.x, neighbour.y)))
            continue;

        if(!open.TryGetValue((neighbour.x, neighbour.y), out neighbourDistance))
        {
            neighbourDistance = distances[(neighbour.x, neighbour.y)];
            open.Add((neighbour.x, neighbour.y), neighbourDistance);
        }

        if (neighbour == endPoint) 
        {
            var endgvalue = currentDistance.GValue + neighbour.risk;
            var endfvalue = endgvalue + neighbourDistance.HValue;

            if(neighbourDistance.FValue > endfvalue)
            {
                distances[(neighbour.x, neighbour.y)].GValue = endgvalue;
                distances[(neighbour.x, neighbour.y)].PreviousPoint = currentDistance.Point;
            }
            open.Remove((currentPoint.x, currentPoint.y));
            break;
        }

        var gvalue = currentDistance.GValue + neighbour.risk;
        var fvalue = gvalue + neighbourDistance.HValue;
        
        if(neighbourDistance.FValue > fvalue)
        {
            distances[(neighbour.x, neighbour.y)].GValue = gvalue;
            distances[(neighbour.x, neighbour.y)].PreviousPoint = currentDistance.Point;
        }
    }

    closed.Add((currentPoint.x, currentPoint.y), currentDistance);
    open.Remove((currentPoint.x, currentPoint.y));
} while (open.Count > 0);
stopWatch.Stop();

Console.WriteLine($"Computing result took: {stopWatch.Elapsed:mm\\:ss\\.ff}");
Console.WriteLine($"Minimal risk: {distances[(grid.Length - 1, grid.Length - 1)].FValue}");

return;

int GetDistance(Point startPoint, Point endPoint)
{
    return Math.Abs((startPoint.x + startPoint.y) - (endPoint.x + endPoint.y));
}

Point[] NextStep(Point point)
{
    //Console.WriteLine($"Calculate next steps for: {point.x},{point.y}");

    var newPoints = new (int x, int y)[]
    {
        (point.x - 1, point.y),
        (point.x + 1, point.y),
        (point.x, point.y - 1),
        (point.x, point.y + 1),
    }.Where(newPoint => newPoint.x >= 0 && newPoint.y >= 0 && newPoint.x < grid[0].Length && newPoint.y < grid.Length)
    .ToArray();

    return newPoints.Select(newPoint => grid[newPoint.y][newPoint.x])
        .ToArray();
}

void DrawRoute(List<Point> route)
{
    if (route.Count == 0)
    {
        Console.Write($"{0},{0}({0})");
        return;
    }

    Console.Write($"{0},{0}({0}) -> ");
    foreach (var point in route.Take(route.Count() - 1))
    {
        Console.Write($"{point.x},{point.y}({point.risk}) -> ");
    }
    var goal = route.Last();
    Console.Write($"{goal.x},{goal.y}({goal.risk})");
}

Point[][] CreateGrid(Point[][] input, int scale)
{
    var grid = Enumerable.Range(0, length * scale).Select(y => Enumerable.Range(0, length * scale).Select(x => new Point()).ToArray()).ToArray();

    for (int i = 0; i < scale; ++i)
    {
        for (int y = 0; y < input.Length; ++y)
        {
            for (int x = 0; x < input[y].Length; ++x)
            {
                var originalPoint = input[y][x];
                var newPoint = new Point
                {
                    x = x + length * i,
                    y = y,
                    risk = originalPoint.risk + 1 * i > 9 ? Math.Abs((9 - (originalPoint.risk + 1 * i))) : originalPoint.risk + 1 * i
                };
                grid[y][x + length * i] = newPoint;
            }
        }
    }

    for (int i = 0; i < scale; ++i)
    {
        for (int y = 0; y < originalInput.Length; ++y)
        {
            for (int x = 0; x < grid[y].Length; ++x)
            {
                var originalPoint = grid[y][x];
                var newPoint = new Point
                {
                    x = x,
                    y = y + length * i,
                    risk = originalPoint.risk + 1 * i > 9 ? Math.Abs((9 - (originalPoint.risk + 1 * i))) : originalPoint.risk + 1 * i
                };
                grid[y + length * i][x] = newPoint;
            }
        }
    }

    return grid;
}

public class Point
{
    public int x;
    public int y;
    public int risk;

    public override string ToString()
    {
        return $"{x},{y} ({risk})";
    }
}

public class RiskDistance
{
    public Point Point { get; set; }
    public Point? PreviousPoint { get; set; } = null;
    public int FValue => GValue + HValue;
    public int GValue { get; set; }
    public int HValue { get; set; }
}


