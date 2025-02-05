# Fonsion - Hotel Room Booking System (Backend)

This is the backend service for the **Fonsion** hotel booking platform, built using .NET Core, following Clean Architecture principles to ensure maintainability and scalability.

## 🚀 Technologies Used
- **.NET Core** – Backend framework
- **Entity Framework Core** – ORM for database management
- **SQL Server** – Relational database
- **MediatR** – Implements CQRS pattern for structured request handling
- **FluentValidation** – Ensures robust data validation
- **Identity** – Manages user authentication and authorization
- **Unit of Work Pattern** – Ensures transactional integrity and separation of concerns

## 🎯 Features
- 🏨 Manage hotel rooms and reservations
- 📝 Apply and validate promo codes
- 🔑 Secure user authentication with **ASP.NET Core Identity**
- 📊 Fetch room availability and details
- 🛠 Implements **Separation of Concerns** for clean and maintainable code

## 🏗 **Architecture Overview**
This project follows **Clean Architecture**, ensuring a modular, testable, and scalable codebase. The main layers include:

1. **Domain Layer**  
   - Contains core business logic, entities, and domain events.
2. **Application Layer**  
   - Implements **CQRS (MediatR)** for command and query separation.
   - Defines use cases and interacts with the persistence layer via the repository pattern.
3. **Infrastructure Layer**  
   - Implements **Entity Framework Core** for data access.
   - Handles external dependencies such as **Identity for authentication**.
   - Uses **Unit of Work Pattern** to maintain transactional consistency.
4. **Presentation Layer**  
   - Exposes API endpoints via **ASP.NET Core Web API**.
   - Implements **FluentValidation** for request validation.

## 🛠 Installation & Setup
To run the backend locally:

1. **Clone the repository**
   ```sh
   git clone https://github.com/njemecc/fonsion.be.git
   cd fonsion.be
    ```
2. **Configure database connection**
 Update appsettings.json with your SQL Server credentials.

3. **Apply Migrations**
     ```sh
     dotnet ef database update
     ```
4. **Run the api**
     ```
      dotnet run
     ```

## 📷 Screenshots

# Home Page 🚀
![header](https://github.com/user-attachments/assets/83d9f1e2-2609-44ca-96a8-39762bcec961)

# Rooms🏨
![rooms](https://github.com/user-attachments/assets/5a069119-b496-4ce6-b344-7d9c4b622d14)

# My reservations 📝
![reservations](https://github.com/user-attachments/assets/75f26b71-123d-494c-95c5-ea3c1f812317)

# Make a reservation 📝
![reservation](https://github.com/user-attachments/assets/416ab999-ebf0-4a29-88fc-b2b0a6607585)
