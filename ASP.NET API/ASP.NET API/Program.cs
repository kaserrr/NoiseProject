using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ASP.NET_API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ContextModels>(opt =>
    opt.UseInMemoryDatabase("SensorInfo"));
builder.Services.AddDbContext<ContextUplinkData>(opt =>
    opt.UseInMemoryDatabase("SensorInfo"));

// Add Swagger for API documentation (if needed).
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Add the UplinkController to the request pipeline.
app.MapControllers();

app.Run();
