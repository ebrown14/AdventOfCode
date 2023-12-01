IEnumerable<string> lines = File.ReadLines("Inputs.txt");

PartOne(lines);
PartTwo(lines);

static void PartOne(IEnumerable<string> lines)
{
    List<int> numbers = GetSumForEachElf(lines);
    var max = numbers.Max();
    Console.WriteLine(max);
}

static void PartTwo(IEnumerable<string> lines)
{
    List<int> numbers = GetSumForEachElf(lines).OrderByDescending(num => num).ToList();
    var sum = numbers[0] + numbers[1] + numbers[2];
    Console.WriteLine(sum);
}

static List<int> GetSumForEachElf(IEnumerable<string> lines)
{
    List<int> numbers = new List<int>();
    int sum = 0;
    foreach (var line in lines)
    {
        if (line != "")
        {
            sum += int.Parse(line);
        }
        else
        {
            numbers.Add(sum);
            sum = 0;
        }
    }

    return numbers;
}