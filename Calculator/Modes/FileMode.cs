using System.Diagnostics.CodeAnalysis;
using Calculator.Wrapper.Console;

namespace Calculator.Modes;

public class FileMode : IMode
{
    private readonly IConsoleIO _console;

    public FileMode(IConsoleIO console)
    {
        _console = console;
    }

    public string Name => "FROM FILE MODE";
    
    public IEnumerable<string?> GetExpressions()
    {
        using (var streamReader = new StreamReader(@"input.txt"))
        {
            while (!streamReader.EndOfStream)
            {
                yield return streamReader.ReadLine();
            }
        }
    }

    public void SetResult(decimal value, string input)
    {
        using (var streamWriter = new StreamWriter(@"output.txt", true))
        {
            streamWriter.WriteLine($"{input} = {value}");
        }
    }

    public void SetResult(string value)
    {
        using (var streamWriter = new StreamWriter("output.txt"))
        {
            foreach (var line in GetExpressions())
            {
                streamWriter.WriteLine($"{line} = {value}");
            }
        }
    }
}