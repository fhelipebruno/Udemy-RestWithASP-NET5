using Microsoft.EntityFrameworkCore;
using Udemy_RestWithASP_NET5.Model.Context;
using Udemy_RestWithASP_NET5.Business;
using Udemy_RestWithASP_NET5.Business.Implementations;
using Udemy_RestWithASP_NET5.Repository;
using Serilog;
using Udemy_RestWithASP_NET5.Repository.Generic;
using System.Net.Http.Headers;
using Udemy_RestWithASP_NET5.Hypermedia.Filters;
using Udemy_RestWithASP_NET5.Hypermedia.Enricher;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

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

builder.Services.AddMvc(options => {
    options.RespectBrowserAcceptHeader = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml").ToString());
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json").ToString());
}).AddXmlSerializerFormatters();

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEricherList.Add(new BookEnricher());

builder.Services.AddSingleton(filterOptions);

builder.Services.AddApiVersioning();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1",
        new OpenApiInfo {
            Title = "REST API's From 0 to Azure with ASP.NET Core 6 and Docker",
            Version = "v1",
            Description = "API RESTful devoped in course 'REST API's From 0 to Azure with ASP.NET Core 6 and Docker'",
            Contact = new OpenApiContact {
                Name = "Fhelipe Bruno",
                Url = new Uri("https://github.com/fhelipebruno")
            }
        }); 
});

//Dependency Injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseRouting();

app.UseSwagger(); 

app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json",
        "REST API's From 0 to Azure with ASP.NET Core 6 and Docker - v1");
});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");

app.UseRewriter(option);

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

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
