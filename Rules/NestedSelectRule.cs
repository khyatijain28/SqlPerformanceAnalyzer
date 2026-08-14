using SqlPerformanceAnalyzer.Interfaces;
using SqlPerformanceAnalyzer.Models;

namespace SqlPerformanceAnalyzer.Rules;

public class NestedSelectRule : ISqlRule
{
    public Issue? Analyze(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return null;

        var upperQuery = query.ToUpper().Trim();

        if (!upperQuery.StartsWith("SELECT"))
            return null;

        // Find the SELECT column list — everything between SELECT and FROM
        int fromIndex = upperQuery.IndexOf(" FROM ");
        if (fromIndex == -1)
            return null;

        string selectClause = upperQuery.Substring(0, fromIndex);

        // A nested SELECT inside the column list will contain another SELECT keyword
        // after the first one (i.e., after position 6)
        if (selectClause.IndexOf("SELECT", 6) != -1)
        {
            return new Issue
            {
                Title = "Nested SELECT in column list",
                Severity = "High",
                Recommendation = "Scalar subqueries in the SELECT list execute once per row and can severely hurt performance. " +
                                 "Replace with a JOIN or a CTE for better execution plan efficiency."
            };
        }

        return null;
    }
}
