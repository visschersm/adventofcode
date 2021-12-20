using System.Text;

var input = File.ReadLines("test-input4.txt").First();

var binaryArray = input.Select(c => HexToBinary(c.ToString()));
var packet = string.Join("", binaryArray);
Console.WriteLine($"Packet: {packet}");

long counter = 0;

ReadPacket();

Console.WriteLine($"Total version: {counter}");

void ReadPacket()
{
    var versionBin = packet[0..3];
    var version = BinaryToDecimal(new string(versionBin));
    counter += version;
    Console.WriteLine($"Version: {versionBin} - {version}");

    var typeId = packet[3..6];
    var type = BinaryToDecimal(typeId);
    Console.WriteLine($"TypeId: {typeId}: type: {type}");

    packet = packet[6..];
    type switch
    {
        4 => LiteralValue(),
        _ => ReadOperatorPacket()
    };
}

void ReadOperatorPacket()
{
    Console.WriteLine($"Operator packet found: {packet}");

    var lengthTypeId = packet[0];
    Console.WriteLine($"LengthTypeID: {lengthTypeId}");

    lengthTypeId switch
    {
        '0' => LengthPacket(packet[1..]),
        '1' => AmountPacket(packet[1..]),
        _ => throw new NotImplementedException()
    };
}

void AmountPacket()
{
    Console.WriteLine($"Amount packet found: {packet}");

    var subPacketAmount = BinaryToDecimal(new string(packet[..11]));
    Console.WriteLine($"SubPacketAmount: {subPacketAmount}");
    packet = packet[11..];

    for (int i = 0; i < subPacketAmount; ++i)
    {
        ReadPacket();
    }
}

void LengthPacket()
{
    Console.WriteLine($"Length packet found {packet}");
    var subPacketLength = BinaryToDecimal(new string(packet[..15]));
    Console.WriteLine($"SubPacketLength: {subPacketLength}");

    packet = packet[15..];
    Console.WriteLine($"SubPacket: {subPacket}");

    int readBytes = 0;
    int packetLength = subPacket.Length;

    do
    {
        packet = ReadPacket(packet);
        readBytes = packetLength - packet.Length;
        Console.WriteLine($"Read bytes: {readBytes}");
    } while (readBytes < subPacketLength);
}

void LiteralValue()
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
}

long HexToDecimal(string hexValue) => Convert.ToInt64(hexValue, 16);
string DecimalToHex(long decValue) => decValue.ToString("X");
long BinaryToDecimal(string binaryValue) => Convert.ToInt64(binaryValue, 2);
string DecimalToBinary(long decValue) => Convert.ToString(decValue, 2).PadLeft(4, '0');

string HexToBinary(string hexValue) => DecimalToBinary(HexToDecimal(hexValue));
string BinaryToHex(string binaryValue) => DecimalToHex(BinaryToDecimal(binaryValue));