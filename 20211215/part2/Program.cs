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

var grid = Enumerable.Range(0, length * scale).Select(y => Enumerable.Range(0, length * scale).Select(x => new Point()).ToArray()).ToArray();

for (int i = 0; i < scale; ++i)
{
    for (int y = 0; y < originalInput.Length; ++y)
    {
        for (int x = 0; x < originalInput[y].Length; ++x)
        {
            var originalPoint = originalInput[y][x];
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

var points = grid.SelectMany(pointArray => pointArray.Select(point => point)).ToArray();

List<RiskDistance> visited = new();

var distances = points.Select(point => new RiskDistance
{
    Point = point,
    PreviousPoint = null,
    MinRisk = int.MaxValue
}).ToList();

List<RiskDistance> unvisited = new();
unvisited.AddRange(distances);

var startPoint = grid[0][0];
var endPoint = grid[grid.Length - 1][grid[0].Length - 1];

var startDistance = distances.Single(x => x.Point == startPoint);
startDistance.MinRisk = 0;

int counter = 0;

do
{
    if (counter++ % 100 == 0)
        Console.WriteLine($"Visited: {visited.Count}, Unvisited: {unvisited.Count}");

    var currentDistance = unvisited.OrderBy(x => x.MinRisk).First();

    var unvisitedNeigbours = NextStep(currentDistance.Point)
        .Where(neighbour => unvisited.Any(y => y.Point == neighbour))
        .ToArray();

    foreach (var neighbour in unvisitedNeigbours)
    {
        var neighbourDistance = unvisited.SingleOrDefault(x => x.Point == neighbour);
        if (neighbourDistance == null) continue;

        var distanceToStart = neighbour.risk + currentDistance.MinRisk;
        if (neighbourDistance.MinRisk > distanceToStart)
        {
            distances.Single(x => x == neighbourDistance).MinRisk = neighbour.risk + currentDistance.MinRisk;
            distances.Single(x => x == neighbourDistance).PreviousPoint = currentDistance.Point;
        }
    }

    visited.Add(currentDistance);
    unvisited.Remove(currentDistance);
} while (unvisited.Count > 0);

Console.WriteLine($"Minimal risk: {distances.Last().MinRisk}");
return;

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
    public int MinRisk { get; set; }
}