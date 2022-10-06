using System;
using System.IO;

var filename = "input.txt";
var data = File.ReadAllText(filename).Select(c => c == '(' ? 1 : -1);

Console.WriteLine("Santa is on the {0}th floor", data.Sum());