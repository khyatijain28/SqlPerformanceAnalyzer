# SQL Performance Analyzer

A lightweight **ASP.NET Core Web API** that analyzes SQL queries for common performance anti-patterns and returns a scored report with actionable recommendations.

Built with a **pluggable rule engine** — each rule is an independent class implementing `ISqlRule`, making it easy to add new checks without modifying existing logic.

---

## Features

- Analyzes any raw SQL `SELECT` query via REST API
- Returns a **performance score (0–100)** with detected issues
- Each issue includes a **severity level** and **recommendation**
- Clean architecture: Interface → Service → Rules (Open/Closed Principle)
- Swagger UI for easy testing

---

## Rules Implemented

| Rule | Severity | Description |
|---|---|---|
| `SelectStarRule` | Warning | Flags `SELECT *` — recommends specifying columns explicitly |
| `MissingWhereRule` | High | Flags full-table SELECT queries with no WHERE clause |

---

## Tech Stack

- **C# / .NET** (ASP.NET Core Web API)
- **Swagger / Swashbuckle** for API documentation
- **Dependency Injection** for pluggable rule registration
- **OOP / SOLID principles** — rules implement `ISqlRule` interface

---

## Project Structure

```
SqlPerformanceAnalyzer/
├── Controllers/
│   └── AnalyzerController.cs     # POST /api/analyzer
├── Services/
│   └── SqlAnalyzerService.cs     # Runs all rules, computes score
├── Interfaces/
│   └── ISqlRule.cs               # Contract for all rules
├── Rules/
│   ├── SelectStarRule.cs         # Detects SELECT *
│   └── MissingWhereRule.cs       # Detects missing WHERE clause
├── Models/
│   ├── QueryRequest.cs           # Input model
│   ├── QueryResult.cs            # Output model (score + issues)
│   └── Issue.cs                  # Issue detail (title, severity, recommendation)
└── Program.cs                    # DI registration + app setup
```

---

## Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (v10.0 or later)

### Run Locally

```bash
git clone https://github.com/khyatijain28/SqlPerformanceAnalyzer.git
cd SqlPerformanceAnalyzer
dotnet run
```

Swagger UI will be available at:
```
https://localhost:{port}/swagger
```

---

## API Usage

### `POST /api/analyzer`

**Request Body:**
```json
{
  "query": "SELECT * FROM Vendors"
}
```

**Response:**
```json
{
  "score": 80,
  "issues": [
    {
      "title": "Avoid SELECT *",
      "severity": "Warning",
      "recommendation": "Specify only the required columns instead of using SELECT *."
    }
  ]
}
```

**Clean query example:**
```json
{
  "query": "SELECT VendorId, VendorName FROM Vendors WHERE IsActive = 1"
}
```
```json
{
  "score": 100,
  "issues": []
}
```

---

## Adding a New Rule

1. Create a new class in `/Rules/` implementing `ISqlRule`
2. Register it in `Program.cs`

```csharp
// Rules/YourNewRule.cs
public class YourNewRule : ISqlRule
{
    public Issue? Analyze(string query)
    {
        // your logic here
        return null;
    }
}

// Program.cs
builder.Services.AddScoped<ISqlRule, YourNewRule>();
```

That's it — the service picks it up automatically.

---

## Author

**Khyati Jain** — .NET Developer | C# | ASP.NET MVC | SQL Server  
[LinkedIn](https://www.linkedin.com/in/khyatijain28) · [GitHub](https://github.com/khyatijain28)
