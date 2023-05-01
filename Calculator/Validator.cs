using System.Text.RegularExpressions;
using Calculator.ExpressionException;

namespace Calculator;

public static class Validator
{
    public static void DeleteIfExist()
    {
        if (File.Exists(@"output.txt"))
        {
            File.Delete(@"output.txt");
        }
    }
    
    public static void AnyWordCharacter(string? input)
    {
        if (Regex.IsMatch(input!, @"[\p{L}\p{Mn}\p{Pc}]+"))
        {
            throw new AnyWordCharacterException();
        }
    }

    public static void NotOperationSymbol(string? input)
    {
        if (Regex.IsMatch(input!, @"[^\w\s\,\d*+/()-]+"))
        {
            throw new NotOperationSymbolException();
        }
    }

    public static void DigitOnEdges(string? input)
    {
        if (!Regex.IsMatch(input!, @"^\s*\-*\s*\(*\s*\-*\s*\d+")
            || !Regex.IsMatch(input!, @"\s*\d+\s*\)*\s*$"))
        {
            throw new MarginException();
        }
    }

    public static void CorrectQueue(string? input)
    {
        if (Regex.IsMatch(input!, @"\d+\s+\d+") && Regex.IsMatch(input!, @"[*+/(-]\s*[**+/)-]"))
        {
            throw new WrongQueueException();
        }
    }

    public static void CorrectBreaks(string input)
    {
        
    }
}