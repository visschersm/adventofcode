using System.Text.RegularExpressions;

namespace AdventOfCode.Y2021;

[Solution(2021, 18)]
public class Solution18
{
    Node node, node1, node2, result, foo;

    [Part1]
    public void Part1(string filename)
    {
        var input = File.ReadLines("input.txt");

        var firstNode = ParseToNode(input.First());
        foreach(var line in input.Skip(1))
        {
            var secondNode = ParseToNode(line);

            Console.WriteLine($"\t{firstNode}");
            Console.WriteLine($"+\t{secondNode}");
            firstNode += secondNode;
            firstNode.Reduce();
            Console.WriteLine($"\t{firstNode}");
            Console.WriteLine();
        }

        Console.WriteLine($"Magnitude: {firstNode.Magnitude()}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var input = File.ReadLines(filename);
        var inputNodes = input.Select(line => ParseToNode(line)).ToArray();

        var highestMagnitude = int.MinValue;

        for(int i = 0; i < inputNodes.Length; ++i)
        {
            for(int j = 0; j < inputNodes.Length; ++j)
            {
                if(i == j) continue;
                
                var node1 = inputNodes[i];
                var node2 = inputNodes[j];

                var result1 = node1 + node2;
                result1.Reduce();

                var result2 = node2 + node1;
                result2.Reduce();

                highestMagnitude = Math.Max(Math.Max(highestMagnitude, result1.Magnitude()), result2.Magnitude());
            }
        }

        Console.WriteLine($"Max magnitude: {highestMagnitude}");
    }

    Node ParseToNode(string line)
    {
        Node? currentNode = null;

        bool firstValue = false;
        bool secondValue = false;

        foreach (var c in line)
        {
            if (c == '[')
            {
                if (firstValue)
                {
                    currentNode.a = new Node();
                    currentNode.a.parent = currentNode;
                    currentNode = currentNode.a;
                    continue;
                }

                if (secondValue)
                {
                    currentNode.b = new Node();
                    currentNode.b.parent = currentNode;
                    currentNode = currentNode.b;
                    firstValue = true;
                    secondValue = false;
                    continue;
                }

                currentNode = new Node();
                firstValue = true;
                continue;
            }

            if (char.IsDigit(c))
            {
                if (firstValue)
                {
                    currentNode.a = new Node(int.Parse(c.ToString()));
                    currentNode.a.parent = currentNode;
                    firstValue = false;
                    continue;
                }

                if (secondValue)
                {
                    currentNode.b = new Node(int.Parse(c.ToString()));
                    currentNode.b.parent = currentNode;
                    secondValue = false;
                    continue;
                }

                throw new NotImplementedException();
            }


            if (c == ',')
            {
                secondValue = true;
                continue;
            }

            if (c == ']')
            {
                currentNode = currentNode?.parent ?? currentNode;
                continue;
            }
        }

        return currentNode;
    }
}

public class Node
{
    public Node() { }
    public Node(int a, int b)
    {
        this.a = new Node(a);
        this.a.parent = this;
        this.b = new Node(b);
        this.b.parent = this;
    }

    public Node(int value)
    {
        this.value = value;
    }

    public Node(int a, Node b)
    {
        this.a = new Node(a);
        this.a.parent = this;
        this.b = b;
        this.b.parent = this;
    }

    public Node(Node a, int b)
    {
        this.a = a;
        this.a.parent = this;
        this.b = new Node(b);
        this.b.parent = this;
    }

    public Node(Node a, Node b)
    {
        this.a = a;
        this.a.parent = this;
        this.b = b;
        this.b.parent = this;
    }

    public static Node operator +(Node a, Node b)
    {
        return new Node(a, b);
    }

    public override string ToString()
    {
        if (value != null) return $"{value}";

        return $"[{a},{b}]";
    }

    public void Reduce()
    {
        // Console.WriteLine($"Before reduce:\t{this}");
        bool active = false;
        do
        {
            active = false;
            while(Explode(0))
            {
                // Console.WriteLine($"After explode:\t{this}");
                active = true;
            } 
            if(Split(0))
            {
                active = true;
                // Console.WriteLine($"After split:\t{this}");
            }
        } while (active);
    }

    public bool Explode(int depth)
    {
        if (depth >= 4)
        {
            var isExploding = this.a?.value != null && this.b?.value != null;

            if (isExploding)
            {
                int avalue = (int)(this.a.value);
                int bvalue = (int)(this.b.value);

                var leftNeighbour = this.NearestLeftNeighbour();
                if (leftNeighbour != null)
                {
                    leftNeighbour.value += avalue;
                }

                var rightNeighbour = this.NearestRightNeighbour();
                if (rightNeighbour != null)
                {
                    rightNeighbour.value += bvalue;
                }

                this.a = null;
                this.b = null;
                this.value = 0;
                return true;
            }
        }

        if (a?.Explode(depth + 1) ?? false) return true;
        if (b?.Explode(depth + 1) ?? false) return true;

        return false;
    }

    public bool Split(int depth)
    {
        bool result = false;

        if (this.value > 9)
        {
            this.a = new Node((int)Math.Floor((float)this.value / 2.0f));
            this.a.parent = this;
            this.b = new Node((int)Math.Ceiling((float)this.value / 2.0f));
            this.b.parent = this;
            this.value = null;
            return true;
        }

        if (a?.Split(depth + 1) ?? false) return true;
        if (b?.Split(depth + 1) ?? false) return true;

        return false;
    }

    public int Magnitude()
    {
        if(this.a == null && this.b == null)
            return this.value!.Value;

        // [1,2]
        // 3 * a + 2 * b
        return 3 * this.a.Magnitude() + 2 * this.b.Magnitude(); 
    }

    public Node? parent;
    public Node? a;
    public Node? b;

    public int? value;
}

public static class NodeExtensions
{
    public static Node? LeftValueNode(this Node source)
    {
        if (source.value != null) return source;
        if (source.a.value != null) return source.a;
        if (source.a != null) return LeftValueNode(source.a);
        if (source.b != null) return LeftValueNode(source.b);

        throw new NotImplementedException();
    }

    public static Node? RightValueNode(this Node source)
    {
        if (source.value != null) return source;
        if (source.b.value != null) return source.b;
        if (source.b != null) return RightValueNode(source.b);
        if (source.a != null) return RightValueNode(source.a);

        throw new NotImplementedException();
    }

    public static Node? NearestRightNeighbour(this Node source, Node? node = null)
    {
        if (source?.parent != null)
        {
            if (source.parent.b.value != null && source.parent.b != source)
                return source.parent.b;

            if (source.parent.b.value == null && source.parent.b != source)
                return source.parent.b.LeftValueNode();

            if (source.parent.b == source)
                return source.parent.NearestRightNeighbour();
        }

        return null;
    }

    public static Node? NearestLeftNeighbour(this Node source)
    {
        if (source?.parent != null)
        {
            if (source.parent.a.value != null && source.parent.a != source)
                return source.parent.a;

            if (source.parent.a.value == null && source.parent.a != source)
                return source.parent.a.RightValueNode();

            if (source.parent.a == source)
                return source.parent.NearestLeftNeighbour();
        }

        return null;
    }

    public static Node GetRoot(this Node source)
    {
        if (source.parent == null) return source;

        return GetRoot(source.parent);
    }
}