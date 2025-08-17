using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Models
{
    public sealed class Result
    {
        public bool Succeeded { get; init; }
        public string[] Errors { get; init; } = Array.Empty<string>();

        public static Result Success() => new() { Succeeded = true };
        public static Result Failure(params string[] errors) => new() { Succeeded = false, Errors = errors };
    }
}
