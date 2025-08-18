using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Interfaces
{
    public interface ISmsService
    {
        public Task SendSmsAsync(string number, string message, CancellationToken ct = default);
    }
}
