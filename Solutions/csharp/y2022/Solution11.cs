using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using AdventOfCode;

namespace Solutions.y2022d11;

[Solution(2022, 11)]
public class Solution11
{
    [Part1]
    public void Part1(string filename)
    {
        Monkey[] monkeys = GetMonkeys(filename);

        for (int i = 0; i < 20; ++i)
        {
            Round(monkeys);
        }

        var ic = monkeys.OrderByDescending(monkey => monkey.InspectedCount).Select(monkey => monkey.InspectedCount).Take(2).ToArray();
        Console.WriteLine($"Level of monkey business: {ic[0] * ic[1]}");
    }

    [Part2]
    public void Part2(string filename)
    {
        var monkeys = GetMonkeys(filename);

        for (int i = 1; i <= 10000; ++i)
        {
            Round(monkeys, false);
            if(i == 1 || i == 20 || i == 1000 || i == 2000)
            {
                Console.WriteLine($"== AFTER ROUND {i} ==");
                foreach(var m in monkeys)
                {
                    Console.WriteLine($"Monkey {m.id} inspected {m.InspectedCount} items");
                }
                Console.WriteLine();
            }
        }

        var ic = monkeys.OrderByDescending(monkey => monkey.InspectedCount).Select(monkey => monkey.InspectedCount).Take(2).ToArray();
        Console.WriteLine($"Level of monkey business: {ic[0] * ic[1]}");
    }

    public Monkey[] GetMonkeys(string filename)
    {
        var result = new List<Monkey>();
        Monkey monkey = null!;

        foreach (var line in File.ReadLines(filename).Select(line => line.Trim().ToLowerInvariant()))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            if (line.StartsWith("monkey"))
            {
                var id = int.Parse(line.Replace(":", "").Split(" ")[1]);
                monkey = new Monkey(id);
                result.Add(monkey);
                continue;
            }
            else if (line.StartsWith("starting items"))
            {
                monkey.Items = line.Replace("starting items: ", "")
                    .Split(",")
                    .Select(x => x.Trim())
                    .ToList();

                continue;
            }
            else if (line.StartsWith("operation:"))
            {
                var tempLine = line.Replace("operation: new = old ", "");
                //     Operation: new = old * 19
                monkey.OperationType = tempLine[0];

                var value = tempLine.Replace(monkey.OperationType.ToString(), "").Trim();

                if (value != "old")
                {
                    monkey.OperationValue = value;
                }

                continue;
            }
            else if (line.StartsWith("test:"))
            {
                monkey.TestValue = int.Parse(line.Replace("test: divisible by ", ""));
            }
            else if (line.StartsWith("if true:"))
            {
                //If true: throw to monkey 2
                monkey.TrueValue = int.Parse(line.Replace("if true: throw to monkey", ""));

            }
            else if (line.StartsWith("if false:"))
            {
                //If false: throw to monkey 3
                monkey.FalseValue = int.Parse(line.Replace("if false: throw to monkey", ""));
            }
            else
            {
                throw new NotImplementedException(line);
            }
        }

