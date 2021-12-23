var input = File.ReadLines("test-input1.txt");

// For example, [1,2] + [[3,4],5] becomes [[1,2],[[3,4],5]].
Node node1 = new Node(1, 2);
Node node2 = new Node(new Node(3,4), 5);

Node result = node1 + node2;

Console.WriteLine($"{node1} + {node2} = {result}");
//[[[[[9,8],1],2],3],4] becomes [[[[0,9],2],3],4] (the 9 has no regular number to its left, so it is not added to any regular number).
Node explodingNode = new Node(new Node(new Node(new Node(new Node(9, 8), 1), 2), 3), 4);
Console.WriteLine($"Exploding:\t{explodingNode}");
explodingNode.Reduce();
Console.WriteLine($"Exploded:\t{explodingNode}");

// [7,[6,[5,[4,[3,2]]]]] becomes [7,[6,[5,[7,0]]]] (the 2 has no regular number to its right, and so it is not added to any regular number).
explodingNode = new Node(7, new Node(6, new Node(5, new Node(4, new Node(3, 2)))));
Console.WriteLine($"Exploding:\t{explodingNode}");
explodingNode.Reduce();
Console.WriteLine($"Exploded:\t{explodingNode}");


// [[6,[5,[4,[3,2]]]],1] becomes [[6,[5,[7,0]]],3]
explodingNode = new Node(new Node(6, new Node(5, new Node(4, new Node(3, 2)))), 1);
Console.WriteLine($"Exploding:\t{explodingNode}");
explodingNode.Reduce();
Console.WriteLine($"Exploded:\t{explodingNode}");

//[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]] becomes [[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]] 
//(the pair [3,2] is unaffected because the pair [7,3] is further to the left; 
// [3,2] would explode on the next action).
// [[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]] becomes [[3,[2,[8,0]]],[9,[5,[7,0]]]].
explodingNode = new Node(new Node(3, new Node(2, new Node(1, new Node(7, 3)))), new Node(6, new Node(5, new Node(4, new Node(3, 2)))));
Console.WriteLine($"Exploding:\t{explodingNode}");
explodingNode.Reduce();
Console.WriteLine($"Exploded:\t{explodingNode}");

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
            var isExploding = this.a?.value != null && this.b?.value != null;

            if(isExploding)
            {
                var root = this.parent.parent.parent.parent;
                
                int avalue = (int)(this.a.value);
                int bvalue = (int)(this.b.value);

                this.a = null;
                this.b = null;

                if(this.parent.a.value != null)
                {
                    this.parent.a.value += avalue;
                }
                else if(this.parent.parent.a.value != null)
                {
                    this.parent.parent.a.value += avalue;
                }
                else if(this.parent.parent.parent.a.value != null)
                {
                    this.parent.parent.parent.a.value += avalue;
                }
                else if(this.parent.parent.parent.parent.a.value != null)
                {
                    this.parent.parent.parent.parent.a.value += avalue;
                }
                
                if(this.parent.b.value != null)
                {
                    this.parent.b.value += bvalue;
                }
                else if(this.parent.parent.b.value != null)
                {
                    this.parent.parent.b.value += bvalue;
                } 
                else if(this.parent.parent.parent.b.value != null)
                {
                    this.parent.parent.parent.b.value += bvalue;
                }
                else if(this.parent.parent.parent.parent.b.value != null)
                {
                    this.parent.parent.parent.parent.b.value += bvalue;
                }

                this.value = 0;
            }
        }

        a?.Explode(depth + 1);
        b?.Explode(depth + 1);
    }

    Node? parent;
    Node? a;
    Node? b;

    int? value;
}