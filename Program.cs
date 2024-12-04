
IDay[] days = [ 
    new TwentyThree.Day1(),
    new TwentyFour.Day1() 
    ];

foreach (var day in days)
{
    day.Initialize();
    Console.WriteLine($"{day.Title} Part 1: {day.SolvePart1()}");
    Console.WriteLine($"{day.Title} Part 2: {day.SolvePart2()}");
    Console.WriteLine();
}