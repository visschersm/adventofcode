namespace AdventOfCode.Y2015;

[Solution(2015, 2)]
public class Solution02
{
    [Part1]
    public void Part1(string filename)
    {
        var data = File.ReadAllLines(filename).Select(line => 
        {
            // lxwxh
            var splitted = line.Split("x");
            return new Dimensions 
            {
                l = int.Parse(splitted[0]),
                w = int.Parse(splitted[1]),
                h = int.Parse(splitted[2])
            };
        });

        int total = 0;
        foreach(var gift in data)
        {
            var minSide = min(min(gift.l * gift.w, gift.w * gift.h), gift.h * gift.l);
            total += surface(gift) + minSide;
        }

        Console.WriteLine(total);
        int surface(Dimensions gift)
        {
            return 2*gift.l*gift.w + 2*gift.w*gift.h + 2*gift.h*gift.l;
        }
    }

    [Part2]
    public void Part2(string filename)
    {

    }

    T min<T>(T a, T b)
        where T : IComparable<T>
    {
        return a.CompareTo(b) < 0 ? a : b;
    }

    private struct Dimensions
    {
        public int l, w, h;
    }
}