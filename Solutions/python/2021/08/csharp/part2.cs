var input = File.ReadAllLines("input.txt")
    .Select(x =>
    {
        var startIndex = x.IndexOf("|");
        return new
        {
            Input = x.Substring(0, startIndex).Trim().Split(" "),
            Output = x.Substring(startIndex + 2).Trim().Split(" ")
        };
    })
    .ToList();

int sum = 0;
foreach (var line in input)
{
    var patterns = DeterminePattern(line.Input);

    var numbers = line.Output.Select(x => DetermineNumber(x, patterns));
    var number = int.Parse(string.Join("", numbers));
    sum += number;
}

Console.WriteLine($"Total: {sum}");

// 1 2seg
// 4 4seg
// 7 3seg
// 8 7seg

string[] DeterminePattern(string[] input)
{
    string[] patterns = new string[10];
    foreach (var pattern in input.Where(x => x.Length == 2 || x.Length == 4 || x.Length == 3 || x.Length == 7))
    {
        if (pattern.Length == 2) patterns[1] = pattern;
        else if (pattern.Length == 4) patterns[4] = pattern;
        else if (pattern.Length == 3) patterns[7] = pattern;
        else if (pattern.Length == 7) patterns[8] = pattern;
    }

    var ab = patterns[1];
    var abef = patterns[4];
    var abd = patterns[7];
    var abcdefg = patterns[8];

    var d = new string(patterns[7].Except(ab).ToArray());
    var ef = new string(patterns[4].Except(patterns[1]).ToArray());

    var c = input.Where(x => x.Length == 5).Select(x => new string(x.Except(patterns[4]).Except(d).ToArray())).First(x => x.Length == 1);
    var g = new string(patterns[8].Except(ab).Except(c).Except(d).Except(ef).ToArray());

    patterns[2] = input.Where(x => x.Length == 5).First(x => x.Contains(g));

    var b = new string(patterns[1].Except(patterns[2]).ToArray());
    var a = new string(ab.Except(b).ToArray());

    patterns[9] = new string(patterns[8].Except(g).ToArray());
    patterns[6] = new string(patterns[8].Except(a).ToArray());
    patterns[5] = new string(patterns[8].Except(a).Except(g).ToArray());

    var e = new string(patterns[8].Except(patterns[2]).Except(b).ToArray());
    var f = new string(ef.Except(e).ToArray());

    patterns[0] = new string(patterns[8].Except(f).ToArray());
    patterns[3] = new string(patterns[8].Except(e).Except(g).ToArray());

    // int counter = 0;
    // foreach (var pattern in patterns)
    // {
    //     Console.WriteLine($"Pattern for {counter} {pattern}");
    //     counter++;
    // }
    return patterns;
}

string DetermineNumber(string str, string[] patterns)
{
    for (int i = 0; i < 10; ++i)
    {
        if (patterns[i].Length != str.Length)
            continue;

        if (str.All(c => patterns[i].Contains(c)))
            return $"{i}";
    }

    throw new NotImplementedException(str);
}