using AdventOfCode;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace Solutions.y2022d14;

[Solution(2022, 14)]
public class Solution14
{
    public bool sandInMotion = false;

    [Part1]
    public void Part1(string filename)
    {
        Console.WriteLine("Part1 deactivated");

        return;
        Line[] paths = GetPaths(filename);
        var map = CreateMap(paths);

        while (Cycle(ref map))
        {
            //Draw(map);
        }

        int sandCounter = 0;
        foreach (var point in map.points)
        {
            if (point.PointType == PointType.Sand)
            {
                sandCounter++;
            }
        }

        Console.WriteLine($"Sand count before abyss: {sandCounter}");
    }

    [Part2]
    public void Part2(string filename)
    {
        Line[] paths = GetPaths(filename);
        var map = CreateMap(paths, true);

        while (Cycle(ref map))
        {
            //Draw(map);
        }

        int sandCounter = 0;
        foreach (var point in map.points)
        {
            if (point.PointType == PointType.Sand)
            {
                sandCounter++;
            }
        }

        Console.WriteLine($"Sand count before abyss: {sandCounter}");
    }

    private bool Cycle(ref Map map)
    {
        if (!map.IsSandInMotion())
        {
            map[map.StartPoint.x, map.StartPoint.y].PointType = PointType.SandInMotion;
            map.SandInMotion = new Point(map.StartPoint.x, map.StartPoint.y);
            return true;
        }

        if (map.SandInMotion.x == map.StartPoint.x && map.SandInMotion.y == map.StartPoint.y)
        {
            map[map.SandInMotion.x, map.SandInMotion.y].PointType = PointType.Start;
        }
        else
        {
            map[map.SandInMotion.x, map.SandInMotion.y].PointType = PointType.Air;
        }

        if (map.floor)
        {
            if (map.SandInMotion.x + 1 > map.MaxX || map.SandInMotion.x - 1 < map.MinX)
            {
                map = RescaleMap(map);
            }
        }

        if (map[map.SandInMotion.x, map.SandInMotion.y + 1].PointType == PointType.Air)
        {
            map[map.SandInMotion.x, map.SandInMotion.y + 1].PointType = PointType.SandInMotion;
        }
        else if (map[map.SandInMotion.x - 1, map.SandInMotion.y + 1].PointType == PointType.Air)
        {
            map[map.SandInMotion.x - 1, map.SandInMotion.y + 1].PointType = PointType.SandInMotion;
            map.SandInMotion.x -= 1;
        }
        else if (map[map.SandInMotion.x + 1, map.SandInMotion.y + 1].PointType == PointType.Air)
        {
            map[map.SandInMotion.x + 1, map.SandInMotion.y + 1].PointType = PointType.SandInMotion;
            map.SandInMotion.x += 1;
        }
        else
        {
            map[map.SandInMotion.x, map.SandInMotion.y] ??= new Point(map.SandInMotion.x, map.SandInMotion.y)
            {
                PointType = PointType.Sand
            };

            map[map.SandInMotion.x, map.SandInMotion.y].PointType = PointType.Sand;
            if (map.SandInMotion.x == 500 && map.SandInMotion.y == 0)
            {
                return false;
            }
            map.SandInMotion = null;
            return true;
        }

        if (map.SandInMotion.y + 1 < map.MaxY)
        {
            map.SandInMotion.y += 1;
            return true;
        }

        return false;
    }

    private Map RescaleMap(Map map)
    {
        var newMap = new Map
        {
            MinX = map.MinX - 1,
            MaxX = map.MaxX + 1,
            MinY = map.MinY,
            MaxY = map.MaxY,
        };

        newMap.floor = map.floor;
        newMap.points = new Point[newMap.MaxX - newMap.MinX + 1, newMap.MaxY - newMap.MinY + 1];
        newMap.StartPoint = map.StartPoint;

        newMap.SandInMotion = map.SandInMotion;
        //newMap.SandInMotion.x += 1;
        //newMap.StartPoint.x += 1;

        for (var y = map.MinY; y <= map.MaxY; ++y)
        {
            for (var x = map.MinX; x <= map.MaxX; ++x)
            {
                newMap[x, y] = map[x, y];
            }
        }

        for (var y = newMap.MinY; y <= newMap.MaxY; ++y)
        {
            for (var x = newMap.MinX; x <= newMap.MaxX; ++x)
            {
                newMap[x, y] ??= new Point(x, y) { PointType = PointType.Air };
            }
        }

        if (newMap.floor)
        {
            for (int x = newMap.MinX; x <= newMap.MaxX; ++x)
            {
                newMap[x, newMap.MaxY].PointType = PointType.Rock;
            }
        }

        return newMap;
    }

