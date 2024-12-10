using System.Diagnostics;
using System.Reflection;

/*Console.WriteLine("Enter year to run (2024):");
int year;
while (!int.TryParse(Console.ReadLine(), out year) || year < 2024 || year > 2024) 
    Console.WriteLine("Invalid year. Please enter a year between 2024 and 2024.");
*/

int year = 2024;
var days = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => t.GetInterfaces().Contains(typeof(IDay)))
    .Select(t => (IDay?)Activator.CreateInstance(t))
    .Where(d => d is not null && d.Year == year)
    .OrderBy(d => d!.Day);


foreach (var day in days)
{
    if (day is null) continue;
    day.Initialize();
    Stopwatch sp = Stopwatch.StartNew();
    Console.WriteLine($"{day.Title} Part 1: {day.SolvePart1()} ({sp.Elapsed})");
    sp.Restart();
    Console.WriteLine($"{day.Title} Part 2: {day.SolvePart2()} ({sp.Elapsed})");
    Console.WriteLine();
}