using CounterOnionLight.API.DTOs;
using CounterOnionLight.Core.ApplicationServices;
using CounterOnionLight.Core.DomainServices;
using CounterOnionLight.Infrastructure;
using CounterOnionLight.Infrastructure.Infrastructure.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<CounterService>();
builder.Services.AddScoped<ICounterRepository, CounterEntityFrameworkDbFirstRepository>();
builder.Services.AddDbContext<CounterDbContext>();
var app = builder.Build();
app.UseHttpsRedirection();

app.MapPost("/counters/{id}/increment", async (
    int id,
    IncrementRequest request,
    CounterService service) =>
{
    try
    {
        var value = await service.IncrementAsync(id, request.Who);

        return Results.Ok(new { value });
    }
    catch (Exception)
    {
        return Results.Conflict("Counter was updated by someone else");
    }
});


app.Run();
