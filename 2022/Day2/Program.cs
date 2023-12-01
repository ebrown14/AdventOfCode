IEnumerable<string> lines = File.ReadLines("Inputs.txt");

PartOne(lines);
PartTwo(lines);

static void PartOne(IEnumerable<string> lines)
{
    int mySum = 0;
    foreach (var line in lines)
    {
        var moves = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var opponentMove = GetRpsMove(moves[0]);
        var myMove = GetRpsMove(moves[1]);
        var sumForTurn = CalculatePointsForTurn(opponentMove, myMove);
        mySum += sumForTurn;
    }
    Console.WriteLine(mySum);
}

static void PartTwo(IEnumerable<string> lines)
{
    int mySum = 0;
    foreach (var line in lines)
    {
        var moves = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var opponentMove = GetRpsMove(moves[0]);
        var result = GetRpsResult(moves[1]);
        var myMove = GetMyMoveFromOpponentMoveAndResult(opponentMove, result);
        var sumForTurn = CalculatePointsForTurn(opponentMove, myMove);
        mySum += sumForTurn;
    }

    Console.WriteLine(mySum);
    
    static RpsMove GetMyMoveFromOpponentMoveAndResult(RpsMove opponentMove, RpsResult result)
    {
        if (result == RpsResult.Lose)
        {
            switch (opponentMove)
            {
                case RpsMove.Rock:
                    return RpsMove.Scissors;
                case RpsMove.Paper:
                    return RpsMove.Rock;
                case RpsMove.Scissors:
                    return RpsMove.Paper;
            }
        }
        else if (result == RpsResult.Draw)
        {
            return opponentMove;
        }
        else if (result == RpsResult.Win)
        {
            switch (opponentMove)
            {
                case RpsMove.Rock:
                    return RpsMove.Paper;
                case RpsMove.Paper:
                    return RpsMove.Scissors;
                case RpsMove.Scissors:
                    return RpsMove.Rock;
            }
        }

        return RpsMove.Paper;
    }
}

static int CalculatePointsForTurn(RpsMove opponent, RpsMove myMove)
{
    int mySum = (int)myMove;

    if (myMove == opponent)
    {
        mySum += 3;
    }
    else if ((myMove == RpsMove.Rock && opponent == RpsMove.Scissors)
             || (myMove == RpsMove.Scissors && opponent == RpsMove.Paper)
             || (myMove == RpsMove.Paper && opponent == RpsMove.Rock))
    {
        mySum += 6;
    }

    return mySum;
}

static RpsMove GetRpsMove(string move) => move switch
{
    "A" => RpsMove.Rock,
    "B" => RpsMove.Paper,
    "C" => RpsMove.Scissors,
    "X" => RpsMove.Rock,
    "Y" => RpsMove.Paper,
    "Z" => RpsMove.Scissors,
    _ => throw new ArgumentOutOfRangeException(nameof(move), move, null)
};

static RpsResult GetRpsResult(string result) => result switch
{
    "X" => RpsResult.Lose,
    "Y" => RpsResult.Draw,
    "Z" => RpsResult.Win,
    _ => throw new ArgumentOutOfRangeException(nameof(result), result, null)
};

enum RpsResult
{
    Win,
    Draw,
    Lose
}
enum RpsMove
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}
