namespace AdventOfCode.Lib;

public class FileHelper
{
    public static IEnumerable<char> ReadByCharacter(string filename)
    {
        foreach (char c in File.ReadAllText(filename))
        {
            yield return c;
        }
    }

    public static IEnumerable<string> ReadByLine(string filename)
    {
        foreach (string line in File.ReadLines(filename))
        {
            yield return line;
        }
    }
}