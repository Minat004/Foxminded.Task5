using System.Text.RegularExpressions;

namespace Calculator;

public class Separator
{
    private const string PATTERN = @"\d+,?\d*|[*+/()-]";

    private readonly string _input;
    private readonly Regex _regex;

    public Separator(string input)
    {
        _input = input;
        _regex = new Regex(PATTERN);
    }

    public MatchCollection GetMatches()
    {
        var matches = _regex.Matches(_input);
        return matches;
    }
}