using Calculator.Modes;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Calculator.Wrapper.Console;

public class ConsoleIO : IConsoleIO
{
    public string? ReadLine()
    {
        return System.Console.ReadLine();
    }

    public void WriteLine(FormattableString value)
    {
        AnsiConsole.MarkupLineInterpolated(value);
    }

    public void WriteLine(string value)
    {
        AnsiConsole.MarkupLine(value);
    }

    public void WriteLine()
    {
        System.Console.WriteLine();
    }

    public void Write(IRenderable value)
    {
        AnsiConsole.Write(value);
    }

    public void Write(FormattableString value)
    {
        AnsiConsole.MarkupInterpolated(value);
    }

    public void Write(string value)
    {
        AnsiConsole.Markup(value);
    }

    public string Prompt(IPrompt<string> value)
    {
        return AnsiConsole.Prompt(value);
    }

    public IMode Prompt(IPrompt<IMode> value)
    {
        return AnsiConsole.Prompt(value);
    }

    public string Prompt()
    {
        return string.Empty;
    }

    public void WriteLine(decimal value)
    {
        System.Console.WriteLine(value);
    }
}