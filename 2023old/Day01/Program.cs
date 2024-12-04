using System.Text;

IEnumerable<string> lines = File.ReadLines("Inputs.txt");
PartOne(lines);
PartTwo(lines);

void PartOne(IEnumerable<string> lines)
{
    int sum = 0;
    
    foreach (var line in lines)
    {
        var num = int.Parse(GetCalibrationValue(line));
        sum += num;
    }

    Console.WriteLine(sum);

    string GetCalibrationValue(string line)
    {
        var nums = line.Select(c => c).Where(char.IsDigit);
        return $"{nums.First()}{nums.Last()}";
    }
}

void PartTwo(IEnumerable<string> lines)
{
    int sum = 0;
    
    foreach (var line in lines)
    {
        var num = int.Parse(GetCalibrationValue(line));
        sum += num;
    }

    Console.WriteLine(sum);

    string GetCalibrationValue(string line)
    {
        var numbers = new string[]
        {
            "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
        };

        List<char> foundNumbers = new();
        StringBuilder sb = new();

        for (var i = 0; i < line.Length; i++)
        {
            if (Char.IsDigit(line[i]))
            {
                foundNumbers.Add(line[i]);
                continue;
            }

            sb.Append(line[i]);
            for (var j = i + 1; j < line.Length; j++)
            {
                sb.Append(line[j]);
                if (numbers.Contains(sb.ToString()))
                {
                    foundNumbers.Add(ConvertWordToNumber(sb.ToString()));
                    sb.Clear();
                    break;
                }
            }
            sb.Clear();
        }

        return $"{foundNumbers.First()}{foundNumbers.Last()}";
    }

    char ConvertWordToNumber(string number) => number switch
    {
        "zero" => '0',
        "one" => '1',
        "two" => '2',
        "three" => '3',
        "four" => '4',
        "five" => '5',
        "six" => '6',
        "seven" => '7',
        "eight" => '8',
        "nine" => '9',
        _ => throw new Exception("Invalid number")
    };
}