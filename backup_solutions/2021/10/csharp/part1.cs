Tuple<char, char>[] validPairs = new Tuple<char, char>[]
{
    new Tuple<char, char>('[', ']'),
    new Tuple<char, char>('(', ')'),
    new Tuple<char, char>('<', '>'),
    new Tuple<char, char>('{', '}')
};

Tuple<char, int>[] scoreTable = new Tuple<char, int>[]
{
    new Tuple<char, int>(')', 3),
    new Tuple<char, int>(']', 57),
    new Tuple<char, int>('}', 1197),
    new Tuple<char, int>('>', 25137),
};

Tuple<char, int>[] autoCompleteScoreTable = new Tuple<char, int>[]
{
    new Tuple<char, int>(')', 1),
    new Tuple<char, int>(']', 2),
    new Tuple<char, int>('}', 3),
    new Tuple<char, int>('>', 4),
};

var input = File.ReadAllLines("input.txt");

List<long> scores = new();

List<string> invalidLines = new();
List<string> validLines = new();

foreach (var line in input)
{
    var addedChars = ValidateInput(line);

    if (!addedChars.Any())
        continue;

    Console.Write($"{new string(addedChars)}");

    long score = 0;
    foreach (var addedChar in addedChars)
    {
        score *= 5;
        score += autoCompleteScoreTable.Single(c => c.Item1 == addedChar).Item2;
    }

    Console.Write($" {score}");
    Console.WriteLine();
    scores.Add(score);
}

var orderedScores = scores.OrderBy(x => x).ToArray();

Console.WriteLine($"Middle score: {orderedScores[scores.Count() / 2]}");

char[] ValidateInput(string line)
{
    List<char> blocks = new List<char>();

    foreach (var c in line)
    {
        if (!validPairs.Any(t => t.Item1 == c || t.Item2 == c))
            throw new NotImplementedException();

        if (validPairs.SingleOrDefault(t => t.Item1 == c)?.Item1 is char openSeparator)
        {
            blocks.Add(openSeparator);
        }

        if (validPairs.SingleOrDefault(t => t.Item2 == c)?.Item2 is char closingSeparator)
        {
            var matchingOpenSeparator = MatchSeparator(closingSeparator);

            if (blocks.Last() != matchingOpenSeparator)
            {
                var matchingClosingSeparator = MatchSeparator(blocks.Last());
                //Console.WriteLine($"Expected {matchingClosingSeparator}, but found {closingSeparator} instead.");
                return Array.Empty<char>();
            }

            blocks.RemoveAt(blocks.Count - 1);
        }
    }

    blocks.Reverse();
    return blocks.Select(x => MatchSeparator(x)).ToArray();
}

char MatchSeparator(char separator)
{
    return separator switch
    {
        '[' => ']',
        ']' => '[',
        '(' => ')',
        ')' => '(',
        '<' => '>',
        '>' => '<',
        '{' => '}',
        '}' => '{',
        _ => throw new NotImplementedException(separator.ToString())
    };
}