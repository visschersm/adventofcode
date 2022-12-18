using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using AdventOfCode;
using static Solutions.y2022d15.Solution15;

namespace Solutions.y2022d15;

[Solution(2022, 15)]
public class Solution15
{
    [Part1]
    public void Part1(string filename)
    {
        return;
        int lineNumber = 2000000;

        List<Device> devices = GetDevices(filename);
        var sensors = devices.Where(device => device.GetType() == typeof(Sensor)).Cast<Sensor>().ToList();
        var beacons = devices.Where(device => device.GetType() == typeof(Beacon))
            .Where(beacon => beacon.position.y == lineNumber)
            .Select(beacon => beacon.position.x)
            .ToList();

        var minX = sensors.Min(sensor => sensor.position.x - sensor.ManhattanDistance);
        var maxX = sensors.Max(sensor => sensor.position.x + sensor.ManhattanDistance);

        int result = 0;
        for (int x = minX; x <= maxX; x++)
        {
            bool covered = false;
            foreach (var sensor in sensors)
            {
                if (sensor.position.x == x && sensor.position.y == lineNumber)
                    continue;

                if (beacons.Contains(x))
                    continue;

                var manhattanDistance = ManhattanDistance(new Position(x, lineNumber), sensor.position);
                if (manhattanDistance <= sensor.ManhattanDistance)
                {
                    covered = true;
                }
            }

            if (covered)
                result++;
        }

        Console.WriteLine($"The destres signal is not in {result} spots on line {lineNumber}");
    }

    [Part2]
    public void Part2(string filename)
    {
        List<Device> devices = GetDevices(filename);
        var sensors = devices.Where(device => device.GetType() == typeof(Sensor))
            .Cast<Sensor>()
            .ToArray();

        int minX = 0;
        int maxX = 4000000;
        int minY = 0;
        int maxY = 4000000;

        var distressPosition = GetDistressSignal(minX, minY, maxX, maxY, sensors);

        var result = GetTuningFrequency(distressPosition);
        Console.WriteLine($"The tuning frequency = {result}");
    }

    private Position GetDistressSignal(int minX, int minY, int maxX, int maxY, Sensor[] sensors)
    {
        for (var y = minY; y <= maxY; ++y)
        {
            Console.WriteLine($"y: {y}");
            for (var x = minX; x <= maxX; ++x)
            {
                var sensor = Covered(sensors, x, y);
                if (sensor != null)
                {
                    var delta = sensor.ManhattanDistance - ManhattanDistance(sensor.position, new Position(x, y));
                    if (delta > 0)
                        x += delta;
                }
                else
                {
                    return new Position(x, y);
                }
            }
        }

        throw new Exception();
    }

    private Sensor? Covered(Sensor[] sensors, int x, int y)
    {
        foreach (var sensor in sensors)
        {
            var manhattanDistance = ManhattanDistance(new Position(x, y), sensor.position);

            if (manhattanDistance <= sensor.ManhattanDistance)
            {
                return sensor;
            }
        }

        return null;
    }

    private long GetTuningFrequency(Position distressPosition)
    {
        return distressPosition.x * 4000000 + distressPosition.y;
    }

    private static List<Device> GetDevices(string filename)
    {
        List<Device> devices = new List<Device>();
        foreach (var line in File.ReadAllLines(filename))
        {
            // Sensor at x=2, y=18: closest beacon is at x=-2, y=15
            Regex regex = new Regex(@"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)");
            var match = regex.Match(line);

            var beacon = new Beacon(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
            devices.Add(beacon);
            var sensor = new Sensor(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            sensor.ClosestBeacon = beacon;
            devices.Add(sensor);
        }

        return devices;
    }

    public static int ManhattanDistance(Position a, Position b) => Math.Abs(b.x - a.x) + Math.Abs(b.y - a.y);

    public class Device
    {
        public Position position;

        public Device(int x, int y) : this(new Position(x, y))
        { }

        public Device(Position position) => this.position = position;
    }

    public class Sensor : Device
    {
        public Beacon ClosestBeacon
        {
            set
            {
                ManhattanDistance = ManhattanDistance(this.position, value.position);
            }
        }

        public int ManhattanDistance { get; internal set; }

        public Sensor(int x, int y) : base(x, y)
        { }

        public Sensor(Position position) : base(position)
        { }
    }

    public class Beacon : Device
    {
        public Beacon(int x, int y) : this(new Position(x, y))
        { }

        public Beacon(Position position) : base(position)
        {
        }
    }

    public struct Position
    {
        public int x, y;

        public Position()
        { }

        public Position(int x, int y) => (this.x, this.y) = (x, y);
    }

    public class Map
    {
        private List<Device> devices;
        private int minX;
        private int maxX;
        private int minY;
        private int maxY;
        private List<List<bool?>> points = new List<List<bool?>>();

        public Map(List<Device> devices)
        {
            this.devices = devices;
            var sensors = devices.Where(x => x.GetType() == typeof(Sensor)).Cast<Sensor>().ToArray();

            this.minX = 0;
            this.maxX = 20;
            this.minY = 0;
            this.maxY = 20;

            List<List<bool?>> result = new List<List<bool?>>();

            for (int y = minY; y <= maxY; ++y)
            {
                var row = new List<bool?>();
                result.Add(row);
                for (int x = minX; x <= maxX; ++x)
                {
                    row.Add(null);
                }
            }

            points = result;

            foreach (var device in devices)
            {
                if (device.position.x >= this.minX && device.position.x <= this.maxX && device.position.y >= this.minY && device.position.y <= this.maxY)
                {
                    this[device.position.x, device.position.y] = true;
                }
            }

            foreach (var sensor in sensors)
            {
                for (int y = minY; y <= maxY; ++y)
                {
                    for (int x = minX; x <= maxX; ++x)
                    {
                        var manhattanDistance = ManhattanDistance(new Position(x, y), sensor.position);
                        if (manhattanDistance <= sensor.ManhattanDistance)
                        {
                            if (this[x, y] != true)
                                this[x, y] = false;
                        }
                    }
                }
            }
        }

        public bool? this[int x, int y]
        {
            get
            {
                return points[y - minY][x - minX];
            }
            set
            {
                points[y - minY][x - minX] = value;
            }
        }

        public List<bool?> this[int y]
        {
            get
            {
                return points[y - minY];
            }
        }

        internal void Draw()
        {
            for (int y = minY; y < maxY; ++y)
            {
                Console.Write($"{y}\t");
                for (int x = minX; x < maxX; ++x)
                {
                    if (this[x, y] == null)
                        Console.Write(".");
                    else if (this[x, y] == false)
                        Console.Write("#");
                    else
                        Console.Write("S");
                }
                Console.Write("\n");
            }
        }
    }
}