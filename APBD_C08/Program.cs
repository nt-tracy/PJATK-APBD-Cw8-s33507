using APBD_C08.Context;
using APBD_C08.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<MyDbContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddScoped<IPatientsService, PatientsService>();


// Dodawanie kontrolerów itp.
builder.Services.AddControllers();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/openapi/v1.json", "Zadanie8"));
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();