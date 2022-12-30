using ManagementApi;
using ManagementApi.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.SetupSwagger();

var auth0ConfigurationSection = builder.Configuration.GetSection("Auth0");
builder.Services.SetupAuthentication(auth0ConfigurationSection.Get<Auth0Configuration>());

builder.Services.Configure<Auth0Configuration>(auth0ConfigurationSection);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers().RequireAuthorization());

app.Run();
