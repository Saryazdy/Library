using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Common
{
    public abstract class BaseEvent : INotification
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
