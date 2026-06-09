# MiniERP

MiniERP is a lightweight Enterprise Resource Planning (ERP) application built with ASP.NET Core and PostgreSQL. The project is designed as a portfolio application to demonstrate modern software development practices, including layered architecture, Entity Framework Core, Docker-based infrastructure, and business-oriented domain modeling.

## Project Goals

The primary objective of this project is to showcase the development of a small-to-medium business management system while applying industry-standard design principles and technologies.

Key areas of focus include:

* ASP.NET Core MVC
* Entity Framework Core (Code First)
* PostgreSQL
* Docker
* Layered Architecture
* Dependency Injection
* REST API Development
* Authentication and Authorization
* Inventory Management
* Reporting

## Architecture

The solution follows a layered architecture to separate concerns and improve maintainability.

### Solution Structure

```text
MiniERP
│
├── MiniERP.Web
├── MiniERP.Application
├── MiniERP.Domain
└── MiniERP.Infrastructure
```

### Project Responsibilities

#### MiniERP.Web

Presentation layer responsible for:

* MVC Controllers
* Razor Views
* User Interface
* Dependency Injection Configuration

#### MiniERP.Application

Application layer responsible for:

* Business Use Cases
* DTOs
* Service Interfaces
* Validation Logic

#### MiniERP.Domain

Core business layer responsible for:

* Domain Entities
* Business Rules
* Enumerations
* Domain Contracts

#### MiniERP.Infrastructure

Infrastructure layer responsible for:

* Entity Framework Core
* PostgreSQL Access
* Repository Implementations
* External Services

## Planned Features

### Product Management

* Product Catalog
* Categories
* Product Activation/Deactivation

### Customer Management

* Customer Records
* Contact Information
* Customer History

### Supplier Management

* Supplier Records
* Purchase Tracking

### Inventory Management

* Stock Control
* Inventory Movements
* Kardex Tracking

### Sales Management

* Sales Orders
* Sales Details
* Stock Deduction

### Purchasing

* Purchase Orders
* Stock Replenishment

### Security

* User Authentication
* Role-Based Access Control
* Audit Logging

### Reporting

* Inventory Reports
* Sales Reports
* Purchase Reports
* Product Performance Reports

## Technology Stack

### Backend

* ASP.NET Core
* Entity Framework Core
* C#

### Database

* PostgreSQL

### Infrastructure

* Docker
* Docker Compose

### Frontend

* Razor Views
* Bootstrap
* JavaScript

## Development Approach

This project follows a Code First approach using Entity Framework Core migrations. Database schema changes are managed through version-controlled migrations, allowing the application and database to evolve together throughout development.

## Current Status

🚧 Project under active development.

The initial phase focuses on establishing the core architecture, database infrastructure, and foundational business modules before expanding into inventory, sales, purchasing, and reporting capabilities.

## License

This project is intended for educational and portfolio purposes.
