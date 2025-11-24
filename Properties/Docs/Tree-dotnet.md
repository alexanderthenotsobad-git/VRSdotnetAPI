# VRSAPI Project Structure

## Overview
VRSAPI (Virtual Restaurant Solutions API) - .NET 8 Web API with Entity Framework Core and MySQL integration.

## Directory Structure

```
VRSAPI/
├── Controllers/
│   └── MenuController.cs           # API controllers for HTTP endpoints
├── DTOs/
│   └── MenuItemDto.cs              # Data Transfer Objects for API responses
├── Data/
│   └── VRSDbContext.cs             # Entity Framework database context
├── Models/
│   ├── MenuItem.cs                 # Menu item entity model
│   └── MenuItemImage.cs            # Menu item image entity model
├── Properties/
│   └── launchSettings.json         # Development server configuration
├── Program.cs                      # Application entry point & configuration
├── README.md                       # Project documentation
├── VRSAPI.csproj                   # Project file with dependencies
├── VRSAPI.http                     # HTTP request testing file
├── appsettings.Development.json    # Development environment settings
├── appsettings.json                # Production environment settings
└── wsl-tls-fix.sh                  # WSL TLS certificate fix script
```

## Key Directories & Files

### `/Controllers` - API Controllers
- **MenuController.cs** - Handles HTTP requests for menu operations
- Implements RESTful endpoints for CRUD operations
- Returns JSON responses using DTOs

### `/DTOs` - Data Transfer Objects
- **MenuItemDto.cs** - Shapes data for API responses
- Provides clean separation between internal models and API contracts
- Handles data transformation for frontend consumption

### `/Data` - Database Layer
- **VRSDbContext.cs** - Entity Framework Core database context
- Manages database connections and entity relationships
- Handles MySQL database operations via Pomelo provider

### `/Models` - Entity Models
- **MenuItem.cs** - Represents menu_items table structure
- **MenuItemImage.cs** - Represents menu_item_images table structure
- Maps to existing MySQL database schema with snake_case columns

### `/Properties` - Development Configuration
- **launchSettings.json** - Defines development server settings
- Configures ports, environment variables, and debugging options

## Configuration Files

### Application Configuration
- **Program.cs** - Main application entry point
  - Configures Entity Framework with MySQL
  - Sets up CORS for frontend integration
  - Configures Swagger/OpenAPI documentation
  - Registers dependency injection services

### Environment Settings
- **appsettings.json** - Production configuration
  - Database connection strings (direct connection)
  - Kestrel server configuration (port 3002)
  - Logging levels and allowed hosts

- **appsettings.Development.json** - Development configuration
  - SSH tunnel database connection (localhost:3305)
  - Enhanced logging for debugging
  - Development-specific settings

### Project Configuration
- **VRSAPI.csproj** - .NET project file
  - NuGet package dependencies
  - Target framework (.NET 8)
  - Build and runtime settings

## Development Tools

### Testing & Debugging
- **VRSAPI.http** - HTTP request collection for testing API endpoints
- **wsl-tls-fix.sh** - Custom script to resolve WSL TLS certificate issues

## Architecture Notes

**Technology Stack:**
- **Framework**: .NET 8 Web API
- **ORM**: Entity Framework Core 8.0
- **Database**: MySQL with Pomelo provider
- **Documentation**: Swagger/OpenAPI
- **Development**: WSL Ubuntu with VS Code

**Project Patterns:**
- **Clean Architecture** - Separation of concerns
- **Repository Pattern** - Data access abstraction via EF Core
- **DTO Pattern** - API response data shaping
- **Dependency Injection** - Service registration and management

## Database Integration

**Connection Details:**
- **Development**: SSH tunnel to `localhost:3305`
- **Production**: Direct connection to `208.113.129.7:3305`
- **Database**: `nvrs_dotnet`
- **Provider**: Pomelo.EntityFrameworkCore.MySql

**Entity Mapping:**
- Snake_case database columns mapped to PascalCase C# properties
- Foreign key relationships between MenuItem and MenuItemImage
- Automatic change tracking via Entity Framework

## API Endpoints

**Planned Implementation:**
- `GET /` - Retrieve all menu items
- `POST /createMenuItem` - Create new menu item
- `PUT /menu/{id}` - Update existing menu item
- `DELETE /menu/{id}` - Remove menu item
- Image management endpoints for upload/retrieval

**Frontend Integration:**
- **Current Node.js API**: `https://api.alexanderthenotsobad.us`
- **New .NET API**: `http://localhost:3002` (development)
- **Next.js Frontend**: Seamless drop-in replacement

**Total Files:** 13 files across 6 directories