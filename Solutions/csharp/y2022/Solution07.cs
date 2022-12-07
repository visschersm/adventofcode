using System.Text.RegularExpressions;
using AdventOfCode;

namespace Solutions.y2022d07;

[Solution(2022, 7)]
public class Solution07
{
    [Part1]
    public void Part1(string filename)
    {
        var tree = BuildSystem(filename);

        var result = Flatten(tree)
            .Where(node => node.Type == NodeType.FOLDER)
            .Select(node => node.Size())
            .Where(node => node <= 100000)
            .Sum();

        //Find all of the directories with a total size of at most 100000. What is the sum of the total sizes of those directories?
        Console.WriteLine($"Sum of folders upto 100000: {result}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var tree = BuildSystem(filename);

        var totalSize = 70000000;
        var updateSize = 30000000;

        var nodes = Flatten(tree)
            .Where(node => node.Type == NodeType.FOLDER)
            .Select(node => node.Size());

        var freeSpace = totalSize - nodes.Max();
        var spaceNeeded = updateSize - freeSpace; 
        Console.WriteLine($"Space needed: {spaceNeeded}");

        var result = nodes.Where(node => node >= spaceNeeded).Order().First();
        // Find the smallest directory that, if deleted, would free up enough space on the filesystem 
        // to run the update. What is the total size of that directory?
        Console.WriteLine($"Minimum folder size to be deleted: {result}");
    }

    public IEnumerable<Node> Flatten(Node node)
    {
        foreach(var child in node.children)
        {
            foreach(var r in Flatten(child))
            {
                yield return r;
            }
        }

        yield return node;
    }

    public Node BuildSystem(string filename)
    {
        var root = new Node
        {
            Type = NodeType.FOLDER,
            Path = "/",
        };

        Node currentNode = root;

        foreach(var line in FileHelper.ReadByLine(filename))
        {
            if(line.StartsWith("$"))
            {
                // Command
                var command = new string(line.Skip(2).Take(2).ToArray());
                if(command == "cd")
                {
                    var path = new string(line.Skip(5).ToArray());
                    if(path == "..")
                    {
                        currentNode = currentNode.parent;
                        continue;
                    }
                    
                    if(path == "/")
                    {
                        currentNode = root;
                        continue;
                    }
                        
                    currentNode = currentNode.children.Single(node => node.Path == path);
                    continue;
                }
            }
            else // Listing
            {
                if(line.StartsWith("dir"))
                {
                    var path = line.Substring(4);
                    if(currentNode.children.Any(node => node.Path == path))
                        continue;

                    currentNode.children.Add(new Node
                    {
                        Path = path,
                        parent = currentNode,
                        Type = NodeType.FOLDER
                    });
                }
                else 
                {
                    var path = line.Split(" ")[1];
                    if(currentNode.children.Any(node => node.Path == path))
                        continue;
                        
                    var regex = Regex.Match(line, @"\d+");
                    var size = int.Parse(regex.ToString());

                    currentNode.children.Add(new Node
                    {
                        Path = path,
                        parent = currentNode,
                        Type = NodeType.FILE,
                        Filesize = size,
                    });
                }
            }
        }

        return root;
    }
}

public class Node
{
    public NodeType Type;
    public Node parent = null!;
    public string Path = null!;
    public int? Filesize;
    public List<Node> children = new List<Node>();

    public void Print(int depth)
    {
        var print = Type == NodeType.FILE 
            ? $"{Path} (file, size={Filesize})" 
            : $"{Path} (dir)";
        
        var tabs = string.Join("", Enumerable.Range(0, depth).Select(x => "\t").ToArray());
        Console.WriteLine($"{tabs}{print}");
        ++depth;
        foreach(var child in children)
        {
            child.Print(depth);
        }
    }

    public int Size()
    {
        return Filesize ?? children.Sum(x => x.Size());
    }
}

public enum NodeType
{
    FOLDER,
    FILE
}
