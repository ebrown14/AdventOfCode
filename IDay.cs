public interface IDay
{
    public int Year => 1970;
    public int Day => 0;
    public void Initialize();
    public string SolvePart1();
    public string SolvePart2();

    public string Title => $"{Year} Day {Day}";
}
