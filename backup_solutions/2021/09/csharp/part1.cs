var input = File.ReadAllLines("input.txt")
     .Select(line => line.Select(c => int.Parse(c.ToString())).ToArray())
     .ToArray();

List<int> lowPoints = new List<int>();

for(int x = 0; x < input.Length; ++x)
{
    for(int y = 0; y < input[x].Length; ++y)
    {
        if(LowPoint(x, y))
        {
            lowPoints.Add(input[x][y]);
        }
    }
}

Console.WriteLine($"Total: {lowPoints.Sum(x => x + 1)}");

bool LowPoint(int x, int y)
{
    var point = input[x][y];

    if((x - 1) >= 0 && point >= input[x - 1][y]) return false;
    if((x + 1) < input.Length && point >= input[x + 1][y]) return false;
    if((y - 1) >= 0 && point >= input[x][y - 1]) return false;
    if((y + 1) < input[x].Length && point >= input[x][y + 1]) return false;

    return true;
}
