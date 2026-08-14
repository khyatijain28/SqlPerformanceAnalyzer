using SqlPerformanceAnalyzer.Interfaces;
using SqlPerformanceAnalyzer.Models;
using SqlPerformanceAnalyzer.Constants;
using SqlPerformanceAnalyzer.Helpers;

namespace SqlPerformanceAnalyzer.Rules;

public class NoLockHintRule : ISqlRule
{
    public Issue? Analyze(string query)
    {
        if (QueryHelper.IsEmpty(query))
            return null;

        bool hasNoLock = QueryHelper.Contains(query, "WITH (NOLOCK)") ||
                         QueryHelper.Contains(query, "WITH(NOLOCK)");

        if (hasNoLock)
        {
            return new Issue
            {
                Title = "NOLOCK hint detected",
                Severity = Severity.Warning,
                Recommendation = "WITH (NOLOCK) allows dirty reads — your query may return uncommitted, inconsistent, " +
                                 "or phantom data. Avoid it in financial or audit-sensitive queries. " +
                                 "Consider READ COMMITTED SNAPSHOT isolation level instead."
            };
        }

        return null;
    }
}