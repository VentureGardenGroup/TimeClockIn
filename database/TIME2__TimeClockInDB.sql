USE [TimeClockIn]

/****** Object:  Table [dbo].[EmployeeClockIn]    Script Date: 18-Apr-16 8:40:23 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[EmployeeClockIn](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeUserId] [nvarchar](50) NOT NULL,
	[LocationName] [nvarchar](50) NOT NULL,
	[ClockInDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EmployeeClockIns] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[EmployeeLocationDetails]    Script Date: 18-Apr-16 8:40:23 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[EmployeeLocationDetails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeClockInId] [int] NOT NULL,
	[Latitude] [decimal](12, 7) NOT NULL,
	[Longitude] [decimal](12, 7) NOT NULL,
	[Address] [nvarchar](150) NOT NULL,
	[LocationName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EmployeeLocationDetails] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[Location]    Script Date: 18-Apr-16 8:40:23 AM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Location](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[LocationName] [nvarchar](50) NOT NULL,
	[Latitude] [decimal](12, 7) NOT NULL,
	[Longitude] [decimal](12, 7) NOT NULL,
	[Address] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 

