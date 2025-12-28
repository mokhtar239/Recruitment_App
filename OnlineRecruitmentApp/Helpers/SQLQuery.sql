-- =============================================
-- Online Recruitment Application Database Setup
-- =============================================
-- Run this script in SQL Server Management Studio (SSMS)
-- =============================================

-- Step 1: Create the Database
CREATE DATABASE [online recruitment application];
GO

-- Step 2: Use the Database
USE [online recruitment application];
GO

-- Step 3: Create Tables

-- USER Table
CREATE TABLE [USER] (
    USER_ID INT IDENTITY(1,1) PRIMARY KEY,
    ID INT NULL,
    EMP_ID INT NULL,
    USER_NAME NVARCHAR(100) NOT NULL,
    PASSWORD NVARCHAR(100) NOT NULL,
    EMAIL NVARCHAR(100) NOT NULL,
    ROLE NVARCHAR(50) NOT NULL
);
GO

-- EMPLOYER Table
CREATE TABLE [EMPLOYER] (
    EMP_ID INT PRIMARY KEY,
    USER_ID INT NOT NULL,
    COMPANY_NAME NVARCHAR(200),
    FOREIGN KEY (USER_ID) REFERENCES [USER](USER_ID)
);
GO

-- JOB_SEEKER Table
CREATE TABLE [JOB_SEEKER] (
    ID INT PRIMARY KEY,
    USER_ID INT NOT NULL,
    FULL_NAME NVARCHAR(200),
    CITY NVARCHAR(100),
    CV_LINK NVARCHAR(500),
    EXPERIENCE_YEARS INT DEFAULT 0,
    INDUSTRY NVARCHAR(100),
    FOREIGN KEY (USER_ID) REFERENCES [USER](USER_ID)
);
GO

-- JOB_VACANCY Table
CREATE TABLE [JOB_VACANCY] (
    JOB_VACANCY_ID INT IDENTITY(1,1) PRIMARY KEY,
    EMP_ID INT NOT NULL,
    DATE_POSTED DATETIME DEFAULT GETDATE(),
    EMPLOYER_ID INT,
    TITLE NVARCHAR(200) NOT NULL,
    DESRIPTION NVARCHAR(MAX),
    EXPERIENCE_REQUIRED INT DEFAULT 0,
    IS_HIDDEN BIT DEFAULT 0,
    FIELD NVARCHAR(100),
    LOCATION NVARCHAR(200),
    FOREIGN KEY (EMP_ID) REFERENCES [EMPLOYER](EMP_ID)
);
GO

-- APPLY Table (Job Applications)
CREATE TABLE [APPLY] (
    APPLY_ID INT IDENTITY(1,1) PRIMARY KEY,
    JOB_VACANCY_ID INT NOT NULL,
    ID INT NOT NULL,
    APPLY_DATE DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (JOB_VACANCY_ID) REFERENCES [JOB_VACANCY](JOB_VACANCY_ID),
    FOREIGN KEY (ID) REFERENCES [JOB_SEEKER](ID)
);
GO

-- SAVED_VACANCY Table
CREATE TABLE [SAVED_VACANCY] (
    SAVED_ID INT IDENTITY(1,1) PRIMARY KEY,
    ID_SEEKER INT NOT NULL,
    ID_VACANCY INT,
    ID INT,
    JOB_VACANCY_ID INT NOT NULL,
    DATE_SAVED DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (JOB_VACANCY_ID) REFERENCES [JOB_VACANCY](JOB_VACANCY_ID),
    FOREIGN KEY (ID_SEEKER) REFERENCES [JOB_SEEKER](ID)
);
GO

-- =============================================
-- Step 4: Insert Sample Data
-- =============================================

-- Insert Admin User
INSERT INTO [USER] (USER_NAME, PASSWORD, EMAIL, ROLE)
VALUES ('admin', 'admin123', 'admin@jobconnect.com', 'admin');
GO

-- Insert Sample Employer User
INSERT INTO [USER] (USER_NAME, PASSWORD, EMAIL, ROLE)
VALUES ('employer1', 'password123', 'employer@company.com', 'employer');

-- Get the USER_ID of the employer we just created
DECLARE @EmpUserId INT = SCOPE_IDENTITY();

-- Insert into EMPLOYER table
INSERT INTO [EMPLOYER] (EMP_ID, USER_ID, COMPANY_NAME)
VALUES (@EmpUserId, @EmpUserId, 'Tech Solutions Inc.');
GO

-- Insert Sample Job Seeker User
INSERT INTO [USER] (USER_NAME, PASSWORD, EMAIL, ROLE)
VALUES ('jobseeker1', 'password123', 'seeker@email.com', 'job seeker');

-- Get the USER_ID of the job seeker we just created
DECLARE @SeekerId INT = SCOPE_IDENTITY();

-- Insert into JOB_SEEKER table
INSERT INTO [JOB_SEEKER] (ID, USER_ID, FULL_NAME, CITY, CV_LINK, EXPERIENCE_YEARS, INDUSTRY)
VALUES (@SeekerId, @SeekerId, 'John Smith', 'New York', 'https://linkedin.com/in/johnsmith', 3, 'Information Technology');
GO

-- Insert Sample Job Vacancies
INSERT INTO [JOB_VACANCY] (EMP_ID, TITLE, DESRIPTION, EXPERIENCE_REQUIRED, IS_HIDDEN, FIELD, LOCATION)
VALUES (2, 'Software Developer', 'Looking for an experienced software developer with C# and .NET skills.', 2, 0, 'IT', 'New York');

INSERT INTO [JOB_VACANCY] (EMP_ID, TITLE, DESRIPTION, EXPERIENCE_REQUIRED, IS_HIDDEN, FIELD, LOCATION)
VALUES (2, 'Web Designer', 'Creative web designer needed for our marketing team.', 1, 0, 'Design', 'Los Angeles');

INSERT INTO [JOB_VACANCY] (EMP_ID, TITLE, DESRIPTION, EXPERIENCE_REQUIRED, IS_HIDDEN, FIELD, LOCATION)
VALUES (2, 'Project Manager', 'Experienced PM to lead software development projects.', 5, 0, 'Management', 'Chicago');
GO

-- =============================================
-- Done! Database is ready.
-- =============================================

-- Test Accounts:
-- ================
-- Admin:      username: admin        password: admin123
-- Employer:   username: employer1    password: password123
-- Job Seeker: username: jobseeker1   password: password123

PRINT 'Database setup completed successfully!';
PRINT '';
PRINT 'Test Accounts:';
PRINT '==============';
PRINT 'Admin:      username: admin        password: admin123';
PRINT 'Employer:   username: employer1    password: password123';
PRINT 'Job Seeker: username: jobseeker1   password: password123';
GO