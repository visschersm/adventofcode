using System.Text;

var input = File.ReadLines("input.txt").First();

var binaryArray = input.Select(c => HexToBinary(c.ToString()));
var packet = string.Join("", binaryArray);
Console.WriteLine($"Packet: {packet}");

long counter = 0;

ReadPacket(packet);

Console.WriteLine($"Total version: {counter}");

string ReadPacket(string packet)
{
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