﻿using System.Text;
using System;
using System.Security.AccessControl;
using System.Collections.Generic;

namespace AdventOfCode.Y2021;

[Solution(2021, 4)]
public class Solution04
{
    private int[] numbersDrawn = [];
    private int amountOfNumbersDrawn = 0;

    [Part1]
    public void Part1(string filename)
    {
        var input = File.ReadAllLines(filename);
        int[] numbersDrawn = input.First().Split(",").Select(x => int.Parse(x)).ToArray();
        var amountOfNumbersDrawn = 0;

        var cardData = input.Skip(2).ToList();

        var cards = CreateCard(cardData).ToList();

        var counter = 0;
        bool winnerFound = false;
        do
        {
            var numberDrawn = DrawNextNumber();
            Console.WriteLine($"Number drawn: {numberDrawn}!");

            foreach (var card in cards)
            {
                card.DrawMarked();
                Console.WriteLine();
            }

            foreach (var card in cards)
            {
                (bool complete, int index) = card.CheckCard(numberDrawn);
                if (complete)
                {
                    var unmarkedScore = card.GetUnmarkedSum();
                    Console.WriteLine($"Card with index: {card.Index + 1} won!");
                    Console.WriteLine($"Card unmarked score: {unmarkedScore}");
                    Console.WriteLine($"Total score: {unmarkedScore * numberDrawn}");
                    winnerFound = complete;
                    break;
                }
            };

            counter++;
        }
        while (!winnerFound);



        // void DrawCards(List<Card> cards)
        // {
        //     cards.ForEach(card => 
        //     {
        //         card.Draw(); 
        //         Console.WriteLine();
        //     });
        // }


        IEnumerable<Card> CreateCard(List<string> cardData)
        {
            int cardIndex = 0;
            Card card = new Card(cardIndex);
            int rowIndex = 0;
            int columnIndex = 0;
            foreach (var row in cardData)
            {
                //Console.WriteLine($"Row found in card data: {row}");
                if (string.IsNullOrWhiteSpace(row))
                {
                    yield return card;
                    card = new Card(++cardIndex);
                    rowIndex = 0;
                    columnIndex = 0;
                    continue;
                }

                //Console.WriteLine($"Row to parse: {row}");
                var splittedData = System.Text.RegularExpressions.Regex.Split(row, @"\s+").Where(x => !string.IsNullOrWhiteSpace(x));
                //Console.WriteLine(string.Join(",", splittedData));

                foreach (var number in splittedData)
                {
                    card.Numbers[rowIndex % 5, columnIndex % 5] = int.Parse(number);
                    columnIndex++;
                }

                rowIndex++;

                if (row.Equals(cardData.Last()))
                    yield return card;
            }
        }

        int DrawNextNumber() => numbersDrawn[amountOfNumbersDrawn++];
    }

    [Part2]
    public void Part2(string filename)
    {
        var input = File.ReadAllLines(filename);
        numbersDrawn = input.First().Split(",").Select(x => int.Parse(x)).ToArray();
        var amountOfNumbersDrawn = 0;

        var cardData = input.Skip(2).ToList();

        var cards = CreateCard(cardData).ToList();

        var counter = 0;
        bool loserFound = false;
        do
        {
            foreach (var card in cards)
            {
                card.Draw();
                card.DrawMarked();
                Console.WriteLine();
            }

            cards = cards.Where(x => !x.IsComplete()).ToList();

            var numberDrawn = DrawNextNumber();
            Console.WriteLine($"Number drawn: {numberDrawn}!");

            foreach (var card in cards)
            {
                (bool complete, int index) = card.CheckCard(numberDrawn);
                if (complete)
                {
                    if (cards.Count() == 1)
                    {
                        Console.WriteLine("Losing card found");
                        var unmarkedScore = card.GetUnmarkedSum();
                        Console.WriteLine($"Card with index: {card.Index + 1} loses!");
                        Console.WriteLine($"Card unmarked score: {unmarkedScore}");
                        Console.WriteLine($"Total score: {unmarkedScore * numberDrawn}");
                        loserFound = true;
                    }
                }
            };
        }
        while (!loserFound);
    }

