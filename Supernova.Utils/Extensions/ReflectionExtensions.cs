using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Supernova.Utils.Extensions
{

    public static class ReflectionExtensions
    {
        public static bool Implements<T>(this Type objectType)
        {
            foreach (Type intf in objectType.GetInterfaces())
            {
                if (intf.GetType() == typeof(T)) return true;
            }
            return false;
        }
    }

}
