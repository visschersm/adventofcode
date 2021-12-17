Point[][] input = File.ReadAllLines("input.txt")
    .Select((line, y) =>
        line.Select((value, x) =>
            new Point(x, y, int.Parse(value.ToString()))
        ).ToArray()
    ).ToArray();

// Console.WriteLine("Found input:");
// for(int y = 0; y < input.Length; ++y)
// {
//     for(int x = 0; x < input[y].Length; ++x)
//     {
//         Console.Write(input[y][x]);
//     }
//     Console.WriteLine();
// }

var points = input.SelectMany(pointArray => pointArray.Select(point => point));
List<RiskDistance> visited = new();

var distances = points.Select(point => new RiskDistance
{
    Point = point,
    PreviousPoint = null,
    MinRisk = int.MaxValue
}).ToList();

List<RiskDistance> unvisited = new();
unvisited.AddRange(distances);

var startPoint = input[0][0];
var endPoint = input[input.Length - 1][input[0].Length - 1];

var startDistance = distances.Single(x => x.Point == startPoint);
startDistance.MinRisk = 0;

do
{
    Console.WriteLine($"Visited: {visited.Count}, Unvisited: {unvisited.Count}");
    var currentDistance = unvisited.OrderBy(x => x.MinRisk).First();

    var unvisitedNeigbours = NextStep(currentDistance.Point);

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

List<List<Point[]>> paths = new();
long bestRouteRisk = input[0].Sum(x => x.risk) + input.SelectMany(arr => arr.Where(point => point.x == 999)).Sum(x => x.risk);

Console.WriteLine($"Grid: {input[0].Length}x{input.Length}: {input[0].Length * input.Length}");
try
{
    FindPath(input[0][0], input[input.Length - 1][input[0].Length - 1], new List<Point>());
}
catch (Exception exception)
{
    Console.WriteLine(exception);
}

Console.WriteLine($"Paths found: {paths.Count}");
Console.WriteLine($"Best route risk level: {bestRouteRisk}");

void FindPath(
    Point currentPoint,
    Point goal,
    List<Point> goneRoute)
{
    Console.WriteLine($"CurrentPoint: {currentPoint.x},{currentPoint.y}");
    long currentRiskLevel = goneRoute.Sum(x => (long)x.risk);
    long minimalRemainingRiskLevel = (input.Length + input[0].Length) - (currentPoint.x + currentPoint.y);

    if ((currentRiskLevel + minimalRemainingRiskLevel) > bestRouteRisk)
    {
        // Console.WriteLine($"Less optimal route {currentRiskLevel} - {bestRouteRisk}");
        return;
    }

    // Console.WriteLine($"Current point: {currentPoint.x},{currentPoint.y}");
    // DrawRoute(goneRoute);
    // Console.WriteLine();

    var nextSteps = NextStep(currentPoint);

    //foreach (var nextStep in nextSteps)
    //{
    //    // Console.WriteLine($"NextStep: {nextStep.x},{nextStep.y}");
    //}

    foreach (var nextStep in nextSteps)
    {
        if (goneRoute.Contains(nextStep)) continue;
        if (nextStep.x == 0 && nextStep.y == 0) continue;

        goneRoute.Add(input[nextStep.y][nextStep.x]);

        if ((nextStep.x, nextStep.y) == (goal.x, goal.y))
        {
            Console.WriteLine($"### Goal found!: {goneRoute.Sum(point => point.risk)}");

            // DrawRoute(goneRoute.ToList());

            // Console.WriteLine();

            currentRiskLevel = goneRoute.Sum(x => x.risk);
            if (bestRouteRisk > currentRiskLevel)
                bestRouteRisk = currentRiskLevel;

            goneRoute.Remove(goneRoute.Last());
            return;
        };

        FindPath(nextStep, goal, goneRoute);
        goneRoute.Remove(goneRoute.Last());
    }

    // Console.WriteLine("No more steps");
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
    }.Where(newPoint => newPoint.x >= 0 && newPoint.y >= 0 && newPoint.x < input[0].Length && newPoint.y < input.Length)
    .ToArray();

    return newPoints.Select(newPoint => input[newPoint.y][newPoint.x])
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

public record Point(int x, int y, int risk);

public class RiskDistance
{
    public Point Point { get; set; }
    public Point? PreviousPoint { get; set; } = null;
    public int MinRisk { get; set; }

    //public int Risk()
    //{
    //    return MinRisk + PreviousPoint?.Risk() ?? 0;
    //}
}