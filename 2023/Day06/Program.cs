var lines = File.ReadLines("Inputs.txt").ToArray();

PartOne(lines);
PartTwo(lines);

void PartOne(string[] lines)
{
    var times = lines[0].Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse);
    var distances = lines[1].Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse);

    List<int> possibilities = new();
    for (int i = 0; i < times.Count(); i++)
    {
        var time = times.ElementAt(i);
        var distance = distances.ElementAt(i);
        var winningTimesHeld = new List<int>();
        for (int j = time; j >= 0; j--)
        {
            var timeLeft = time - j;
            if (j * timeLeft > distance)
            {
                winningTimesHeld.Add(j);
            }
        }

        possibilities.Add(winningTimesHeld.Count);
    }
    
    Console.WriteLine(possibilities.Aggregate(1, (acc, num) => acc * num));
}

void PartTwo(string[] lines)
{
    var time = long.Parse(string.Join("", lines[0].Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)));
        
    var distance = long.Parse(string.Join("", lines[1].Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)));
    
    var winningTimesHeld = new List<long>();
    for (long j = time; j >= 0; j--)
    {
        var timeLeft = time - j;
        if (j * timeLeft > distance)
        {
            winningTimesHeld.Add(j);
        }
    }
    
    Console.WriteLine(winningTimesHeld.Count);
}