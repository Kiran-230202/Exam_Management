# Exam Management Assignment Project - .NET 8 + Angular 19

## Project Overview

This is a full-stack job assignment project using:

- **Backend:** .NET 8 + Entity Framework Core 9.0.2  
- **Frontend:** Angular 19  
- **Database:** Microsoft SQL Server  

The project implements a **Exam Management System**, including basic CRUD operations for Students and Subjects. The database is created and updated using EF Core migrations.  

---

## Table of Contents

1. [Clone the Repository](#clone-the-repository)
2. [Backend Setup](#backend-setup)
3. [Insert Dummy Data](#insert-dummy-data)
4. [Run Backend API](#run-backend-api)
5. [Frontend Setup](#frontend-setup)
6. [Verify Project](#verify-project)
7. [Notes](#notes)
8. [Contact](#contact)

---

## Clone the Repository
git clone https:[https://github.com/Kiran-230202/Exam_Management](https://github.com/Kiran-230202/Exam_Management.git)
cd RepoName

## Backend Setup

1.Open the Backend_API folder in Visual Studio 2022 (or your preferred IDE).
2.Update the connection string in appsettings.json:

    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YourDBName;Trusted_Connection=True;TrustServerCertificate=True;"
    }

3.Open Package Manager Console (PMC) and run:
    Update-Database
  OR via CLI:
  dotnet ef database update
This will create all tables and apply migrations.

## Insert Dummy Data
After the database is created, run the following SQL script in SQL Server Management Studio (SSMS):

  -- Insert dummy students
  
        INSERT INTO Students (StudentId, Name, Age, Email)
        VALUES 
        (1, 'John Doe', 20, 'john.doe@example.com'),
        (2, 'Jane Smith', 21, 'jane.smith@example.com'),
        (3, 'Michael Brown', 22, 'michael.brown@example.com');
    
  -- Insert dummy subjects
  
      INSERT INTO SubjectMsts (SubjectId, SubjectName, Credits)
      VALUES
      (1, 'Mathematics', 4),
      (2, 'Physics', 3),
      (3, 'Computer Science', 5);

## Run Backend API
1.In Visual Studio, press F5 or click Run.

2.The API will start at a URL like:
  https://localhost:5001
  
Keep this URL in mind for the frontend Angular project.

## Frontend Setup (Angular 19)
1.Open the Frontend_Angular folder in a terminal.

2.Install dependencies:

    npm install
3.Run the Angular project:

    ng serve -o
    
This will open the UI in your default browser at http://localhost:4200.
## Verify Project
  Open http://localhost:4200 in the browser.
  
  Test CRUD operations for Students and Subjects.
  
  The frontend will communicate with the backend API running on https://localhost:5001.
## Notes  
- Ensure SQL Server is running before updating the database.
- If you change the API URL, update it in src/environments/environment.ts.
- EF Core migrations handle database creation; no separate SQL script is needed for tables.
- Dummy data script is only for sample data testing.
