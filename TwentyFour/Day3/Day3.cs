using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.TwentyFour
{
    public class Day3 : IDay
    {
        public int Year => 2024;
        public int Day => 3;
        private IEnumerable<string> lines;

        public void Initialize()
        {
            var path = Globals.GetPath(this);
            lines = File.ReadLines(Path.Combine(path, "input.txt"));
        }

        public string SolvePart1()
        {

            var regex = @"mul\(\d*,\d*\)";
            int sum = 0;
            foreach (var line in lines)
            {
                var matches = Regex.Matches(line, regex);
                var nums = matches.Select(m => m.Value.Split(["mul(", ",", ")"], StringSplitOptions.RemoveEmptyEntries));
                foreach (var num in nums)
                {
                    sum += int.Parse(num[0]) * int.Parse(num[1]);
                }
            }

            return sum.ToString();
        }

        public string SolvePart2()
        {
            throw new NotImplementedException();
        }
    }
}
