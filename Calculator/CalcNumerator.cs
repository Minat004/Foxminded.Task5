using System.Text.RegularExpressions;
using Calculator.ExpressionException;
using Calculator.Modes;

namespace Calculator;

public class CalcNumerator
{
    private readonly Regex _regex;
    private readonly IMode _mode;

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

    public CalcNumerator(IMode mode)
    {
        _mode = mode;
        _regex = new Regex(PATTERN);
    }

    public void Calculation()
    {
        Validator.DeleteIfExist();
        
        foreach (var input in _mode.GetExpressions())
        {
            try
            {
                Validator.AnyWordCharacter(input);
                Validator.NotOperationSymbol(input);
                Validator.DigitOnEdges(input);
                Validator.CorrectQueue(input);
                Validator.CorrectBreaks(input);
                
                var digits = new Stack<decimal>();
                var operations = new Stack<string>();
            
                foreach (Match match in _regex.Matches(input!))
                {
                    SelectStack(input!, match, digits, operations);
                }

                while (operations.Count != 1 && digits.Count != 2)
                {
                    digits.Push(Operation(operations.Pop(), digits.Pop(), digits.Pop()));
                }
            
                _mode.SetResult(input!, Operation(operations.Pop(), digits.Pop(), digits.Pop()));
            }
            catch (Exception e)
            {
                _mode.SetResult(input!, e.Message);
            }
        }
    }

    #region private_methods
    private static void SelectStack(string input, Capture match, Stack<decimal> digits, Stack<string> operations)
    {
        if (decimal.TryParse(match.Value, out var digit))
        {
            ParseMinusToDigit(input, digits, operations, digit);
            return;
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

    private static void ParseMinusToDigit(string input, Stack<decimal> digits, Stack<string> operations, decimal digit)
    {
        var isStartWithMinus = operations.Count != 0 && digits.Count == 0 && operations.Peek() == "-";
        var isMinusAfterOpenBreak = operations.Count >= 2 && Validator.IsOpenBreakMinusDigitQueue(input, digit);

        if (isStartWithMinus || isMinusAfterOpenBreak)
        {
            digits.Push(-1 * digit);
            operations.Pop();
            return;
        }
        
        digits.Push(digit);
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
        switch (operation)
        {
            case "*":
                return second * first;
            case "+":
                return second + first;
            case "/":
                if (first == 0)
                {
                    throw new DivideByZeroException();
                }
                return second / first;
            case "-":
                return second - first;
            default:
                throw new NotOperationSymbolException();
        }
    }
    #endregion
}