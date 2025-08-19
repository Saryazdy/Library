using FluentValidation;
using Library.Application.Common.Behaviours;
using Library.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;



namespace Library.Application
{
    // کلاس باید static باشد
    public static class DependencyInjection
    {
       
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            // FluentValidation
            services.AddValidatorsFromAssembly(assembly);

            // AutoMapper
            services.AddAutoMapper(cfg => { }, assembly);

            // Pipeline Behaviours (ترتیب مهمه)
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            // اختیاری:
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RetryBehaviour<,>));
      
            return services;
        }
    }
}
