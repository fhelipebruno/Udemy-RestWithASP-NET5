using Microsoft.EntityFrameworkCore;
using Udemy_RestWithASP_NET5.Model.Context;
using Udemy_RestWithASP_NET5.Services;
using Udemy_RestWithASP_NET5.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

IConfiguration Configuration = builder.Configuration;

var connection = Configuration["MySQLConnection:MySQLConnectionString"];

builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version())));

//Dependency Injection
builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
