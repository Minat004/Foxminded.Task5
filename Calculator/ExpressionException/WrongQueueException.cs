namespace Calculator.ExpressionException;

public class WrongQueueException : Exception
{
    private const string _message = "Wrong input. Wrong queue inline.";
    
    public WrongQueueException() : base(_message) { }
}