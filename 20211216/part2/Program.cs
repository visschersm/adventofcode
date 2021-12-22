using System.Text;

var input = File.ReadLines("input.txt").First();

var binaryArray = input.Select(c => HexToBinary(c.ToString()));
var packet = string.Join("", binaryArray);
Console.WriteLine($"Packet: {packet}");

var result = ReadPacket();

Console.WriteLine($"Result: {result}");

long ReadPacket()
{
    var versionBin = packet[0..3];
    var version = BinaryToDecimal(new string(versionBin));
    Console.WriteLine($"Version: {versionBin} - {version}");

    var typeId = packet[3..6];
    var type = BinaryToDecimal(typeId);
    Console.WriteLine($"TypeId: {typeId}: type: {type}");

    packet = packet[6..];

    return type switch
    {
        0 => ReadOperatorPacket(Operator.SUM),
        1 => ReadOperatorPacket(Operator.MUL),
        2 => ReadOperatorPacket(Operator.MIN),
        3 => ReadOperatorPacket(Operator.MAX),
        4 => LiteralValue(),
        5 => ReadOperatorPacket(Operator.GT),
        6 => ReadOperatorPacket(Operator.LT),
        7 => ReadOperatorPacket(Operator.EQ),
        _ => throw new NotImplementedException()
    };
}

long ReadOperatorPacket(Operator op)
{
    Console.WriteLine($"Operator packet found: {packet}");

    var lengthTypeId = packet[0];
    Console.WriteLine($"LengthTypeID: {lengthTypeId}");

    packet = packet[1..];
    return lengthTypeId switch
    {
        '0' => LengthPacket(op),
        '1' => AmountPacket(op),
        _ => throw new NotImplementedException()
    };
}

long AmountPacket(Operator op)
{
    Console.WriteLine($"Amount packet found: {packet}");

    var subPacketAmount = BinaryToDecimal(new string(packet[..11]));
    Console.WriteLine($"SubPacketAmount: {subPacketAmount}");
    packet = packet[11..];

    long? result = null;
    for (int i = 0; i < subPacketAmount; ++i)
    {
        var value = ReadPacket();
        result = Calculate(result, value, op);
    }

    return result ?? throw new Exception("No result found");
}

long LengthPacket(Operator op)
{
    Console.WriteLine($"Length packet found {packet}");
    var subPacketLength = BinaryToDecimal(new string(packet[..15]));
    Console.WriteLine($"SubPacketLength: {subPacketLength}");

    packet = packet[15..];
    Console.WriteLine($"SubPacket: {packet}");

    int readBytes = 0;
    int packetLength = packet.Length;
    
    long? result = null;

    do
    {
        var value = ReadPacket();
        result = Calculate(result, value, op);
        
        readBytes = packetLength - packet.Length;
        Console.WriteLine($"Read bytes: {readBytes}");
    } while (readBytes < subPacketLength);

    return result ?? throw new Exception("No result found");
}

long LiteralValue()
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
    } while (!lastPacket);

    var value = BinaryToDecimal(valueBuilder.ToString());

    Console.WriteLine($"Value: {value}");
    return value;
}

long Calculate(long? result, long value, Operator op)
{
    if(result == null)
        return value;

    return op switch
    {
        Operator.SUM => result!.Value + value,
        Operator.MUL => result!.Value * value,
        Operator.MIN => Math.Min(result!.Value, value),
        Operator.MAX => Math.Max(result!.Value, value),
        Operator.GT => result!.Value > value ? 1 : 0,
        Operator.LT => result!.Value < value ? 1 : 0,
        Operator.EQ => result!.Value == value ? 1 : 0,
        _ => throw new NotImplementedException()
    };
}

long HexToDecimal(string hexValue) => Convert.ToInt64(hexValue, 16);
string DecimalToHex(long decValue) => decValue.ToString("X");
long BinaryToDecimal(string binaryValue) => Convert.ToInt64(binaryValue, 2);
string DecimalToBinary(long decValue) => Convert.ToString(decValue, 2).PadLeft(4, '0');

string HexToBinary(string hexValue) => DecimalToBinary(HexToDecimal(hexValue));
string BinaryToHex(string binaryValue) => DecimalToHex(BinaryToDecimal(binaryValue));

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