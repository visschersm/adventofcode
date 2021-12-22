var input = File.ReadLines("test-input1.txt");

// For example, [1,2] + [[3,4],5] becomes [[1,2],[[3,4],5]].
Node node1 = new Node(1, 2);
Node node2 = new Node(new Node(3,4), 5);

Node result = node1 + node2;

Console.WriteLine($"{node1} + {node2} = {result}");
//[[[[[9,8],1],2],3],4] becomes [[[[0,9],2],3],4]
//[[[[[9,8],1],2],3],4] becomes [[[[0,9],2],3],4] (the 9 has no regular number to its left, so it is not added to any regular number).
Node explodingNode = new Node(new Node(new Node(new Node(new Node(9, 8), 1), 2), 3), 4);
Console.WriteLine($"Exploding:\t{explodingNode}");
explodingNode.Reduce();
Console.WriteLine($"Exploded:\t{explodingNode}");

//[7,[6,[5,[4,[3,2]]]]] becomes [7,[6,[5,[7,0]]]] (the 2 has no regular number to its right, and so it is not added to any regular number).
Node otherExplodingNode = new Node(7, new Node(6, new Node(5, new Node(4, new Node(3, 2)))));
Console.WriteLine($"Exploding:\t{otherExplodingNode}");
otherExplodingNode.Reduce();
Console.WriteLine($"Exploded:\t{otherExplodingNode}");
public class Node
{
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
        if(value != null) return $"{value}";
        
        return $"[{a},{b}]";    
    }

    public void Reduce()
    {
        Explode(0);
        //Split();
    }

    public void Explode(int depth)
    {
        if(depth == 4)
        {
            var grandParent = parent.parent;
            var explodingNode = parent?.b;

            if(explodingNode != null)
            {
                if(explodingNode?.parent?.b?.value != null)
                {
                    int value = (int)(explodingNode!.b!.value! + explodingNode!.parent.b!.value!);
                    grandParent.a = new Node(0, value);
                }

                if(explodingNode?.a?.value != null)
                {
                    int value = (int)(explodingNode!.a!.value! + explodingNode!.parent.a!.value!);
                    grandParent.b = new Node(value, 0);
                }
            }
        }

        a?.Explode(depth + 1);
        b?.Explode(depth + 1);
        return;
        // b.Explode(++depth);
        //a?.a?.a?.a
        //a?.a?.a?.b
        // var grandParent = a?.a;
        // var parent = grandParent?.a;
        // var explodingNode = parent?.a;
        // if(explodingNode != null)
        // {
        //     if(explodingNode == explodingNode.parent.a)
        //     {
        //         int value = (int)(explodingNode!.b!.value! + explodingNode!.parent.b!.value!);
        //         grandParent.a = new Node(0, value);
        //     }

        //     if(explodingNode == explodingNode.parent.b)
        //     {
        //         int value = (int)(explodingNode!.a!.value! + explodingNode!.parent.a!.value!);
        //         grandParent.a = new Node(value, 0);
        //     }
        //     //[[9,8],1]
        //     // explodingNode.parent.a.value = explodingNode.a.value + explodingNode.parent.a.value;
        //     // Console.WriteLine($"ANode: {explodingNode.parent.a}");
        //     // explodingNode.parent.b.value = explodingNode.b.value + explodingNode.parent.b.value;
        //     // Console.WriteLine($"Calculate: [{explodingNode.parent.a.value},{explodingNode.parent.b.value}]");
        //     //explodingNode.b.value + explodingNode.parent.b.value;
        // }
        // else
        // {
        //     Console.WriteLine("No explode");
        // }
    }

    Node? parent;
    Node? a;
    Node? b;

    int? value;
}