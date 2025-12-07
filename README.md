# README - GoodHamburger API v1.0.0

A clean architecture .NET 8.0 web API for managing hamburger orders with a dual data access strategy using Entity Framework Core and Dapper.

## Architecture

The solution follows Clean Architecture principles with four main projects organized in layers: [2](#10-1) 

```
src/
├── 1- DistributedServices/
│   └── GoodHamburger/           # Web API layer - Controllers and endpoints
├── 2- Core/
│   ├── CoreLibrary/             # Business logic - Services and DTOs
│   └── Domain/                  # Entities and queries - Core domain objects
└── 3- Infrastructure/
    └── infrastructure/          # Data access - EF Core and repositories
```

### Data Access Strategy

- **Write Operations**: Entity Framework Core 9.0.0 with Repository and Unit of Work patterns for transactional integrity 
- **Read Operations**: Dapper 2.1.66 for optimized queries without change tracking overhead [4](#10-3) 

## Technology Stack

| Technology | Version | Purpose | Project |
|------------|---------|---------|---------|
| **.NET** | 8.0 | Runtime framework | All projects [5](#10-4)  |
| **ASP.NET Core** | 8.0 | Web framework with dependency injection | GoodHamburger [6](#10-5)  |
| **Entity Framework Core** | 9.0.0 | ORM for write operations | infrastructure [3](#10-2)  |
| **Dapper** | 2.1.66 | Micro-ORM for read operations | Domain [4](#10-3)  |
| **PostgreSQL** | - | Database | - [7](#10-6)  |
| **Npgsql** | 9.0.1 | PostgreSQL driver | infrastructure, Domain [7](#10-6)  |
| **AutoMapper** | 14.0.0 | Object-to-object mapping | CoreLibrary [8](#10-7)  |
| **Swashbuckle.AspNetCore** | 6.6.2 | API documentation and Swagger UI | GoodHamburger [9](#10-8)  |

## Running the Project with Docker Compose

### Prerequisites

- Docker Desktop installed and running
- Git

### Docker Setup

1. Clone the repository:
```bash
git clone https://github.com/Danteocean/STgenetics-Colombia.git
cd STgenetics-Colombia
```

2. Run with Docker Compose:
```bash
docker-compose up --build
```

The application will be available at:
- **Swagger UI**: `http://localhost:5000/swagger/index.html`
[You can also use the collection located at:](GoodHamburger_API.postman_collection.json)
### Application Startup

The application configures services in `Program.cs`:
- Core layer services (business logic)
- Infrastructure layer (DbContext and repositories)
- Controllers for API endpoints
- Swagger for API documentation

## API Endpoints

The application exposes two main controllers:

### MenuController (`/Menu`)
- `GET /Menu/GetMenu` - Retrieve complete menu
- `GET /Menu/GetSandwiches` - Get sandwich options
- `GET /Menu/GetExtras` - Get extra ingredients

### OrderController (`/Order`)
- `POST /Order/InsertOrder` - Create new order
- `PUT /Order/UpdateOrder` - Update existing order
- `DELETE /Order/DeleteOrder` - Remove order
- `GET /Order/GetOrders` - List all orders

## Notes

Current version: 1.0.0 (based on assembly versions)

The application uses a dual data access approach where Entity Framework Core handles write operations with change tracking and transaction management, while Dapper provides optimized read-only queries for better performance. 

The database connection string is configured to use "DefaultConnection" and is read from the application's Configuration.

Wiki pages you might want to explore:
- [Data Access Layer (Danteocean/STgenetics-Colombia)](https://deepwiki.com/Danteocean/STgenetics-Colombia/1-overview)