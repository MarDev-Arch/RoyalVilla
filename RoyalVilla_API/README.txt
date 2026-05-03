Products API

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
