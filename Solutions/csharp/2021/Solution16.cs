using System.Text;

namespace AdventOfCode.Y2021;

[Solution(2021, 16)]
public class Solution16
{
    [Part1]
    public void Part1(string filename)
    {
        var input = File.ReadLines(filename).First();

        var binaryArray = input.Select(c => HexToBinary(c.ToString()));
        var packet = string.Join("", binaryArray);
        Console.WriteLine($"Packet: {packet}");

        //int counter = ReadPacket(packet);

        // Console.WriteLine($"Total version: {counter}");
        Console.WriteLine("No valid answer yet for Part1");
    }

    [Part2]
    public void Part2(string filename)
    {
        var input = File.ReadLines("input.txt").First();

        var binaryArray = input.Select(c => HexToBinary(c.ToString()));
        var packet = string.Join("", binaryArray);
        Console.WriteLine($"Packet: {packet}");

        //var result = ReadPacket2(packet);

        // Console.WriteLine($"Result: {result}");
        Console.WriteLine("No valid answer yet for Part2");
    }

    string ReadPacket(string packet)
    {
        long counter = 0;
        var versionBin = packet[0..3];
        var version = BinaryToDecimal(new string(versionBin));
        counter += version;
        Console.WriteLine($"Version: {versionBin} - {version}");

        var typeId = packet[3..6];
        var type = BinaryToDecimal(typeId);
        Console.WriteLine($"TypeId: {typeId}: type: {type}");

        var subPacket = packet[6..];
        return type switch
        {
            4 => LiteralValue(subPacket),
            _ => ReadOperatorPacket(subPacket)
        };
    }

    // long ReadPacket2(string packet)
    // {
    //     var versionBin = packet[0..3];
    //     var version = BinaryToDecimal(new string(versionBin));
    //     Console.WriteLine($"Version: {versionBin} - {version}");

    //     var typeId = packet[3..6];
    //     var type = BinaryToDecimal(typeId);
    //     Console.WriteLine($"TypeId: {typeId}: type: {type}");

    //     packet = packet[6..];

    //     return type switch
    //     {
    //         0 => ReadOperatorPacket(Operator.SUM),
    //         1 => ReadOperatorPacket(Operator.MUL),
    //         2 => ReadOperatorPacket(Operator.MIN),
    //         3 => ReadOperatorPacket(Operator.MAX),
    //         4 => LiteralValue(),
    //         5 => ReadOperatorPacket(Operator.GT),
    //         6 => ReadOperatorPacket(Operator.LT),
    //         7 => ReadOperatorPacket(Operator.EQ),
    //         _ => throw new NotImplementedException()
    //     };
    // }

    string ReadOperatorPacket(string packet)
    {
        Console.WriteLine($"Operator packet found: {packet}");

        var lengthTypeId = packet[0];
        Console.WriteLine($"LengthTypeID: {lengthTypeId}");

        return lengthTypeId switch
        {
            '0' => LengthPacket(packet[1..]),
            '1' => AmountPacket(packet[1..]),
            _ => throw new NotImplementedException()
        };
    }

    string AmountPacket(string packet)
    {
        Console.WriteLine($"Amount packet found: {packet}");

        var subPacketAmount = BinaryToDecimal(new string(packet[..11]));
        Console.WriteLine($"SubPacketAmount: {subPacketAmount}");
        var subPacket = packet[11..];

        for(int i = 0; i < subPacketAmount; ++i)
        {
            subPacket = ReadPacket(subPacket);
        }

        return subPacket;
    }

    string LengthPacket(string packet)
    {
        Console.WriteLine($"Length packet found {packet}");
        var subPacketLength = BinaryToDecimal(new string(packet[..15]));
        Console.WriteLine($"SubPacketLength: {subPacketLength}");

        var subPacket = packet[15..];
        Console.WriteLine($"SubPacket: {subPacket}");

        int readBytes = 0;
        int packetLength = subPacket.Length;

        do
        {
            subPacket = ReadPacket(subPacket);
            readBytes = packetLength - subPacket.Length;
            Console.WriteLine($"Read bytes: {readBytes}");
        } while(readBytes < subPacketLength);

        return subPacket;
    }

    string LiteralValue(string packet)
    {
        Console.WriteLine($"Literal packet found: {packet}");
        StringBuilder valueBuilder = new StringBuilder();

        bool lastPacket = false;

        do
        {    
            lastPacket = packet[0] == '0';

            valueBuilder.Append(packet[1..5]);
            packet = packet[5..];
            
            Console.WriteLine($"{valueBuilder}");
            Console.WriteLine($"Remaining packet: {packet}");
        } while(!lastPacket);

        var value = BinaryToDecimal(valueBuilder.ToString());

        Console.WriteLine($"Value: {value}");

        return packet;
    }

    long HexToDecimal(string hexValue) => Convert.ToInt64(hexValue, 16);
    string DecimalToHex(long decValue) => decValue.ToString("X");
    long BinaryToDecimal(string binaryValue) => Convert.ToInt64(binaryValue, 2);
    string DecimalToBinary(long decValue) => Convert.ToString(decValue, 2).PadLeft(4, '0');

    string HexToBinary(string hexValue) => DecimalToBinary(HexToDecimal(hexValue));
    string BinaryToHex(string binaryValue) => DecimalToHex(BinaryToDecimal(binaryValue));

}
public enum Operator
{
    SUM,
    MUL,
    MIN,
    MAX,
    GT,
    LT,
    EQ
}