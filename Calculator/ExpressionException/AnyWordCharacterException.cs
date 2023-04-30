namespace Calculator.ExpressionException;

public class AnyWordCharacterException : Exception
{
    private const string _message = "Wrong input. Expression need to be without word character.";
        
    public AnyWordCharacterException() : base(_message) { }
}