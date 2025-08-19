using Library.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Helper
{
    public static class IsbnFaker
    {
        private static Random _rnd = new Random();

        public static Isbn? RandomIsbn()
        {
            // با احتمال 50% null باشد
            if (_rnd.NextDouble() < 0.5) return null;

            // تولید ISBN-13 تصادفی معتبر
            string isbn;
            do
            {
                isbn = GenerateIsbn13();
            } while (!IsValidIsbn13(isbn));

            return Isbn.Create(isbn);
        }

        private static string GenerateIsbn13()
        {
            // 12 رقم اول تصادفی
            int[] digits = new int[12];
            for (int i = 0; i < 12; i++)
                digits[i] = _rnd.Next(0, 10);

            // محاسبه رقم کنترل
            int sum = 0;
            for (int i = 0; i < 12; i++)
                sum += (i % 2 == 0) ? digits[i] : digits[i] * 3;

            int check = (10 - (sum % 10)) % 10;

            return string.Concat(string.Join("", digits), check);
        }

        private static bool IsValidIsbn13(string s)
        {
            int sum = 0;
            for (int i = 0; i < 12; i++)
                sum += (i % 2 == 0 ? 1 : 3) * (s[i] - '0');

            int check = (10 - (sum % 10)) % 10;

            return check == (s[12] - '0');
        }
    }
}
