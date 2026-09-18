# TodoList

A simple task management web application built with **ASP.NET Core MVC** and **Entity Framework Core**.

## Features

* User registration and login
* Create, edit and delete tasks
* Mark tasks as completed
* Task search and filtering
* Sorting by date and priority
* Pagination
* Soft delete
* User-specific task management

## Technologies

* C#
* .NET 8
* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* Bootstrap
* Cookie Authentication

## Project Structure

```text
TodoList
├── CoreLayer
│   ├── DTOs
│   ├── Entities
│   ├── Services
│   └── Utilities
│
├── DataLayer
│   ├── Context
│   └── Migrations
│
└── TodoList
    ├── Controllers
    ├── Models
    ├── Views
    └── wwwroot
```

The project separates application logic, data access, and the MVC presentation layer.

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/MahdiEbrahimiDev/TodoList.git
cd TodoList
```

### 2. Configure the database

Update the connection string in:

```text
TodoList/appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQL2022;Database=TodoListDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### 3. Apply migrations

```bash
dotnet ef database update
```

### 4. Run the application

```bash
dotnet restore
dotnet run
```

Then open the local URL shown in the terminal.

## Purpose

This project was built as a practical ASP.NET Core MVC project to work with authentication, Entity Framework Core, database relationships, CRUD operations, filtering, sorting, pagination, and layered application structure.