    IEnumerable<Card> CreateCard(List<string> cardData)
    {
        int cardIndex = 0;
        Card card = new Card(cardIndex);
        int rowIndex = 0;
        int columnIndex = 0;
        foreach (var row in cardData)
        {
            //Console.WriteLine($"Row found in card data: {row}");
            if (string.IsNullOrWhiteSpace(row))
            {
                yield return card;
                card = new Card(++cardIndex);
                rowIndex = 0;
                columnIndex = 0;
                continue;
            }

            //Console.WriteLine($"Row to parse: {row}");
            var splittedData = System.Text.RegularExpressions.Regex.Split(row, @"\s+").Where(x => !string.IsNullOrWhiteSpace(x));
            //Console.WriteLine(string.Join(",", splittedData));

            foreach (var number in splittedData)
            {
                card.Numbers[rowIndex % 5, columnIndex % 5] = int.Parse(number);
                columnIndex++;
            }

            rowIndex++;

            if (row.Equals(cardData.Last()))
                yield return card;
        }
    }

    int DrawNextNumber() => numbersDrawn[amountOfNumbersDrawn++];
}

public class Card
{
    public int Index { get; }
    public Card(int index) => Index = index;
    public int[,] Numbers = new int[5, 5];
    public int[,] MarkedNumbers = new int[5, 5]
    {
         { -1, -1, -1, -1, -1 },
         { -1, -1, -1, -1, -1 },
         { -1, -1, -1, -1, -1 },
         { -1, -1, -1, -1, -1 },
         { -1, -1, -1, -1, -1 }
    };

    public void Draw()
    {
        StringBuilder str = new StringBuilder();
        for (int y = 0; y < 5; ++y)
        {
            for (int x = 0; x < 5; ++x)
            {
                if (x != 0) str.Append("\t");
                str.Append(Numbers[y, x]);
            }
            str.Append(Environment.NewLine);
        }

        Console.WriteLine(str);
    }

    public void DrawMarked()
    {
        StringBuilder str = new StringBuilder();
        for (int y = 0; y < 5; ++y)
        {
            for (int x = 0; x < 5; ++x)
            {
                if (x != 0) str.Append("\t");
                str.Append(MarkedNumbers[y, x]);
            }
            str.Append(Environment.NewLine);
        }

        Console.WriteLine(str);
    }

    public (bool winner, int cardIndex) CheckCard(int numberDrawn)
    {
        for (int y = 0; y < 5; ++y)
        {
            for (int x = 0; x < 5; ++x)
            {
                if (Numbers[y, x] == numberDrawn)
                {
                    MarkedNumbers[y, x] = numberDrawn;
                }
            }
        }

        for (int i = 0; i < 5; ++i)
        {
            if (IsColumnComplete(i) || IsRowComplete(i))
            {
                return (true, 0);
            }
        }

        return (false, -1);
    }

    public int GetUnmarkedSum()
    {
        int result = 0;
        for (int y = 0; y < 5; ++y)
        {
            for (int x = 0; x < 5; ++x)
            {
                if (MarkedNumbers[y, x] == -1)
                    result += Numbers[y, x];
            }
        }

        return result;
    }

    public bool IsComplete()
    {
        for (int i = 0; i < 5; ++i)
        {
            if (IsColumnComplete(i) || IsRowComplete(i))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsColumnComplete(int columnIndex)
    {
        for (int y = 0; y < 5; ++y)
        {
            if (MarkedNumbers[y, columnIndex] == -1)
                return false;
        }

        Console.WriteLine("Column complete!");
        return true;
    }

    private bool IsRowComplete(int rowIndex)
    {
        for (int x = 0; x < 5; ++x)
        {
            if (MarkedNumbers[rowIndex, x] == -1)
                return false;
        }

        Console.WriteLine("Row complete!");
        return true;
    }
}