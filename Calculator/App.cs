using Calculator.Wrapper.Console;
using Microsoft.Extensions.Configuration;

namespace Calculator;

public class App
{
    private readonly IConfiguration _configuration;
    private readonly IConsoleIO _console;

    public App(IConfiguration configuration, IConsoleIO console)
    {
        _configuration = configuration;
        _console = console;
    }

    public Task RunAsync()
    {
        const string EXPRESSION = @"1+2*(3+4/2-(1+2))*2+1";

        var calc = new CalcNumerator(EXPRESSION, _configuration, _console);
        
        calc.Header();
        calc.SelectMode();

        Console.WriteLine();
        Console.WriteLine(calc.Calculation());

        return Task.CompletedTask;
    }
}