        return result.ToArray();
    }

    public void Round(Monkey[] monkeys, bool relief = true)
    {
        // Inspects:
        //  Worry -> Operation
        //  Divide by 3
        //  Test

        //Monkey inspects an item with a worry level of 79.
        //Worry level is multiplied by 19 to 1501.
        //Monkey gets bored with item.Worry level is divided by 3 to 500.
        //Current worry level is not divisible by 23.
        //Item with worry level 500 is thrown to monkey 3.

        var mlc = monkeys.Select(x => x.TestValue).Aggregate((x, y) => x * y);
        foreach (var monkey in monkeys)
        {
            var items = new string[monkey.Items.Count];
            monkey.Items.CopyTo(items);

            for(int i = 0; i < monkey.Items.Count; ++i)
            {
                var item = items[i];
                //Console.WriteLine($"Monkey inspects an item with a worry level of {item}");
                monkey.InspectedCount++;
                var newValue = Calculate(monkey, item);
                //Console.WriteLine($"Worry level is {GetOperationDesc(monkey)} by {monkey.OperationValue ?? item} to {newValue}");
                if(relief)
                {
                    newValue = DivideString(newValue, 3);
                    //Console.WriteLine($"Monkey gets bored with item. Worry level is divided by 3 to {newValue}");
                }

                var modValue = ModString(newValue, monkey.TestValue);
                var test = modValue == 0;
                newValue = ModString(newValue, mlc).ToString();
                //Console.WriteLine($"Current worry level is {(test ? "divisible" : "not divisible")} by {monkey.TestValue}");
                if (test)
                {
                    //Console.WriteLine($"Item with worry level {newValue} is thrown to monkey {monkey.TrueValue}");
                    monkeys.Single(m => m.id == monkey.TrueValue).Items.Add(newValue);
                }
                else
                {
                    //Console.WriteLine($"Item with worry level {newValue} is thrown to monkey {monkey.FalseValue}");
                    monkeys.Single(m => m.id == monkey.FalseValue).Items.Add(newValue);
                }
            }

            monkey.Items = new List<string>();
        }
    }

    private string GetOperationDesc(Monkey monkey)
    {
        return monkey.OperationType switch
        {
            '*' => "multiplied",
            '+' => "increased",
            _ => throw new NotImplementedException(monkey.OperationType.ToString())
        };
    }

    private string Calculate(Monkey monkey, string item)
    {
        var old = item;
        var value = monkey.OperationValue ?? old;

        return monkey.OperationType switch
        {
            '*' => MultiplyString(old, value),
            '+' => AddString(old, value),
            _ => throw new NotImplementedException(monkey.OperationType.ToString())
        };
    }

    private string MultiplyString(string a, string b)
    {
        int len1 = a.Length;
        int len2 = b.Length;
        if (len1 == 0 || len2 == 0)
            return "0";

        // will keep the result number in vector
        // in reverse order
        int[] result = new int[len1 + len2];

        // Below two indexes are used to
        // find positions in result.
        int i_n1 = 0;
        int i_n2 = 0;
        int i;

        // Go from right to left in num1
        for (i = len1 - 1; i >= 0; i--)
        {
            int carry = 0;
            int n1 = a[i] - '0';

            // To shift position to left after every
            // multipliccharAtion of a digit in num2
            i_n2 = 0;

            // Go from right to left in num2            
            for (int j = len2 - 1; j >= 0; j--)
            {
                // Take current digit of second number
                int n2 = b[j] - '0';

                // Multiply with current digit of first number
                // and add result to previously stored result
                // charAt current position.
                int sum = n1 * n2 + result[i_n1 + i_n2] + carry;

                // Carry for next itercharAtion
                carry = sum / 10;

                // Store result
                result[i_n1 + i_n2] = sum % 10;

                i_n2++;
            }

            // store carry in next cell
            if (carry > 0)
                result[i_n1 + i_n2] += carry;

            // To shift position to left after every
            // multipliccharAtion of a digit in num1.
            i_n1++;
        }

        // ignore '0's from the right
        i = result.Length - 1;
        while (i >= 0 && result[i] == 0)
            i--;

        // If all were '0's - means either both
        // or one of num1 or num2 were '0'
        if (i == -1)
            return "0";

        // genercharAte the result String
        String s = "";

        while (i >= 0)
            s += (result[i--]);

        return s;

        ////       [7][9]
        ////       [1][9]
        //// ------------
        ////       [8][1]    (9*9)
        ////    [6][3][0]    (9*70)
        ////       [9][0]    (10*9)
        ////    [7][0][0]    (10*70)
        //// ------------
        //// [1][5][0][1]
        //string result = "";
        
        //var maxIndex = Math.Max(a.Length, b.Length);
        ////a = a.PadLeft(maxIndex, '0');
        ////b = b.PadLeft(maxIndex, '0');

        //var remainder = 0;
        //int iCounter = 0;

        //for (int i = a.Length - 1; i >= 0; --i)
        //{
        //    int jCounter = 0;

        //    for (int j = b.Length - 1; j >= 0; --j)
        //    {
        //        var temp = remainder + int.Parse(a[i].ToString()) * int.Parse(b[j].ToString());
        //        var foo = temp.ToString().PadRight(iCounter + jCounter + temp.ToString().Length, '0');
        //        result = AddString(result, foo);
        //        jCounter++;
        //    }

        //    iCounter++;
        //}

        //if(remainder != 0)
        //{
        //    result = new string(result.Insert(0, (remainder % 10).ToString()).ToArray());
        //}

        //int startIndex = 0;
        //foreach (var c in result)
        //{
        //    if (c == '-') break;
        //    if (int.Parse(c.ToString()) != 0)
        //        break;

        //    startIndex++;
        //}

        //result = result[startIndex..];

        //if (result == "")
        //    return "0";

        //return result;
    }

    public string SubtractString(string a, string b)
    {
        //  [5][0][0]
        // -[0][2][3]
        
        //  [5][0][0]
        // -[0][0][0]

        //  [5][0][0]
        // -[0][2][0]
        //
        //  [4][0][0]
        //    [10][0]
        // -[0][2][0]

        //  [4][8][0]
        // -[0][0][3]
        //
        //  [4][7][0]
        // [0][0][10]
        // -[0][0][3]
        
        //  [4][7][7]


        // [5][0][0]
        // [6][0][0]


        // [1][7]
        // [2][3]
        //
        // [1][4]
        // [2][0]
        // [-1][4]
        // 
        
        
        // [-1][4]
        string result = "";
        var maxIndex = Math.Max(a.Length, b.Length);

        a = a.PadLeft(maxIndex, '0');
        b = b.PadLeft(maxIndex, '0');
        result = a;

        int? remainder = null;
        for(int i = maxIndex - 1; i >= 0; --i)
        {
            var left = remainder == null
                ? int.Parse(a[i].ToString())
                : remainder == -1
                    ? int.Parse(a[i].ToString()) - 1
                    : remainder;

            var temp = left - int.Parse(b[i].ToString());

            if(temp < 0)
            {
                
                var part1 = result[0..i];
                var f = (10 + temp).ToString();
                var part2 = i == maxIndex - 1 ? "" : result[(i + 1)..];

                result = string.Concat(result[0..i] + f, part2);
                
                if (i != 0)
                {
                    remainder = int.Parse(result[i - 1].ToString()) - 1;
                }
                else
                {
                    var value = (10 - int.Parse(result[i + 1].ToString())).ToString();
                    result = string.Concat("-", value);
                }
            }
            else
            {
                var part1 = result[0..i];
                var f = temp.ToString();
                var part2 = i == maxIndex - 1 ? "" : result[(i + 1)..];

                result = string.Concat(result[0..i] + f, part2);
                remainder = null;
            }
        }

        if (remainder != null)
        {
            var f1 = remainder.ToString();
            var p2 = result[1..];

            result = string.Concat(remainder.ToString(), result[1..]);
        }

        if (result == "") return "0";

        int startIndex = 0;
        foreach(var c in result)
        {
            if (c == '-') break;
            if (int.Parse(c.ToString()) != 0)
                break;
            
            startIndex++;
        }

        result = result[startIndex..];

        if (result == "") 
            return "0";
        return result;
    }

    public string AddString(string a, string b)
    {
        // Before proceeding further, make sure length
        // of str2 is larger.
        if (a.Length > b.Length)
        {
            (a, b) = (b, a);
        }

        // Take an empty string for storing result
        string str = "";

        // Calculate length of both string
        int n1 = a.Length, n2 = b.Length;

        // Reverse both of strings
        char[] ch = a.ToCharArray();
        Array.Reverse(ch);
        a = new string(ch);
        char[] ch1 = b.ToCharArray();
        Array.Reverse(ch1);
        b = new string(ch1);

        int carry = 0;
        for (int i = 0; i < n1; i++)
        {
            // Do school mathematics, compute sum of
            // current digits and carry
            int sum = ((int)(a[i] - '0') +
                    (int)(b[i] - '0') + carry);
            str += (char)(sum % 10 + '0');

            // Calculate carry for next step
            carry = sum / 10;
        }

        // Add remaining digits of larger number
        for (int i = n1; i < n2; i++)
        {
            int sum = ((int)(b[i] - '0') + carry);
            str += (char)(sum % 10 + '0');
            carry = sum / 10;
        }

        // Add remaining carry
        if (carry > 0)
            str += (char)(carry + '0');

        // reverse resultant string
        char[] ch2 = str.ToCharArray();
        Array.Reverse(ch2);
        str = new string(ch2);

        return str;

        //string result = "";

        //var maxIndex = Math.Max(a.Length, b.Length);
        //a = a.PadLeft(maxIndex, '0');
        //b = b.PadLeft(maxIndex, '0');

        //// [0][-6]
        //// [1][7]

        //// [1][7]
        //// [0][-6]
        //if (a.StartsWith("-"))
        //{
        //    return SubtractString(b, a[1..]);
        //}
        //else if(b.StartsWith("-"))
        //{
        //    return SubtractString(a, b[1..]);
        //}

        //var remainder = 0;
        //for(int i = maxIndex - 1; i >= 0; --i)
        //{
            

        //    var temp = remainder + int.Parse(a[i].ToString()) + int.Parse(b[i].ToString());
        //    if (temp > 9)
        //    {
        //        remainder = temp / 10;
        //    }
        //    else
        //    {
        //        remainder = 0;
        //    }

        //    result = new string(result.Insert(0, (temp % 10).ToString()).ToArray());
        //}

        //if(remainder != 0)
        //{
        //    result = new string(result.Insert(0, remainder.ToString()).ToArray());
        //}

        //return result;
    }

    public string DivideString(string a, int divisor)
    {
        string result = "";
        
        int index = 0;
        int temp = (int)(a[index] - '0');
        while(temp < divisor)
        {
            if (index + 1 >= a.Length)
                return "0";

            temp = temp * 10 + (int)(a[index + 1] - '0');
            index++;
        }

        ++index;

        while(a.Length > index)
        {
            result += (char)(temp / divisor + '0');
            temp = (temp % divisor) * 10 + (int)(a[index] - '0');
            index++;
        }

        result += (char)(temp / divisor + '0');
        
        if (result.Length == 0)
            return "0";

        return result;
    }

    public int ModString(string a, int divisor)
    {
        var temp = DivideString(a, divisor);
        var foo = MultiplyString(temp, divisor.ToString());
        var result = SubtractString(a, foo);
        return int.Parse(result);
    }

    public class Monkey
    {
        public int id;
        public List<string> Items { get; set; } = new List<string>();
        public Expression<Func<int, int>> Operation { get; set; } = null!;
        public int TestValue { get; set; }
        public int TrueValue { get; set; }
        public int FalseValue { get; set; }
        public string? OperationValue { get; set; }
        public char OperationType { get; internal set; }
        public long InspectedCount { get; internal set; }

        public Monkey(int id)
        {
            this.id = id;
        }
    }
}
