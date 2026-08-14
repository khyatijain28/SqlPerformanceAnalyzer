using SqlPerformanceAnalyzer.Interfaces;
using SqlPerformanceAnalyzer.Rules;
using SqlPerformanceAnalyzer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddScoped<ISqlRule, SelectStarRule>();
builder.Services.AddScoped<ISqlRule, MissingWhereRule>();

builder.Services.AddScoped<SqlAnalyzerService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();