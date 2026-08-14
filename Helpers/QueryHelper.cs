namespace SqlPerformanceAnalyzer.Helpers;

public static class QueryHelper
{
    /// <summary>
    /// Returns true if the query is null, empty, or whitespace.
    /// </summary>
    public static bool IsEmpty(string query) => string.IsNullOrWhiteSpace(query);

    /// <summary>
    /// Trims and uppercases the query for consistent rule matching.
    /// </summary>
    public static string Normalize(string query) => query.Trim().ToUpper();

    /// <summary>
    /// Returns true if the query is a SELECT statement.
    /// </summary>
    public static bool IsSelectQuery(string query) => Normalize(query).StartsWith("SELECT");

    /// <summary>
    /// Returns true if the query contains the given keyword (case-insensitive).
    /// </summary>
    public static bool Contains(string query, string keyword) =>
        Normalize(query).Contains(keyword.ToUpper());
}