using SqlPerformanceAnalyzer.Interfaces;
using SqlPerformanceAnalyzer.Models;
using SqlPerformanceAnalyzer.Constants;
using SqlPerformanceAnalyzer.Helpers;

namespace SqlPerformanceAnalyzer.Rules;

public class MissingWhereRule : ISqlRule
{
    public Issue? Analyze(string query)
    {
        if (QueryHelper.IsEmpty(query))
            return null;

        if (!QueryHelper.IsSelectQuery(query))
            return null;

        if (QueryHelper.Contains(query, "COUNT("))
            return null;

        if (!QueryHelper.Contains(query, "WHERE"))
        {
            return new Issue
            {
                Title = "Missing WHERE clause",
                Severity = Severity.High,
                Recommendation = "This query may scan the entire table. Add a WHERE clause if appropriate."
            };
        }

        return null;
    }
}