using System.Linq;
using AdventOfCode;

namespace AdventOfCode.Y2021;

[Solution(2021, 03)]
public class Solution03
{
    [Part1]
    public void Part1(string filename)
    {
        Console.WriteLine("Hello, World!");

        var input = File.ReadAllLines(filename);
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
    }

    [Part2]
    public void Part2(string filename)
    {
        Console.WriteLine("Hello, World!");

        var input = File.ReadAllLines(filename);
        var charArrays = input.Select(x => x.ToCharArray()).ToArray();
        var rowCount = input.Count();
        double halfRowCount = rowCount / 2.0;
        var maxColumnCount = charArrays.First().Count();

        var testSets = charArrays;
        Console.WriteLine("Oxygen");
        Console.WriteLine($"Remaining testSets: {string.Join(", ", testSets.Select(row => string.Join("", row)))}");
        var oxyTestResult = foo(0, true, maxColumnCount, testSets);
        var oxyRate = string.Join("", oxyTestResult);
        Console.WriteLine($"OxyRate: {oxyRate}");
        var oxyValue = BinaryToDec(oxyRate);
        Console.WriteLine($"OxyValue: {oxyValue}");

        testSets = charArrays;
        Console.WriteLine("CO2");
        Console.WriteLine($"Remaining testSets: {string.Join(", ", testSets.Select(row => string.Join("", row)))}");
        var co2TestResult = foo(0, false, maxColumnCount, testSets);
        var co2Rate = string.Join("", co2TestResult);
        Console.WriteLine($"CO2Rate: {co2Rate}");
        var co2Value = BinaryToDec(co2Rate);
        Console.WriteLine($"CO2Value: {co2Value}");

        Console.WriteLine($"Life Support value: {oxyValue * co2Value}");
    }

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

    char[] foo(int columnIndex, bool mostCommon, int maxColumnCount, char[][] testSets)
    {
        if (columnIndex >= maxColumnCount)
            return Array.Empty<char>();

        var rowCount = testSets.Count();
        double halfRowCount = rowCount / 2.0;
        var column = GetColumn(testSets, columnIndex);
        var summedColumn = column.Sum();

        var bitCirteria = mostCommon ? GetMostCommon(column, halfRowCount) : GetLeastCommon(column, halfRowCount);

        testSets = testSets.Where(x => int.Parse(x[columnIndex].ToString()) == bitCirteria).ToArray();
        Console.WriteLine($"Column: {columnIndex} ({string.Join("", column)}), ({summedColumn})/({rowCount}) Bit Criteria: {bitCirteria}");
        Console.WriteLine($"Remaining testSets: {string.Join(", ", testSets.Select(row => string.Join("", row)))}");

        if (testSets.Count() == 1)
            return testSets.Single();

        if (testSets.Count() == 0)
            return Array.Empty<char>();

        return foo(++columnIndex, mostCommon, maxColumnCount, testSets);
    }

    int GetMostCommon(int[] column, double halfRowCount)
    {
        var summedColumn = column.Sum();
        return summedColumn >= halfRowCount ? 1 : 0;
    }

    int GetLeastCommon(int[] column, double halfRowCount)
    {
        var summedColumn = column.Sum();
        return summedColumn < halfRowCount ? 1 : 0;
    }

    int[] GetColumn(char[][] testSet, int columnIndex)
    {
        return testSet.Select(x => int.Parse(x[columnIndex].ToString())).ToArray();
    }
}

