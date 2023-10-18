using Microsoft.AspNetCore.Mvc;
using TheRide.API.Infrastructure;
using TheRide.API.Interfaces;
using TheRide.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning(o =>
{
    o.ReportApiVersions = true;
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
});

// Register settings.
builder.Services.AddCarsStoreSettings(builder.Configuration);
builder.Services.AddModelsStoreSettings(builder.Configuration);

// Register interfaces.
builder.Services.AddSingleton<ICarsAccessor, CarsAccessor>();
builder.Services.AddSingleton<IModelsAccessor, ModelsAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();