    private Line[] GetPaths(string filename)
    {
        List<Line> paths = new List<Line>();

        foreach (var line in File.ReadLines(filename))
        {
            var parts = line.Split("->");
            var points = line.Split("->").Select(part =>
            {
                var cords = part.Split(",").Select(int.Parse).ToArray();
                return new Point(cords[0], cords[1]);
            }).ToArray();

            var previousPoint = points[0];

            for (int i = 1; i < points.Length; i++)
            {
                paths.Add(new Line(previousPoint, points[i]));
                previousPoint = points[i];
            }
        }

        return paths.ToArray();
    }

    private Map CreateMap(IEnumerable<Line> paths, bool floor = false)
    {
        Map map = new();

        var xcords = paths.SelectMany(x => new[] { x.start.x, x.end.x });
        var ycords = paths.SelectMany(x => new[] { x.start.y, x.end.y });

        var minx = xcords.Min() - 1;
        var maxx = Math.Max(xcords.Max(), 500) + 1;
        var miny = Math.Min(ycords.Min(), 0);
        var maxy = ycords.Max();

        if (floor)
        {
            maxy += 2;
            map.floor = true;
        }

        map.points = new Point[maxx - minx + 1, maxy - miny + 1];
        map.MinX = minx;
        map.MaxX = maxx;
        map.MinY = miny;
        map.MaxY = maxy;

        for (int y = miny; y <= maxy; ++y)
        {
            for (int x = minx; x <= maxx; ++x)
            {
                map[x, y] = new Point(x, y)
                {
                    PointType = PointType.Air
                };
            }
        }

        foreach (var path in paths)
        {
            for (int y = path.start.y; y <= path.end.y; ++y)
            {
                for (int x = path.start.x; x <= path.end.x; ++x)
                {
                    map[x, y].PointType = PointType.Rock;
                }
            }
        }

        if (floor)
        {
            for (int x = map.MinX; x <= map.MaxX; ++x)
            {
                map[x, map.MaxY].PointType = PointType.Rock;
            }
        }

        map[500, 0].PointType = PointType.Start;
        map.StartPoint = new Point(500, 0) { PointType = PointType.Start };
        return map;
    }

    private void Draw(Map map)
    {
        for (int y = map.MinY; y <= map.MaxY; y++)
        {
            for (int x = map.MinX; x <= map.MaxX; x++)
            {
                map[x, y] ??= new Point(x, y)
                {
                    PointType = y == map.MaxY ? PointType.Rock : PointType.Air
                };
                Console.Write(map[x, y].PointType switch
                {
                    PointType.Start => "+",
                    PointType.Air => ".",
                    PointType.Sand => "o",
                    PointType.Rock => "#",
                    PointType.SandInMotion => "0",
                    _ => throw new NotImplementedException()
                });
            }
            Console.Write("\n");
        }
    }

    public class Point
    {
        public Point()
        { }

        public Point(int x, int y)
        { this.x = x; this.y = y; }

        public int x, y;

        public PointType PointType;
    }

    public class Line
    {
        public Point start;
        public Point end;

        public Line(Point start, Point end)
        {
            if (start.x > end.x || start.y > end.y)
                (start, end) = (end, start);

            this.start = start;
            this.end = end;
        }
    }

    public class Map
    {
        public Point[,] points = new Point[,] { };

        public Point this[int x, int y]
        {
            get
            {
                return points[x - MinX, y - MinY];
            }
            set
            {
                points[x - MinX, y - MinY] = value;
            }
        }

        public int MinX { get; internal set; }
        public int MaxX { get; internal set; }
        public int MinY { get; internal set; }
        public int MaxY { get; internal set; }
        public Point StartPoint { get; internal set; }

        public Point? SandInMotion;
        internal bool floor;

        internal bool IsSandInMotion()
        {
            return SandInMotion != null;
        }
    }

    public enum PointType
    {
        Air,
        Rock,
        Sand,
        Start,
        SandInMotion,
    }
}