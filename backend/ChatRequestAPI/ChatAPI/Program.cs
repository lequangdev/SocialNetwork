using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using SignalRChat;
using ChatRequestAPI;
using Infrastructure;
using Infrastructure.DependencyInjection.Extentions;
using Infrastructure.Jwt;
using Infrastructure.RabitMq.MessageBus.ConsumerService.Interface;
using Infrastructure.RabitMq.MessageBus.ConsumerService;
using Infrastructure.Serilog;
using Infrastructure.Redis;
using DataAccessLayer.EF_core;
var builder = WebApplication.CreateBuilder(args);



// Configure CORS ( cho phép gửi request )
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
        builder.AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials(); ;
    });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR(); // Add SignalR services

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

// signalR (WebSocket)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatRequestAPI V1");
    });
}

// Enable CORS
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();
IConfiguration configuration = app.Configuration;
IWebHostEnvironment environment = app.Environment;
app.UseRouting();
app.MapControllers();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHub<ChatHub>("/chatHub"); 

app.Run();
