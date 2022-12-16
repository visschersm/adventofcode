using AdventOfCode.Lib;
using AdventOfCode;
using System.Xml;

namespace Solutions.y2022d12;

[Solution(2022, 12)]
public class Solution12
{
    [Part1]
    public void Part1(string filename)
    {
        var startPositions = new char[] { 'S' };
        var map = ReadMap(filename, startPositions);

        var unvisited = GetUnvisited(map);
        List<Node> visited = Dijkstra(unvisited);

        var end = visited.Where(x => x.Point.x == map.End.x && x.Point.y == map.End.y).Single();
        int steps = ComputeDistance(end);

        // What is the fewest steps required to move from your current position to the location that should get the best signal?
        Console.WriteLine($"The fewest steps required: {steps}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var startPositions = new []{'S', 'a'};
        var map = ReadMap(filename, startPositions);

        var unvisited = GetUnvisited(map);
        List<Node> visited = Dijkstra(unvisited);
        
        var end = visited.Where(x => x.Point.x == map.End.x && x.Point.y == map.End.y).Single();
        int steps = ComputeDistance(end);

        // What is the fewest steps required to move from your current position to the location that should get the best signal?
        Console.WriteLine($"The fewest steps required: {steps}");
    }

    public struct Point
    {
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x, y;
    }

    public class Node
    {
        public Node? Previous;
        public int Distance;
        public char Height;

        public Point Point { get; internal set; }
    }

    public Map ReadMap(string filename, char[] startPositionsValues)
    {
        int yCounter = 0;
        Dictionary<Point, char> points = new Dictionary<Point, char>();
        List<Point> startPositions = new List<Point>();
        Point? end = null;

        foreach (var line in File.ReadLines(filename))
        {
            int xCounter = 0;
            foreach (var c in line)
            {
                char weight = c == 'S' ? 'a' : c == 'E' ? 'z' : c;
                var point = new Point(xCounter, yCounter);

                if (startPositionsValues.Contains(c))
                {
                    startPositions.Add(point);
                }

                if (c == 'E')
                {
                    end = point;
                }

                points.Add(point, weight);
                xCounter++;
            }

            yCounter++;
        }

        if (!startPositions.Any()) throw new NullReferenceException("Start was not provided");
        if (end == null) throw new NullReferenceException("End was not provided");

        return new Map
        {
            StartPositions = startPositions.ToArray(),
            End = end.Value,
            Points = points
        };
    }

    public class Map
    {
        public Point[] StartPositions;
        public Point End;
        public Dictionary<Point, char> Points = new Dictionary<Point, char>();
    }

    public List<Node> GetUnvisited(Map map)
    {
        return map.Points.Select(point => new Node
        {
            Previous = null,
            Distance = map.StartPositions.Any(sp => point.Key.x == sp.x && point.Key.y == sp.y) ? 0 : int.MaxValue,
            Height = point.Value,
            Point = point.Key
        }).ToList();
    }

    public List<Node> Dijkstra(List<Node> unvisited)
    {
        var visited = new List<Node>();
        
        while (unvisited.Any())
        {
            var current = unvisited.OrderBy(u => u.Distance).First();
            var neighbours = unvisited.Where(u => !(u.Point.x == current.Point.x && u.Point.y == current.Point.y))
                .Where(u => u.Height == current.Height || (u.Height - current.Height) <= 1)
                .Where(u => Math.Abs(u.Point.x - current.Point.x) <= 1 && Math.Abs(u.Point.y - current.Point.y) <= 1)
                .Where(u => !(Math.Abs(u.Point.x - current.Point.x) == 1 && Math.Abs(u.Point.y - current.Point.y) == 1))
                .ToArray();

            foreach (var neighbour in neighbours)
            {
                if (current.Distance + 1 < neighbour.Distance)
                {
                    neighbour.Distance = current.Distance + 1;
                    neighbour.Previous = current;
                }
            }

            visited.Add(current);
            unvisited.Remove(current);
        }

        return visited;
    }

    public int ComputeDistance(Node node)
    {
        int steps = 0;
        
        while (node.Previous != null)
        {
            steps++;
            node = node.Previous;
        };

        return steps;
    }
}