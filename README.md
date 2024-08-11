MoltaqaTech - E-learning Community Website
MoltaqaTech is a comprehensive e-learning platform designed to foster a community for students in the tech industry. The platform provides a rich set of features, enabling users to learn, share knowledge, and engage with others in the tech community. This project encompasses both frontend and backend development, with a focus on scalability, security, and user experience.

Table of Contents
Features
Courses Module
Blogs Module
Zoom Meetings Module
Events Module
Security
Authentication & Authorization
Frontend
Backend
Architecture
Design Patterns
Database
Installation
Usage
Contributing
License
Features
Courses Module
User Roles: Users can register as students or as both students and instructors.
Course Creation: Instructors can create courses, defining learning objectives, descriptions, costs, and discounts.
Course Structure: Courses are divided into sections, with each section containing items such as lectures or quizzes.
Quizzes: Quizzes are multiple-choice, and students must pass them to proceed to the next section.
Progress Tracking: The system tracks the student's progress in each course they are enrolled in.
Wishlist: Students can add courses to a wishlist for future purchase.
Payments: Secure payment integration for purchasing courses.
Reviews: Students can post reviews on courses they have purchased.
Blogs Module
Blog Creation: Users can create blog posts about tech-related topics.
Categories & Subjects: Blog posts can be categorized and filtered by subject.
Post Statistics: The platform tracks the number of views and posting dates, allowing users to sort by these metrics.
Comments: Other users can comment on blog posts, fostering discussion.
Zoom Meetings Module
Zoom Integration: Users can create live Zoom meetings directly from the platform.
Meeting Participation: Other users can join Zoom meetings from within the website.
Events Module
Event Announcements: Users can announce events, detailing speakers, locations, and topics.
Security
JWT Authentication: JSON Web Tokens are used for secure authentication.
Token Expiry & Blacklisting: Tokens expire after 24 hours and are blacklisted upon logout, with blacklisted tokens stored in Redis and cleared after expiration.
Authentication & Authorization
Social Login: Users can sign up and log in using Facebook and Gmail.
Email Verification: A verification email is sent to users upon signup, and account verification is required.
Frontend
Framework: The frontend is developed using Angular, a Single Page Application (SPA) framework known for its speed, efficiency, and component-based architecture.
Features:
Guards & Routing: Ensures users can only access permitted areas of the application.
Services: Reusable services are used to interact with backend APIs.
Dependency Injection: Provides a clean and efficient way to manage dependencies.
Interfaces: Ensures strong typing and better code organization.
Backend
Architecture
The backend is built using the .NET Web API framework, organized into a modular Onion Architecture that promotes maintainability and scalability.

API Layer: Exposes the application's functionality via RESTful endpoints.
Core Layer: Contains the domain entities and business logic.
Repository Layer: Handles data access logic, interacting with the SQL Server database.
Service Layer: Coordinates between the repository and API layers, managing business logic and ensuring data integrity.
Design Patterns
Unit of Work Pattern: Ensures that a series of operations are treated as a single transaction.
Repository Pattern: Abstracts data access, allowing for easier testing and maintenance.
Dependency Injection Pattern: Promotes loose coupling and better testability.
Specification Pattern: Encapsulates business rules within reusable and composable specifications.
Database
SQL Server: The project uses a single SQL Server database, combining both the identity context and business context. This design choice allows the identity user table to include fields for student and instructor IDs.
Installation
Clone the Repository:
bash
Copy code
git clone https://github.com/yourusername/moltaqatech.git
Install Frontend Dependencies:
bash
Copy code
cd frontend
npm install
Install Backend Dependencies:
bash
Copy code
cd ../backend
dotnet restore
Setup Database:
Configure your SQL Server database and update the connection strings in the appsettings.json file.
Run migrations to set up the database schema:
bash
Copy code
dotnet ef database update
Usage
Run the Backend:
bash
Copy code
cd backend
dotnet run
Run the Frontend:
bash
Copy code
cd ../frontend
ng serve
Access the Application:
Open your web browser and navigate to http://localhost:4200.
Contributing
We welcome contributions! Please see our CONTRIBUTING.md for guidelines on how to contribute to this project.

License
This project is licensed under the MIT License - see the LICENSE file for details.
