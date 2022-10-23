using Microsoft.EntityFrameworkCore;
using Udemy_RestWithASP_NET5.Model.Context;
using Udemy_RestWithASP_NET5.Business;
using Udemy_RestWithASP_NET5.Business.Implementations;
using Udemy_RestWithASP_NET5.Repository.Implementations;
using Udemy_RestWithASP_NET5.Repository;
using Serilog;
using Udemy_RestWithASP_NET5.Repository.Generic;

var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// Add services to the container.
builder.Services.AddControllers();

var connection = Configuration["MySQLConnection:MySQLConnectionString"];

builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version())));

if (Environment.IsDevelopment()) {
    MigrateDatabase(connection);
}

builder.Services.AddApiVersioning();

//Dependency Injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//Implementação de migrations
void MigrateDatabase(string connection) {
    try {
        var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
        var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg)) {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true,
        };
        evolve.Migrate();
    }
    catch (Exception ex) {
        Log.Error("Databse migration failed", ex);
        throw;
    }
}
