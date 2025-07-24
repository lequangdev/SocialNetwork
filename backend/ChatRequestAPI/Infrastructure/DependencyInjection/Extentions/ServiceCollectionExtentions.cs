using System.Reflection;
using Infrastructure.DependencyInjection.Options;
using Infrastructure.Serilog;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Security.Claims;




namespace Infrastructure.DependencyInjection.Extentions
{
    public static class ServiceCollectionExtentions
    {
        // MasstransitRabtiMQ configuration
        public static IServiceCollection AddConfigureMasstransitRabtiMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var masstransitConfiguration = new MasstransitConfiguration();
            configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);

            services.AddMassTransit(mt =>
            {
                mt.AddConsumers(Assembly.GetExecutingAssembly());
                mt.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, h =>
                    {
                        h.Username(masstransitConfiguration.UserName);
                        h.Password(masstransitConfiguration.Password);
                    });
                    bus.ConfigureEndpoints(context);
                });
            });

            return services;
        }

        // Serilog configuration
        public static IServiceCollection AddConfigureSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            var serilogConfiguration = new SerilogConfiguration();
            configuration.GetSection(nameof(SerilogConfiguration)).Bind(serilogConfiguration);

            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Is(Enum.Parse<LogEventLevel>(serilogConfiguration.MinimumLevel));

            var sinkActions = new Dictionary<string, Action<LoggerConfiguration, SerilogSink>>
            {
                { "Console", (config, sink) => config.WriteTo.Console() },
                { "File", (config, sink) =>
                    {
                        if (sink.Args.ContainsKey("path") && sink.Args.ContainsKey("rollingInterval"))
                        {
                            var path = sink.Args["path"];
                            var rollingInterval = Enum.Parse<RollingInterval>(sink.Args["rollingInterval"]);
                            config.WriteTo.File(
                                path,
                                rollingInterval: rollingInterval
                            );
                        }
                    }
                }
            };

            foreach (var sink in serilogConfiguration.WriteTo)
            {
                if (sinkActions.TryGetValue(sink.Name, out var configureSink))
                {
                    configureSink(loggerConfig, sink);
                }
            }

            Log.Logger = loggerConfig.CreateLogger();

            services.AddSingleton(Log.Logger);

            return services;
        }
        public static IHostBuilder UseSerilogLogging(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseSerilog(Log.Logger);
        }

        // Jwt configuration
        public static IServiceCollection AddAuthenticationJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(configuration.GetSection(nameof(JwtConfiguration)));
            var jwtConfiguration = configuration.GetSection(nameof(JwtConfiguration)).Get<JwtConfiguration>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidAudience = jwtConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // SignalR gửi access_token qua query string chứ không phải Authorization header
                        var accessToken = context.Request.Query["access_token"];

                        // Kiểm tra nếu đường dẫn là đến Hub (tuỳ vào endpoint của bạn)
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chatHub"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    },

                    OnTokenValidated = context =>
                    {
                        // Gán ClaimTypes.NameIdentifier để SignalR biết user là ai
                        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                        var userId = claimsIdentity.FindFirst("user_id")?.Value;

                        if (!string.IsNullOrEmpty(userId))
                        {
                            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
        // Redis cache
        public static IServiceCollection AddConfigureCache(this IServiceCollection services, IConfiguration configuration) 
        {
            var redisConfiguration = new RedisConfiguration();
            configuration.GetSection("RedisConfiguration").Bind(redisConfiguration);
            services.AddSingleton(redisConfiguration);
            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConfiguration.ConnectionString));
            services.AddStackExchangeRedisCache(option => option.Configuration = redisConfiguration.ConnectionString);
            return services; 
        }

    }
}
