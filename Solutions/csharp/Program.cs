using System.Reflection;
using System.Text.RegularExpressions;
using AdventOfCode;

if (args.Length == 0)
{
    Console.WriteLine("Please provide a date");
    return;
}

string pattern = @"((19|20)\d\d)/(0?[1-9]|1[012])";
Regex rg = new Regex(pattern);

var date = args[0];
if (!rg.IsMatch(date))
{
    Console.WriteLine("Date not in correct format");
    return;
}

var dateParts = date.Split("/");
int year = int.Parse(dateParts[0]);
int day = int.Parse(dateParts[1]);

var filename = args.Length >= 2 ? args[1] : $"Inputs/{year}/{day:00}.txt";

var solutions = Assembly.GetExecutingAssembly().GetTypes()
    .Where(type => type.IsDefined(typeof(SolutionAttribute)))
    .ToArray();

var solutionType = Assembly.GetExecutingAssembly().GetTypes()
    .Where(type => type.IsDefined(typeof(SolutionAttribute)))
    .Where(type => type.GetCustomAttribute<SolutionAttribute>()?.Year == year)
    .Where(type => type.GetCustomAttribute<SolutionAttribute>()?.Day == day)
    .SingleOrDefault();

if (solutionType == null)
{
    Console.WriteLine($"Solution for {year}/{day} not found");
    return;
}

var solution = Activator.CreateInstance(solutionType);

var part1Method = solutionType.GetMethods()
    .Where(method => method.IsDefined(typeof(Part1Attribute)))
    .SingleOrDefault();

if (part1Method == null)
{
    Console.WriteLine($"Part1 is not defined for {year}/{day}");
    return;
}

var part2Method = solutionType.GetMethods()
    .Where(method => method.IsDefined(typeof(Part2Attribute)))
    .SingleOrDefault();

if (part2Method == null)
{
    Console.WriteLine($"Part2 is not defined for {year}/{day}");
    return;
}

Console.Write("Answer part1: ");
part1Method.Invoke(solution, new[] { filename });
Console.Write("Answer part2: ");
part2Method.Invoke(solution, new[] { filename });
