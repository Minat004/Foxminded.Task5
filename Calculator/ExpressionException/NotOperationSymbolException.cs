namespace Calculator.ExpressionException;

public class NotOperationSymbolException : Exception
{
    private const string _message = "Wrong input. Not correct symbol operation.";
    
    public NotOperationSymbolException() : base(_message) { }
}