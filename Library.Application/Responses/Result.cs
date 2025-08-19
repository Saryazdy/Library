﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Responses
{
    namespace ProjectName.Application.Responses
    {
        public class Result<T>
        {
            public bool IsSuccess { get; private set; }
            public T? Data { get; private set; }
            public string[] Errors { get; private set; } = Array.Empty<string>();

            private Result() { }

            public static Result<T> Success(T data) => new Result<T>
            {
                IsSuccess = true,
                Data = data
            };

            public static Result<T> Failure(params string[] errors) => new Result<T>
            {
                IsSuccess = false,
                Errors = errors
            };
        }
    }
}
