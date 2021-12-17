var input = File.ReadAllLines("test-input.txt")
    .Select((line, y) => line.Select((value, x) => (y: y, x: x, risk: value)).ToArray())
    .ToArray();

// for(var y = 0; y < input.Length; ++y)
// {
//     for(var x = 0; x < input[y].Length; ++x)
//     {
//         Console.WriteLine($"{x},{y}: {input[y][x].risk}");
//     }
// }

FindPath((0,0), (9,9));

// return paths
void FindPath((int x, int y) start, (int x, int y) goal, List<(int x, int y, int risk)> goneRoute)
{
    // right, down, left, up
    foreach(var nextStep in NextStep(start))
    {
        if(goneRoute.Contains(input[nextStep.y][nextStep.x])) continue;
        goneRoute.Add(input[nextStep.y][nextStep.x]);

        if(nextStep == goal) return;

        return FindPath(nextStep, goal, goneRoute);
    }
}

(int x, int y)[] NextStep((int x, int y) point)
{
    return new []
    {
        (point.x--, point.y),
        (point.x++, point.y),
        (point.x, point.y--),
        (point.x, point.y++),
    }.Where(point => point.x >= 0 && point.y >= 0 && point.x < input[0].Length && point.y < input.Length)
    .ToArray();
}

enum Direction
{
    Right,
    Down,
    Left,
    Up
}
