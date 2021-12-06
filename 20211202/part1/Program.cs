// forward increases horizontal position
// down increasees depth
// up decreases depth

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

int horizontalValue = meassurements.Where(meassurement => meassurement.Direction == "forward")
    .Sum(meassurement => meassurement.Value);

int downValue = meassurements.Where(meassurement => meassurement.Direction == "down")
    .Sum(meassurement => meassurement.Value);
int upValue = meassurements.Where(meassurement => meassurement.Direction == "up")
    .Sum(meassurement => meassurement.Value);

int depth = downValue - upValue;
int totalValue = horizontalValue * depth;

Console.WriteLine($"HorizontalValue: {horizontalValue}, Depth: {depth}, TotalValue: {totalValue}");
