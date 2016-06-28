using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supernova.Utils.Extensions
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Limits the current value to the specified minimum value.
        /// </summary>
        /// <typeparam name="T">The type of the instance, implementing the IComparable&lt;T&gt; interface.</typeparam>
        /// <param name="value">The current instance.</param>
        /// <param name="minValue">The minimum value to limit the instance to.</param>
        /// <returns>The specified minimum value, or the value itself if it is larger than the specified minimum value.</returns>
        public static T Min<T>(this T value, T minValue) where T : IComparable<T>
        {
            return
                value.CompareTo(minValue) < 0
                ? minValue
                : value;
        }

        /// <summary>
        /// Limits the current value to the specified maximum value.
        /// </summary>
        /// <typeparam name="T">The type of the instance, implementing the IComparable&lt;T&gt; interface.</typeparam>
        /// <param name="value">The current instance.</param>
        /// <param name="minValue">The maximum value to limit the instance to.</param>
        /// <returns>The specified maximum value, or the value itself if it is smaller than the specified maximum value.</returns>
        public static T Max<T>(this T value, T maxValue) where T : IComparable<T>
        {
            return
                value.CompareTo(maxValue) > 0
                ? maxValue
                : value;
        }

        /// <summary>
        /// Constrains ('clamps') a value to a range specified by a lower and upper bound.
        /// </summary>
        /// <typeparam name="T">The type of the instance, implementing the IComparable&lt;T&gt; interface.</typeparam>
        /// <param name="value">The current instance.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The value itself if it falls within the specified range; the minimum value if the value is smaller; or the maximum value if it is larger.</returns>
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        {
            T result = value;

            if (value.CompareTo(min) < 0)
            {
                result = min;
            }
            else if (value.CompareTo(max) > 0)
            {
                result = max;
            }

            return result;
        }

        /// <summary>
        /// Determines whether an enumerable generic collection contains the current value as determined by the default equality comparer for the type.
        /// </summary>
        /// <typeparam name="T">The type of the current object or value.</typeparam>
        /// <param name="source">The current object or value.</param>
        /// <param name="values">The enumerable generic collection to search.</param>
        /// <returns>True if the current value or object exists in the specified collection; otherwise false.</returns>
        public static bool In<T>(this T source, IEnumerable<T> values)
        {
            if (source == null) throw new ArgumentException(nameof(source));
            return (values != null && values.Contains(source));
        }

        /// <summary>
        /// Determines whether an enumerable generic collection contains the current value. An instance of IEqualityComparer&lt;T&gt; is used to test for equality.
        /// </summary>
        /// <typeparam name="T">The type of the current object or value.</typeparam>
        /// <param name="source">The current object or value.</param>
        /// <param name="values">The enumerable generic collection to search.</param>
        /// <returns>True if the current value or object exists in the specified collection; otherwise false.</returns>
        public static bool In<T>(this T source, IEnumerable<T> values, IEqualityComparer<T> comparer)
        {
            if (source == null) throw new ArgumentException(nameof(source));
            return (values != null && values.Contains(source, comparer));
        }

        /// <summary>
        /// Returns a specified number of elements of a collection starting at a specified index.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The current enumerable generic collection.</param>
        /// <param name="start">The start index within the collection.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>An instance of System.Collections.Generic.List containing the matching elements.</returns>
        public static List<T> Elements<T>(this IEnumerable<T> collection, int start, int count)
        {
            if (collection.Count() < start) throw new ArgumentOutOfRangeException(nameof(start), "The collection does not contain the required number of elements.");
            if (collection.Count() < start + count) throw new ArgumentOutOfRangeException(nameof(count), "The collection does not contain the required number of elements.");

            List<T> elements = new List<T>();

            for (int i = start; i < start + count; i++)
            {
                elements.Add(collection.ElementAt(i));
            }

            return elements;
        }

        /// <summary>
        /// Returns all elements of a sequence between the specified indices.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The current enumerable generic collection.</param>
        /// <param name="start">The lower bound of the range.</param>
        /// <param name="end">The upper bound of the range.</param>
        /// <returns>An instance of System.Collections.Generic.List containing the matching elements.</returns>
        public static List<T> ElementsInRange<T>(this IEnumerable<T> collection, int start, int end)
        {
            List<T> elements = new List<T>();

            for (int i = start; i < end; i++)
            {
                elements.Add(collection.ElementAt(i));
            }

            return elements;
        }

        /// <summary>
        /// Splits a collection in chunks containing a specified number of elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The current enumerable generic collection.</param>
        /// <param name="size">The chunk size.</param>
        /// <returns>A System.Collections.Generic.List containing the chunks.</returns>
        public static List<List<T>> Chunk<T>(this IEnumerable<T> source, int size)
        {
            List<List<T>> result = new List<List<T>>();
            if (!source.Any()) return result;

            List<T> current = new List<T>();
            result.Add(current);

            int i = 0;
            foreach (T item in source)
            {
                if (i >= size)
                {
                    i = 0;
                    current = new List<T>();
                    result.Add(current);
                }

                i++;
                current.Add(item);
            }

            return result;
        }

    }
}
