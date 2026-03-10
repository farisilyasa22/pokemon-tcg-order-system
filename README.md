# pokemon-tcg-order-system

This project is for learning purposes.

A backend system that allows users to reserve limited Pokémon TCG products
during a booking window without overselling.

---

## Purpose
The purpose of this project is to practice backend system design by building
a realistic booking / reservation system with inventory constraints.

---

## User Roles

### Customer
- View available Pokémon TCG products
- Reserve a product if stock is available
- View and cancel their reservations

### Admin
- Create and manage products
- Set available stock
- Open and close booking windows
- View reservations

---

## Core Features
- Product listing with limited stock
- Reservation of products during a booking window
- Prevention of overselling
- Reservation expiration if not confirmed

---

## Core Rules
- The system must never oversell products
- One reservation holds one unit of a product
- Reservations start in a PENDING state
- Reservations expire after a fixed time if not confirmed
- Inventory is the source of truth and is managed in the database

---

## Assumptions
- Traffic is relatively low (small-scale system)
- A single backend application is sufficient
- A relational database is used
- Strong consistency is preferred over high availability

---

## Entities & Database Design

### User
- 'id' (primary key)
- 'username'
- 'email'
- 'password' (hashed)
- 'role' ('CUSTOMER' or 'ADMIN')
- 'created_at'
- 'updated_at'

### Product
- 'id' (primary key)
- 'name'
- 'release_date'
- 'total_stock'
- 'booking_start_time'
- 'booking_end_time'
- 'status' ('UPCOMING', 'OPEN', 'CLOSED')
- 'created_at'
- 'updated_at'

### Inventory
- 'id' (primary key)
- 'product_id' (foreign key -> Product)
- 'available_quantity'
- 'created_at'
- 'updated_at'

### Reservation
- 'id' (primary key)
- 'user_id' (foreign key -> User)
- 'product_id' (foreign key -> Product)
- 'status' ('PENDING', 'CONFIRMED', 'EXPIRED', 'CANCELLED')
- 'created_at'
- 'expires_at'
- 'updated_at'

---

## Relationships
- A user can have many reservations
- A product can have many reservations
- Each product has one inventory record

---

## Key Design Decisions
- Inventory is separate from Product for atomic updates
- Reservations reference both Product and User
- Reservations expire and return stock automatically
- Database transactions enforce consistency
- EF Core is used with code-first approach for maintainability
- Background services handle reservation expiry without blocking main requests

---

## Tech Stack
- ASP.NET Core Web API
- Entity Framework Core (Code-First)
- PostgreSQL (local dev)
- xUnit for unit testing
- Docker (optional)
- Git for version control
- Swagger/OpenAPI for testing endpoints

---

## Project Structure
- Controllers/   → Handle API endpoints
- Services/      → Business logic
- Repositories/  → Database access
- Models/        → EF Core entities
- Data/          → DbContext and migrations
- BackgroundServices/ → Reservation expiry jobs

---

## Sample Endpoints
- GET /products → List available products
- POST /reservations → Create a reservation
- GET /reservations → List user reservations
- PUT /reservations/{id}/cancel → Cancel a reservation

---

## Concurrency & Transactions
- Reservation creation uses EF Core transactions to prevent overselling
- Inventory updates are atomic
- Background job ensures expired reservations return stock safely
- Supports basic concurrency simulation to test multiple simultaneous reservations

---

## Testing & Validation
- Unit tests for reservation logic (xUnit)
- Input validation using DataAnnotations
- Role-based authorization in controllers
- Optional: use InMemoryDatabase for test isolation

---

## Visuals (Optional)
- ER Diagram: Users, Products, Inventory, Reservations
- Flowchart: Booking → Reservation → Expiry
- Helps explain system architecture quickly in interviews

---

## Future Improvements
- Modular monolith can be split into microservices later
- Background services could become separate worker services
- Could add caching, message queues, and CI/CD pipelines for scaling
- Dockerize project for easy deployment
- Add JWT-based authentication for more secure API

---

## Out of Scope
- Real payment gateway
- Shipping and fulfillment
- Notifications (email / SMS / push)
- Mobile or frontend application
