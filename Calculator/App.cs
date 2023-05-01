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
        var calcIO = new CalcIO(_configuration, _console);

        calcIO.Header();
        var mode = calcIO.SelectMode();

        var calc = new CalcNumerator(mode);

        // while (true)
        // {
        //     try
        //     {
        //         calc.Calculation(mode);
        //     }
        //     catch (Exception e)
        //     {
        //         mode.SetResult(e.Message);
        //         throw;
        //     }
        // }
        
        try
        {
            calc.Calculation();
        }
        catch (Exception e)
        {
            mode.SetResult(e.Message);
            throw;
        }
        
        return Task.CompletedTask;
    }
}