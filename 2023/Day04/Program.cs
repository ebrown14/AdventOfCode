IEnumerable<string> lines = File.ReadLines("Inputs.txt");

PartOne(lines);
PartTwo(lines);

void PartOne(IEnumerable<string> lines)
{
    int points = 0;
    foreach (var line in lines)
    {
        var matches = 0;
        var pointsForGame = 0;
        var card = new GameCard(line);

        foreach (var number in card.CardNumbers)
        {
            if (card.WinningNumbers.Contains(number))
            {
                matches++;
                pointsForGame = matches == 1 ? 1 : pointsForGame * 2;
            }
        }

        points += pointsForGame;
    }

    Console.WriteLine(points);
}

void PartTwo(IEnumerable<string> lines)
{
    var allCards = lines.Select(line => new GameCard(line))
                                            .ToDictionary(card => card.GameNumber, card => new List<GameCard> { card });

    for (var i = 1; i < allCards.Count; i++)
    {
        var cards = allCards[i];
        foreach (var card in cards)
        {
            var matches = card.CardNumbers.Count(number => card.WinningNumbers.Contains(number));
            
            for (var j = 1; j <= matches; j++)
                allCards[allCards[card.GameNumber + j].First().GameNumber].Add(allCards[card.GameNumber + j].First());
        }
    }

    var sumOfCards = allCards.Keys.Sum(key => allCards[key].Count);
    Console.WriteLine(sumOfCards);
}


class GameCard
{
    public int GameNumber { get; set; }
    public int[] WinningNumbers { get; set; }
    public int[] CardNumbers { get; set; }

    public GameCard(string line)
    {
        var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
        GameNumber = int.Parse(parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
        var numParts = parts[1].Split('|', StringSplitOptions.RemoveEmptyEntries);

        WinningNumbers = numParts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray();
        CardNumbers = numParts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray();
    }

    public override string ToString()
    {
        return $"Game {GameNumber.ToString(),2}: {string.Join(' ', WinningNumbers)} | {string.Join(' ', CardNumbers)}";
    }
}