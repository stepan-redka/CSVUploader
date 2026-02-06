# TestApp

ASP.NET Core web application for contact management with CSV import functionality.

## Architecture

Clean Architecture implementation with clear separation of concerns:

### Core Layer
Domain entities and interfaces independent of external dependencies.

- **Entities**: Domain models (Contact, ErrorViewModel)
- **Interfaces**: Repository and service contracts

### Infrastructure Layer
External dependencies and data access implementations.

- **Data**: DbContext and database configurations
- **Repositories**: Data access implementations
- **Services**: Business logic implementations

### Web Layer
Presentation logic and user interface.

- **Controllers**: HTTP request handlers
- **DTOs**: Data transfer objects with validation
- **Views**: Razor templates
- **Middleware**: Custom middleware components

## Prerequisites

- .NET 8.0 SDK
- Docker and Docker Compose (recommended)
- SQL Server 2022 (if not using Docker)

## Quick Start

### Using Docker Compose (Recommended)

```bash
docker-compose up --build
```

Application available at: `http://localhost:5000`

### Manual Setup

1. Configure database connection:
   ```bash
   cp TestApp/appsettings.Development.json.example TestApp/appsettings.Development.json
   ```
   Update connection string with your credentials.

2. Run migrations:
   ```bash
   dotnet ef database update --project TestApp/TestApp.csproj
   ```

3. Start application:
   ```bash
   dotnet run --project TestApp/TestApp.csproj
   ```

## Features

- Contact CRUD operations
- CSV file import with validation
- Asynchronous data operations
- Structured logging
- Global exception handling
- Data validation with DTOs

## Technology Stack

- ASP.NET Core 8.0 
- Entity Framework Core
- SQL Server 2022
- Docker

## Project Structure

```
TestApp/
├── Core/
│   ├── Entities/           # Domain models
│   └── Interfaces/         # Repository and service contracts
├── Infrastructure/
│   ├── Data/              # DbContext and migrations
│   ├── Repositories/      # Data access implementations
│   └── Services/          # Business logic implementations
└── Web/
    ├── Controllers/       # HTTP endpoints
    ├── DTOs/             # Data transfer objects
    ├── Middleware/       # Custom middleware
    ├── Views/            # Razor views
    └── wwwroot/          # Static files
```

## Configuration

Connection strings and logging configuration are managed through `appsettings.json`. Environment-specific settings should be placed in `appsettings.Development.json` or `appsettings.Production.json`.

For production deployments, use environment variables or secure secret management solutions.
