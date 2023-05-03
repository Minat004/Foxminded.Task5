using Calculator.Modes;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Calculator.Tests;

public class CalcNumeratorTests
{
    [Theory]
    [InlineData("1+2+3+4", 10)]
    [InlineData("1+2*(3+4/2-(1+2))*2", 9)]
    [InlineData("99,98*12,3", 1229.754)]
    public void CalculationTest(string input, decimal expected)
    {
        var mockMode = new Mock<IMode>();
        
        mockMode.Setup(x => x.GetExpressions()).Returns(new List<string?> {input});

        var calc = new CalcNumerator(mockMode.Object);

        calc.Calculation();
        
        mockMode.Verify(x => x.SetResult(input, expected), Times.Once);
    }
    
    private static IConfiguration GetConfig()
    {
        var inMemory = new Dictionary<string, string>
        {
            { "AppName", "Calculator" },
            { "Version", "1.0" },
            { "Input", "input.txt" },
            { "Output", "output.txt" }
        };
        
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemory!)
            .Build();

        return configuration;
    }
}