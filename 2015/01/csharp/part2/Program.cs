using System;
using System.IO;
using System.Linq;

var filename = "input.txt";
var floor = 0;
var tryCounter = 0;

var text = File.ReadAllText(filename).ToList();
File.ReadAllText(filename).Foreach()


foreach(var c in text)
{
    tryCounter++;
    floor += c == '(' ? 1 : -1;
    if(floor == -1) break;
};

Console.WriteLine("Santa found the basement after {0} tries", tryCounter);