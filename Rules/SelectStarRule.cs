using SqlPerformanceAnalyzer.Interfaces;
using SqlPerformanceAnalyzer.Models;
using SqlPerformanceAnalyzer.Constants;
using SqlPerformanceAnalyzer.Helpers;

namespace SqlPerformanceAnalyzer.Rules;

public class SelectStarRule : ISqlRule
{
    public Issue? Analyze(string query)
    {
        if (QueryHelper.IsEmpty(query))
            return null;

        if (query.Contains("SELECT *", StringComparison.OrdinalIgnoreCase))
        {
            return new Issue
            {
                Title = "Avoid SELECT *",
                Severity = Severity.Warning,
                Recommendation = "Specify only the required columns instead of using SELECT *."
            };
        }

        return null;
    }
}