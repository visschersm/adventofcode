using System.Text.RegularExpressions;

Node node, node1, node2, result, foo;

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
return;

// For example, [1,2] + [[3,4],5] becomes [[1,2],[[3,4],5]]
node1 = new Node(1, 2);
node2 = new Node(new Node(3, 4), 5);

result = node1 + node2;
Console.WriteLine($"Addition: {node1} + {node2} = {result}");
Console.WriteLine();

foo = new Node(1, new Node(2, 3));
Console.WriteLine($"Foo: {foo}: {foo.a} leftnode: {foo.a.NearestLeftNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.b.a} leftnode: {foo.b.a.NearestLeftNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.b.b} leftnode: {foo.b.b.NearestLeftNeighbour()}");
Console.WriteLine();

foo = new Node(new Node(1, 2), new Node(3, 4));
Console.WriteLine($"Foo: {foo}: {foo.a.a} leftnode: {foo.a.a.NearestLeftNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.a.b} leftnode: {foo.a.b.NearestLeftNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.b.a} leftnode: {foo.b.a.NearestLeftNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.b.b} leftnode: {foo.b.b.NearestLeftNeighbour()}");
Console.WriteLine();

foo = new Node(new Node(1, 2), 3);
Console.WriteLine($"Foo: {foo}: {foo.a.a} leftnode: {foo.a.a.NearestLeftNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.a.b} leftnode: {foo.a.b.NearestLeftNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.b} leftnode: {foo.b.NearestLeftNeighbour()}");
Console.WriteLine();

foo = new Node(new Node(new Node(new Node(new Node(9, 8), 1), 2), 3), 4);
Console.WriteLine($"Foo: {foo}: {foo.a.a.a.a.a} leftnode: {foo.a.a.a.a.a.NearestLeftNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.a.a.a.a.b} leftnode: {foo.a.a.a.a.b.NearestLeftNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.a.a.a.b} leftnode: {foo.a.a.a.b.NearestLeftNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.a.a.b} leftnode: {foo.a.a.b.NearestLeftNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.a.b} leftnode: {foo.a.b.NearestLeftNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.b} leftnode: {foo.b.NearestLeftNeighbour()}");
Console.WriteLine();

foo = new Node(new Node(new Node(new Node(new Node(9, 8), 1), 2), 3), 4);
Console.WriteLine($"Foo: {foo}: {foo.a.a.a.a.a} rightnode: {foo.a.a.a.a.a.NearestRightNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.a.a.a.a.b} rightnode: {foo.a.a.a.a.b.NearestRightNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.a.a.a.b} rightnode: {foo.a.a.a.b.NearestRightNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.a.a.b} rightnode: {foo.a.a.b.NearestRightNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.a.b} rightnode: {foo.a.b.NearestRightNeighbour()}");
Console.WriteLine($"Foo: {foo}: {foo.b} rightnode: {foo.b.NearestRightNeighbour()}");
Console.WriteLine();

foo = new Node(new Node(1, 2), new Node(3, 4));
Console.WriteLine($"Source: {foo} - {foo.b}");
Console.WriteLine($"Left: {foo.b.NearestLeftNeighbour()}");
Console.WriteLine();

//[[[[[9,8],1],2],3],4] becomes [[[[0,9],2],3],4] (the 9 has no regular number to its left, so it is not added to any regular number).
foo = new Node(new Node(new Node(new Node(new Node(9, 8), 1), 2), 3), 4);
Console.WriteLine($"Exploding:\t{foo}");
foo.Reduce();
Console.WriteLine($"Exploded:\t{foo}");
Console.WriteLine();

// // [7,[6,[5,[4,[3,2]]]]] becomes [7,[6,[5,[7,0]]]] (the 2 has no regular number to its right, and so it is not added to any regular number).
foo = new Node(7, new Node(6, new Node(5, new Node(4, new Node(3, 2)))));
Console.WriteLine($"Exploding:\t{foo}");
foo.Reduce();
Console.WriteLine($"Exploded:\t{foo}");
Console.WriteLine();


// [[6,[5,[4,[3,2]]]],1] becomes [[6,[5,[7,0]]],3]
foo = new Node(new Node(6, new Node(5, new Node(4, new Node(3, 2)))), 1);
Console.WriteLine($"Exploding:\t{foo}");
foo.Reduce();
Console.WriteLine($"Exploded:\t{foo}");
Console.WriteLine();

//[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]] becomes [[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]] 
//(the pair [3,2] is unaffected because the pair [7,3] is further to the left; 
// [3,2] would explode on the next action).
// [[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]] becomes [[3,[2,[8,0]]],[9,[5,[7,0]]]].
foo = new Node(new Node(3, new Node(2, new Node(1, new Node(7, 3)))), new Node(6, new Node(5, new Node(4, new Node(3, 2)))));
Console.WriteLine($"Exploding:\t{foo}");
foo.Reduce();
Console.WriteLine($"Exploded:\t{foo}");
Console.WriteLine();

// [[[[4,3],4],4],[7,[[8,4],9]]] + [1,1]:
// after addition: [[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]] 
// after explode:  [[[[0,7],4],[7,[[8,4],9]]],[1,1]]
// after explode:  [[[[0,7],4],[15,[0,13]]],[1,1]]
// after split:    [[[[0,7],4],[[7,8],[0,13]]],[1,1]]
// after split:    [[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]
//                 [[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]
// after explode:  [[[[0,7],4],[[7,8],[6,0]]],[8,1]]
//                 [[[[0,7],4],[[7,8],[6,0]]],[8,1]]
node1 = new Node(new Node(new Node(new Node(4, 3), 4), 4), new Node(7, new Node(new Node(8, 4), 9)));
node2 = new Node(1, 1);
result = node1 + node2;
Console.WriteLine($"{node1} + {node2} = {result}");
result.Reduce();
Console.WriteLine($"Reduced: {result}");

// [1,1]
// [2,2]
// [3,3]
// [4,4]
// [[[[1,1],[2,2]],[3,3]],[4,4]]
node = new Node(1, 1) + new Node(2, 2);
node.Reduce();
node += new Node(3, 3);
node.Reduce();
node += new Node(4, 4);
node.Reduce();
Console.WriteLine($"First sum: {node}");
Console.WriteLine();

// [1,1]
// [2,2]
// [3,3]
// [4,4]
// [5,5]
// [[[[3,0],[5,3]],[4,4]],[5,5]]
node = new Node(1, 1) + new Node(2, 2);
node.Reduce();
node += new Node(3, 3);
node.Reduce();
node += new Node(4, 4);
node.Reduce();
node += new Node(5, 5);
node.Reduce();
Console.WriteLine($"Second sum: {node}");
Console.WriteLine();

// [1,1]
// [2,2]
// [3,3]
// [4,4]
// [5,5]
// [6,6]
// [[[[5,0],[7,4]],[5,5]],[6,6]]
node = new Node(1, 1) + new Node(2, 2);
node.Reduce();
node += new Node(3, 3);
node.Reduce();
node += new Node(4, 4);
node.Reduce();
node += new Node(5, 5);
node.Reduce();
node += new Node(6, 6);
node.Reduce();
Console.WriteLine($"Third sum: {node}");
Console.WriteLine();

// var firstNode = ParseToNode(input.First());
// foreach(var line in input.Skip(1))
// {

// }

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