namespace AdventOfCode.Y2015;

[Solution(2015, 2)]
public class Solution02
{
    [Part1]
    public void Part1(string filename)
    {
        var data = File.ReadAllLines(filename).Select(line => new Dimensions(line));
        int total = data.Sum(gift => gift.Surface() + gift.MinSurface());
        
        Console.WriteLine($"The elves need to order \"{total}\" square feet of wrapping paper");
    }

    [Part2]
    public void Part2(string filename)
    {
        var data = File.ReadAllLines(filename).Select(line => new Dimensions(line));
        var total = data.Sum(gift =>
        {
            return new[] { gift.l, gift.w, gift.h }.Order().Take(2).Sum(x => x * 2) + gift.Cubed();
        });
        
        Console.WriteLine($"The elves need to order \"{total}\" feet of ribbon.");
    }

    private record struct Dimensions
    {
        public Dimensions(string dimensions)
        {
            var splitted = dimensions.Split("x");
            l = int.Parse(splitted[0]);
            w = int.Parse(splitted[1]);
            h = int.Parse(splitted[2]);
        } 

        public int l, w, h;
        public int MinSurface() => Math.Min(Math.Min(l * w, w * h), h * l);
        public int Surface() => 2 * l * w + 2 * w * h + 2 * h * l;
        public int Cubed() => l * w * h;
        public (int, int) ShortestPerimeter()
        {
            //new[] {l, w, h}.Order().Take(2);
            var v1 = l * w;
            var v2 = w * h;
            var v3 = h * l;

            var faces = new[] { v1, v2, v3 };
            return (faces.Order().Take(1).Single(), faces.Order().Skip(1).Take(1).Single());
        }
    }
}
