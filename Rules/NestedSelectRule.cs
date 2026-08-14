using SqlPerformanceAnalyzer.Interfaces;
using SqlPerformanceAnalyzer.Models;
using SqlPerformanceAnalyzer.Constants;
using SqlPerformanceAnalyzer.Helpers;

namespace SqlPerformanceAnalyzer.Rules;

public class NestedSelectRule : ISqlRule
{
    public Issue? Analyze(string query)
    {
        if (QueryHelper.IsEmpty(query))
            return null;

        if (!QueryHelper.IsSelectQuery(query))
            return null;

        int fromIndex = QueryHelper.Normalize(query).IndexOf(" FROM ");
        if (fromIndex == -1)
            return null;

        string selectClause = QueryHelper.Normalize(query).Substring(0, fromIndex);

        if (selectClause.IndexOf("SELECT", 6) != -1)
        {
            return new Issue
            {
                Title = "Nested SELECT in column list",
                Severity = Severity.High,
                Recommendation = "Scalar subqueries in the SELECT list execute once per row and can severely hurt performance. " +
                                 "Replace with a JOIN or a CTE for better execution plan efficiency."
            };
        }

        return null;
    }
}