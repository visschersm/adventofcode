using System.Linq;
using System.Collections.Generic;

Console.WriteLine("Hello, World!");

var input = File.ReadAllLines("input.txt");
var charArrays = input.Select(x => x.ToCharArray()).ToArray();
var rowCount = input.Count();
double halfRowCount = rowCount / 2.0;
var maxColumnCount = charArrays.First().Count();

var testSets = charArrays;
Console.WriteLine("Oxygen");
Console.WriteLine($"Remaining testSets: {string.Join(", ", testSets.Select(row => string.Join("", row)))}");
var oxyTestResult = foo(0, true);
var oxyRate = string.Join("", oxyTestResult);
Console.WriteLine($"OxyRate: {oxyRate}");
var oxyValue = BinaryToDec(oxyRate);
Console.WriteLine($"OxyValue: {oxyValue}");

testSets = charArrays;
Console.WriteLine("CO2");
Console.WriteLine($"Remaining testSets: {string.Join(", ", testSets.Select(row => string.Join("", row)))}");
var co2TestResult = foo(0, false);
var co2Rate = string.Join("", co2TestResult);
Console.WriteLine($"CO2Rate: {co2Rate}");
var co2Value = BinaryToDec(co2Rate);
Console.WriteLine($"CO2Value: {co2Value}");

Console.WriteLine($"Life Support value: {oxyValue * co2Value}");

char[] foo(int columnIndex, bool mostCommon)
{
    if (columnIndex >= maxColumnCount)
        return Array.Empty<char>();

    rowCount = testSets.Count();
    halfRowCount = rowCount / 2.0;
    var column = GetColumn(testSets, columnIndex);
    var summedColumn = column.Sum();

    var bitCirteria = mostCommon ? GetMostCommon(column) : GetLeastCommon(column);

    testSets = testSets.Where(x => int.Parse(x[columnIndex].ToString()) == bitCirteria).ToArray();
    Console.WriteLine($"Column: {columnIndex} ({string.Join("", column)}), ({summedColumn})/({rowCount}) Bit Criteria: {bitCirteria}");
    Console.WriteLine($"Remaining testSets: {string.Join(", ", testSets.Select(row => string.Join("", row)))}");

    if (testSets.Count() == 1)
        return testSets.Single();

    if (testSets.Count() == 0)
        return Array.Empty<char>();

    return foo(++columnIndex, mostCommon);
}

int GetMostCommon(int[] column)
{
    var summedColumn = column.Sum();
    return summedColumn >= halfRowCount ? 1 : 0;
}

int GetLeastCommon(int[] column)
{
    var summedColumn = column.Sum();
    return summedColumn < halfRowCount ? 1 : 0;
}

int[] GetColumn(char[][] testSet, int columnIndex)
{
    return testSet.Select(x => int.Parse(x[columnIndex].ToString())).ToArray();
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