var input = File.ReadAllLines("input.txt")
     .Select(line => line.Select(c => int.Parse(c.ToString())).ToArray())
     .ToArray();

List<Point> lowPoints = new List<Point>();

for(int y = 0; y < input.Length; ++y)
{
    for(int x = 0; x < input[y].Length; ++x)
    {
        if(LowPoint(x, y))
        {
            //Console.WriteLine($"Add lowPoint: {x},{y}");
            lowPoints.Add(new Point(x, y));
        }
    }
}

List<List<Point>> Basins = new List<List<Point>>();
foreach(var point in lowPoints)
{
    //var point = lowPoints.Skip(3).First();
    var results = Basin(point);
    results.Add(point);
    Basins.Add(results.Distinct().ToList());
}

foreach(var basin in Basins.OrderByDescending(x => x.Count()))
{
    Console.WriteLine($"Basin of size: {basin.Count()}");
}

int total = 1;

foreach(var basin in Basins.OrderByDescending(x => x.Count()).Take(3))
{
    total *= basin.Count();
}

Console.WriteLine($"Total: {total}");



List<Point> Basin(Point point)
{
    List<Point> result = new();

    var(x, y) = point;

    //Console.WriteLine($"Point: {x},{y} value: {PointValue(point)}");
    if(x - 1 >= 0)
    {
        var pointLeft = new Point(x - 1, y);

        //Console.WriteLine($"PointLeft: {PointValue(pointLeft)}");
        if(PointValue(pointLeft) > PointValue(point) && PointValue(pointLeft) != 9)
        {
            result.Add(pointLeft);
            result.AddRange(Basin(pointLeft));
        }
    }
    
    if(x + 1 < input[y].Length)
    {
        var pointRight = new Point(x + 1, y);
        //Console.WriteLine($"PointRight: {PointValue(pointRight)}");
        if(PointValue(pointRight) > PointValue(point) && PointValue(pointRight) != 9)
        {
            result.Add(pointRight);
            result.AddRange(Basin(pointRight));
        }
    }
    
    if(y - 1 > 0)
    {
        var pointUp = new Point(x, y - 1);
        //Console.WriteLine($"PointUp: {PointValue(pointUp)}");

        if(PointValue(pointUp) > PointValue(point) && PointValue(pointUp) != 9)
        {
            result.Add(pointUp);
            result.AddRange(Basin(pointUp));
        }
    }

    if(y + 1 < input.Length)
    {
        var pointDown = new Point(x, y + 1);
        //Console.WriteLine($"PointDown: {PointValue(pointDown)}");

        if(PointValue(pointDown) > PointValue(point) && PointValue(pointDown) != 9)
        {
            result.Add(pointDown);
            result.AddRange(Basin(pointDown));
        }
    }
    
    return result;
}

bool LowPoint(int x, int y)
{
    var point = input[y][x];

    if((y - 1) >= 0 && point >= input[y - 1][x]) return false;
    if((y + 1) < input.Length && point >= input[y + 1][x]) return false;
    if((x - 1) >= 0 && point >= input[y][x - 1]) return false;
    if((x + 1) < input[y].Length && point >= input[y][x + 1]) return false;

    return true;
}

int PointValue(Point point)
{
    return input[point.y][point.x];
}

record Point(int x, int y);

