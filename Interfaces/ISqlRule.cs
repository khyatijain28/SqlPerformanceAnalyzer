using SqlPerformanceAnalyzer.Models;

namespace SqlPerformanceAnalyzer.Interfaces;

public interface ISqlRule
{
    Issue? Analyze(string query);
}