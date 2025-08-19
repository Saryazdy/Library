namespace Library.API.Common.Services
{
    using Library.Application.Common.Behaviours;
    using Library.Application.Common.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId =>
            _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public bool IsInRole(string role) =>
            _httpContextAccessor.HttpContext?.User?.IsInRole(role) ?? false;

        public Task<bool> HasPolicyAsync(string policy, CancellationToken ct)
        {
            // 👇 اینجا بستگی به سیاست‌هات داره
            // می‌تونی از IAuthorizationService استفاده کنی
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return Task.FromResult(false);

            var authService = httpContext.RequestServices.GetRequiredService<IAuthorizationService>();
            return authService.AuthorizeAsync(httpContext.User, policy)
                              .ContinueWith(t => t.Result.Succeeded, ct);
        }
    }

}
