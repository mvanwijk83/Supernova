using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supernova.Utils.Extensions
{
    /// <summary>
    /// Contains extension methods operating on numeric types.
    /// </summary>
    public static class NumericExtensions
    {
        /// <summary>
        /// Returns a boolean indicating whether the given integer represents an odd value.
        /// </summary>
        /// <param name="i">The current 32-bit integer.</param>
        public static bool IsOdd(this int i)
        {
            return !(i % 2 == 0);
        }

        /// <summary>
        /// Returns a boolean indicating whether the given integer represents an even value.
        /// </summary>
        /// <param name="i">The current 32-bit integer.</param>
        public static bool IsEven(this int i)
        {
            return (i % 2 == 0);
        }

        /// <summary>
        /// Returns a boolean indicating whether the given integer falls within a defined range of values.
        /// </summary>
        /// <param name="i">The current 32-bit integer.</param>
        /// <param name="min">The lower bound of the range.</param>
        /// <param name="max">The upper bound of the range.</param>
        /// <param name="exclusive">Indicates whether to return true if the current integer is equal to one of the range bounds (default is false).</param>
        /// <returns></returns>
        public static bool Between(this int i, int min, int max, bool exclusive = false)
        {
            if (!exclusive)
            {
                return (i >= min && i <= max);
            }
            else
            {
                return (i > min && i < max);
            }
        }

        /// <summary>
        /// Returns a boolean indicating whether the System.Type is numeric.
        /// </summary>
        /// <param name="t">The current System.Type.</param>
        public static bool IsNumeric(this Type t)
        {
            if (t == null) throw new ArgumentException(nameof(t));

            switch (Type.GetTypeCode(t))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                case TypeCode.Object:
                    // if a type is wrapped by Nullable<T>, TypeCode is returned as Object, so its underlying type must be inspected
                    if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return NumericExtensions.IsNumeric(Nullable.GetUnderlyingType(t));
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

    }
}
