namespace Ag.Quiver.Extensions;

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
    public static bool AgStartsWithUpper(this string? str)
    {
        return (string.IsNullOrEmpty(str)) ? false : char.IsUpper(str[0]);
    }

    /// <summary>
    /// Checks to see if the first letter is an lower case character
    /// </summary>
    /// <param name="str">the string to check</param>
    /// <returns>returns true if the first character is lowercase</returns>
    public static bool AgStartsWithLower(this string? str)
    {
        return (string.IsNullOrEmpty(str)) ? false : char.IsLower(str[0]);
    }

    /// <summary>
    /// Performs a simple case insensitive equality comparison 
    /// </summary>
    /// <param name="str">The source string for the comparison (cannot be null)</param>
    /// <param name="str1">The target string for the comparison</param>
    /// <returns>Returns true for a case-insensitive equality</returns>
    public static bool AgIsEqual(this string str, string str1)
    {
        return (str==null) ? false : str.Equals(str1, StringComparison.OrdinalIgnoreCase);
    }

}
