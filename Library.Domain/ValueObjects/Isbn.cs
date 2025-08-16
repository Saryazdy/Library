using Library.Domain.Common;

using Library.Domain.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Domain.ValueObjects
{
    /// <summary>
    /// Isbn به‌صورت ValueObject با اعتبارسنجی ISBN-10 و ISBN-13
    /// </summary>
    public class Isbn : ValueObject
    {
        public string Value { get; }

        private Isbn(string normalized)
        {
            Value = normalized;
        }

        public static Isbn Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new DomainException("ISBN cannot be empty.");

            // حذف dash و space
            var normalized = Regex.Replace(input, "[-\\s]", "");

            if (normalized.Length == 10 && IsValidIsbn10(normalized))
                return new Isbn(normalized);

            if (normalized.Length == 13 && IsValidIsbn13(normalized))
                return new Isbn(normalized);

            throw new DomainException("Invalid ISBN format.");
        }

        // ISBN-10: وزن 10..1، 'X' = 10
        private static bool IsValidIsbn10(string s)
        {
            if (!Regex.IsMatch(s, "^[0-9]{9}[0-9X]$")) return false;

            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                int v = (s[i] == 'X') ? 10 : (s[i] - '0');
                sum += v * (10 - i);
            }
            return sum % 11 == 0;
        }

        // ISBN-13: وزن‌های 1 و 3
        private static bool IsValidIsbn13(string s)
        {
            if (!Regex.IsMatch(s, "^[0-9]{13}$")) return false;

            int sum = 0;
            for (int i = 0; i < 13; i++)
            {
                int v = s[i] - '0';
                sum += (i % 2 == 0) ? v : v * 3;
            }
            return sum % 10 == 0;
        }

        protected override System.Collections.Generic.IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
