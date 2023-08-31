using Ag.Tools.Extensions;

namespace Ag.Tools
{
    /// <summary>
    /// a framework for error codes mapping into string errors.
    /// </summary>
    public class AgError
    {
        /// <summary>
        /// Success
        /// </summary>
        public static int SUCCESS = 0;
        /// <summary>
        /// General exception error
        /// </summary>
        public static int E_EXCEPTION = 998;

        /// <summary>
        /// Other or unknown error
        /// </summary>
        public static int E_OTHER = 999;
    }
    public class AgErrorInfo
    {
        /// <summary>
        /// dictionary for error codes and strings
        /// </summary>
        private static Dictionary<int, string> _emap = new Dictionary<int, string>()
        {
            { AgError.SUCCESS, nameof(AgError.SUCCESS) },
            { AgError.E_OTHER, nameof(AgError.E_OTHER) },
            { AgError.E_EXCEPTION, nameof(AgError.E_EXCEPTION) }

        };
        /// <summary>
        /// Internal AgError code
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// string message for the AgError code
        /// </summary>
        public string? ErrorMsg { get; set; }
        public AgErrorInfo() => _init(AgError.SUCCESS);
        public AgErrorInfo(int errorCode, string? errorMsg = null) => _init(errorCode, errorMsg);
        private void _init(int errorCode, string? errorMsg = null)
        {
            ErrorCode = errorCode;
            ErrorMsg = errorMsg ?? LoadErrorMessage(errorCode);

        }

        /// <summary>
        /// Loads error message
        /// </summary>
        /// <param name="errorCode">one of the AgError codes</param>
        /// <returns>Returns the resource string for the error code</returns>
        public string LoadErrorMessage(int errorCode)
        {
            return $"AgError.{_emap.GetValueOrDefault(errorCode, string.Empty)}".AgLoadString();
        }

    }
}
