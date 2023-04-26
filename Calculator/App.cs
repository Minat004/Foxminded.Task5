using Microsoft.Extensions.Configuration;
using Spectre.Console;
using Wrapper.Console;

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
        _console.Write(new FigletText($"{_configuration["AppName"]} {_configuration["Version"]}")
            .LeftJustified()
            .Color(Color.Green));
        return Task.CompletedTask;
    }
}