using System.Reflection;
using System.Drawing;
var input = File.ReadAllLines("input.txt")
    .Select((line, y) => line.Select((c, x) => new Point
        {
            X = x,
            Y = y,
            Value = int.Parse(c.ToString())
        }).ToArray())
    .ToArray();

int amountOfSteps = 100;
int flashCount = 0;
Console.WriteLine("Before any steps");
DrawGrid();

for(int i = 0; i < amountOfSteps; ++i)
{
    IncrementValues();
    CheckForFlash();

    flashCount += input.SelectMany(line => line.Select(point => point)).Count(point => point.Flashed);
    ResetFlash();

    if(i < 10 || (i + 1) % 10 == 0)
    {
        Console.WriteLine();
        Console.WriteLine($"After step {i + 1}");
        DrawGrid();
    }
}

Console.WriteLine($"After step {amountOfSteps} there have been a total of {flashCount} flashes.");

void IncrementValues()
{
    for(int y = 0; y < input.Length; ++y)
    {
        for(int x = 0; x < input[y].Length; ++x)
        {
            input[y][x].Value++;
        }
    }
}

void CheckForFlash()
{
    for(int y = 0; y < input.Length; ++y)
    {
        for(int x = 0; x < input[y].Length; ++x)
        {
            CheckPointForFlash(input[y][x]);
        }
    }
}

void CheckPointForFlash(Point point)
{
    if(point.Value > 9 && !point.Flashed)
    {
        point.Flashed = true;
        var x = point.X;
        var y = point.Y;

        if(y - 1 >= 0) Flashed(input[y - 1][x]);
        if(y + 1 < input.Length) Flashed(input[y + 1][x]);
        if(x - 1 >= 0) Flashed(input[y][x - 1]);
        if(x + 1 < input[y].Length) Flashed(input[y][x + 1]);

        if(y - 1 >= 0 && x - 1 >= 0) Flashed(input[y - 1][x - 1]);
        if(y + 1 < input.Length && x - 1 >= 0) Flashed(input[y + 1][x - 1]);
        if(y - 1 >= 0 && x + 1 < input[y].Length) Flashed(input[y - 1][x + 1]);
        if(y + 1 < input.Length && x + 1 < input[y].Length) Flashed(input[y + 1][x + 1]);
        
    }
}

void Flashed(Point point)
{
    point.Value += 1;
    CheckPointForFlash(point);
}

void ResetFlash()
{
    for(int y = 0; y < input.Length; ++y)
    {
        for(int x = 0; x < input[y].Length; ++x)
        {
            var point = input[y][x];
            if(point.Flashed)
            {
                point.Flashed = false;
                point.Value = 0;
            }
        }
    }
}

void DrawGrid()
{
    for(int y = 0; y < input.Length; ++y)
    {
        for(int x = 0; x < input[y].Length; ++x)
        {
            Console.Write(input[y][x]);
        }
        Console.WriteLine();
    }
}





class Point
{
    public int X = 0;
    public int Y = 0;
    public int Value = 0;
    public bool Flashed = false;

    public override string ToString()
    {
        return $"{Value}";
    }
}
