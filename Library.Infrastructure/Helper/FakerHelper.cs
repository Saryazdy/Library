using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Helper
{
    public static class FakerHelper
    {
        public static Faker<T> CreateFaker<T>(Func<Faker, object[]> ctorArgsProvider) where T : class
        {
            var type = typeof(T);

            // پیدا کردن هر سازنده که تعداد پارامترش با تعداد آرایه برابر باشه
            var ctor = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                           .FirstOrDefault(c => c.GetParameters().Length == ctorArgsProvider(new Faker()).Length);

            if (ctor == null)
                throw new InvalidOperationException($"No suitable private constructor found for {type.Name}");

            var faker = new Faker<T>()
                .CustomInstantiator(f =>
                {
                    var args = ctorArgsProvider(f);
                    return (T)ctor.Invoke(args);
                });

            return faker;
        }
    }
}
