using Library.Application.Common.Interfaces;
using Library.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class SmsService : ISmsService
    {
        private readonly SmsSettings _settings;

        public SmsService(IOptions<SmsSettings> options)
        {
            _settings = options.Value;
        }

        public Task SendSmsAsync(string number, string message, CancellationToken ct = default)
        {
            Console.WriteLine($"Sending SMS via {_settings.ApiUrl} to {number}");
            return Task.CompletedTask;
        }
    }
}
