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
using Udemy_RestWithASP_NET5.Services.Implementations;
using Udemy_RestWithASP_NET5.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Udemy_RestWithASP_NET5.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration = builder.Configuration;
IWebHostEnvironment Environment = builder.Environment;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var tokenConfigurations = new TokenConfiguration();
new ConfigureFromConfigurationOptions<TokenConfiguration>(
    Configuration.GetSection("TokenConfigurations")
    ).Configure(tokenConfigurations);

builder.Services.AddSingleton(tokenConfigurations);

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenConfigurations.Issuer,
            ValidAudience = tokenConfigurations.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
        };
    });

builder.Services.AddAuthorization(auth => {
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => {
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

// Add services to the container.
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

var connection = Configuration["MySQLConnection:MySQLConnectionString"];

builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version())));

//if (Environment.IsDevelopment()) {
//    MigrateDatabase(connection);
//}

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
            Title = "REST API's From 0 to Azure with ASP.NET Core 6 and Docker - Foo Bar",
            Version = "v1",
            Description = "API RESTful devoped in course 'REST API's From 0 to Azure with ASP.NET Core 6 and Docker'",
            Contact = new OpenApiContact {
                Name = "Fhelipe Bruno",
                Url = new Uri("https://github.com/fhelipebruno")
            }
        }); 
});

//Dependency Injection

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
builder.Services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();
builder.Services.AddScoped<IFileBusiness, FileBusinessImplementation>();

builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseSwagger(); 

app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json",
        "REST API's From 0 to Azure with ASP.NET Core 6 and Docker - v1");
});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

});

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
