using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; private set; }
        public T? Data { get; private set; }
        public List<string> Errors { get; private set; } = new();

        private ApiResponse(bool success, T? data, List<string>? errors)
        {
            Success = success;
            Data = data;
            Errors = errors ?? new List<string>();
        }

        public static ApiResponse<T> Ok(T data) =>
            new ApiResponse<T>(true, data, null);

        public static ApiResponse<T> Fail(params string[] errors) =>
            new ApiResponse<T>(false, default, errors.ToList());
    }
}

