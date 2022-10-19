using AdventOfCode;

namespace Solutions.Y2021;

[Solution(2021, 2)]
public class Solution02
{
    [Part1]
    public void Part1(string filename)
    {
        // forward increases horizontal position
        // down increasees depth
        // up decreases depth

        var input = File.ReadAllLines(filename);

        var meassurements = input.Select(line =>
        {
            var direction = new string(line.TakeWhile(char.IsLetter).ToArray());
            return new
            {
                Direction = direction,
                Value = int.Parse(line.Substring(direction.Length))
            };
        });

        int horizontalValue = meassurements.Where(meassurement => meassurement.Direction == "forward")
            .Sum(meassurement => meassurement.Value);

        int downValue = meassurements.Where(meassurement => meassurement.Direction == "down")
            .Sum(meassurement => meassurement.Value);
        int upValue = meassurements.Where(meassurement => meassurement.Direction == "up")
            .Sum(meassurement => meassurement.Value);

        int depth = downValue - upValue;
        int totalValue = horizontalValue * depth;

        Console.WriteLine($"HorizontalValue: {horizontalValue}, Depth: {depth}, TotalValue: {totalValue}");

    }

    [Part2]
    public void Part2(string filename)
    {
        // forward increases horizontal position
        // forward increases depth * aim
        // down increasees aim
        // up decreases aim

        var input = File.ReadAllLines(filename);

        var meassurements = input.Select(line =>
        {
            var direction = new string(line.TakeWhile(char.IsLetter).ToArray());
            return new
            {
                Direction = direction,
                Value = int.Parse(line.Substring(direction.Length))
            };
        });

        int horizontalValue = 0;
        int depth = 0;
        int aim = 0;

        foreach (var meassurement in meassurements)
        {
            switch (meassurement.Direction)
            {
                case "up":
                    aim -= meassurement.Value;
                    break;
                case "down":
                    aim += meassurement.Value;
                    break;
                case "forward":
                    horizontalValue += meassurement.Value;
                    depth += aim * meassurement.Value;
                    break;
            }

            Console.WriteLine($"Direction: {meassurement.Direction}, Value: {meassurement.Value}, HorizontalValue: {horizontalValue}, Depth: {depth}, Aim {aim}");
        }

        Console.WriteLine($"TotalValue: {horizontalValue * depth}");
    }
}