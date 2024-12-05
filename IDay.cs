public interface IDay
{
    public int Year { get; }
    public int Day { get; }
    public void Initialize();
    public string SolvePart1();
    public string SolvePart2();

    public string Title => $"{Year} Day {Day}";
    public string GetPath(Type t) => $"{t.Namespace}//Day{Day}//";
    public string GetPath() => GetPath(GetType());
}

public static class Globals
{
    public static string GetPath(IDay t) => $"{t.GetType().Namespace.Replace("AdventOfCode.", "")}//Day{t.Day}//";

}