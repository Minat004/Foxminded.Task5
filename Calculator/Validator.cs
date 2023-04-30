using System.Text.RegularExpressions;
using Calculator.ExpressionException;

namespace Calculator;

public class Validator
{
    private readonly string _input;
    private readonly string _pattern;

    public Validator(string input, string pattern)
    {
        _input = input;
        _pattern = pattern;
    }

    public void AnyWordCharacter()
    {
        if (Regex.IsMatch(_input, @"\w+"))
        {
            throw new AnyWordCharacterException();
        }
    }

    public void NotOperationSymbol()
    {
        if (Regex.IsMatch(_input, @"[^\w\s\,\d*+/()-]+"))
        {
            throw new NotOperationSymbolException();
        }
    }

    public void StartWithDigits()
    {
        if (Regex.IsMatch(_input, @"^\s*\d+"))
        {
            throw new MarginException();
        }
    }
    
    public void EndWithDigits()
    {
        if (Regex.IsMatch(_input, @"\d+\s*$"))
        {
            throw new MarginException();
        }
    }
}