using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.TwentyFour
{
    public class Day6 : IDay
    {
        public int Year => 2024;
        public int Day => 6;

        char[][] grid;
        private int symbolX;
        int symbolY;
        private char symbol = '^';
        Direction direction = Direction.Up;
        public void Initialize()
        {
            grid = File.ReadAllLines(Globals.GetPath(this)).Select(l => l.ToCharArray()).ToArray();
            (symbolX, symbolY) = FindSymbol();
            Console.WriteLine($"Symbol at {symbolX}, {symbolY}");
        }

        void PrintGrid()
        {
            StringBuilder sb = new("Grid:");
            //Console.WriteLine("Grid:");
            foreach (var l in grid)
            {
                foreach (var c in l)
                {
                    sb.Append(c);
                    //Console.Write(c);
                }
                sb.AppendLine();
                //Console.WriteLine();
            }
            Console.WriteLine(sb.ToString());
            Console.WriteLine();
        }

        (int, int) FindSymbol()
        {
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == symbol)
                    {
                        return (j, i);
                    }
                }
            }
            return (-1, -1);
        }

        public string SolvePart1()
        {
            int steps = 0;
            HashSet<string> positions = new();
            while (true)
            {
                if (symbolX < 0 || symbolX >= grid[0].Length || symbolY < 0 || symbolY >= grid.Length)
                    break;
                if (grid[symbolY][symbolX] == ' ')
                    break;

                // check if next step is a blockage
                switch (direction)
                {
                    case Direction.Up:
                        if (symbolY == 0)
                            goto done;
                        if (symbolY - 1 < 0 || grid[symbolY - 1][symbolX] == '#')
                        {
                            direction = Turn();
                            continue;
                        }
                        break;
                    case Direction.Down:
                        if (symbolY + 1 == grid.Length)
                            goto done;
                        if (symbolY + 1 >= grid.Length || grid[symbolY + 1][symbolX] == '#')
                        {
                            direction = Turn();
                            continue;
                        }
                        break;
                    case Direction.Left:
                        // check if we are at the end of the grid
                        if (symbolX == 0)
                            goto done;
                        if (symbolX - 1 < 0 || grid[symbolY][symbolX - 1] == '#')
                        {
                            direction = Turn();
                            continue;
                        }
                        break;
                    case Direction.Right:
                        if (symbolX + 1 == grid[0].Length)
                            goto done;
                        if (symbolX + 1 >= grid[0].Length || grid[symbolY][symbolX + 1] == '#')
                        {
                            direction = Turn();
                            continue;
                        }
                        break;
                }

                Move();
                if (positions.Add($"{symbolX},{symbolY}"))
                {
                    Console.WriteLine("Adding {0}, {1}", symbolX, symbolY);
                }
                Thread.Sleep(2000);
                
                PrintGrid();
            }
            done:
            PrintGrid();
            return positions.Count.ToString();
        }

        void Move()
        {
            switch (direction)
            {
                case Direction.Up:
                    grid[symbolY][symbolX] = 'X';
                    symbolY--;
                    grid[symbolY][symbolX] = '^';
                    break;
                case Direction.Down:
                    grid[symbolY][symbolX] = 'X';
                    symbolY++;
                    grid[symbolY][symbolX] = 'v';
                    break;
                case Direction.Left:
                    grid[symbolY][symbolX] = 'X';
                    symbolX--;
                    grid[symbolY][symbolX] = '<';
                    break;
                case Direction.Right:
                    grid[symbolY][symbolX] = 'X';
                    symbolX++;
                    grid[symbolY][symbolX] = '>';
                    break;
            }
        }

        public string SolvePart2()
        {
            throw new NotImplementedException();
        }

        Direction Turn() => direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new NotImplementedException()
        };
        enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}
