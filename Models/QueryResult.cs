namespace SqlPerformanceAnalyzer.Models;

public class QueryResult
{
    public int Score { get; set; }

    public List<Issue> Issues { get; set; } = new();
}