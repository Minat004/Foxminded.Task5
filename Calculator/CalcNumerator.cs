using System.Text.RegularExpressions;
using Calculator.Wrapper.Console;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace Calculator;

public class CalcNumerator
{
    private const string PATTERN = @"\d+,?\d*|[*+/()-]";

    private static readonly Dictionary<string, int> _operationPriority = new()
    {
        {"*", 2},
        {"+", 1},
        {"/", 2},
        {"-", 1},
        {"(", -1},
        {")", 0}
    };
    
    private readonly string _input;
    private readonly IConfiguration _configuration;
    private readonly IConsoleIO _console;
    private readonly Regex _regex;
    private readonly string[]? _modes;

    public CalcNumerator(string input, IConfiguration configuration, IConsoleIO console)
    {
        _input = input;
        _configuration = configuration;
        _console = console;
        _regex = new Regex(PATTERN);
        _modes = _configuration.GetSection("Modes").Get<string[]>();
    }

    public void Header()
    {
        _console.Write(new FigletText($"{_configuration["AppName"]} {_configuration["Version"]}")
            .LeftJustified()
            .Color(Color.Green));
    }

    public string SelectMode()
    {
        var mode = _console.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Select mode:[/]")
                .PageSize(3)
                .AddChoices(_modes!));
        
        _console.WriteLine($"[green]{mode} mode![/]");

        return mode;
    }

    public decimal Calculation()
    {
        var digits = new Stack<decimal>();
        var operations = new Stack<string>();
        
        foreach (Match match in _regex.Matches(_input))
        {
            SelectStack(match, digits, operations);
        }

        return Operation(operations.Pop(), digits.Pop(), digits.Pop());
    }

    #region private_methods
    private static void SelectStack(Capture match, Stack<decimal> digits, Stack<string> operations)
    {
        if (decimal.TryParse(match.Value, out var digit))
        {
            digits.Push(digit);
            return;
        }

        if (!Array.Exists(_operationPriority.Keys.ToArray(), x => x == match.Value))
        {
            throw new Exception("Exception. Wrong input");
        }
        
        var isPriority = operations.Count != 0 && _operationPriority[match.Value] <= _operationPriority[operations.Peek()];
        var isOpenBreak = _operationPriority[match.Value] == -1;
        var isCloseBreak = _operationPriority[match.Value] == 0;

        if (!isPriority || isOpenBreak)
        {
            operations.Push(match.Value);
            return;
        }

        if (isCloseBreak)
        {
            SearchOpenBreak(digits, operations);
            return;
        }
        
        InlinePriority(match, digits, operations);
        
        operations.Push(match.Value);
        
    }

    private static void InlinePriority(Capture match, Stack<decimal> digits, Stack<string> operations)
    {
        while (operations.Count != 0 && _operationPriority[match.Value] <= _operationPriority[operations.Peek()])
        {
            var resultOperation = Operation(operations.Pop(), digits.Pop(), digits.Pop());

            digits.Push(resultOperation);
        }
    }

    private static void SearchOpenBreak(Stack<decimal> digits, Stack<string> operations)
    {
        while (_operationPriority[operations.Peek()] != -1)
        {
            var resultOperation = Operation(operations.Pop(), digits.Pop(), digits.Pop());

            digits.Push(resultOperation);
        }

        operations.Pop();
    }

    private static decimal Operation(string operation, decimal first, decimal second)
    {
        return operation switch
        {
            "*" => second * first,
            "+" => second + first,
            "/" => second / first,
            "-" => second - first,
            _ => throw new Exception("Exception. Wrong Input")
        };
    }
    #endregion
}