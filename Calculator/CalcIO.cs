using Calculator.Modes;
using Calculator.Wrapper.Console;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using FileMode = Calculator.Modes.FileMode;

namespace Calculator;

public class CalcIO
{   
    private static readonly List<IMode> MODES = new()
    {
        new ConsoleMode(new ConsoleIO()),
        new FileMode(new ConsoleIO())
    };
    
    private readonly IConsoleIO _console;
    private readonly IConfiguration _configuration;
    private readonly IEnumerable<string> _modes;


    public CalcIO(IConfiguration configuration, IConsoleIO console)
    {
        _console = console;
        _configuration = configuration;
        _modes = MODES.Select(x => x.Name);
    }
    
    public void Header()
    {
        _console.Write(new FigletText($"{_configuration["AppName"]} {_configuration["Version"]}")
            .LeftJustified()
            .Color(Color.Green));
    }
    
    public IMode SelectMode()
    {
        var mode = _console.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Select mode:[/]")
                .PageSize(3)
                .AddChoices(_modes));
        
        _console.WriteLine($"[green]{mode}[/]");
        
        return MODES.FirstOrDefault(x => x.Name == mode)!;
    }
}