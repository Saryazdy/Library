using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Common
{
    public interface IHasDomainEvent
    {
        IReadOnlyCollection<BaseEvent> DomainEvents { get; }
        void ClearDomainEvents();
    }
}
