using System.Text.Json.Serialization;
using Artisan.Tools.Extensions;

namespace Artisan.Tools
{
    public class AgException : System.Exception //AgErrorInfo
    {
        private AgErrorInfo _err = new AgErrorInfo();
        public AgErrorInfo ErrorInfo { get { return _err; } }
        public AgErrorInfo SetLastError(AgException aex)
        {
            return _err = new AgErrorInfo(aex.ErrorInfo);
        }

        /// <summary>
        /// Internal AgError code
        /// </summary>
        public int ErrorCode { 
            get { return _err.ErrorCode; }
            set { _err.ErrorCode = value; }
        }
        /// <summary>
        /// string message for the AgError code
        /// </summary>
        public string? ErrorMsg {
            get { return _err.ErrorMsg; }
            set { _err.ErrorMsg = value; }
        }
        public string LoadErrorMessage(int i) {  return _err.LoadErrorMessage(i); }

        /// <summary>
        /// checks the current error code
        /// </summary>
        /// <returns>Returns TRUE if the error code is success</returns>
        public bool IsSuccess() { return ErrorCode == AgError.SUCCESS; }

        public AgException() => SetErrorInfo(AgError.SUCCESS);
        public AgException(Exception ex) => SetErrorInfo(AgError.E_OTHER);
        public AgException(int errorCode) => SetErrorInfo(errorCode);
        public AgException(AgException aex) => SetErrorInfo(aex.ErrorCode);
        public AgException(AgErrorInfo ex)
        {
        }
        public AgException SetErrorInfo(int errorCode)
        {
            ErrorCode = errorCode;
            ErrorMsg = LoadErrorMessage(errorCode);
            return this;
        }
    }
}
