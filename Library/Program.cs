using Library.API.Common.Services;
using Library.API.Controllers.Middlewares;
using Library.Application;
using Library.Application.Common.Behaviours;
using Library.Application.Common.Interfaces;
using Library.Infrastructure;
using Library.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddApplication(); // MediatR, Validators, etc.
builder.Services.AddInfrastructureServices(builder.Configuration); // DbContext, UoW, Repos
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<ISmsService, SmsService>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();