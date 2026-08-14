using SqlPerformanceAnalyzer.Interfaces;
using SqlPerformanceAnalyzer.Models;

namespace SqlPerformanceAnalyzer.Rules;

public class MissingWhereRule : ISqlRule
{
    public Issue? Analyze(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return null;

        var upperQuery = query.ToUpper();

        // Check if it's a SELECT query
        if (!upperQuery.StartsWith("SELECT"))
            return null;

        // Ignore COUNT queries for now
        if (upperQuery.Contains("COUNT("))
            return null;

        // If there's no WHERE clause, return an issue
        if (!upperQuery.Contains("WHERE"))
        {
            return new Issue
            {
                Title = "Missing WHERE clause",
                Severity = "High",
                Recommendation = "This query may scan the entire table. Add a WHERE clause if appropriate."
            };
        }

        return null;
    }
}