using Artisan.Tools.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artisan.Tools.Error_Handling
{
    /// <summary>
    /// Allows us to return complex information from an API call
    /// </summary>
    /// <typeparam name="T">Type of the return</typeparam>
    public class AgResult<T> : AgException
    {
        /// <summary>
        /// Implementation specific return/result object. This could be null.
        /// </summary>
        public T? Result { get; set; }

        /// <summary>
        /// Creates object
        /// </summary>
        public AgResult() : this(true) { }

        public AgResult(bool createInstance = true)
        {
            if (createInstance && (typeof(T) != typeof(string)))
                Result = Activator.CreateInstance<T>();
        }

        public AgResult(T result, int ErrorCode = 0)
        {
            this.Result = (result == null) ? Activator.CreateInstance<T>() : result;
            this.SetErrorInfo(ErrorCode);
        }
    }

    /// <summary>
    /// Helper class for results where a result is not required
    /// </summary>
    public class AgResult : AgResult<AgResponse>
    {
    }

}
