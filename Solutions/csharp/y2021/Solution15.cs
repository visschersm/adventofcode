using System.Diagnostics;

namespace AdventOfCode.Y2021;

[Solution(2021, 15)]
public class Solution15
{
    [Part1]
    public void Part1(string filename)
    {
        Point[][] input = File.ReadAllLines("input.txt")
            .Select((line, y) =>
                line.Select((value, x) =>
                    new Point
                    {
                        x = x, 
                        y = y, 
                        risk = int.Parse(value.ToString())
                    }
                ).ToArray()
            ).ToArray();

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

            var unvisitedNeigbours = NextStep(currentDistance.Point, input);

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
            FindPath(
                input[0][0], 
                input[input.Length - 1][input[0].Length - 1], 
                new List<Point>(),
                bestRouteRisk,
                input);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }

        Console.WriteLine($"Paths found: {paths.Count}");
        Console.WriteLine($"Best route risk level: {bestRouteRisk}");
    }

    [Part2]
    public void Part2(string filename)
    {
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

        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        do
        {
            if (counter++ % 100 == 0)
                Console.WriteLine($"Visited: {visited.Count}, Unvisited: {unvisited.Count}");

            var currentDistance = unvisited.OrderBy(x => x.MinRisk).First();

            var unvisitedNeigbours = NextStep(currentDistance.Point, originalInput)
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

        Console.WriteLine($"Computing result took: {stopWatch.Elapsed:mm\\:ss\\.ff}");
        Console.WriteLine($"Minimal risk: {distances.Last().MinRisk}");
    }

    [Part2]
    public void Part2_AStar(string filename)
    {
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

        var grid = CreateGrid(originalInput, scale, length, originalInput);
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

            var unvisitedNeigbours = NextStep(currentDistance.Point, originalInput)
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
    }

    void FindPath(
        Point currentPoint,
        Point goal,
        List<Point> goneRoute, 
        long bestRouteRisk,
        Point[][] input)
    {
        Console.WriteLine($"CurrentPoint: {currentPoint.x},{currentPoint.y}");
        long currentRiskLevel = goneRoute.Sum(x => (long)x.risk);
        long minimalRemainingRiskLevel = (input.Length + input[0].Length) - (currentPoint.x + currentPoint.y);

        if ((currentRiskLevel + minimalRemainingRiskLevel) > bestRouteRisk)
        {
            return;
        }

        var nextSteps = NextStep(currentPoint, input);

        foreach (var nextStep in nextSteps)
        {
            if (goneRoute.Contains(nextStep)) continue;
            if (nextStep.x == 0 && nextStep.y == 0) continue;

            goneRoute.Add(input[nextStep.y][nextStep.x]);

            if ((nextStep.x, nextStep.y) == (goal.x, goal.y))
            {
                Console.WriteLine($"### Goal found!: {goneRoute.Sum(point => point.risk)}");

                currentRiskLevel = goneRoute.Sum(x => x.risk);
                if (bestRouteRisk > currentRiskLevel)
                    bestRouteRisk = currentRiskLevel;

                goneRoute.Remove(goneRoute.Last());
                return;
            };

            FindPath(nextStep, goal, goneRoute, bestRouteRisk, input);
            goneRoute.Remove(goneRoute.Last());
        }
    }

    Point[] NextStep(Point point, Point[][] input)
    {
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

    int GetDistance(Point startPoint, Point endPoint)
    {
        return Math.Abs((startPoint.x + startPoint.y) - (endPoint.x + endPoint.y));
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
    };

    public class RiskDistance
    {
        public Point Point { get; set; }
        public Point? PreviousPoint { get; set; } = null;
        public int MinRisk { get; set; }
        public int FValue => GValue + HValue;
        public int GValue { get; set; }
        public int HValue { get; set; }
    }

    Point[][] CreateGrid(Point[][] input, int scale, int length, Point[][] originalInput)
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
}