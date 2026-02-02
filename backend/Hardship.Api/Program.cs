using Hardship.Api.Data;
using Hardship.Api.Data.Repositories;
using Hardship.Api.Middleware;
using Dapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<HardshipApplicationValidator>();
builder.Services.AddFluentValidationAutoValidation();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Hardship Application API",
        Version = "v1"
    });
});

// Infrastructure
builder.Services.AddSingleton<DbConnectionFactory>();
builder.Services.AddSingleton<HardshipApplicationRepository>();

var app = builder.Build();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<DbConnectionFactory>();
    using var connection = factory.CreateConnection();
    connection.Open();

    var sql = File.ReadAllText("Data/Schema.sql");
    connection.Execute(sql);
}

// Global exception middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Swagger (Development only)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hardship Application API V1");
    });
}

// Pipeline
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
