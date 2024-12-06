using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.TwentyFour;

public class Day4 : IDay
{
    public int Year => 2024;
    public int Day => 4;

    private List<List<char>> matrix = new();

    public void Initialize()
    {
        var path = Globals.GetPath(this);
        var line = File.ReadLines(Path.Combine(path));

        foreach (var l in line)
        {
            var currentList = new List<char>();
            matrix.Add(currentList);
            foreach (var c in l)
            {
                currentList.Add(c);
            }
        }
    }

    void PrintMatrix()
    {
        foreach (var l in matrix)
        {
            foreach (var c in l)
            {
                Console.Write(c);
            }

            Console.WriteLine();
        }
    }

    public string SolvePart1()
    {
        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Count; j++)
            {
                if (matrix[i][j] == 'X')
                {
                    matrix[i][j] = GetNextLetter(matrix[i][j]);
                }
            }
        }

        return "Not Done";
    }

    char GetNextLetter(char l) => l switch {
        'X' => 'M',
        'M' => 'A',
        'A' => 'S',
        'S' => '.',
    };

    public string SolvePart2()
    {
        return "Not Done";
    }
}

