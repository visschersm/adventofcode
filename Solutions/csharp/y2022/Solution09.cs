using System.Collections.Generic;
using AdventOfCode;

namespace Solutions.y2022d09;

[Solution(2022, 9)]
public class Solution09
{
    [Part1]
    public void Part1(string filename)
    {
        List<Position> positions = new List<Position>
        {
            new Position(0,0)
        };
        
        Position head = new Position(0,0);
        Position tail = new Position(0,0);

        foreach(var command in File.ReadLines(filename))
        {
            var parts = command.Split(" ");
            var direction = parts[0];
            var amount = int.Parse(parts[1]);

            for(int i = 0; i < amount; ++i)
            {
                head = head.Move(direction);
                tail = tail.Chase(head);
                positions.Add(tail);
            }
        }

        Console.WriteLine($"All tail positions: {positions.Count()}");
        Console.WriteLine($"Distinct tail positions: {positions.GroupBy(pos => new {pos.x, pos.y}).Count()}");
    }

    [Part2]
    public void Part2(string filename)
    {
        List<Position> positions = new List<Position>();
        
        List<Position> rope = Enumerable.Range(0, 10).Select(x => new Position(0,0)).ToList();
        var head = rope.First();

        Console.WriteLine("Initial state");
        Draw(rope.ToArray());

        foreach(var command in File.ReadLines(filename))
        {
            var parts = command.Split(" ");
            var direction = parts[0];
            var amount = int.Parse(parts[1]);

            for(int i = 0; i < amount; ++i)
            {
                head.SetPosition(head.Move(direction));
                var previousKnot = head;
                foreach(var knot in rope)
                {
                    knot.SetPosition(knot.Chase(previousKnot));
                    previousKnot = knot;
                }
                
                positions.Add(new Position(rope.Last().x, rope.Last().y));
                //Console.WriteLine($"Position: {rope.Last().x},{rope.Last().y}");
            }

            //Draw(rope.ToArray());
        }

        Console.WriteLine($"All tail positions: {positions.Count()}");
        Console.WriteLine($"Distinct tail positions: {positions.GroupBy(pos => new {pos.x, pos.y}).Count()}");
    }

    public void Draw(Position[] positions)
    {
        var minX = positions.Min(pos => pos.x);
        var maxX = Math.Max(6, positions.Max(pos => pos.x));
        var minY = positions.Min(pos => pos.y);
        var maxY = Math.Max(6, positions.Max(pos => pos.y));

        Console.WriteLine();
        for(int y = minY; y <= maxY; ++y)
        {
            for(int x = minX; x <= maxX; ++x)
            {
                int? index = null;
                for (var i = 0; i < positions.Length; ++i)
                {
                    if (positions[i].x == x && positions[i].y == y)
                    {
                        index = i;
                        break;
                    }
                }

                if(index == null)
                {
                    Console.Write(".");
                }
                else
                {
                    Console.Write($"{index}");
                    index = null;
                }
            }
            Console.Write("\n");
        }
    }

    public class Position
    {
        public int x, y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Position Move(string direction)
        {
            switch(direction)
            {
                case "U":
                    this.y += 1;
                break;
                case "D":
                    this.y -= 1;
                    break;
                case "L":
                    this.x -= 1;
                    break;
                case "R":
                    this.x += 1;
                    break;
            }

            return new Position(this.x, this.y);
        }

        public Position Chase(Position head)
        {
            var xdelta = head.x - this.x;
            var ydelta = head.y - this.y;

            if (Math.Abs(xdelta) <= 1 && Math.Abs(ydelta) <= 1)
                return new Position(x, y);

            if (Math.Abs(xdelta) >= 1 && Math.Abs(ydelta) >= 1)
            {
                x += 1 * (xdelta / Math.Abs(xdelta));
                y += 1 * (ydelta / Math.Abs(ydelta));
                return new Position(x, y);
            }
            if(Math.Abs(xdelta) > 1)
                return new Position(x + 1 * (xdelta / Math.Abs(xdelta)), y);

            if(Math.Abs(ydelta) > 1)
                return new Position(x, y + 1 * (ydelta / Math.Abs(ydelta)));

            throw new NotImplementedException();
        }

        public void SetPosition(Position position)
        {
            x = position.x;
            y = position.y;
        }
    }
}
