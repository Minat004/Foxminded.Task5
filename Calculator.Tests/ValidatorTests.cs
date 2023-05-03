namespace Calculator.Tests;

public class ValidatorTests
{
    [Theory]
    [InlineData(@"a")]
    [InlineData(@"1+x+3+4")]
    [InlineData(@"x+y-x/y*x")]
    [InlineData(@" x  + y -   x  /  y*x    ")]
    [InlineData(@"100,45 + s + qewrh + 234")]
    [InlineData(@" abc    defog ")]
    [InlineData(@"ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    public void AnyWordCharacterTest(string? input)
    {
        Assert.Throws<AnyWordCharacterException>(() => Validator.AnyWordCharacter(input));
    }

    [Theory]
    [InlineData(@"1!2@3#4$5+6/7*8-9^10")]
    [InlineData(@"1+2+3%+4/5")]
    [InlineData(@"  1 + 2 + 3 % + 4 / 5   ")]
    [InlineData(@"!@#$%^&*_=<>.")]
    public void NotOperationSymbolTest(string? input)
    {
        Assert.Throws<NotOperationSymbolException>(() => Validator.NotOperationSymbol(input));
    }
    
    [Theory]
    [InlineData(@"*1+2+4-56+")]
    [InlineData(@"   +1+2+4-56/")]
    [InlineData(@"  )(-1+2+4-56")]
    [InlineData(@"/   1+2+4-56")]
    [InlineData(@",1+2+4-56")]
    [InlineData(@"1+2+4-56-")]
    [InlineData(@"1+2+4-56   (     ")]
    public void DigitOnEdgesTest(string? input)
    {
        Assert.Throws<MarginException>(() => Validator.DigitOnEdges(input));
    }
    
    [Theory]
    [InlineData(@"1++2+4-56")]
    [InlineData(@"1+2 3+4")]
    public void CorrectQueueTest(string? input)
    {
        Assert.Throws<WrongQueueException>(() => Validator.CorrectQueue(input));
    }
    
    [Theory]
    [InlineData(@"((1+2)")]
    [InlineData(@"(1+(2/(3*4))")]
    [InlineData(@"1+)(2/(3*4))")]
    public void CorrectBreaksTest(string? input)
    {
        Assert.Throws<NotCorrectBreaksException>(() => Validator.CorrectBreaks(input));
    }
}