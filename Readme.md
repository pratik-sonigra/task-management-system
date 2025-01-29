# Task Management System

This repository contains the **Frontend**, **Backend**, and **SQL Scripts** for a task management system.

---

## **Project Structure**
task-management-project/
├── frontend/ # Angular frontend
├── backend/ # .NET backend
├── sql-scripts/ # SQL scripts for database setup
└── README.md # Project documentation

---

## **Setup Instructions**

### **Prerequisites**
- Docker and Docker Compose
- Node.js (for local development)
- Angular CLI (for frontend)
- .NET SDK (for backend)

---

### **1. Clone the Repository**
```bash
git clone <your-repo-url>
cd task-management-project
2. Build and Run the Project in Docker
bash
docker-compose up --build
3. Frontend (Angular)
bash
cd frontend
npm install
ng serve
4. Backend (ASP.NET Core)
bash
cd backend
dotnet restore
dotnet run
5. Database Setup
Run the SQL scripts from the sql-scripts/ directory to set up the database.
