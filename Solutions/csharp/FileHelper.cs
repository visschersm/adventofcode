public class FileHelper
{
    public static IEnumerable<char> ReadByCharacter(string filename)
    {
        foreach(char c in File.ReadAllText(filename))
        {
            yield return c;
        }
    }
}