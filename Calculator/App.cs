using System.Collections;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using Wrapper.Console;

namespace Calculator;

public class App
{
    private readonly IConfiguration _configuration;
    private readonly IConsoleIO _console;

    public Task RunAsync()
    {
        _console.Write(new FigletText($"{_configuration["AppName"]} {_configuration["Version"]}")
            .LeftJustified()
            .Color(Color.Green));
        
        const string EXPRESSION = @"1+2*(3+4/2-(1+2))*2+1";

        var calc = new CalcNumerator(EXPRESSION);

        Console.WriteLine();
        Console.WriteLine(calc.Calculation());

        return Task.CompletedTask;
    }

    public App(IConfiguration configuration, IConsoleIO console)
    {
        _configuration = configuration;
        _console = console;
    }
}