USE [master]
GO

/****** Object:  Database [TaskManagementSystemDB]    Script Date: 29-01-2025 17:16:55 ******/
CREATE DATABASE [TaskManagementSystemDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TaskManagementSystemDB', FILENAME = N'C:\Users\prati\TaskManagementSystemDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TaskManagementSystemDB_log', FILENAME = N'C:\Users\prati\TaskManagementSystemDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TaskManagementSystemDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [TaskManagementSystemDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [TaskManagementSystemDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [TaskManagementSystemDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET  DISABLE_BROKER 
GO

ALTER DATABASE [TaskManagementSystemDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [TaskManagementSystemDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [TaskManagementSystemDB] SET  MULTI_USER 
GO

ALTER DATABASE [TaskManagementSystemDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [TaskManagementSystemDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [TaskManagementSystemDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [TaskManagementSystemDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [TaskManagementSystemDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [TaskManagementSystemDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [TaskManagementSystemDB] SET QUERY_STORE = OFF
GO

ALTER DATABASE [TaskManagementSystemDB] SET  READ_WRITE 
GO

USE [TaskManagementSystemDB]
GO
/****** Object:  Table [dbo].[ActivityLog]    Script Date: 29-01-2025 17:16:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
	[ChangeTypeId] [int] NOT NULL,
	[ChangeDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChangeType]    Script Date: 29-01-2025 17:16:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChangeType](
	[ChangeTypeId] [int] IDENTITY(1,1) NOT NULL,
	[ChangeTypeName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ChangeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ChangeTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 29-01-2025 17:16:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 29-01-2025 17:16:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[TaskId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[AssignedUserId] [int] NULL,
	[StatusId] [int] NOT NULL,
	[DueDate] [datetime] NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskStatus]    Script Date: 29-01-2025 17:16:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskStatus](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[StatusName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 29-01-2025 17:16:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[RoleId] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ActivityLog] ADD  DEFAULT (getdate()) FOR [ChangeDate]
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[ActivityLog]  WITH CHECK ADD FOREIGN KEY([ChangeTypeId])
REFERENCES [dbo].[ChangeType] ([ChangeTypeId])
GO
ALTER TABLE [dbo].[ActivityLog]  WITH CHECK ADD FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([TaskId])
GO
ALTER TABLE [dbo].[ActivityLog]  WITH CHECK ADD FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([AssignedUserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([StatusId])
REFERENCES [dbo].[TaskStatus] ([StatusId])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
/****** Object:  StoredProcedure [dbo].[usp_GetDashboardSummary]    Script Date: 29-01-2025 17:16:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetDashboardSummary]
AS
BEGIN
    -- Return total tasks
    SELECT COUNT(*) AS TotalTasks FROM Tasks;

    -- Return task counts by status
    SELECT ts.StatusName AS Status, COUNT(*) AS Count
    FROM Tasks t
	INNER JOIN TaskStatus ts ON t.StatusId = ts.StatusId
    GROUP BY ts.StatusName
	ORDER BY ts.StatusName DESC;

    -- Return task counts per user
    SELECT u.Username AS UserName, COUNT(t.TaskId) AS TaskCount
    FROM Tasks t
    JOIN Users u ON t.AssignedUserId = u.UserId
    GROUP BY u.Username
	ORDER BY COUNT(t.TaskId) DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetTasks]    Script Date: 29-01-2025 17:16:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetTasks]
    @Title NVARCHAR(255) = NULL,
    @StatusId INT = NULL,
    @DueDate DATE = NULL,
	@UserId INT = NULL
AS
BEGIN
    SELECT 
        t.TaskId,
        t.Title,
        t.Description,
        ts.StatusName AS Status, 
		ts.StatusId AS StatusId,
        au.Username AS AssignedUser,
		au.UserId AS AssignedUserId,
        t.DueDate,
        c.Username AS CreatedBy, 
        ISNULL(t.CreatedAt, GETDATE()) AS CreatedAt
    FROM 
        Tasks t
    LEFT JOIN TaskStatus ts ON t.StatusId = ts.StatusId
    LEFT JOIN Users au ON t.AssignedUserId = au.UserId
    LEFT JOIN Users c ON t.CreatedBy = c.UserId
    WHERE 
        (@Title IS NULL OR t.Title LIKE '%' + @Title + '%') AND
        (@StatusId IS NULL OR t.StatusId = @StatusId) AND
        (@DueDate IS NULL OR CAST(t.DueDate AS DATE) = CAST(@DueDate AS DATE)) AND
		(@UserId IS NULL OR t.AssignedUserId = @UserId)
END;
GO
