﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Interfaces
{
    public interface IEmailService
    {
         Task SendEmailAsync(string to, string subject, string body, CancellationToken ct = default);
    }
}
