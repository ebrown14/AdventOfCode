using System.Security.Cryptography.X509Certificates;
using System.Text;

IEnumerable<string> lines = File.ReadLines("Inputs.txt");

PartOne(lines);

void PartOne(IEnumerable<string> lines)
{
    List<ElfGame> games = new List<ElfGame>();
    foreach (var line in lines)
    {
        games.Add(new ElfGame(line));
    }

    var sumOfGameIds = games.Where(game => game.Pulls.All(p => p.ValidPull))
        .Select(game => game.GameNumber)
        .Sum();
    Console.WriteLine(sumOfGameIds);
    
    PartTwo(games);
}

void PartTwo(IEnumerable<ElfGame> games)
{
    int powerSum = 0;
    foreach (var game in games)
    {
        var theoreticalPull = GetTheoreticalPull(game);
        var power = (theoreticalPull.RedCubes ?? 1) * (theoreticalPull.BlueCubes ?? 1) * (theoreticalPull.GreenCubes ?? 1);
        powerSum += power;
    }   
    Console.WriteLine(powerSum);
    
    ElfGamePull GetTheoreticalPull(ElfGame game)
    {
        return new ElfGamePull()
        {
            RedCubes = game.Pulls.Max(p => p.RedCubes),
            BlueCubes = game.Pulls.Max(p => p.BlueCubes),
            GreenCubes = game.Pulls.Max(p => p.GreenCubes)
        };
    }
}

class ElfGame
{
    public int GameNumber { get; set; } = 0;
    public List<ElfGamePull> Pulls { get; set; } = new List<ElfGamePull>();
    public ElfGame(string line)
    {
        var splitOnColon = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
        GameNumber = int.Parse(splitOnColon[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]); 
        
        var differentPulls = splitOnColon[1].Split(';', StringSplitOptions.RemoveEmptyEntries);
        foreach (var pull in differentPulls)
        {
            ElfGamePull gamePull = new ElfGamePull();
            var cubeColors = pull.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var cubeColor in cubeColors)
            {
                var colorCount = cubeColor.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                switch (colorCount[1])
                {
                    case "red":
                        var redCubes = int.Parse(colorCount[0]);
                        if (redCubes > 12)
                            gamePull.ValidPull = false;
                        gamePull.RedCubes = redCubes;
                        break;
                    case "blue":
                        var blueCubes = int.Parse(colorCount[0]);
                        if (blueCubes > 14)
                            gamePull.ValidPull = false;
                        gamePull.BlueCubes = blueCubes;
                        break;
                    case "green":
                        var greenCubes = int.Parse(colorCount[0]);
                        if (greenCubes > 13)
                            gamePull.ValidPull = false;
                        gamePull.GreenCubes = greenCubes;
                        break;
                }
            }
            Pulls.Add(gamePull);
        }
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"Game {GameNumber}:");
        foreach (var pull in Pulls)
        {
            stringBuilder.Append($"{pull};");
        }
        return stringBuilder.ToString();
    }
}

class ElfGamePull
{
    public int? RedCubes { get; set; }
    public int? BlueCubes { get; set; }
    public int? GreenCubes { get; set; }
    public bool ValidPull { get; set; } = true;
    
    public override string ToString()
    {
        return $" {(RedCubes != null ? $"{RedCubes} red" : "")}{(BlueCubes != null ? $", {BlueCubes} blue" : "")}" +
               $"{(GreenCubes != null ? $", {GreenCubes} green" : "")}";
    }
}