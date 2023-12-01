using System.Text;

PartOne();
PartTwo();

static void PartOne()
{
    int sum = 0;

    using StreamReader reader = new("Inputs.txt");
    while (!reader.EndOfStream)
    {
        var num = int.Parse(GetCalibrationValue(reader.ReadLine()));
        sum += num;
    }
    Console.WriteLine(sum);

        static string GetCalibrationValue(string line)
    {
        var nums = line.Select(c => c).Where(char.IsDigit);
        return $"{nums.First()}{nums.Last()}";
    }
}

static void PartTwo()
{
    int sum = 0;

    using StreamReader reader = new("Inputs.txt");
    while (!reader.EndOfStream)
    {
        var num = int.Parse(GetCalibrationValue(reader.ReadLine()));
        sum += num;
    }
    Console.WriteLine(sum);

    static string GetCalibrationValue(string line)
    {
        var numbers = new string[]
        {
            "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
        };
        
        List<char> foundNumbers = new();
        StringBuilder sb = new();
        
        for (int i = 0; i < line.Length; i++)
        {
            var currentChar = line[i];
            if (Char.IsDigit(currentChar))
            {
                foundNumbers.Add(currentChar);
                continue;
            }
            sb.Append(currentChar);
            for (int j = i; j < line.Length; j++)
            {
                if (j != i)
                {
                    var nextCurrentChar = line[j];
                    sb.Append(nextCurrentChar);
                }

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
    
    static char ConvertWordToNumber(string number) => number switch
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