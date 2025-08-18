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
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }
        public Task SendEmailAsync(string to, string subject, string body, CancellationToken ct = default)
        {
            // مثال آموزشی: فقط نمایش
            Console.WriteLine($"Sending email via {_settings.SmtpServer}:{_settings.Port} to {to}");
            return Task.CompletedTask;
        }
    }
}
