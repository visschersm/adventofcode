namespace AdventOfCode.Y2021;

[Solution(2021, 19)]
public class Solution19
{
    [Part1]
    public void Part1(string filename)
    {
        List<Beacon> beacons = new()
        {
            new (-500, 1000, -1500),
            new (1501, 0, -500)
        };

        Scanner scanner = new Scanner(500, 0, -500);
        var beacon = beacons.First();
        Console.WriteLine($"Scanner: ({scanner.x}, {scanner.y}, {scanner.z}) - Beacon: ({beacon.x}, {beacon.y}, {beacon.z}) - {Found(scanner, beacon)}");
        Detect(scanner, beacons);

        void Detect(Scanner scanner, List<Beacon> beacons)
        {
            var detectedBeacons = beacons
                .Where(beacon => Found(scanner, beacon));

            Console.WriteLine($"Detected: {string.Join("\n", detectedBeacons)}");
        }

        bool Found(Scanner scanner, Beacon beacon)
        {
            return scanner.x - 1000 <= beacon.x && scanner.x + 1000 >= beacon.x
                && scanner.y - 1000 <= beacon.y && scanner.y + 1000 >= beacon.y
                && scanner.z - 1000 <= beacon.z && scanner.z + 1000 >= beacon.z;

        }

        public record Scanner(int x, int y, int z);
        public record Beacon(int x, int y, int z);
    }
}
