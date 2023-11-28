using System.Reflection;

namespace Artisan.Tools.Extensions
{
    /// <summary>
    /// Extensions for general objects and classes. These leverage reflection
    /// to help access props and other stuff
    /// </summary>
    public static class ObjectExtensions
    {
        private static BindingFlags _bindFlags =
            BindingFlags.FlattenHierarchy |
            BindingFlags.GetProperty |
            BindingFlags.IgnoreCase |
            BindingFlags.Instance |
            BindingFlags.NonPublic |
            BindingFlags.Public |
            BindingFlags.Static;

        /// <summary>
        /// Checks to see if the object contains a property with this name
        /// </summary>
        /// <param name="o">the object to check</param>
        /// <param name="p">the name of the property (case-insensitive)</param>
        /// <param name="isPublic">if true, only public properties are checked</param>
        /// <returns>returns true if the object contains a property with the supplied name</returns>
        public static bool AgHasProp(this object o, string p, bool isPublic = true)
        {
            bool hasProp = false;
            try
            {
                BindingFlags bf = BindingFlags.Instance;
                if (isPublic)
                    bf |= BindingFlags.Public;
                var vProps = o.GetType().GetProperties(bf);
                hasProp = null != Array.Find(vProps, property => true == property.Name.AgIsEqual(p));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return hasProp;
        }



    }
}
