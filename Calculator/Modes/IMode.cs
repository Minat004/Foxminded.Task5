namespace Calculator.Modes;

public interface IMode
{
    public string Name { get; }

    public IEnumerable<string?> GetExpressions();

    public void SetResult(string input, decimal value);
    
    public void SetResult(string input, string value);
}