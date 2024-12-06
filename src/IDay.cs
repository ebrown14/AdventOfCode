using System.Diagnostics.CodeAnalysis;

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
    public static string GetPath([NotNull] IDay day, string? suffix = null)
    {
        var @type = day.GetType();
        return $"{@type.Namespace!.Replace("AdventOfCode.", "")}//Inputs//{@type.Name!}{suffix}.txt";
    }

}