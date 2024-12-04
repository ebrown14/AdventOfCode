public interface IDay
{
    public int Year { get; }
    public int Day { get; }
    public void Initialize();
    public string SolvePart1();
    public string SolvePart2();

    public string Title => $"{Year} Day {Day}";
}
