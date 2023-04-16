using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Promart.Core
{
    public static class StringHelper
    {
        public const int MIN_PHONE_LENGTH = 10;
        public const int MAX_PHONE_LENGTH = 11;

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

        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }

        public static string ApplyPhoneMask(this string value)
        {
            if (value.Length < MIN_PHONE_LENGTH || value.Length > MAX_PHONE_LENGTH)
                return value;

            string phone = value;
            string ddd = phone.Substring(0, 2);
            string number = phone.Substring(ddd.Length, phone.Length - ddd.Length);

            string lastNumbers = number.Substring(number.Length - 4);
            string firstNumbers = number.Substring(0, number.Length - lastNumbers.Length);

            return string.Concat(
                "(",
                ddd,
                ") ",
                firstNumbers,
                "-",
                lastNumbers);
        }
    }
}