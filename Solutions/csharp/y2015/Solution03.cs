using AdventOfCode;

namespace Solutions.y2015d03;

[Solution(2015, 3)]
public class Solution03
{
    [Part1]
    public void Part1(string filename)
    {
        var currentPosition = (0, 0);

        Dictionary<(int, int), int> gifts = new();

        gifts.Add(currentPosition, 1);

        foreach(var c in FileHelper.ReadByCharacter(filename))
        {
            currentPosition = c switch
            {
                '<' => (currentPosition.Item1 - 1, currentPosition.Item2),
                '^' => (currentPosition.Item1, currentPosition.Item2 + 1),
                '>' => (currentPosition.Item1  + 1, currentPosition.Item2),
                'v' => (currentPosition.Item1, currentPosition.Item2 - 1),
                _ => throw new InvalidDataException()
            };

            if(!gifts.ContainsKey(currentPosition))
                gifts.Add(currentPosition, 0);

            gifts[currentPosition] += 1;
        }

        Console.WriteLine($"Number of houses that receive a present: {gifts.Count}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var startPosition = (0, 0);
        var santaCurrentPosition = startPosition;
        var robotCurrentPosition = startPosition;

        Dictionary<(int, int), int> gifts = new();

        gifts.Add(startPosition, 1);
        
        int counter = 0;
        foreach(var c in FileHelper.ReadByCharacter(filename))
        {
            counter++;
            var santaTurn = counter % 2 == 0;
            
            var currentPosition = santaTurn ? santaCurrentPosition : robotCurrentPosition;

            var newPosition = c switch
            {
                '<' => (currentPosition.Item1 - 1, currentPosition.Item2),
                '^' => (currentPosition.Item1, currentPosition.Item2 + 1),
                '>' => (currentPosition.Item1  + 1, currentPosition.Item2),
                'v' => (currentPosition.Item1, currentPosition.Item2 - 1),
                _ => throw new InvalidDataException()
            };

            if(santaTurn) santaCurrentPosition = newPosition;
            else robotCurrentPosition = newPosition;

            if(!gifts.ContainsKey(newPosition))
                gifts.Add(newPosition, 0);

            gifts[newPosition] += 1;
        }

        Console.WriteLine($"Number of houses that receive a present: {gifts.Count}");
    }
}
