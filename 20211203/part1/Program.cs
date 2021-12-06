using System.Linq;

Console.WriteLine("Hello, World!");

var input = File.ReadAllLines("input.txt");
var charArrays = input.Select(x => x.ToCharArray()).ToArray();
var inputCount = charArrays.Count();

Console.WriteLine($"InputCount: {inputCount}");

var gammaRate = new List<char>();
var epsilonRate = new List<char>();

for (int i = 0; i < charArrays.First().Count(); ++i)
{
    var column = charArrays.Select(x => int.Parse(x[i].ToString()));
    var summedValue = column.Sum(x => x);

    if (summedValue >= (double)inputCount / 2)
    {
        gammaRate.Add('1');
        epsilonRate.Add('0');
    }
    else
    {
        epsilonRate.Add('1');
        gammaRate.Add('0');
    }


    Console.WriteLine($"Column: {i} ({string.Join("", column)})\tSum: {summedValue}\t{summedValue}|{(double)inputCount / 2}");
}

var gammaValue = 0;
var epsilonValue = 0;

gammaValue = BinaryToDec(new string(gammaRate!.ToArray()));
epsilonValue = BinaryToDec(new string(epsilonRate!.ToArray()));

Console.WriteLine($"Gamma: {string.Join("", gammaRate)} ({gammaValue})");
Console.WriteLine($"Epsilon: {string.Join("", epsilonRate)} ({epsilonValue})");

Console.WriteLine($"Powerlevel: {gammaValue * epsilonValue}");

static int BinaryToDec(string input)
{
    Console.WriteLine($"BinaryToDec: {input}");
    char[] array = input.ToCharArray();
    // Reverse since 16-8-4-2-1 not 1-2-4-8-16. 
    Array.Reverse(array);
    /*
     * [0] = 1
     * [1] = 2
     * [2] = 4
     * etc
     */
    int sum = 0;

    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] == '1')
        {
            sum += (int)Math.Pow(2, i);
        }

    }

    return sum;
}