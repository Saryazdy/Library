using Library.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class LoggerService : ILoggerService
    {
        public void LogInfo(string message) => Console.WriteLine($"INFO: {message}");
        public void LogWarning(string message) => Console.WriteLine($"WARN: {message}");
        public void LogError(string message, Exception? ex = null)
            => Console.WriteLine($"ERROR: {message} {(ex != null ? ex.Message : "")}");
    }
}
