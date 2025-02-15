using Infrastructure;
using Infrastructure.DependencyInjection.Extentions;
using Infrastructure.RabitMq.MessageBus.ConsumerService.Interface;
using Infrastructure.RabitMq.MessageBus.ConsumerService;
using Infrastructure.Serilog;
using MassTransit;
using MasstransitRabitMQ.contract.Abstractions.IntergrationEvents;
using Infrastructure.Jwt;
using Infrastructure.DependencyInjection.Options;
using Infrastructure.Redis;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Config masstransit rabitmq
builder.Services.AddConfigureMasstransitRabtiMQ(builder.Configuration);
// Producer injection
builder.Services.AddScoped<IProducer, Producer>();
// Consumer injection
builder.Services.AddScoped<ISmsService, SmsService>();

// Config serilog
builder.Services.AddConfigureSerilog(builder.Configuration);
// serilog injection
builder.Services.AddSingleton<ILoggingService, LoggingService>();
builder.Host.UseSerilogLogging();

// config jwt
builder.Services.AddAuthenticationJWT(builder.Configuration);
// Jwt injection
builder.Services.AddScoped<IJwtService, JwtService>();

// config EntityFrameWork core
builder.Services.AddEntityFrameWorkCore(builder.Configuration);

// Attribute cache
builder.Services.AddConfigureCache(builder.Configuration);
// Attribute injection
builder.Services.AddSingleton<IResponseCacheService, ResponseCacheService>();

builder.Services.AddAuthorization();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

