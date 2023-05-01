using Calculator.Modes;
using Moq;

namespace Calculator.Tests;

public class CalcNumeratorTests
{
    [Theory]
    [InlineData("1+2+3+4", 10)]
    [InlineData("1+2*(3+4/2-(1+2))*2", 9)]
    public void CalculationTest(string input, decimal expected)
    {
        var mock = new Mock<IMode>();
        mock.Setup(x => x.GetExpressions()).Returns(new List<string?> {input});
        
        var calc = new CalcNumerator(mock.Object);

        calc.Calculation();
        
        mock.Verify(x => x.SetResult(expected, input), Times.Once);
    }
}