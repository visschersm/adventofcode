using AdventOfCode;

namespace Solutions.Y2021;

[Solution(2021, 1)]
public class Solution01
{
    [Part1]
    public void Part1(string filename)
    {
        var measurements = File.ReadAllLines(filename).Select(x => int.Parse(x));//.ToArray();

        var current = measurements.First(); 
        var result = measurements.Skip(1).Aggregate(0, (result, next) => 
        {
            result += next > current ? 1 : 0;
            current = next;
            return result;
        });
        
        Console.WriteLine($"The amount of meassurments larger: {result}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var measurements = File.ReadAllLines(filename).Select(x => int.Parse(x)).ToArray();

        var result = Enumerable.Range(1, measurements.Count() - 3).Aggregate(0, (result, index) =>
        {
            int current = measurements[index - 1] + measurements[index] + measurements[index + 1];
            int next = measurements[index] + measurements[index + 1] + measurements[index + 2];
            
            result += current < next ? 1 : 0;
            return result;
        });
        
        Console.WriteLine($"increasedCounter: {result}");
    }
}