using Infrastructure.Jwt;
using Infrastructure.DependencyInjection.Extentions;

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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxyAuth"))
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxyChat"))
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxyPost"));

// config jwt
builder.Services.AddAuthenticationJWT(builder.Configuration);

// AuthenticatedOnly
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AuthenticatedOnly", policy =>
        policy.RequireAuthenticatedUser());
});



var app = builder.Build();
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();
app.Run();
