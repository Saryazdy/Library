
// DbContext
using Library.Application.Common.Interfaces;
using Library.Infrastructure.Data;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Services;
using Library.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
            using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<LibraryDbContext>(options =>
            {
                var cs = configuration.GetConnectionString("LibraryDatabase");
                options.UseSqlServer(cs, opt => opt.EnableRetryOnFailure());
            });

            // Memory cache
            services.AddMemoryCache();

            // External services
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<SmsSettings>(configuration.GetSection("SmsSettings"));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISmsService, SmsService>();

            // Generic Repository
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));



            // UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

    }
}

