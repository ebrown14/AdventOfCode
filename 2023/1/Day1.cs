using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyThree;

public class Day1 : IDay
{
    public int Year => 2023;
    public int Day { get; } = 1;

    IEnumerable<string> lines;
    public void Initialize()
    {
        lines = File.ReadLines($@"{Year}/{Day}/Inputs.txt");
    }

    public string SolvePart1()
    {
        int sum = 0;

        foreach (var line in lines)
        {
            var num = int.Parse(GetCalibrationValue(line));
            sum += num;
        }

        return sum.ToString();

        string GetCalibrationValue(string line)
        {
            var nums = line.Select(c => c).Where(char.IsDigit);
            return $"{nums.First()}{nums.Last()}";
        }
    }

    public string SolvePart2()
    {
        int sum = 0;

        foreach (var line in lines)
        {
            var num = int.Parse(GetCalibrationValue(line));
            sum += num;
        }

        return sum.ToString();

        string GetCalibrationValue(string line)
        {
            var numbers = new string[]
            {
        "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
            };

            List<char> foundNumbers = new();
            StringBuilder sb = new();

            for (var i = 0; i < line.Length; i++)
            {
                if (Char.IsDigit(line[i]))
                {
                    foundNumbers.Add(line[i]);
                    continue;
                }

                sb.Append(line[i]);
                for (var j = i + 1; j < line.Length; j++)
                {
                    sb.Append(line[j]);
                    if (numbers.Contains(sb.ToString()))
                    {
                        foundNumbers.Add(ConvertWordToNumber(sb.ToString()));
                        sb.Clear();
                        break;
                    }
                }
                sb.Clear();
            }

            return $"{foundNumbers.First()}{foundNumbers.Last()}";
        }

        char ConvertWordToNumber(string number) => number switch
        {
            "zero" => '0',
            "one" => '1',
            "two" => '2',
            "three" => '3',
            "four" => '4',
            "five" => '5',
            "six" => '6',
            "seven" => '7',
            "eight" => '8',
            "nine" => '9',
            _ => throw new Exception("Invalid number")
        };
    }
}
