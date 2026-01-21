# CLAUDE.md - MyApp API

## Tech Stack
- .NET 7, ASP.NET Core  APIs
- Entity Framework Core with SQL Server
- MediatR for CQRS
- FluentValidation for request validation

## Project Structure
- `InterviewCoach.API/` — Entry point, endpoints, middleware
- `InterviewCoach.Application/` — Use cases, handlers, DTOs
- `InterviewCoach.Domain/` — Entities, value objects, domain events
- `InterviewCoach.Infrastructure.Persistence/` — EF Core, external services

## Commands
- Build: `dotnet build`
- Test: `dotnet test`
- Run: `dotnet run --project src/MyApp.Api`

## Coding Standards
- Use primary constructors for dependency injection
- Always pass CancellationToken to async methods
- Validation goes in FluentValidation validators, not handlers
- Never hardcode connection strings or secrets

## Workflow Rules
- ALWAYS create a git branch before making changes
- Run `dotnet test` after every implementation
- Keep commits atomic and focused