using AdventOfCode;

namespace Solutions.y2022d10;

[Solution(2022, 10)]
public class Solution10
{
    [Part1]
    public void Part1(string filename)
    {
        // 20th cycle and every 40 cycles after that (that is, during the 20th, 60th, 100th, 140th, 180th, and 220th cycles).
        var cycleChecks = Enumerable.Range(0, 6).Select(x => 40 * x + 20);
        int totalValue = 0;
        int xvalue = 1;
        int cycle = 0;

        foreach(var line in File.ReadAllLines(filename))
        {
            if(line.StartsWith("addx"))
            {
                var value = int.Parse(line.Split(" ")[1]);
                IncreaseCycle();
                IncreaseCycle();
                xvalue += value;
            }
            else if(line.StartsWith("noop"))
            {
                IncreaseCycle();
            }
            else
            {
                throw new NotImplementedException($"Command not known: {line}");
            }
        }
        
        Console.WriteLine($"Final xvalue: {xvalue}");
        Console.WriteLine($"Combined signal strength: {totalValue}");

        void IncreaseCycle()
        {
            if(cycleChecks.Contains(++cycle))
            {
                var cycleValue = xvalue * cycle;
                Console.WriteLine($"Cycle: {cycle}, Value: {cycleValue}");
                totalValue += cycleValue;
            }
        }
    }

    [Part2]
    public void Part2(string filename)
    {
        // 20th cycle and every 40 cycles after that (that is, during the 20th, 60th, 100th, 140th, 180th, and 220th cycles).
        var cycleChecks = Enumerable.Range(0, 6).Select(x => 40 * x + 20);
        int xvalue = 1;
        int cycle = 0;

        Console.Write("Cycle   1 -> "); 
        foreach(var line in File.ReadAllLines(filename))
        {
            if(line.StartsWith("addx"))
            {
                var value = int.Parse(line.Split(" ")[1]);
                IncreaseCycle();
                IncreaseCycle();
                xvalue += value;
            }
            else if(line.StartsWith("noop"))
            {
                IncreaseCycle();
            }
            else
            {
                throw new NotImplementedException($"Command not known: {line}");
            }
        }
        
        void IncreaseCycle()
        {
            ++cycle;

            var crt = cycle % 40 - 1;
            if ((xvalue - 1) <= crt && crt <= (xvalue + 1))
            {
                Console.Write("#");
            }
            else
            {
                Console.Write(".");
            }

            if (cycle % 40 == 0)
            {
                Console.Write($" <- Cycle {cycle:000}"); 
                Console.Write("\n");
                if(cycle != 240)
                Console.Write($"Cycle {cycle:000} -> "); 
                return;
            }

            
        }
    }
}
