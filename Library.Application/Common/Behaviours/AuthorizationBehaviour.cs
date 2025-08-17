using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Behaviours
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class AuthorizeAttribute : Attribute
    {
        public string? Policy { get; init; }
        public string[] Roles { get; init; } = Array.Empty<string>();
    }

    public interface ICurrentUserService
    {
        string? UserId { get; }
        bool IsInRole(string role);
        Task<bool> HasPolicyAsync(string policy, CancellationToken ct);
    }

    public sealed class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICurrentUserService _currentUser;

        public AuthorizationBehaviour(ICurrentUserService currentUser) => _currentUser = currentUser;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            var authorizeAttributes = request!.GetType().GetCustomAttributes<AuthorizeAttribute>(true).ToArray();
            if (authorizeAttributes.Length == 0) return await next();

            if (_currentUser.UserId is null)
                throw new Common.Exceptions.ForbiddenAccessException("User is not authenticated.");

            foreach (var attr in authorizeAttributes)
            {
                foreach (var role in attr.Roles)
                    if (!_currentUser.IsInRole(role))
                        throw new Common.Exceptions.ForbiddenAccessException($"Missing role '{role}'.");

                if (!string.IsNullOrWhiteSpace(attr.Policy))
                {
                    var ok = await _currentUser.HasPolicyAsync(attr.Policy!, ct);
                    if (!ok) throw new Common.Exceptions.ForbiddenAccessException($"Missing policy '{attr.Policy}'.");
                }
            }

            return await next();
        }
    }
}
