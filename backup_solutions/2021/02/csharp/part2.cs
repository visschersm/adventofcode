// forward increases horizontal position
// forward increases depth * aim
// down increasees aim
// up decreases aim

var input = File.ReadAllLines("input.txt");

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