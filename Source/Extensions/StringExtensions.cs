using System.Net;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Artisan.Tools.Extensions;

/// <summary>
/// Various string extensions
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Loads a string resource from this assembly
    /// </summary>
    /// <returns>the loaded resource if successful or the original string, or string.Empty</returns>
    public static Assembly? _ra = null;
    public static ResourceManager? _rm = null;
    public static string AgLoadString(this string? str, string defaultString = "")
    {
        string s1 = $"{str ?? string.Empty}";//make a copy
        try
        {
            if (null == _ra)
                _ra = Assembly.GetAssembly(Type.GetType("Artisan.Tools.AgError"));
            if ((null != _ra) && (null == _rm))
                _rm = new ResourceManager($"Artisan.Tools.Properties.Resources", _ra ?? null);
            try
            {
                if (null != _rm)
                    s1 = _rm.GetString(s1) ?? str ?? defaultString;
            }
            catch (Exception)
            {
            }

            if (string.IsNullOrEmpty(s1) && !string.IsNullOrEmpty(defaultString))
                s1 = defaultString;
        }
        catch
        {
        }
        return s1;
    }
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
        return (str == null) ? false : str.Equals(str1, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Replaces (Case Insensitive)
    /// </summary>
    public static string AgReplace(this string s, string oldText, string newText)
    {
        return (string.IsNullOrEmpty(s)) ? s : s.Replace(oldText, newText, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Checks to see if s2 is somewhere in s1 
    /// </summary>
    public static bool AgContains(this string s1, string s2)
    {
        return !string.IsNullOrEmpty(s1) && s1.Contains(s2, StringComparison.CurrentCultureIgnoreCase);
    }

    /// <summary>
    /// checks to see if the string is "1" or "true"
    /// ex: string s1 = "true";
    ///      if (s1.IsTrue())
    /// </summary>
    static public bool AgIsTrue(this string x)
    {
        return (!string.IsNullOrEmpty(x) && ((x == "1") || (x.ToLower() == "true")));
    }
    static public bool AgIsFalse(this string x)
    {
        return (!string.IsNullOrEmpty(x) && ((x == "0") || (x.ToLower() == "false")));
    }


    public static string AgToIpAddress(this string ipAddress)
    {
        IPAddress? ipout = IPAddress.None;
        if (!IPAddress.TryParse(ipAddress, out ipout))
            ipAddress = IPAddress.Any.ToString();
        return ipAddress;
    }




}
