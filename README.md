# Hardship Application System

A simple web application to manage hardship applications. Built with **React + TypeScript** for the frontend, **.NET 7 Web API** for the backend, and **SQLite** as the database.

----------------

## Features

- Create new hardship applications  
- Edit existing applications  
- Delete applications  
- Display a list of all hardship applications  
- Input validation for required fields  
- User-friendly error messages  
- Polished UI with plain CSS styling  

-----------------

## System Requirements

- Node.js >= 18  
- .NET 7 SDK  
- SQLite (included with backend)  

------------------

## Project Structure

hardship-application/
├─ backend/Hardship.Api # .NET Core API project
├─ frontend/hardship-frontend # React + TypeScript frontend
├─ README.md

------------------

## Backend Setup

Open terminal and navigate to the backend folder:
cd backend/Hardship.Api

Restore dependencies:
dotnet restore

Run the backend API:
dotnet run

The API will be available at: https://localhost:5054/api/hardship

Swagger is enabled in development mode: https://localhost:5054/swagger

CORS is configured to allow requests from the frontend at http://localhost:5173.

**Frontend Setup**

Navigate to the frontend folder:

cd frontend/hardship-frontend

Install dependencies:
npm install

Start the development server:
npm run dev

The frontend will be available at: http://localhost:5173

**Usage**

Fill out the Create Hardship Application form to submit a new application.

Click Edit on any application to update it.

Click Delete to remove an application.

The application list updates automatically after changes.

**Validation Rules**

Customer Name: required, max 100 characters

Date of Birth: required, cannot be in the future

Income: required, must be greater than 0

Expenses: required, must be 0 or greater

Hardship Reason: optional, max 500 characters

**Technologies Used**

Frontend: React, TypeScript, Axios, React Query

Backend: .NET 7, Dapper, FluentValidation

Database: SQLite

Styling: Plain CSS

**Notes**

All API endpoints have been tested with Postman.

The frontend uses React Query for data fetching and cache invalidation.

All forms include validation and proper error handling.

**Future Improvements**

Add unit tests for frontend and backend

Add pagination and filtering for the application list

Improve UI styling with a framework like Tailwind or Material UI

**Author**

Elionora El Dahan
