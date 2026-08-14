using SqlPerformanceAnalyzer.Interfaces;
using SqlPerformanceAnalyzer.Models;
using System.Text.RegularExpressions;

namespace SqlPerformanceAnalyzer.Rules;

public class ImplicitConversionRule : ISqlRule
{
    // Matches patterns like: WHERE SomeColumn = 123  or  AND SomeCol = 456
    // Flags cases where a named column is compared to a plain numeric literal (no quotes)
    private static readonly Regex ImplicitConversionPattern = new(
        @"\b(WHERE|AND|OR)\s+\w+\s*=\s*\d+",
        RegexOptions.IgnoreCase | RegexOptions.Compiled
    );

    public Issue? Analyze(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return null;

        var upperQuery = query.ToUpper().Trim();

        if (!upperQuery.StartsWith("SELECT"))
            return null;

        if (ImplicitConversionPattern.IsMatch(query))
        {
            return new Issue
            {
                Title = "Possible implicit type conversion",
                Severity = "High",
                Recommendation = "Comparing a column to a numeric literal (e.g. WHERE VendorCode = 101) can cause implicit " +
                                 "conversion if the column type is VARCHAR or NVARCHAR. This forces SQL Server to convert " +
                                 "every row value, making indexes unusable. Use quoted string literals instead: WHERE VendorCode = '101'."
            };
        }

        return null;
    }
}
