using Calculator.Modes;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Calculator.Wrapper.Console;

public interface IConsoleIO
{
    public string? ReadLine();
    
    public void WriteLine(FormattableString value);
    
    public void WriteLine(string value);
    
    public void WriteLine();
    
    public void Write(IRenderable value);
    
    public void Write(FormattableString value);
    
    public void Write(string value);
    
    public string Prompt(IPrompt<string> value);
    
    public IMode Prompt(IPrompt<IMode> value);
    
    public string Prompt();
    public void WriteLine(decimal calculation);
}