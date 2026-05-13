Web Products API

A simple ASP.NET Core (.NET 10) Web API for managing products with full CRUD operations, built as part of a backend development exercise.

  Features
Health check endpoint (public)
  Secured endpoints using JWT authentication
  Create product
  Get all products
  Filter products by colour
  Update product
  Delete product
  Unit and integration tests included
  Frontend (React/Angular) to consume the API
  Designed with microservices architecture in mind 


How the flows work
1. Public view (before login)
Browser calls GET /villas → Gateway → Villa Service

Villa Service returns all available villas (directly from VillaDB or via pre‑built SearchDB for performance)

2. Registration & login
POST /register → Auth Service creates user → publishes UserRegistered event

POST /login → Auth Service returns JWT/session

Notification Service consumes UserRegistered → sends welcome email

3. Authenticated villa management (after login)
Requests include JWT (validated by Gateway or Auth Service)

Villa Service checks that the authenticated user owns the villa (for edit/delete)

After successful DB operation, Villa Service publishes an event:

VillaCreated

VillaUpdated

VillaDeleted

Search Indexing Service consumes these events → keeps search index synchronised (optional but recommended for fast public search)

Why event‑driven elements are included
Component	Why event‑driven?
Search Indexing	Public villa listing stays fast and up‑to‑date without slowing down the main write operation.
Notification	Sending a welcome email or villa approval alert should not block the registration / villa creation response.
Audit / Analytics (optional, not shown)	You could add a service consuming villa events to track user activity.
Key characteristics
Synchronous CRUD for immediate user feedback (add/edit/delete works like a traditional app)

Asynchronous events for cross‑service concerns (search, notifications)

Auth Service is separate – login state shared via JWT (stateless) or session store

Public villa listing can be served from a read‑optimised search index (eventually consistent) or directly from VillaDB if volume is low





 API Endpoints Public

GET /health → Returns OK
Protected (Requires JWT)
POST /api/products → Create product
GET /api/products → Get all products
GET /api/products/{id} → Get product by ID
GET /api/products/colour/{colour} → Get products by colour
PUT /api/products/{id} → Update product
DELETE /api/products/{id} → Delete product
  Testing
Unit tests for business logic
Integration tests for API endpoints
  Architecture

This API is designed to fit into a microservices, event-driven architecture.

Example Components:
Products Service (this API)
Orders Search
Payments price
  Architecture Diagram
Frontend (React/Angular)
        |
        v
   Products API (.NET 10)
        |
        v
     SQL Server

Other Services:
- Orders Service
- Payments Service
        |
 API Documentation

Available via Scalar:

/scalar
ASP.NET Core (.NET 10)
Entity Framework Core
SQL Server
JWT Authentication
Scalar (API documentation)
 Author

Your Name: Ifeanyi Marvelous Akpati

 GitHub Repository

(https://github.com/MarDev-Arch/RoyalVilla/tree/master/RoyalVilla_API)
