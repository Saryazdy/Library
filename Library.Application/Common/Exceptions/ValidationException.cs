
using FluentValidation.Results;


namespace Library.Application.Common.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();

        public ValidationException(IEnumerable<FluentValidation.Results.ValidationFailure> failures)
            : base("One or more validation failures have occurred.")
        {
            foreach (var group in failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage))
                Errors.Add(group.Key, group.ToArray());
        }
    }
}
