# JobConnect - Online Recruitment Application

A modern Windows Forms application for online job recruitment with a beautiful, professional GUI.

## 🎨 Features & Improvements

### New Modern UI Design
- **Modern Color Scheme**: Professional indigo/purple primary colors with emerald secondary accents
- **Clean Typography**: Uses Segoe UI font family for consistent, readable text
- **Card-Based Layout**: Modern card components for better visual organization
- **Styled DataGridViews**: Beautiful tables with colored headers and alternating row colors
- **Rounded Buttons**: Modern flat buttons with hover effects
- **Professional Forms**: Clean input fields with proper labels and spacing
- **Sidebar Navigation**: Modern sidebar with navigation menu and logout button
- **Tab-Based Organization**: Easy navigation between different sections

### Application Structure

| Form | Purpose | Users |
|------|---------|-------|
| WelcomeForm | Landing page with Login/Register options | All |
| LoginForm | User authentication | All |
| RegisterForm | New user registration | All |
| AdminDashboardForm | Full system management | Admin |
| EmployerDashboardForm | Post and manage jobs | Employers |
| JobSeekerDashboardForm | Browse and apply for jobs | Job Seekers |

### User Roles

1. **Admin**: Can manage all users, view all data (employers, job seekers, jobs, applications)
2. **Employer**: Can post jobs, hide/show job listings, view their posted jobs
3. **Job Seeker**: Can browse jobs, apply to jobs, save jobs, update profile

## 📁 Project Structure

```
OnlineRecruitmentApp/
├── OnlineRecruitmentApp.sln          # Visual Studio Solution
├── README.md                          # This file
└── OnlineRecruitmentApp/
    ├── OnlineRecruitmentApp.csproj   # Project file
    ├── App.config                     # Application configuration
    ├── Program.cs                     # Entry point
    │
    ├── Helpers/
    │   ├── UIHelper.cs               # UI styling utilities
    │   ├── Session.cs                # User session management
    │   └── DatabaseHelper.cs         # Database connection helper
    │
    ├── WelcomeForm.cs                # Welcome/splash screen
    ├── LoginForm.cs                  # Login screen
    ├── RegisterForm.cs               # Registration screen
    ├── AdminDashboardForm.cs         # Admin panel
    ├── EmployerDashboardForm.cs      # Employer dashboard
    ├── JobSeekerDashboardForm.cs     # Job seeker dashboard
    │
    └── Properties/
        └── AssemblyInfo.cs           # Assembly information
```

## 🚀 How to Run

### Prerequisites
- **Visual Studio 2019 or later** (Community, Professional, or Enterprise)
- **.NET Framework 4.7.2** or later
- **SQL Server** (LocalDB, Express, or full version)

### Step-by-Step Instructions

#### Step 1: Configure Database Connection

1. Open `OnlineRecruitmentApp/Helpers/DatabaseHelper.cs`
2. Update the connection string to match your SQL Server:

```csharp
public static string ConnectionString = @"Data Source=YOUR_SERVER_NAME;Initial Catalog=""online recruitment application"";Integrated Security=True";
```

Replace `YOUR_SERVER_NAME` with your SQL Server instance name (e.g., `localhost`, `.\SQLEXPRESS`, `DESKTOP-XXXXX`, etc.)

#### Step 2: Open in Visual Studio

1. Double-click `OnlineRecruitmentApp.sln` to open in Visual Studio
2. Or open Visual Studio → File → Open → Project/Solution → Select the .sln file

#### Step 3: Build the Project

1. In Visual Studio, go to **Build** → **Build Solution** (or press `Ctrl+Shift+B`)
2. Make sure there are no build errors

#### Step 4: Run the Application

1. Press **F5** (Debug) or **Ctrl+F5** (Run without debugging)
2. Or click the green **Start** button in the toolbar

### Database Requirements

The application expects the following database structure (same as the original project):

**Tables:**
- `USER` - Stores user credentials and roles
- `EMPLOYER` - Employer profiles
- `JOB_SEEKER` - Job seeker profiles
- `JOB_VACANCY` - Job listings
- `APPLY` - Job applications
- `SAVED_VACANCY` - Saved jobs

If you have the original database, the new application will work with it without any changes.

## 🎯 Usage Guide

### For Administrators
1. Login with admin credentials
2. Use the tabbed interface to:
   - **Users Management**: Add, update, delete users
   - **Employers**: View all employer data
   - **Job Seekers**: View all job seeker profiles
   - **Job Listings**: View all posted jobs
   - **Saved Jobs**: View saved job data

### For Employers
1. Register/Login as an employer
2. **Post New Job**: Fill in job details and click "Post Job"
3. **Manage Jobs**: View your posted jobs in the table
4. **Hide/Show Jobs**: Control job visibility to job seekers

### For Job Seekers
1. Register/Login as a job seeker
2. **Browse Jobs**: View all available jobs
3. **Apply**: Select a job and click "Apply"
4. **Save Job**: Save jobs for later
5. **Update Profile**: Keep your profile updated

## 🔧 Customization

### Changing Colors
Edit `UIHelper.cs` to customize the color scheme:

```csharp
public static Color PrimaryColor = Color.FromArgb(79, 70, 229);      // Main color
public static Color SecondaryColor = Color.FromArgb(16, 185, 129);   // Accent color
public static Color DangerColor = Color.FromArgb(239, 68, 68);       // Delete/danger
```

### Changing Fonts
Edit the font definitions in `UIHelper.cs`:

```csharp
public static Font TitleFont = new Font("Segoe UI", 24, FontStyle.Bold);
public static Font ButtonFont = new Font("Segoe UI Semibold", 10);
```

## 📝 Notes

- The application uses Windows Forms (.NET Framework 4.7.2)
- All styling is done programmatically without additional libraries
- The database schema is compatible with the original project
- Session management is handled through a static `Session` class

## 🐛 Troubleshooting

### "Cannot connect to database"
- Verify SQL Server is running
- Check the connection string in `DatabaseHelper.cs`
- Ensure the database exists and is accessible

### "Login failed"
- Make sure the user exists in the database
- Check username and password are correct
- Verify the database has data

### Build Errors
- Ensure .NET Framework 4.7.2 is installed
- Try Clean Solution → Rebuild Solution

## 📄 License

This project is for educational purposes.

---
**Developed with ❤️ using C# Windows Forms**
