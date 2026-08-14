using SqlPerformanceAnalyzer.Interfaces;
using SqlPerformanceAnalyzer.Models;

namespace SqlPerformanceAnalyzer.Rules;

public class OrderByWithoutTopRule : ISqlRule
{
    public Issue? Analyze(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return null;

        var upperQuery = query.ToUpper().Trim();

        if (!upperQuery.Contains("ORDER BY"))
            return null;

        bool hasTop = upperQuery.Contains("TOP ");
        bool hasFetch = upperQuery.Contains("FETCH");

        if (!hasTop && !hasFetch)
        {
            return new Issue
            {
                Title = "ORDER BY without TOP or FETCH",
                Severity = "Warning",
                Recommendation = "Using ORDER BY without TOP or FETCH NEXT causes the database to sort the entire result set. " +
                                 "Add TOP N or use OFFSET...FETCH NEXT to limit rows and reduce sort cost."
            };
        }

        return null;
    }
}
