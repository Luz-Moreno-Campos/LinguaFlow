# LinguaFlow 🌍📚

**LinguaFlow** is a web-based language course platform built with .NET 8. 
It connects students with specialized language tutors across various study programs (Business, Travel, Academic, Specific Purposes, and more).

The application is engineered using an **N-Tier Architecture** to maintain clean separation of concerns, scalability, and maintainability.

---

## 🏗️ Architecture & Project Structure (N-Tier)

The solution is divided into four main projects:

* **`LinguaFlow.DAL` (Data Access Layer):** 
  * Implements Entity Framework Core with a **Code-First** approach and custom configurations via **Fluent API**.
  * Manages database contexts (`DbContext`), migrations, repositories, and direct database persistence with SQL Server.
* **`LinguaFlow.BLL` (Business Logic Layer):** 
  * Contains core domain logic, service layers, authorization workflows, business validation rules, and enrollment operations.
* **`LinguaFlow.Models`:** 
  * Contains domain entities, ViewModels, DTOs, and Data Annotations for validation across layers.
* **`LinguaFlow.MVC` (Presentation Layer):** 
  * ASP.NET Core MVC web project handling HTTP requests, UI controllers, Razor views, Razor layouts, and client-side scripts.

---

## 📸 Key Features & Workflow (UI Flow)

### 👤 Anonymous Users
* **Landing Page & Explore Tutors:** Browse available tutors by language, language courses, and availability schedules.
* **Authentication Prompt:** Viewing tutors and courses is open to all visitors, but enrolling requires logging in or registering.

### 🎓 Student Role
* **Course Enrollment:** Select a desired language, choose a course,  view available tutors along with their profiles and availability, and request enrollment with a single click.
* **Enrollment Workflow:** Upon requesting enrollment, students receive instructions outlining payment details and notification that the tutor will reach out via email to schedule an introductory meeting. Following the meeting, the student completes payment and confirms the enrollment within their panel to begin classes.
* **Student Panel (`My Courses`):** Monitor course statuses and manage enrollment actions (Confirm or Cancel pending enrollments).
* **Profile Management (`My Profile`):** Update personal details and manage account credentials / password changes.

### 🛡️ Admin Role
* **Dashboard & Metrics:** Key performance indicators including total students, total tutors, total enrollments, pending fees, and pending payments. *(Note: Payment received tracking is currently scheduled for upcoming release).*
* **Tutor Management:** Full CRUD functionality, detailed profile views, and filtering options by name and language.
* **Student Management:** Full CRUD functionality, filtering options  and detailed views for registered students.
* **Enrollment Oversight:** Comprehensive listing of all system enrollments with multi-criteria filtering (by Student, Tutor, Course, and Status).

---

## 🛠️ Tech Stack & Dependencies

* **Framework:** .NET 8
* **Architecture:** N-Tier (DAL, BLL, Models, MVC)
* **Database & ORM:** SQL Server, Entity Framework Core 8 (Code-First using Fluent API)
* **Identity & Security:** ASP.NET Core Identity (Role-based Authorization: `Admin`, `Student`)
* **Frontend & UI:** Razor Views, HTML5, CSS3, Bootstrap 5
* **Client-Side Validation:** 
  * **jQuery & jQuery Validation:** Handles client-side form validation rules.
  * **jQuery Unobtrusive Validation:** Parses C# Data Annotations directly from Razor views to show instant error messages in the browser without reloading the page

### Key NuGet Packages
* `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
* `Microsoft.AspNetCore.Identity.UI`
* `Microsoft.EntityFrameworkCore.SqlServer`
* `Microsoft.EntityFrameworkCore.Tools`
* `Microsoft.EntityFrameworkCore.Design`

---

## 🗄️ Domain Architecture (EF Core Code-First)

The domain model consists of **7 core entities** managed via EF Core Code-First with custom Fluent API mappings:

* **`Student` & `Tutor`:** Profiles linked to Identity security principals. Tutors teach a specific `Language` and provide 1-on-1 instruction. *(Note: Full Identity account features for Tutors are scheduled for a future release).*
* **`Language`:** Defines the language taught by a tutor.
* **`Course`:** Catalog of specialized study programs (e.g., Business, Academic, Travel, Specific Purposes). Courses are taught 1-on-1; students can take multiple courses with the same or different tutors, but cannot duplicate the same course-tutor combination.
* **`Enrollment`:** Central join entity connecting `Student`, `Tutor`, and `Course`. Tracks registration state (`PendingConfirmation`, `Confirmed`, `Cancelled`).
* **`Payment`:** Auto-generated upon enrollment to manage student transaction details and payment statuses.
* **`TutorFee`:** Auto-generated upon enrollment to track compensation records for tutors.

*(Note: Admin is implemented strictly as a role with dedicated Controllers, Services, and Views, rather than a standalone domain entity).*

---

## 🚀 Getting Started

### Prerequisites
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or Full Instance)
* Visual Studio 2022 / VS Code

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/your-username/LinguaFlow.git](https://github.com/your-username/LinguaFlow.git)
   cd LinguaFlow

2. **Configure Connection String:**
   Open `appsettings.json` located in `LinguaFlow.MVC` and configure your connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LinguaFlowDb;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }

3. **Apply Database Migrations:**  
   Run the EF Core database update targeting `LinguaFlow.DAL` as the data project and `LinguaFlow.MVC` as the startup project:  
   `dotnet ef database update --project LinguaFlow.DAL --startup-project LinguaFlow.MVC`  

   *(Alternatively, in Visual Studio Package Manager Console, set `LinguaFlow.DAL` as Default Project and run `Update-Database`).*  

4. **Run the Application:**  
   Start the application by running the MVC project:  
   `dotnet run --project LinguaFlow.MVC`  

   Open your browser and navigate to `https://localhost:7000`.  

---

## 📋 Roadmap / Upcoming Features

- [ ] **Tutor Identity Integration:** Full user account registration, login, and dashboard access for Tutors.  
- [ ] **Payment Receipts Integration:** Complete processing, approval, and record-keeping for received payments in the Admin Panel.  
- [ ] **Email Notifications:** Automated email delivery for payment details and meeting scheduling upon course enrollment.  
- [ ] **In-App Messaging System:** Direct communication channel between tutors and students.  

---