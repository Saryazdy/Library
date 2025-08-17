using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Models
{
    public sealed class LookupDto
    {
        public Guid Id { get; init; }
        public string DisplayName { get; init; } = default!;
    }
}
