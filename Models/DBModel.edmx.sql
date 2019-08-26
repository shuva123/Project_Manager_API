
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/24/2019 18:09:07
-- Generated from EDMX file: C:\Users\Admin\Downloads\Project_Manager_API\Project_Manager_API\Models\DBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Final_Project];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Parent_Task]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parent_Task];
GO
IF OBJECT_ID(N'[dbo].[Project]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Project];
GO
IF OBJECT_ID(N'[dbo].[Task]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Task];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Parent_Task'
CREATE TABLE [dbo].[Parent_Task] (
    [Parent_ID] int IDENTITY(1,1) NOT NULL,
    [Parent_Task1] varchar(max)  NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [Project_ID] int IDENTITY(1,1) NOT NULL,
    [ProjectName] varchar(50)  NULL,
    [Start_Date] datetime  NULL,
    [End_Date] datetime  NULL,
    [Priority] int  NULL,
    [IsCompleted] varchar(2)  NULL
);
GO

-- Creating table 'Tasks'
CREATE TABLE [dbo].[Tasks] (
    [Task_ID] int IDENTITY(1,1) NOT NULL,
    [Project_ID] int  NULL,
    [Task1] varchar(50)  NULL,
    [Start_Date] datetime  NULL,
    [End_Date] datetime  NULL,
    [Priority] int  NULL,
    [Status] varchar(50)  NULL,
    [Parent_ID] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [User_ID] int IDENTITY(1,1) NOT NULL,
    [First_Name] varchar(max)  NULL,
    [Last_Name] varchar(max)  NULL,
    [Employee_ID] varchar(50)  NULL,
    [Project_ID] int  NULL,
    [Task_ID] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Parent_ID] in table 'Parent_Task'
ALTER TABLE [dbo].[Parent_Task]
ADD CONSTRAINT [PK_Parent_Task]
    PRIMARY KEY CLUSTERED ([Parent_ID] ASC);
GO

-- Creating primary key on [Project_ID] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([Project_ID] ASC);
GO

-- Creating primary key on [Task_ID] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [PK_Tasks]
    PRIMARY KEY CLUSTERED ([Task_ID] ASC);
GO

-- Creating primary key on [User_ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([User_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------