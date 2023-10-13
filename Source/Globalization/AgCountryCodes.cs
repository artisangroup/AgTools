using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ag.Tools.Extensions;

namespace Ag.Tools.Globalization
{
    /// <summary>
    /// Static class which resolves country codes and country names
    /// </summary>
    public class AgCountryCodes
    {
        private static object _lock = new object();
        private static Dictionary<string, string> _ccDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Loads the country code information from the CSV file (https://timezonedb.com/download)
        /// </summary>
        /// <returns>returns the initialized dictionary</returns>
        private Dictionary<string, string> _init()
        {
            try
            {
                lock (_lock)
                {
                    if (_ccDict.Count == 0)
                    {
                        IEnumerable<string> vDict = File.ReadLines(".\\_data\\country.csv");
                        vDict?.ToList()?.ForEach(line =>
                        {
                            if (!string.IsNullOrEmpty(line))
                            {
                                string[] foo = line.Split(',', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                                if (foo != null && foo.Count() == 2)
                                    _ccDict.TryAdd(foo[0].ToUpper(), foo[1]);
                            }
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return _ccDict;
        }

        /// <summary>
        /// Resolves the two digit country code into a country name
        /// </summary>
        /// <param name="countryCode">2 digit country code such as "US"</param>
        /// <returns>The string version for the country code, such as "United States". returns "" if not found</returns>
        public string NameFromCode(string countryCode)
        {
            string s = "";
            _init().TryGetValue(countryCode, out s);
            return s;
        }

        /// <summary>
        /// Resolves the country name into a 2 digit code
        /// </summary>
        /// <param name="countryName">The long, case insensitive, version of the country, such as "UniTED statES"</param>
        /// <returns>Returns the two digit country code for the name</returns>
        public string CodeFromName(string countryName)
        {
            return _init().Where( k => k.Value.AgIsEqual(countryName)).FirstOrDefault().Key?.ToUpper()??string.Empty;
        }
    }
}
