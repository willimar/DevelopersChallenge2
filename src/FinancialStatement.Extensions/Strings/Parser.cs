using FinancialExtensions.Exceptions;
using System;

namespace System
{
    public static class Parser
    {
        public static string Parse(this string value, string startFlag, string endFlag)
        {
            var (Start, End) = value.GetPositions(startFlag, endFlag);

            return value.Substring(Start + startFlag.Length, (End - Start) - startFlag.Length);
        }

        public static (string ParsedValue, string NewString) ParseAndRemove(this string value, string startFlag, string endFlag)
        {
            var (Start, End) = value.GetPositions(startFlag, endFlag);

            var parsedValue = value.Substring(Start + startFlag.Length, (End - Start) - startFlag.Length);
            var newString = value.Remove(Start, End - Start + endFlag.Length);

            return (parsedValue, newString);
        }

        public static string RemoveSpecialChars(this string value)
        {
            return value
                .Replace("\n", string.Empty)
                .Replace("\r", string.Empty);
        }

        private static (int Start, int End) GetPositions(this string value, string startFlag, string endFlag)
        {
            var start = string.Empty == startFlag ? 0 : value.IndexOf(startFlag);
            var end = string.Empty == endFlag ? -1 : value.IndexOf(endFlag, start + 1);

            if (start < 0)
            {
                throw new FlagValueException(nameof(startFlag));
            }

            if (end < 0)
            {
                end = value.Length;
            }

            return (start, end);
        }

        public static bool ParseCheck(this string value, string startFlag, string endFlag)
        {
            var start = value.IndexOf(startFlag);
            var end = value.IndexOf(endFlag, start + 1);

            return start >= 0 && end >= 0;
        }
    }
}
