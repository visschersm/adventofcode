using AdventOfCode;
using AdventOfCode.Y2021;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Solutions.y2022d16;

[Solution(2022, 16)]
public class Solution16
{
    [Part1]
    public void Part1(string filename)
    {
        var timeleft = 30;
        var valves = GetValves(filename);
        var map = GenerateMap(valves.ToArray());

        var startValve = valves.Single(valve => valve.Id == "AA");
        var otherValves = valves.Where(v => v.Id != startValve.Id && v.FlowRate > 0).ToArray();

        var cache = new Dictionary<string, int>();
        var maxValue = Compute(cache, map, timeleft, startValve, otherValves);

        Console.WriteLine($"Total released pressure: {maxValue}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var timeleft = 26;
        var valves = GetValves(filename);
        var map = GenerateMap(valves.ToArray());

        var startValve = valves.Single(valve => valve.Id == "AA");
        var otherValves = valves.Where(v => v.Id != startValve.Id && v.FlowRate > 0).ToArray();

        var cache = new Dictionary<string, int>();
        var maxValue = Compute(cache, map, timeleft, startValve, otherValves);

        Console.WriteLine($"Total released pressure: {maxValue}");
    }

    private Dictionary<(string, string), int> GenerateMap(Valve[] valves)
    {
        var result = new Dictionary<(string, string), int>();

        foreach (var valve in valves)
        {
            var route = GetRoute(valve, valves.ToList());
            foreach (var node in route)
            {
                result[(valve.Id, node.Id)] = node.Distance;
            }
        }

        return result;
    }

    private int Compute(
        Dictionary<string, int> cache,
        Dictionary<(string, string), int> map,
        int timeleft,
        Valve currentValve,
        Valve[] valves)
    {
        if (timeleft <= 0) return 0;
        string key = CreateKey(timeleft, currentValve, valves);

        if (!cache.ContainsKey(key))
        {
            if (currentValve.FlowRate > 0)
                timeleft -= 1;

            var currentFlow = Open(timeleft, currentValve);
            int maxFlow = Move(cache, map, timeleft, currentValve, valves);

            cache[key] = maxFlow + currentFlow;
        }

        return cache[key];
    }

    private static string CreateKey(int timeleft, Valve currentValve, Valve[] valves)
    {
        var v = string.Join("-", valves.OrderBy(x => x.Id).Select(x => x.Id));
        var key = $"{timeleft} {currentValve.Id}-{v}";
        return key;
    }

    private int Move(
        Dictionary<string, int> cache,
        Dictionary<(string, string), int> map,
        int timeleft,
        Valve currentValve,
        Valve[] valves)
    {
        int maxFlow = 0;

        foreach (var valve in valves)
        {
            var distance = map[(currentValve.Id, valve.Id)];

            if (distance + 1 >= timeleft) continue;

            var otherValves = valves.Where(v => v.Id != valve.Id).ToArray();
            maxFlow = Math.Max(maxFlow, Compute(cache, map, timeleft - distance, valve, otherValves));
        }

        return maxFlow;
    }

    private static int Open(int timeleft, Valve currentValve) => currentValve.FlowRate * timeleft;

    private List<Node> GetRoute(Valve startValve, List<Valve> valves)
    {
        var unvisited = valves.Select(valve => new Node
        {
            Id = valve.Id,
            Distance = valve.Id == startValve.Id ? 0 : int.MaxValue,
            Previous = (Node?)null,
            Neighbors = valve.Valves
        }).ToList();

        var visited = new List<Node>();

        while (unvisited.Count > 0)
        {
            var current = unvisited.OrderBy(node => node.Distance).First();
            var neighbors = unvisited.Where(node => current.Neighbors.Contains(node.Id)).ToList();

            foreach (var neighbor in neighbors)
            {
                var distance = current.Distance;
                if (current.Distance + 1 < neighbor.Distance)
                {
                    neighbor.Distance = current.Distance + 1;
                    neighbor.Previous = current;
                }
            }

            visited.Add(current);
            unvisited.Remove(current);
        }

        return visited;
    }

    private List<Valve> GetValves(string filename)
    {
        List<Valve> valves = new();

        Regex regex = new Regex(@"^Valve (?<id>[A-Z][A-Z]) has flow rate=(?<flowRate>\d+); tunnel(s?) lead(s?) to valve(s?) (?<valves>([A-Z][A-Z], ?|[A-Z][A-Z])+)$");

        foreach (var line in File.ReadLines(filename))
        {
            var match = regex.Match(line);
            var id = match.Groups["id"].Value;
            var flowRate = int.Parse(match.Groups["flowRate"].Value);
            var tunnels = match.Groups["valves"].Value.Split(", ").Select(x => x.Trim()).ToList();

            valves.Add(new Valve(id, flowRate, tunnels));
        }

        return valves;
    }

    private class Valve
    {
        public string Id { get; }
        public int FlowRate { get; }
        public List<string> Valves = new List<string>();
        public bool Open { get; set; } = false;

        public Valve(string id, int flowRate, List<string> valves)
        {
            this.Id = id;
            this.FlowRate = flowRate;
            this.Valves = valves;
        }
    }

    private class Node
    {
        public Node? Previous;
        public int Distance;
        public int Weight;

        public List<string> Neighbors = new List<string>();

        public string Id { get; internal set; }
    }
}