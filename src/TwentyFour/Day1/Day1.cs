using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyFour;

internal class Day1 : IDay
{
    public  int Year => 2024;
    public  int Day => 1;

    int[] arr1;
    int[] arr2;
    public  void Initialize()
    {
        var path = Globals.GetPath(this);
        var lines = File.ReadLines(Path.Combine(path))
            .Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        arr1 = lines
            .Select(l => int.Parse(l[0]))
            .ToArray();
        arr2 = lines
            .Select(l => int.Parse(l[1]))
            .ToArray();
    }
    public  string SolvePart1()
    {
        var a = arr1.Order();
        var b = arr2.Order();


        List<int> distances = new();
        for (int i = 0; i < a.Count(); i++)
        {
            distances.Add(Math.Abs(a.ElementAt(i) - b.ElementAt(i)));
        }

        return distances.Sum().ToString();
    }


    public  string SolvePart2()
    {

        List<int> counts = new();
        foreach (var num in arr1)
        {
            int count = 0;
            foreach (var num2 in arr2)
            {
                if (num == num2)
                {
                    count++;
                }
            }
            counts.Add(count * num);
        }

        return counts.Sum().ToString();
    }
}
