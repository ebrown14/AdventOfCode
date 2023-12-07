var lines = File.ReadLines("Inputs.txt").Where(l => l != "").ToArray();

PartOne(lines);

void PartOne(string[] lines)
{
    var seeds = lines.ElementAt(0)
        .Split(':', StringSplitOptions.RemoveEmptyEntries)[1]
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(long.Parse)
        .ToDictionary(n => n, n => 0L);
    
    FarmerMap[] maps = new FarmerMap[]
    {
        new() { Map = new() {seeds}, Name = "seeds"},
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
                Dictionary<long, long> tempDict = new();
                var numberLine = lines[lineIndex];
                var (destination, source, length) = numberLine
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray();
                
                for (long j = 0; j < length; j++)
                {
                    tempDict.Add(source + j, destination + j);
                    
                    //Console.WriteLine("Map: {0}, source: {1}, destination: {2}", maps[mapIndex].Name, source + j, destination + j);
                }
                maps[mapIndex].Map.Add(tempDict);
                lineIndex++;
            }
            
            Dictionary<long, long> tempDict2 = new();
            foreach (var map in maps[mapIndex].Map)
            {
                foreach (var lastMap in maps[mapIndex - 1].Map)
                {
                    foreach (var source in lastMap.Keys)
                    {
                        if (!map.ContainsKey(source) && !tempDict2.ContainsKey(source))
                        {
                            tempDict2.Add(source, lastMap[source]);
                            //Console.WriteLine("Map: {0}, source: {1}, destination: {2}", maps[mapIndex].Name, source, lastMap[source]);
                        }
                    }
                }
            }
        }
    }
    
    Console.WriteLine("Done");
    /*List<long> locations = new();
    foreach (var seed in maps[0].Map.Keys)
    {
        locations.Add(maps[7].Map[maps[6].Map[maps[5].Map[maps[4].Map[maps[3].Map[maps[2].Map[maps[1].Map[seed]]]]]]]);
    }*/

    //Console.WriteLine(locations.Min());
}

class FarmerMap
{
    public List<Dictionary<long, long>> Map { get; set; }
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