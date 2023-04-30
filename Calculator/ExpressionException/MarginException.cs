namespace Calculator.ExpressionException;

public class MarginException : Exception
{
    private const string _message = "Wrong input. Wrong start or end line.";
    
    public MarginException() : base(_message) { }
}