# Sensitive Words Service

A microservice for managing and sanitizing sensitive words in text. This service provides functionality to filter out sensitive words (such as SQL keywords) from text and replace them with asterisks.

## Features

- RESTful API for managing sensitive words (CRUD operations)
- Text sanitization endpoint for filtering sensitive words
- Swagger documentation
- SQL Server database integration using Dapper
- In-memory caching for improved performance
- Comprehensive unit tests
- Case-insensitive word matching
- Input validation and error handling

## Prerequisites

- .NET 8.0 SDK
- SQL Server (2019 or later)
- Visual Studio 2022 or later (recommended)

## Getting Started

1. Clone the repository
2. Create the database using the script in `SensitiveWordsService.Infrastructure/Scripts/CreateDatabase.sql`
3. Update the connection string in `appsettings.json`
4. Run the application

## API Endpoints

### Sensitive Words Management

- `GET /api/SensitiveWords` - Get all sensitive words
- `GET /api/SensitiveWords/{id}` - Get a specific sensitive word
- `POST /api/SensitiveWords` - Create a new sensitive word
- `PUT /api/SensitiveWords/{id}` - Update an existing sensitive word
- `DELETE /api/SensitiveWords/{id}` - Delete a sensitive word

### PowerShell Examples

Here are examples of how to interact with the API using PowerShell:

1. Get all sensitive words:
```powershell
Invoke-RestMethod -Uri "https://localhost:7240/api/v1/SensitiveWords" -Method Get -ContentType "application/json" -SkipCertificateCheck
```

2. Create a new sensitive word:
```powershell
$body = @{ 
    word = "TESTWORD2"
    isActive = $true 
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:7240/api/v1/SensitiveWords" -Method Post -Body $body -ContentType "application/json" -SkipCertificateCheck
```

3. Update a sensitive word:
```powershell
$body = @{ 
    id = 231
    word = "UPDATEDTESTWORD2"
    isActive = $true 
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:7240/api/v1/SensitiveWords/231" -Method Put -Body $body -ContentType "application/json" -SkipCertificateCheck
```

4. Get a specific sensitive word:
```powershell
Invoke-RestMethod -Uri "https://localhost:7240/api/v1/SensitiveWords/231" -Method Get -ContentType "application/json" -SkipCertificateCheck
```

5. Sanitize text:
```powershell
$body = @{ 
    text = "SELECT * FROM users WHERE id = 1" 
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:7240/api/v1/Sanitize" -Method Post -Body $body -ContentType "application/json" -SkipCertificateCheck
```

Note: The `-SkipCertificateCheck` parameter is used for development environments. In production, proper SSL certificates should be used.

### Text Sanitization

- `POST /api/Sanitize` - Sanitize text by replacing sensitive words with asterisks

#### Example Requests and Responses:

1. Basic SQL Query:
```json
// Request
{
    "text": "SELECT * FROM users WHERE id = 1"
}

// Response
{
    "originalText": "SELECT * FROM users WHERE id = 1",
    "sanitizedText": "****** * **** users ***** id = 1"
}
```

2. SQL Injection Attempt:
```json
// Request
{
    "text": "DROP TABLE customers; DELETE FROM orders"
}

// Response
{
    "originalText": "DROP TABLE customers; DELETE FROM orders",
    "sanitizedText": "**** ***** customers; ****** FROM orders"
}
```

The service will:
- Detect and mask SQL keywords and potentially dangerous terms
- Preserve the original text structure
- Return both the original and sanitized versions
- Handle multiple sensitive words in a single text
- Perform case-insensitive matching

## Implementation Details

### Data Model
The service uses the following data model for sensitive words:
```csharp
public class SensitiveWord
{
    public int Id { get; set; }
    public string Word { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
```

### Database Schema
The SQL Server database includes:
- Primary key on Id
- Unique constraint on Word
- Audit fields (CreatedAt, UpdatedAt)
- IsActive flag for soft deletion

### Performance Optimizations

1. **In-Memory Caching**
   - Active sensitive words are cached in memory
   - Cache is refreshed every 5 minutes
   - Reduces database load for frequent sanitization requests
   - Thread-safe implementation

2. **Efficient Database Operations**
   - Uses Dapper for fast and efficient database access
   - Optimized SQL queries with proper indexing
   - Connection pooling for better resource utilization
   - Parameterized queries for security

3. **Asynchronous Operations**
   - All database and service operations are async
   - Improves scalability and responsiveness
   - Proper error handling and logging

### Additional Features

1. **Input Validation**
   - Required field validation
   - String length constraints
   - Null handling
   - Case-insensitive matching

2. **Error Handling**
   - Proper HTTP status codes
   - Detailed error messages
   - Logging of errors and exceptions

3. **API Documentation**
   - Swagger UI integration
   - Detailed endpoint documentation
   - Request/response examples
   - Response type specifications

## Additional Enhancements

1. **Security**
   - API authentication and authorization
   - Rate limiting
   - Input validation and sanitization
   - HTTPS enforcement

2. **Monitoring and Logging**
   - Application insights integration
   - Structured logging
   - Health checks
   - Performance metrics

3. **Deployment**
   - Docker containerization
   - Kubernetes orchestration
   - CI/CD pipeline
   - Blue-green deployment strategy

4. **Testing**
   - Unit tests with xUnit
   - Integration tests
   - Load testing
   - API documentation tests

## Production Deployment

1. **Infrastructure**
   - Deploy to Azure Kubernetes Service (AKS)
   - Use Azure SQL Database
   - Implement Azure Application Insights
   - Set up Azure Key Vault for secrets

2. **High Availability**
   - Multiple replicas
   - Load balancing
   - Database failover
   - Geographic distribution

3. **Monitoring**
   - Application performance monitoring
   - Error tracking
   - Usage analytics
   - Cost optimization

4. **Security**
   - Network security groups
   - SSL/TLS encryption
   - Regular security audits
   - Compliance monitoring

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details. 