# Task Management System 🗂️

This repository contains the **Frontend**, **Backend**, and **SQL Scripts** for a task management system designed to streamline task tracking and collaboration.

---

## **Project Structure** 📂
The project is organized as follows:
task-management-project/
├── frontend/ # Angular frontend
├── backend/ # .NET backend
├── sql-scripts/ # SQL scripts for database setup
└── README.md # Project documentation


---

## **Setup Instructions** ⚙️

### **Prerequisites** 🚀
Before you start, ensure you have the following installed on your local machine:

- **Docker** and **Docker Compose**: For containerized deployment.
- **Node.js**: Required for Angular development.
- **Angular CLI**: For building and serving the frontend.
- **.NET SDK**: For building and running the backend.

### **Steps to Run the Project** 👇

#### **1. Clone the Repository** 
First, clone this repository to your local machine:

```bash
git clone <your-repo-url>
cd task-management-project
2. Build and Run the Project with Docker 🐳
To quickly spin up the application with Docker, use the following command:

bash
Copy code
docker-compose up --build
This will build and run the necessary containers for the frontend, backend, and database.

3. Frontend (Angular) 🔧
To start the Angular frontend:

bash
Copy code
cd frontend
npm install   # Install dependencies
ng serve      # Run the frontend on localhost:4200
4. Backend (ASP.NET Core) 💻
To start the ASP.NET Core backend:

bash
Copy code
cd backend
dotnet restore   # Restore NuGet packages
dotnet run       # Run the backend on localhost:5000
5. Database Setup 🗄️
Run the SQL scripts from the sql-scripts/ directory to set up the database schema and necessary tables.

bash
Copy code
# Navigate to sql-scripts/ directory and execute the SQL scripts.
Features ✨
Create, read, update, and delete tasks.
Assign tasks to users.
Track task completion status.
View task details, deadlines, and progress.
Contributing 🤝
We welcome contributions! If you'd like to improve the project or fix a bug, feel free to submit a pull request. Please make sure to follow the repository's code of conduct and coding guidelines.

License 📝
This project is licensed under the MIT License - see the LICENSE file for details.

Feel free to open an issue or reach out with any questions or feedback. Happy coding! 💻🚀
