using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.TwentyFour
{
    public class Day3 : IDay
    {
        public int Year => 2024;
        public int Day => 3;
        private string line;

        public void Initialize()
        {
            var path = Globals.GetPath(this);
            line = string.Join("", File.ReadLines(Path.Combine(path)));
        }

        public string SolvePart1()
        {
            var regex = @"mul\(\d*,\d*\)";
            int sum = 0;

            var matches = Regex.Matches(line, regex);
            var nums = matches.Select(m => m.Value.Split(["mul(", ",", ")"], StringSplitOptions.RemoveEmptyEntries));
            foreach (var num in nums)
                sum += int.Parse(num[0]) * int.Parse(num[1]);
            
            return sum.ToString();
        }

        public string SolvePart2()
        {
            var regex = @"(mul\(\d*,\d*\)|don't\(\)|do\(\))";
            int sum = 0;

            List<string> muls = new();
            var matches = Regex.Matches(line, regex);
            bool push = true;

            foreach (Match match in matches)
            {
                var value = match.Value;
                push = value switch
                {
                    "do()" => true,
                    "don't()" => false,
                    _ => push
                };

                if (push && Regex.Match(value, @"mul\(\d*,\d*\)").Success)
                {
                    muls.Add(value);
                }
            }

            var nums = muls.Select(m =>
                m.Split(["mul(", ",", ")"], StringSplitOptions.RemoveEmptyEntries));
            foreach (var num in nums)
            {
                sum += int.Parse(num[0]) * int.Parse(num[1]);
            }

            return sum.ToString();
        }
    }
}
