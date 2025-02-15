var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxyAuth"))
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxyChat"))
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxyPost"));


var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapReverseProxy();
app.Run();
