using SqlPerformanceAnalyzer.Interfaces;
using SqlPerformanceAnalyzer.Models;

namespace SqlPerformanceAnalyzer.Services;

public class SqlAnalyzerService
{
    private readonly IEnumerable<ISqlRule> _rules;

    public SqlAnalyzerService(IEnumerable<ISqlRule> rules)
    {
        _rules = rules;
    }

    public QueryResult Analyze(string query)
    {
        var result = new QueryResult
        {
            Score = 100
        };

        foreach (var rule in _rules)
        {
            var issue = rule.Analyze(query);

            if (issue != null)
            {
                result.Issues.Add(issue);
                result.Score -= 10;
            }
        }

        if (result.Score < 0)
            result.Score = 0;

        return result;
    }
}