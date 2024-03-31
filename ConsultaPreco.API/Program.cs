using ConsultaPreco.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configurando o banco de dados em memória do Entity Framework Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase(databaseName: "InMemoryDatabase"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
