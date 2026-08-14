using SqlPerformanceAnalyzer.Interfaces;
using SqlPerformanceAnalyzer.Models;
using SqlPerformanceAnalyzer.Constants;
using SqlPerformanceAnalyzer.Helpers;

namespace SqlPerformanceAnalyzer.Rules;

public class OrderByWithoutTopRule : ISqlRule
{
    public Issue? Analyze(string query)
    {
        if (QueryHelper.IsEmpty(query))
            return null;

        if (!QueryHelper.Contains(query, "ORDER BY"))
            return null;

        bool hasTop = QueryHelper.Contains(query, "TOP ");
        bool hasFetch = QueryHelper.Contains(query, "FETCH");

        if (!hasTop && !hasFetch)
        {
            return new Issue
            {
                Title = "ORDER BY without TOP or FETCH",
                Severity = Severity.Warning,
                Recommendation = "Using ORDER BY without TOP or FETCH NEXT causes the database to sort the entire result set. " +
                                 "Add TOP N or use OFFSET...FETCH NEXT to limit rows and reduce sort cost."
            };
        }

        return null;
    }
}