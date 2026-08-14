using SqlPerformanceAnalyzer.Interfaces;
using SqlPerformanceAnalyzer.Rules;
using SqlPerformanceAnalyzer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register rules
builder.Services.AddScoped<ISqlRule, SelectStarRule>();
builder.Services.AddScoped<ISqlRule, MissingWhereRule>();
builder.Services.AddScoped<ISqlRule, OrderByWithoutTopRule>();
builder.Services.AddScoped<ISqlRule, NestedSelectRule>();
builder.Services.AddScoped<ISqlRule, NoLockHintRule>();
builder.Services.AddScoped<ISqlRule, ImplicitConversionRule>();

builder.Services.AddScoped<SqlAnalyzerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();