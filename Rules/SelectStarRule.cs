using SqlPerformanceAnalyzer.Interfaces;
using SqlPerformanceAnalyzer.Models;

namespace SqlPerformanceAnalyzer.Rules;

public class SelectStarRule : ISqlRule
{
    public Issue? Analyze(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return null;

        if (query.Contains("SELECT *", StringComparison.OrdinalIgnoreCase))
        {
            return new Issue
            {
                Title = "Avoid SELECT *",
                Severity = "Warning",
                Recommendation = "Specify only the required columns instead of using SELECT *."
            };
        }

        return null;
    }
}