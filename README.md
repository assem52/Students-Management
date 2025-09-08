# 🎓 Student Management API

A **.NET 7 Web API** project designed for managing students, with a strong focus on **modern authentication/authorization** and **clean architecture principles**.  
This project is built as a learning-oriented template to understand how **JWT Authentication**, **ASP.NET Core Identity**, and **design patterns** like **Repository** and **Unit of Work (UoW)** come together in a scalable application.

---

## 🚀 Features

- **JWT Authentication**
  - Secure login using JSON Web Tokens.
  - Bearer token support for stateless authentication.
  - Configurable expiration and refresh handling.

- **Role-based Authorization**
  - Role claims embedded in JWT.
  - Fine-grained access to endpoints (`Admin`, `Teacher`, `Student`).

- **ASP.NET Core Identity**
  - Manages users, roles, password hashing, claims.
  - Integrates seamlessly with EF Core.

- **Repository & Unit of Work Pattern**
  - Clean abstraction over EF Core.
  - Testable, maintainable, and scalable data access.
  - Transactional consistency ensured with Unit of Work.

- **Clean Architecture**
  - Separation of concerns across layers:
    - **Domain** → Entities, core logic.
    - **Application** → Services, interfaces, DTOs.
    - **Infrastructure** → EF Core, repositories, identity.
    - **Presentation** → API controllers.

---

## 📂 Project Structure (Clean Arch)

src/
├── StudentManager.Domain # Core business entities & logic
├── StudentManager.Application # Interfaces, DTOs, business services
├── StudentManager.Infrastructure # Data, Identity, Repositories, UoW
└── StudentManager.API # Web API layer (controllers, middleware)

---

## 🔑 Authentication Flow (JWT + Identity)

1. **User Registration** → User is created in the Identity store with hashed password.
2. **User Login** → Identity verifies credentials → issues JWT with claims (roles, user id, etc.).
3. **Bearer Token** → JWT is passed in the `Authorization: Bearer <token>` header.
4. **Authorization** → Middleware validates token, reads roles/claims, grants/denies access.

---

**🛠 Tech Stack**
ASP.NET Core 7 Web API
Entity Framework Core
ASP.NET Core Identity
JWT Authentication
SQL Server
Repository & Unit of Work Pattern
Clean Architecture