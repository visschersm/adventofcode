using AdventOfCode.Lib;
using AdventOfCode;
using System.Data;
using System.Linq;

namespace Solutions.y2022d04;

[Solution(2022, 4)]
public class Solution04
{
    [Part1]
    public void Part1(string filename)
    {
        var result = FileHelper.ReadByLine(filename).Select(line => 
        {
            var pair = line.Split(",");
            var split1 = pair[0].Split("-");
            var split2 = pair[1].Split("-");
            var area1 = (begin: int.Parse(split1[0]), end: int.Parse(split1[1]));
            var area2 = (begin: int.Parse(split2[0]), end: int.Parse(split2[1]));

            if(area1.begin >= area2.begin && area1.end <= area2.end) return true;
            if(area2.begin >= area1.begin && area2.end <= area1.end) return true;
            return false;
        }).ToList().Count(x => x == true);

        Console.WriteLine($"Counter: {result}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var result = FileHelper.ReadByLine(filename).Select(line => 
        {
            var pair = line.Split(",");
            var split1 = pair[0].Split("-");
            var split2 = pair[1].Split("-");
            var area1 = (begin: int.Parse(split1[0]), end: int.Parse(split1[1]));
            var area2 = (begin: int.Parse(split2[0]), end: int.Parse(split2[1]));

            if(area1.begin >= area2.begin && area1.begin <= area2.end) return true;
            if(area2.begin >= area1.begin && area2.end <= area1.end) return true;
            if(area1.end >= area2.begin && area1.end <= area2.end) return true;
            if(area2.end >= area1.begin && area2.end <= area1.end) return true;
            return false;
        }).ToList().Count(x => x == true);

        Console.WriteLine($"Counter: {result}");
    }
}
