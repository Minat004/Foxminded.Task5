namespace Calculator.Modes;

public interface IMode
{
    public string Name { get; }

    public IEnumerable<string?> GetExpressions();

    public void SetResult(decimal value, string input = "");
    
    public void SetResult(string value);
}