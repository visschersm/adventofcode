var input = File.ReadAllLines("test-input.txt")
    .Select((line, y) => line.Select((value, x) => (x: x, y: y, risk: int.Parse(value.ToString()))).ToArray())
    .ToArray();

// Console.WriteLine("Found input:");
// for(int y = 0; y < input.Length; ++y)
// {
//     for(int x = 0; x < input[y].Length; ++x)
//     {
//         Console.Write(input[y][x]);
//     }
//     Console.WriteLine();
// }

List<(int x, int y, int risk)[]> paths = new();
int bestRouteRisk = int.MaxValue;

FindPath((0,0), input[input.Length - 1][input[0].Length -1], new List<(int x, int y, int risk)>());

Console.WriteLine($"Paths found: {paths.Count}");
Console.WriteLine($"Best route risk level: {bestRouteRisk}");

void FindPath(
    (int x, int y) currentPoint, 
    (int x, int y, int risk) goal, 
    List<(int x, int y, int risk)> goneRoute)
{
    Console.WriteLine($"Current point: {currentPoint.x},{currentPoint.y}");
    foreach(var nextStep in NextStep(currentPoint))
    {
        Console.WriteLine($"NextStep: {nextStep.x},{nextStep.y}");
    }
    
    int currentRiskLevel = goneRoute.Sum(x => x.risk);
    if(currentRiskLevel > bestRouteRisk)
    {
        Console.WriteLine($"Less optimal route {currentRiskLevel} - {bestRouteRisk}");
        return;
    }

    foreach(var nextStep in NextStep(currentPoint))
    {
        if(nextStep.x == 0 && nextStep.y == 0) continue;

        if(goneRoute.Contains(input[nextStep.y][nextStep.x])) 
        {
            Console.WriteLine($"Already gone this way: {currentPoint.x},{currentPoint.y} -> {nextStep.x},{nextStep.y}");
            continue;
        }

        goneRoute.Add(input[nextStep.y][nextStep.x]);
        
        if((nextStep.x, nextStep.y) == (goal.x, goal.y)) 
        {
            Console.WriteLine($"### Goal found!: {goneRoute.Sum(point => point.risk)}");

            Console.Write($"{0},{0}({0}) -> ");
            foreach(var point in goneRoute.Take(goneRoute.Count() - 1))
            {
                Console.Write($"{point.x},{point.y}({point.risk}) -> ");
            }
            Console.Write($"{goal.x},{goal.y}({goal.risk})");

            Console.WriteLine();

            currentRiskLevel = goneRoute.Sum(x => x.risk); 
            if(bestRouteRisk > currentRiskLevel)
                bestRouteRisk = currentRiskLevel;

            return;
        };

        FindPath(nextStep, goal, goneRoute);

        goneRoute.Remove(input[currentPoint.y][currentPoint.x]);
    }

    Console.WriteLine("No more steps");
}

(int x, int y)[] NextStep((int x, int y) point)
{
    //Console.WriteLine($"Calculate next steps for: {point.x},{point.y}");

    return new (int x, int y)[]
    {
        (point.x - 1, point.y),
        (point.x + 1, point.y),
        (point.x, point.y - 1),
        (point.x, point.y + 1),
    }
    .Where(newPoint => newPoint.x >= 0 && newPoint.y >= 0 && newPoint.x < input[0].Length && newPoint.y < input.Length)
    .ToArray();
}

