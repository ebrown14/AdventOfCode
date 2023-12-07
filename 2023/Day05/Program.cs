var lines = File.ReadLines("Inputs.txt").Where(l => l != "").ToArray();

PartOne(lines);

void PartOne(string[] lines)
{
    Dictionary<long, long> seeds = lines.ElementAt(0)
        .Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(long.Parse)
        .ToDictionary(n => n, n => n);

    Dictionary<long, long> seedToSoilMap = new();
    Dictionary<long, long> soilToFertilizerMap = new();
    Dictionary<long, long> fertilizerToWaterMap = new();
    Dictionary<long, long> waterToLightMap = new();
    Dictionary<long, long> lightToTempMap = new();
    Dictionary<long, long> tempToHumidityMap = new();
    Dictionary<long, long> humidityToLocationMap = new();

    FarmerMap[] maps = new FarmerMap[]
    {
        new() { Map = seeds, Name = "Seeds" },
        new() { Map = new(), Name = "seedToSoilMap" },
        new() { Map = new(), Name = "soilToFertilizerMap" },
        new() { Map = new(), Name = "fertilizerToWaterMap" },
        new() { Map = new(), Name = "waterToLightMap" },
        new() { Map = new(), Name = "lightToTempMap" },
        new() { Map = new(), Name = "tempToHumidityMap" },
        new() { Map = new(), Name = "humidityToLocationMap" },
    };

    List<int> mapNameIndexes = new();
    for (var index = 1; index < lines.Length; index++)
    {
        var line = lines[index];
        if (!char.IsDigit(line[0]))
            mapNameIndexes.Add(index);
    }
    
    for (int i = 0; i < mapNameIndexes.Count; ++i)
    {
        int mapIndex = i + 1;
        var lineIndex = mapNameIndexes[i] + 1;
        if (char.IsDigit(lines[lineIndex][0]))
        {
            while (lineIndex < lines.Length && char.IsDigit(lines[lineIndex][0]))
            {
                var numberLine = lines[lineIndex];
                var (destination, source, length) = numberLine
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray();
                
                for (long j = 0; j < length; j++)
                {
                    maps[mapIndex].Map.Add(source + j, destination + j);
                    //Console.WriteLine("Map: {0}, source: {1}, destination: {2}", maps[mapIndex].Name, source + j, destination + j);
                }

                lineIndex++;
            }

            foreach (var source in maps[mapIndex - 1].Map.Keys)
            {
                if (!maps[mapIndex].Map.ContainsKey(source))
                {
                    maps[mapIndex].Map.Add(source, source);
                    //Console.WriteLine("source: {0}, destination: {1}", source, source);
                }
            }
        }
    }

    List<long> locations = new();
    foreach (var seed in maps[0].Map.Keys)
    {
        locations.Add(maps[7].Map[maps[6].Map[maps[5].Map[maps[4].Map[maps[3].Map[maps[2].Map[maps[1].Map[seed]]]]]]]);
    }

    Console.WriteLine(locations.Min());
}

class FarmerMap
{
    public Dictionary<long, long> Map { get; set; }
    public string Name { get; set; }
}

public static class ArrayExtensions
{
    public static void Deconstruct<T>(this T[] items, out T t0, out T t1, out T t2)
    {
        t0 = items.Length > 0 ? items[0] : default(T);
        t1 = items.Length > 1 ? items[1] : default(T);
        t2 = items.Length > 2 ? items[2] : default(T);
    }
}