using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.TwentyFour
{
    public class Day5 : IDay
    {
        public int Year => 2024;
        public int Day => 5;

        IEnumerable<Page> pages;
        public void Initialize()
        {
            pages = File.ReadAllLines(Globals.GetPath(this, ".Pt1")).Select(l => {
                var parts = l.Split('|', StringSplitOptions.RemoveEmptyEntries);
                return new Page { Left = int.Parse(parts[0]), Right = int.Parse(parts[1]) };
            });

            var lines2 = File.ReadAllLines(Globals.GetPath(this, ".Pt2"));
        }

        public string SolvePart1()
        {
            //throw new NotImplementedException();
            return "";
        }

        public string SolvePart2()
        {
            //throw new NotImplementedException();
            return "";
        }


        class Page
        {
            public int Left { get; init; }
            public int Right { get; init; }
        }
    }

}
