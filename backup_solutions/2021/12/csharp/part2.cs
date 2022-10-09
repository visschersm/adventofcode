var input = File.ReadAllLines("input.txt");

List<string> completedPaths = new List<string>();
foreach(string startConnection in input.Where(line => line.ToLower().Contains("start")).Select(x => x.ToString()))
{
    var startPoints = startConnection.Split("-");
    var startPoint = startPoints[0] == "start" ? startPoints[0] : startPoints[1];
    string path = startPoint;
    Console.WriteLine($"Start of path: {path}\t {startConnection}");
    CalculateNextPoint(path, startPoint, startConnection);
}

Console.WriteLine($"Completed paths: {string.Join("\n", completedPaths)}");
Console.WriteLine($"Completed paths: {completedPaths.Count()}");

void CalculateNextPoint(string path, string currentPoint, string connection)
{
    if(path.Contains("end")) return;

    var points = connection.Split("-");
    var nextPoint = points[0] == currentPoint ? points[1] : points[0];

    //Console.WriteLine($"Calculate next point: Current path:{path}\tNext connection:{connection}\t currentpoint: {currentPoint}, nextPoint: {nextPoint}");

    if(nextPoint == "start") 
    {
        //Console.WriteLine("Cannot reroute back to start");
        return;
    }

    if(nextPoint == "end")
    {
        path += $",{nextPoint}";
        //Console.WriteLine($"Path completed: {path}");
        //Console.WriteLine();
        if(!completedPaths.Any(x => x == path))
            completedPaths.Add(path);
        return;
    }
        
    if(nextPoint.All(char.IsLower) && path.Contains(nextPoint))
    {
        var smallCaveSecondVisit = path.Split(",").Where(c => c.All(char.IsLower)).GroupBy(x => x);
        if(smallCaveSecondVisit.Any(c => c.Count() > 1))
        {
           // Console.WriteLine($"Cannot reroute back to {nextPoint}");
            return;
        }
        //Console.WriteLine($"First second small cave visit, path: {path}\tnextpoint: {nextPoint}");
    }
        

    path += $",{nextPoint}";
    //Console.WriteLine($"Getting next connections: NextPoint: {nextPoint}");
    var nextConnections = input.Where(x => x.Split("-").Any(c => c.Equals(nextPoint)));
    foreach(string nextConnection in nextConnections)
    {
        CalculateNextPoint(path, nextPoint, nextConnection);
    }

    return;
}
