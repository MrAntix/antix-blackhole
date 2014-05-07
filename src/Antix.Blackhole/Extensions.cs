using System;
using System.Linq;

namespace Antix.Blackhole
{
    internal static class Extensions
    {
        #region Find

        /// <summary>
        ///   <para>Find the first index of any of the passed string values</para>
        /// </summary>
        /// <param name = "text">Text to search</param>
        /// <param name = "startIndex">Staring index</param>
        /// <param name = "delimiters">Protected delimiters</param>
        /// <param name = "stringComparison">String Comparison</param>
        /// <param name = "values">Values to find, finds first</param>
        /// <returns>FindResult, which is an implicit int for the index of the item found,  -1 = not found</returns>
        public static FindResult Find(
            this string text, int startIndex, FindDelimiters delimiters,
            StringComparison stringComparison, params string[] values)
        {
            if (text == null || values == null
                || startIndex < 0 || startIndex > text.Length) return null;
            var found = (from v in values
                         let i = text.IndexOf(v, startIndex, stringComparison)
                         orderby i
                         where i != -1
                         select new FindResult {Found = v, Index = i})
                .FirstOrDefault();

            if (found != null
                && delimiters != null
                && delimiters.Count > 0)
            {
                // check for delimiter open before the found value
                var open = (from d in delimiters
                            let iOpen = text
                                .IndexOf(d.Key, startIndex, stringComparison)
                            orderby iOpen
                            where iOpen != -1 && iOpen < found.Index
                            select new {Delimiter = d, Index = iOpen})
                    .FirstOrDefault();

                if (open != null)
                {
                    // check for valid close on found delimiter
                    var close = Find(text,
                                     open.Index + 1, stringComparison, open.Delimiter.Value);
                    if (close != null)
                        // find after delimiter closed
                        return Find(text,
                                    close.Index + 1, delimiters, stringComparison, values);
                }
            }

            return found;
        }

        #region overloads

        /// <summary>
        ///   <para>Find the first index of any of the passed string values</para>
        /// </summary>
        /// <returns>FindResult, which is an implicit int for the index of the item found,  -1 = not found</returns>
        public static FindResult Find(this string text, StringComparison stringComparison, params string[] values)
        {
            return Find(text, 0, null, stringComparison, values);
        }

        /// <summary>
        ///   <para>Find the first index of any of the passed string values</para>
        /// </summary>
        /// <returns>FindResult, which is an implicit int for the index of the item found,  -1 = not found</returns>
        public static FindResult Find(this string text, FindDelimiters delimiters, params string[] values)
        {
            return Find(text, 0, delimiters, StringComparison.Ordinal, values);
        }

        /// <summary>
        ///   <para>Find the first index of any of the passed string values</para>
        /// </summary>
        /// <returns>FindResult, which is an implicit int for the index of the item found,  -1 = not found</returns>
        public static FindResult Find(this string text, int startIndex, StringComparison stringComparison,
                                      params string[] values)
        {
            return Find(text, startIndex, null, stringComparison, values);
        }

        /// <summary>
        ///   <para>Find the first index of any of the passed string values</para>
        /// </summary>
        /// <returns>FindResult, which is an implicit int for the index of the item found,  -1 = not found</returns>
        public static FindResult Find(this string text, int startIndex, FindDelimiters delimiters,
                                      params string[] values)
        {
            return Find(text, startIndex, delimiters, StringComparison.Ordinal, values);
        }

        /// <summary>
        ///   <para>Find the first index of any of the passed string values</para>
        /// </summary>
        /// <returns>FindResult, which is an implicit int for the index of the item found,  -1 = not found</returns>
        public static FindResult Find(this string text, params string[] values)
        {
            return Find(text, 0, null, StringComparison.Ordinal, values);
        }

        /// <summary>
        ///   <para>Find the first index of any of the passed string values</para>
        /// </summary>
        /// <returns>FindResult, which is an implicit int for the index of the item found,  -1 = not found</returns>
        public static FindResult Find(this string text, int startIndex, params string[] values)
        {
            return Find(text, startIndex, null, StringComparison.Ordinal, values);
        }

        #endregion

        #endregion
    }
}