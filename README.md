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

## Future Enhancements

1. **API Security**
   - API key authentication for external endpoints
   - JWT token-based authentication
   - Role-based access control (RBAC)
   - API key rotation and management
   - Rate limiting
   - HTTPS enforcement

2. **Audit and Logging**
   - Request/response logging for audit purposes
   - Structured logging with correlation IDs
   - Audit trail for sensitive word changes
   - Log aggregation and analysis
   - Application insights integration

3. **Bulk Operations**
   - Bulk import/export of sensitive words
   - Batch processing for text sanitization
   - Bulk update/delete operations
   - Template-based word list management

4. **Enhanced Word Management**
   - Word categorization (SQL, profanity, custom, etc.)
   - Custom replacement patterns (asterisk, custom character, etc.)
   - Word patterns and regex support
   - Word relationships and hierarchies

5. **Performance Improvements**
   - Distributed caching
   - Redis integration
   - Query optimization
   - Load balancing improvements

6. **Monitoring and Analytics**
   - Usage statistics and analytics
   - Performance metrics dashboard
   - Alerting and notifications
   - Cost tracking and optimization

7. **Integration Features**
   - Webhook support for word updates
   - Event-driven architecture
   - Message queue integration
   - Third-party service integrations

8. **Developer Experience**
   - Enhanced API documentation
   - SDK development
   - Code samples and tutorials
   - Development environment improvements

9. **Production Deployment**
   - Docker containerization
   - Kubernetes orchestration
   - CI/CD pipeline
   - Blue-green deployment strategy
   - Azure Kubernetes Service (AKS) deployment
   - Azure SQL Database integration
   - Azure Application Insights implementation
   - Azure Key Vault for secrets
   - High availability setup
   - Geographic distribution
   - Network security groups
   - Regular security audits
   - Compliance monitoring

10. **Testing Enhancements**
    - Integration tests
    - Load testing
    - API documentation tests
    - Performance testing
    - Security testing

11. **Performance Optimization**
    - **Caching Improvements**
      - Multi-level caching strategy (L1, L2, L3)
      - Distributed caching with Redis cluster
      - Cache warming strategies
      - Cache invalidation optimization
      - Cache size monitoring and auto-scaling

    - **Database Optimization**
      - Query performance tuning
      - Index optimization
      - Partitioning for large datasets
      - Read replicas for read-heavy workloads
      - Connection pool optimization
      - Query result caching
      - Batch processing for bulk operations

    - **Memory Management**
      - Memory pooling for string operations
      - Object pooling for frequently created objects
      - Memory usage monitoring
      - Garbage collection optimization
      - Memory leak detection and prevention

    - **Concurrency and Parallelism**
      - Parallel processing for text sanitization
      - Concurrent dictionary for thread-safe operations
      - Async/await optimization
      - Task scheduling improvements
      - Thread pool tuning

    - **Network Optimization**
      - HTTP/2 support
      - Response compression
      - Connection keep-alive
      - Network latency optimization
      - Load balancing improvements
      - CDN integration for global distribution

    - **Resource Utilization**
      - CPU usage optimization
      - I/O operations optimization
      - Disk I/O optimization
      - Network bandwidth optimization
      - Resource monitoring and auto-scaling

    - **Application Architecture**
      - Microservices optimization
      - Service mesh implementation
      - Circuit breaker pattern
      - Bulkhead pattern
      - Retry policies
      - Timeout handling

    - **Monitoring and Profiling**
      - Performance metrics collection
      - Real-time performance monitoring
      - Bottleneck detection
      - Resource usage tracking
      - Performance regression testing
      - A/B testing for optimizations

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details. 