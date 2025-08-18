using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Interfaces
{
    public interface ISpecification<T>
    {
        IQueryable<T> Apply(IQueryable<T> query);
        int? Take { get; }
        int? Skip { get; }
        Expression<Func<T, object>>? OrderBy { get; }
    }
}
