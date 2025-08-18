using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Behaviours
{
    public class CachingBehaviour<TRequest, TResponse>
     : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IMemoryCache _cache;

        public CachingBehaviour(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"{typeof(TRequest).Name}:{request.GetHashCode()}";

            if (_cache.TryGetValue(cacheKey, out TResponse cachedResponse))
            {
                return cachedResponse;
            }

            var response = await next();
            _cache.Set(cacheKey, response, TimeSpan.FromMinutes(5)); // کش ۵ دقیقه‌ای

            return response;
        }
    }
}
