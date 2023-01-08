using System;
using System.Linq;
using System.Text;

namespace Promart.Core
{
    public static class StringHelper
    {
        public static bool IsNumbers(this string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            foreach (char c in text)
            {
                if (!char.IsNumber(c))
                    return false;
            }

            return true;
        }

        public static bool IsLetters(this string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            foreach (char c in text)
            {
                if (!char.IsLetter(c))
                    return false;
            }

            return true;
        }

        public static string ApplyOnlyLetterOrWhiteSpace(this string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            StringBuilder builder = new StringBuilder();

            foreach (char c in text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                    builder.Append(c);
            }

            return builder.ToString();
        }

        public static string ApplyOnlyNumber(this string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            StringBuilder builder = new StringBuilder();

            foreach (char c in text)
            {
                if (char.IsNumber(c))
                    builder.Append(c);
            }

            return builder.ToString();
        }

        public static string ApplyOnlyNumerOrLetter(this string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            StringBuilder builder = new StringBuilder();

            foreach (char c in text)
            {
                if (char.IsNumber(c) || char.IsLetter(c))
                    builder.Append(c);
            }

            return builder.ToString();
        }

        public static string ApplyOnlyNumberOrChars(this string? text, char[] selectedChars)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            StringBuilder builder = new StringBuilder();

            foreach (char c in text)
            {
                if (char.IsNumber(c) || selectedChars.Contains(c))
                    builder.Append(c);
            }

            return builder.ToString();
        }

        public static string? GetNullIfWhiteSpace(this string? text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : text;
        }
    }
}