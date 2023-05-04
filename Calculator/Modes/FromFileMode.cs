using Calculator.Wrapper.Console;

namespace Calculator.Modes;

public class FromFileMode : IMode
{
    private readonly IConsoleIO _console;

    public FromFileMode(IConsoleIO console)
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
        
        _console.WriteLine("[green]File read.[/]");
    }

    public void SetResult(string input, decimal value)
    {
        using (var streamWriter = new StreamWriter(@"output.txt", true))
        {
            streamWriter.WriteLine($"{input} = {value}");
        }
    }

    public void SetResult(string? input, string value)
    {
        using (var streamWriter = new StreamWriter(@"output.txt", true))
        {
            streamWriter.WriteLine($"{input} = {value}");
        }
    }
}