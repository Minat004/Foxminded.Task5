using Calculator.Wrapper.Console;

namespace Calculator.Modes;

public class ConsoleMode : IMode
{
    private readonly IConsoleIO _console;

    public ConsoleMode(IConsoleIO console)
    {
        _console = console;
    }

    public string Name => "CONSOLE MODE";

    public IEnumerable<string?> GetExpressions()
    {
        _console.Write("[blue]<calculate>: [/]");
        
        var result = new List<string?> { _console.ReadLine() };

        return result;
    }

    public void SetResult(decimal value, string input)
    {
        _console.WriteLine($"[green]<result>: {value}[/]");
    }

    public void SetResult(string value)
    {
        _console.WriteLine($"[red]<result>: {value}[/]");
    }
}