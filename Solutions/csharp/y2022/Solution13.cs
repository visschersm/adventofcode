using AdventOfCode;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

namespace Solutions.y2022d13;

[Solution(2022, 13)]
public class Solution13
{
    [Part1]
    public void Part1(string filename)
    {
        List<Packet> packets = GetPackets(filename);

        int rightOrderCounter = 0;

        for (int i = 0; i < packets.Count; ++i)
        {
            var packet = packets[i];
            var result = Compare(packet.left, packet.right);

            if (result == null) throw new NotImplementedException();

            if (result.Value)
            {
                rightOrderCounter += (i + 1);
            }
        }

        Console.WriteLine($"Right order indicies count: {rightOrderCounter}");
    }

    [Part2]
    public void Part2(string filename)
    {
        List<Packet> packets = GetPackets(filename);
        List<Node> nodes = packets.SelectMany(packet => new[] { packet.left!, packet.right! })
            .ToList();

        var parent = new Node(null);
        parent.Nodes.Add(new Node(parent)
        {
            Value = 2
        });
        nodes.Add(parent);

        parent = new Node(null);
        parent.Nodes.Add(new Node(parent)
        {
            Value = 6
        });
        nodes.Add(parent);

        nodes = SortNodes(nodes);

        foreach (var node in nodes)
        {
            node.Draw();
        }

        var indices = LocateKey(nodes, new[] { 2, 6 });

        Console.WriteLine($"Decoder key: {indices.Aggregate((a, b) => a * b)}");
    }

    private List<Node> SortNodes(List<Node> nodes)
    {
        for (int i = 0; i < nodes.Count; ++i)
        {
            for (int j = 0; j < nodes.Count; ++j)
            {
                if (i == j) continue;

                var result = Compare(nodes[i], nodes[j]);
                if (result == null) throw new NotImplementedException();
                if (result.Value)
                {
                    var temp = nodes[i];
                    nodes[i] = nodes[j];
                    nodes[j] = temp;
                }
            }
        }

        return nodes;
    }

    private int[] LocateKey(List<Node> nodes, params int[] nodeValues)
    {
        List<int> result = new List<int>();

        foreach (var nodeValue in nodeValues)
        {
            int counter = 0;
            foreach (var node in nodes)
            {
                counter++;
                if (node.Nodes.Count == 1 && node.Nodes[0].Value == nodeValue)
                {
                    result.Add(counter);
                }
            }
        }

        return result.ToArray();
    }

    private Node? LocateNode(List<Node> nodes, int value)
    {
        foreach (var node in nodes)
        {
            if (node.Nodes.Count > 0)
                return LocateNode(node.Nodes, value);

            if (node.Value == value && node.parent.Nodes.Count == 1)
            {
                return node;
            }
        }

        return null;
    }

    private bool? Compare(Node left, Node right)
    {
        // Both integers
        if (left.Value != null && right.Value != null)
        {
            //Console.WriteLine($"Compare: {left.Value} vs {right.Value}");
            if (left.Value == right.Value) return null;

            return left.Value < right.Value;
        }

        // Both lists
        if (left.Value == null && right.Value == null)
        {
            int min = Math.Min(left.Nodes.Count, right.Nodes.Count);

            for (int i = 0; i < min; ++i)
            {
                var result = Compare(left.Nodes[i], right.Nodes[i]);
                if (result != null) return result;
            }

            if (left.Nodes.Count == right.Nodes.Count)
                return null;

            if (left.Nodes.Count < right.Nodes.Count)
                return true;

            if (left.Nodes.Count > right.Nodes.Count)
                return false;

            throw new NotImplementedException();
        }

        // Left integer, right list
        if (left.Value != null && right.Value == null)
        {
            var value = left.Value;
            var node = new Node(left)
            {
                Value = value
            };
            left.Value = null;
            left.Nodes.Add(node);

            var result = Compare(left, right);
            left.Nodes.Remove(node);
            left.Value = value;
            return result;
        }

        // Left list, right integer
        if (left.Value == null && right.Value != null)
        {
            var value = right.Value;
            var node = new Node(right)
            {
                Value = value
            };
            right.Value = null;
            right.Nodes.Add(node);
            var result = Compare(left, right);
            right.Nodes.Remove(node);
            right.Value = value;
            return result;
        }
        throw new NotImplementedException();
    }

    public class Packet
    {
        internal Node? left;

        internal Node? right;
    }

    public List<Packet> GetPackets(string filename)
    {
        Packet packet = new Packet();

        List<Packet> packets = new List<Packet>();

        var lines = File.ReadLines(filename).ToArray();

        for (int i = 0; i < lines.Count(); ++i)
        {
            var line = lines[i];

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var currentNode = new Node(null);

            if (packet.left == null) packet.left = currentNode;
            else if (packet.right == null)
            {
                packet.right = currentNode;
                packets.Add(packet);
                packet = new Packet();
            }

            while (line.Length > 0)
            {
                // Node
                if (line.StartsWith('['))
                {
                    line = line[1..];
                    var node = new Node(currentNode);
                    currentNode.Nodes.Add(node);
                    currentNode = node;
                    continue;
                }
                else if (line.StartsWith(']'))
                {
                    currentNode = currentNode.parent;
                    line = line[1..];
                    continue;
                }
                else if (line.StartsWith(','))
                {
                    line = line[1..];
                    continue;
                }
                else if (char.IsDigit(line[0]))
                {
                    var regex = new Regex(@"^\d+");
                    if (regex.IsMatch(line))
                    {
                        var match = regex.Match(line);
                        var value = int.Parse(match.Value);
                        var node = new Node(currentNode)
                        {
                            Value = value
                        };
                        currentNode.Nodes.Add(node);
                        line = line[match.Value.Length..];
                        continue;
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        return packets;
    }

    public class Node
    {
        public Node(Node? parent)
        {
            this.parent = parent;
        }

        public List<Node> Nodes = new List<Node>();
        public int? Value;
        public Node? parent;

        internal void Draw()
        {
            Console.Write("[");

            if (Value != null)
            {
                Console.Write(Value.ToString());
                return;
            }

            for (int i = 0; i < Nodes.Count; ++i)
            {
                Nodes[i].Draw();

                if (i < Nodes.Count - 1)
                    Console.Write(",");
            }

            Console.Write("]");

            if (parent == null)
                Console.Write("\n");
        }
    }
}