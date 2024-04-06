namespace PokeDexSharp.Core;

public static class StringExtensions
{
    public static bool EqualsIc(this string? str, string? value)
    {
        return string.Equals(str, value, StringComparison.InvariantCultureIgnoreCase);
    }
}
