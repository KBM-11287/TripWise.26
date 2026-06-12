# TripWise.26

TripWise.26 is a travel planning and itinerary management web application built using a decoupled architecture with ASP.NET Core MVC, ASP.NET Core Web API, and MongoDB.

Users can create accounts, manage trips, organize activities within each trip, and build detailed travel itineraries through a modern web interface.

---

## Project Architecture

TripWise follows a layered, service-oriented architecture:

```text
┌─────────────────────────┐
│ ASP.NET Core MVC Client │
│     (Frontend UI)       │
└────────────┬────────────┘
             │ HttpClient
             ▼
┌─────────────────────────┐
│ ASP.NET Core Web API    │
│  Business Logic Layer   │
└────────────┬────────────┘
             │
             ▼
┌─────────────────────────┐
│ MongoDB Database        │
│    Data Persistence     │
└─────────────────────────┘
```

### Frontend

* ASP.NET Core MVC
* Razor Views
* Bootstrap UI
* Cookie Authentication

### Backend

* ASP.NET Core Web API
* JWT Authentication
* Repository Pattern
* Service Layer
* RESTful Endpoints

### Database

* MongoDB
* MongoDB .NET Driver

---

# Features

## User Management

### Authentication

* User Registration
* User Login
* JWT Token Generation
* Cookie-based Authentication
* Protected Routes using `[Authorize]`

---

## Trip Management

### CRUD Operations

Users can:

* Create Trips
* View Trips
* Edit Trips
* Delete Trips

Each trip contains:

* Title
* Destination
* Start Date
* End Date
* Activities

---

## Activity Management

Activities are nested within Trips.

Users can:

* Create Activities
* View Activities
* Edit Activities
* Delete Activities

Each activity contains:

* Name
* Type
* Description
* Date
* Start Time
* End Time
* Location

---

## Dashboard

Current Dashboard Features:

* View all trips
* Quick access to trip details
* Activity counts per trip
* Trip cards with itinerary summaries

---

# Technology Stack

| Layer          | Technology           |
| -------------- | -------------------- |
| Frontend       | ASP.NET Core MVC     |
| Backend        | ASP.NET Core Web API |
| Database       | MongoDB              |
| Authentication | JWT + Cookies        |
| Styling        | Bootstrap            |
| API Testing    | Swagger/OpenAPI      |
| Serialization  | Newtonsoft.Json      |
| ORM            | MongoDB .NET Driver  |

---

# Design Patterns

The project follows SOLID principles and common enterprise patterns.

## Repository Pattern

Repositories isolate database access from business logic.

Example:

```text
Controllers
    ↓
Services
    ↓
Repositories
    ↓
MongoDB
```

Repositories:

* IUserRepository
* ITripRepository
* IDestinationRepository

---

## Service Layer

Services contain business logic and validation.

Examples:

* AuthService
* TripService
* ActivityService
* DestinationService

---

# Authentication Flow

```text
User Login
     ↓
API validates credentials
     ↓
JWT Token generated
     ↓
Token returned to MVC
     ↓
Token stored in secure cookie
     ↓
MVC sends Bearer Token
     ↓
Protected API endpoints accessed
```

---

# API Endpoints

## Authentication

```http
POST /api/v1.0/auth/register
POST /api/v1.0/auth/login
```

---

## Trips

```http
GET    /api/v1.0/trips
GET    /api/v1.0/trips/{id}
POST   /api/v1.0/trips
PUT    /api/v1.0/trips/{id}
DELETE /api/v1.0/trips/{id}
```

---

## Activities

```http
POST   /api/v1.0/trips/{tripId}/activities
PUT    /api/v1.0/trips/{tripId}/activities/{activityId}
DELETE /api/v1.0/trips/{tripId}/activities/{activityId}
```

---

# Project Structure

```text
TripWise.26
│
├── TripWise.Api
│   ├── Controllers
│   ├── DTOs
│   ├── Models
│   ├── Repositories
│   ├── Services
│   ├── Helpers
│   └── Configuration
│
├── TripWise.Mvc
│   ├── Controllers
│   ├── Models
│   ├── Services
│   ├── Views
│   └── wwwroot
│
└── MongoDB
```

---

# Running the Project

## Prerequisites

Install:

* .NET 8 SDK
* MongoDB Community Server
* Visual Studio 2022
* Git

---

## Clone Repository

```bash
git clone https://github.com/KBM-11287/TripWise.26.git
```

---

## Configure MongoDB

Update:

```json
appsettings.json
```

Example:

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "TripWiseDb"
  }
}
```

---

## Configure JWT

Update:

```json
{
  "Jwt": {
    "Secret": "YourSuperSecureSecretKey",
    "Issuer": "TripWiseApi"
  }
}
```

---

## Run Solution

Configure Visual Studio:

```text
Multiple Startup Projects

✓ TripWise.Api
✓ TripWise.Mvc
```

Run:

```text
F5
```

API:

```text
https://localhost:7038/swagger
```

MVC:

```text
https://localhost:7159
```

---

# Future Enhancements

## Destination Integration

Planned:

* Destination Search
* Destination Database
* Add Destination to Trip
* Destination Details

---

## Maps Integration

Planned:

* Google Maps
* OpenStreetMap
* Interactive Destination Locations
* Coordinate Storage

---

## Calendar View

Stretch Goal:

* Monthly Calendar
* Weekly Itinerary View
* Timeline Scheduling
* FullCalendar.js Integration

---

# Current Status

### Completed

* User Authentication
* JWT Security
* Trip CRUD
* Activity CRUD
* MongoDB Integration
* Repository Pattern
* Swagger Testing
* MVC/API Communication

### In Progress

* Dashboard Enhancements
* Destination Integration

### Planned

* Maps
* Calendar View

---

# Author

**Kabo Bene Masimege**

TripWise.26 was developed as a travel management platform demonstrating modern ASP.NET Core development practices, RESTful API design, MongoDB integration, and secure authentication using JWT.
