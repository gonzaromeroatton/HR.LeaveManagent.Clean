# HR.LeaveManagement.Clean

Clean Architecture, CQRS and DDD example for a Human Resources Leave Management system.

This solution demonstrates how to structure a .NET solution using **Clean Architecture**, **CQRS** (Command–Query Responsibility Segregation) and **Domain-Driven Design (DDD)** principles for a simple HR leave management domain. :contentReference[oaicite:0]{index=0}

---

## Table of Contents

- [Solution Goals](#solution-goals)
- [Domain Overview](#domain-overview)
- [Architecture](#architecture)
  - [Projects](#projects)
  - [Layered Dependencies](#layered-dependencies)
- [CQRS](#cqrs)
  - [Commands](#commands)
  - [Queries](#queries)
- [DDD Building Blocks](#ddd-building-blocks)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Clone & Build](#clone--build)
  - [Configuration](#configuration)
  - [Running the API](#running-the-api)
- [How to Navigate the Code](#how-to-navigate-the-code)
- [Roadmap / Ideas](#roadmap--ideas)
- [License](#license)

---

## Solution Goals

The main purpose of this repository is to serve as a **reference implementation** for:

- Applying **Clean Architecture** in a multi-project .NET solution.
- Using **CQRS** to separate reads and writes at the application layer.
- Modeling a small HR domain using **DDD** concepts (entities, value objects, aggregates, domain services, etc.).
- Isolating **infrastructure and persistence** concerns from the core domain.
- Providing a base that can be extended into a real-life HR Leave Management system.

---

## Domain Overview

The solution models the basic concepts of an HR leave management system, such as:

- **Employees** requesting different types of leave (vacation, sick leave, etc.).
- **Leave Types** with rules (default days, name/description, etc.).
- **Leave Allocations** for each employee and leave type.
- **Leave Requests** going through a simple approval workflow.

The exact details of each concept are implemented in the **Domain** and **Application** layers, keeping the UI and infrastructure independent.

---

## Architecture

This repository follows a typical Clean Architecture layout. At the root of the solution you’ll find the following projects: :contentReference[oaicite:1]{index=1}

```text
HR.LeaveManagement.Clean.sln
├── HR.LeaveManagement.Api
├── HR.LeaveManagement.Application
├── HR.LeaveManagement.Domain
├── HR.LeaveManagement.Infraestructure
└── HR.LeaveManagement.Persistence

Projects
HR.LeaveManagement.Api

    Entry point of the application (presentation layer).

    Exposes the HR Leave Management features as a Web API.

    Maps HTTP requests to commands and queries defined in the Application layer.

    Implements cross-cutting concerns at the API boundary (e.g. exception handling, logging hooks, validation pipeline, etc.).

HR.LeaveManagement.Application

    Contains application logic and use cases.

    Implements CQRS:

        Command handlers for state changes.

        Query handlers for read operations.

    Depends only on abstractions from the Domain layer and interfaces for persistence/infrastructure.

    Contains:

        DTOs / ViewModels used by the API.

        Interfaces for repositories and services.

        Validation and request/response models.

HR.LeaveManagement.Domain

    Pure domain model, independent from any infrastructure or framework concerns.

    Contains:

        Entities and aggregates (e.g. LeaveType, LeaveRequest, LeaveAllocation, Employee, etc.).

        Value Objects and domain-specific enums.

        Domain logic and invariants.

    Has no dependencies on other projects. It is the core of the system.

HR.LeaveManagement.Persistence

    Implements persistence concerns (e.g. ORM mappings, database context, migrations).

    Concrete implementations of repository interfaces defined in Application.

    Responsible for translating between domain entities and database representation.

HR.LeaveManagement.Infraestructure

    Implements infrastructure services that are not directly related to persistence, such as:

        Email sending.

        File system or cloud storage access.

        External services integration (authentication providers, third-party APIs, etc.).

    Provides concrete implementations for interfaces consumed by the Application layer.

Layered Dependencies

The solution keeps dependencies flowing inwards:

[ Presentation ]
HR.LeaveManagement.Api
        ↓
[ Application ]
HR.LeaveManagement.Application
        ↓
[ Domain ]
HR.LeaveManagement.Domain

[ Persistence / Infrastructure ]
HR.LeaveManagement.Persistence
HR.LeaveManagement.Infraestructure
        ↑
  referenced only via interfaces
  defined in Application/Domain

    Domain is at the center and does not depend on any other project.

    Application references Domain and defines interfaces for persistence/infrastructure.

    Api references Application (and usually only indirectly references Persistence/Infrastructure through the DI container).

    Persistence and Infrastructure reference Application (to implement its interfaces) and Domain.

This allows you to replace the outer layers (UI, persistence, infrastructure) without changing the core domain or use cases.
CQRS

The solution follows Command–Query Responsibility Segregation at the application layer:
Commands

    Used for operations that change state:

        e.g. Create leave request, approve/deny request, create or update leave type, allocate leave, etc.

    Typical flow:

        API receives an HTTP request.

        The request is mapped to a Command object.

        A Command Handler executes the use case:

            Loads domain entities from repositories.

            Applies domain logic and invariants.

            Persists changes using repository interfaces.

        Returns a result (e.g. ID of created entity, validation result, etc.).

Queries

    Used for read-only operations:

        e.g. Get all leave types, get leave allocations for a user, get pending leave requests.

    Typical flow:

        API receives HTTP request.

        The request is mapped to a Query object.

        A Query Handler retrieves data (often projection/DTOs).

        Returns a DTO or view model to the API.

This separation keeps read and write concerns independent and simplifies reasoning about each use case.
DDD Building Blocks

Although the project is small, it incorporates several Domain-Driven Design ideas:

    Entities & Aggregates
    Encapsulate identity and behavior of core domain concepts (LeaveType, LeaveRequest, etc.).

    Value Objects
    Used for concepts that are defined by their value rather than identity (e.g. dates, periods, and similar).

    Repositories (as interfaces)
    The Application layer defines repository interfaces that work with domain entities/aggregates. Implementation details are provided in Persistence.

    Domain Logic and Invariants
    Business rules are pushed towards the domain entities and use cases rather than being scattered in controllers.

Getting Started
Prerequisites

Make sure you have at least:

    A recent .NET SDK installed (e.g. .NET 6+).

    A running database engine (e.g. SQL Server, localdb, or any configured provider).

    Git.

    Note: The exact versions and database provider are defined in the project/solution settings and configuration files in the repository.

Clone & Build

git clone https://github.com/gonzaromeroatton/HR.LeaveManagent.Clean.git
cd HR.LeaveManagent.Clean

# Restore dependencies & build
dotnet restore
dotnet build

Configuration

    Open the solution file:

HR.LeaveManagement.Clean.sln

Check the configuration files (e.g. appsettings.json in the Api project and any persistence configuration) to:

    Set the connection string for your database.

    Adjust any email / external service settings if present.

Apply database migrations if the Persistence project is set up for migrations (for example):

    # Example, adjust to your actual startup project and context
    dotnet ef database update

Running the API

From the solution root, run the API project:

cd HR.LeaveManagement.Api
dotnet run

By default the API will start on the configured Kestrel/HTTP port (check your launch settings).

If Swagger/OpenAPI is enabled in the Api project, you can explore the endpoints via the browser (usually /swagger).
How to Navigate the Code

A suggested way to explore the solution:

    Start from the Domain

        Look at the core entities and aggregates.

        Understand the basic business rules.

    Move to Application

        Inspect commands and queries related to each use case.

        See how handlers orchestrate domain logic and repositories.

    Check Persistence & Infrastructure

        See how repositories and services are concretely implemented.

        Review how entities are mapped to the database.

    Finally, look at the Api

        Inspect controllers or endpoints to see how HTTP-level concerns map to application use cases.

        Review how dependency injection wires all the layers together at startup.

Roadmap / Ideas

Some ideas for extending this example:

    Add authentication & authorization for employees and administrators.

    Implement more advanced approval workflows or escalations.

    Add notifications (email / messaging) for request approval or rejection.

    Add more reporting endpoints (e.g. leave balance, usage per period).

    Integrate a UI client (SPA or MVC) consuming the API.

# License

This project is licensed under the GPL-3.0 License – see the LICENSE.txt file for details.
