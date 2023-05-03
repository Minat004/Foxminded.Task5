namespace Calculator.ExpressionException;

public class NotCorrectBreaksException : Exception
{
    private const string _message = "Wrong input. Not correct breaks.";
    
    public NotCorrectBreaksException() : base(_message) { }
}