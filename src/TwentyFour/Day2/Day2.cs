namespace AdventOfCode.TwentyFour;

public class Day2 : IDay
{
    public int Year => 2024;
    public  int Day => 2;

    string[] lines;
    public  void Initialize()
    {
        var path = Globals.GetPath(this);
        lines = File.ReadAllLines(Path.Combine(path));
    }

    public string SolvePart1()
    {
        int safeReports = 0;
        foreach (var line in lines)
        {
            var nums = line.Split(' ').Select(int.Parse).ToArray();
            if (ArrayIsSorted(nums))
            {
                bool good = true;
                for (int i = 0; i < nums.Length - 1; i++)
                {
                    if (!IsDiffGood(nums[i], nums[i + 1])) continue;
                    good = false;
                    break;
                }

                if (good)
                    safeReports++;
                
            }
        }

        return safeReports.ToString();
    }

    bool ArrayIsSorted(int[] arr)
    {
        bool sortedAsc = true;
        var asc = arr.OrderBy(n => n).ToArray();
        for (int i = 0; i < asc.Length; i++)
        {
            if (arr[i] != asc[i])
            {
                sortedAsc = false;
                break;
            }
        }

        var desc = arr.OrderByDescending(n => n).ToArray();
        bool sortedDesc = true;
        for (int i = 0; i < desc.Length; i++)
        {
            if (arr[i] != desc[i])
            {
                sortedDesc = false;
                break;
            }
        }
        return sortedAsc || sortedDesc;
    }

    bool IsDiffGood(int a, int b)
    {
        var diff = Math.Abs(a - b);
        return diff is > 3 or < 1;
    }

    public  string SolvePart2()
    {
        int safeReports = 0;
        List<string> reps = new();
        foreach (var line in lines)
        {
            var nums = line.Split(' ').Select(int.Parse).ToArray();
            for (int j = 0; j < nums.Length; j++)
            {
                bool good = true;
                var altNums = nums.Where((a, b) => b != j).ToArray();
                if (ArrayIsSorted(altNums))
                {
                    for (int i = 0; i < altNums.Length - 1; i++)
                    {
                        if (!IsDiffGood(altNums[i], altNums[i + 1])) continue;
                    good = false;
                    break;
                    }

                    if (good)
                    {
                        safeReports++;
                        reps.Add(line);
                        break;
                    }
                }
            }
        }

        return safeReports.ToString();
    }
}