﻿namespace AdventOfCode.Y2021;

[Solution(2021, 12)]
public class Solution12
{
    [Part1]
    public void Part1(string filename)
    {
        var input = File.ReadAllLines(filename);

        List<string> completedPaths = new List<string>();
        foreach(string startConnection in input.Where(line => line.ToLower().Contains("start")).Select(x => x.ToString()))
        {
            var startPoints = startConnection.Split("-");
            var startPoint = startPoints[0] == "start" ? startPoints[0] : startPoints[1];
            string path = startPoint;
            Console.WriteLine($"Start of path: {path}\t {startConnection}");
            CalculateNextPoint(path, startPoint, startConnection, completedPaths, input);
        }

        Console.WriteLine($"Completed paths: {string.Join("\n", completedPaths)}");
        Console.WriteLine($"Completed paths: {completedPaths.Count()}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var input = File.ReadAllLines(filename);

        List<string> completedPaths = new List<string>();
        foreach(string startConnection in input.Where(line => line.ToLower().Contains("start")).Select(x => x.ToString()))
        {
            var startPoints = startConnection.Split("-");
            var startPoint = startPoints[0] == "start" ? startPoints[0] : startPoints[1];
            string path = startPoint;
            Console.WriteLine($"Start of path: {path}\t {startConnection}");
            CalculateNextPoint(path, startPoint, startConnection, completedPaths, input);
        }

        Console.WriteLine($"Completed paths: {string.Join("\n", completedPaths)}");
        Console.WriteLine($"Completed paths: {completedPaths.Count()}");
    }

    void CalculateNextPoint(
        string path, 
        string currentPoint, 
        string connection, 
        List<string> completedPaths,
        string[] input)
    {
        if(path.Contains("end")) return;

        var points = connection.Split("-");
        var nextPoint = points[0] == currentPoint ? points[1] : points[0];

        Console.WriteLine($"Calculate next point: Current path:{path}\tNext connection:{connection}\t currentpoint: {currentPoint}, nextPoint: {nextPoint}");

        if(nextPoint == "start") 
        {
            Console.WriteLine("Cannot reroute back to start");
            return;
        }

        if(nextPoint == "end")
        {
            path += $",{nextPoint}";
            Console.WriteLine($"Path completed: {path}");
            Console.WriteLine();
            if(!completedPaths.Any(x => x == path))
                completedPaths.Add(path);
            return;
        }
            
        if(nextPoint.All(char.IsLower) && path.Contains(nextPoint))
        {
            Console.WriteLine($"Cannot reroute back to {nextPoint}");
            return;
        }
            

        path += $",{nextPoint}";
        Console.WriteLine($"Getting next connections: NextPoint: {nextPoint}");
        var nextConnections = input.Where(x => x.Split("-").Any(c => c.Equals(nextPoint)));
        foreach(string nextConnection in nextConnections)
        {
            CalculateNextPoint(path, nextPoint, nextConnection, completedPaths, input);
        }

        return;
    }
}