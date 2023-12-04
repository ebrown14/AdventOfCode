using System.Text;

var directions = new int[][]
{
    new[] { -1, 0 },
    new[] { -1, 1 },
    new[] { 0, 1 },
    new[] { 1, 1 },
    new[] { 1, 0 },
    new[] { 1, -1 },
    new[] { 0, -1 },
    new[] { -1, -1 }
};

IEnumerable<string> lines = File.ReadLines("Inputs.txt");

char[][] charMap = lines.Select(line => line.ToCharArray()).ToArray();

PartOne(charMap);
PartTwo(charMap);

void PartOne(char[][] charMap)
{
    List<int> touchingNumbers = new();
    for (int r = 0; r < charMap.Length; r++)
    {
        var sb = new StringBuilder();
        bool touchingSymbol = false;
        for (int c = 0; c < charMap[r].Length; c++)
        {
            var character = charMap[r][c];
            if (char.IsDigit(character))
                sb.Append(character);
            
            if (!touchingSymbol && !IsDot(character))
                touchingSymbol = IsTouchingSymbol(charMap, r, c, sb.ToString());
            
            if ((character == '.' || IsElfSymbol(character) || c == charMap[r].Length - 1) 
                && sb.Length > 0)
            {
                if (touchingSymbol)
                    touchingNumbers.Add(int.Parse(sb.ToString()));
                sb.Clear();
                touchingSymbol = false;
            }
        }
    }

    var sumOfTouching = touchingNumbers.Sum();
    Console.WriteLine(sumOfTouching);
}

void PartTwo(char[][] charMap)
{
    List<int> numbers = new();
    
    for (var r = 0; r < charMap.Length; r++)
    {
        for (var c = 0; c < charMap[r].Length; c++)
        {
            var character = charMap[r][c];
            if (character == '*')
            {
                var direction = IsTouchingNumber(charMap, r, c);
                if (direction.Any())
                {
                    Dictionary<int, int[]> tempNumbers = new();
                    foreach (var dir in direction)
                    {
                        var row = r + dir[0];
                        var col = c + dir[1];
                        
                        while (col > 0 && char.IsDigit(charMap[row][col]))
                            col--;

                        if (!Char.IsDigit(charMap[row][col]))
                            col++;
                        
                        StringBuilder sb = new();
                        while (col < charMap[row].Length && char.IsDigit(charMap[row][col]))
                        {
                            sb.Append(charMap[row][col]);
                            col++;
                        }

                        var number = int.Parse(sb.ToString());
                        if (!tempNumbers.ContainsKey(number)
                            || (tempNumbers.ContainsKey(number)
                                && !tempNumbers[number].SequenceEqual(new[] { r, c })))
                        {
                            tempNumbers.Add(number, new[] { r, c });
                        }
                    }

                    if (tempNumbers.Keys.Count == 2)
                    {
                        numbers.Add(tempNumbers.Keys.ElementAt(0) * tempNumbers.Keys.ElementAt(1));
                    }
                }
            }
        }
    }

    var sum = numbers.Sum();
    Console.WriteLine(sum);
}


bool IsTouchingSymbol(char[][] charMap, int r, int c, string number)
{
    bool touching = false;
    for (int i = 0; i < directions.Length; i++)
    {
        var direction = directions[i];
        var row = r + direction[0];
        var col = c + direction[1];
        if (row < 0 || row >= charMap.Length || col < 0 || col >= charMap[row].Length)
            continue;
        var potentialSymbol = charMap[row][col];
        if (IsElfSymbol(potentialSymbol))
        {
            touching = true;
            break;
        }
    }

    return touching;
}

List<int[]> IsTouchingNumber(char[][] charMap, int r, int c)
{
    List<int[]> touchingDirections = new();
    for (int i = 0; i < directions.Length; i++)
    {
        var direction = directions[i];
        var row = r + direction[0];
        var col = c + direction[1];
        if (row < 0 || row >= charMap.Length || col < 0 || col >= charMap[row].Length)
            continue;
        var potentialNumber = charMap[row][col];
        if (char.IsDigit(potentialNumber))
        {
            touchingDirections.Add(direction);
        }
    }

    return touchingDirections;
}

bool IsElfSymbol(char @char)
{
    return @char != '.' && !char.IsDigit(@char);
}

bool IsDot(char @char)
{
    return @char == '.';
}