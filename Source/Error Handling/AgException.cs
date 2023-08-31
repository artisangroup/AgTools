using Ag.Tools.Extensions;
using System.Text.Json.Serialization;

namespace Ag.Tools
{
    public class AgException : AgErrorInfo
    {
        /// <summary>
        /// checks the current error code
        /// </summary>
        /// <returns>Returns TRUE if the error code is success</returns>
        public bool IsSuccess() { return ErrorCode == AgError.SUCCESS; }

        public AgException(Exception ex)
        {
            SetErrorInfo(AgError.E_OTHER);
        }
        public AgException(int errorCode)
        {
            SetErrorInfo(errorCode);
        }
        public AgException(AgException aex)
        {
            SetErrorInfo(aex.ErrorCode);
        }
        public AgException SetErrorInfo(int errorCode)
        {
            ErrorCode = errorCode;
            ErrorMsg = LoadErrorMessage(errorCode);
            return this;
        }
    }
}
