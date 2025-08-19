using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public string[] Errors { get; }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : base("Validation failed")
        {
            Errors = failures.Select(f => f.ErrorMessage).ToArray();
        }
    }
}
