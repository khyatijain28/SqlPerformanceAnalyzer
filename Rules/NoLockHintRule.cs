using SqlPerformanceAnalyzer.Interfaces;
using SqlPerformanceAnalyzer.Models;

namespace SqlPerformanceAnalyzer.Rules;

public class NoLockHintRule : ISqlRule
{
    public Issue? Analyze(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return null;

        var upperQuery = query.ToUpper().Trim();

        bool hasNoLock = upperQuery.Contains("WITH (NOLOCK)") || upperQuery.Contains("WITH(NOLOCK)");

        if (hasNoLock)
        {
            return new Issue
            {
                Title = "NOLOCK hint detected",
                Severity = "Warning",
                Recommendation = "WITH (NOLOCK) allows dirty reads — your query may return uncommitted, inconsistent, " +
                                 "or phantom data. Avoid it in financial or audit-sensitive queries. " +
                                 "Consider READ COMMITTED SNAPSHOT isolation level instead."
            };
        }

        return null;
    }
}
