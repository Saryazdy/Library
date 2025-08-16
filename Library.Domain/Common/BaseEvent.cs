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
<<<<<<< HEAD

=======
>>>>>>> d3c0b503dfc5f415fa69e49ae0bc5629030139fc
    }
}
