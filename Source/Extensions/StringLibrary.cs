namespace Ax.Quiver.Extensions;

/// <summary>
/// Various string extensions
/// </summary>
public static class StringLibrary
{
    /// <summary>
    /// Checks to see if the first letter is an upper case character
    /// </summary>
    /// <param name="str">the string to check</param>
    /// <returns>returns true if the first character is uppercase</returns>
    public static bool AxStartsWithUpper(this string? str)
    {
        if (string.IsNullOrWhiteSpace(str))
            return false;

        char ch = str[0];
        return char.IsUpper(ch);
    }

}
