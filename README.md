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
