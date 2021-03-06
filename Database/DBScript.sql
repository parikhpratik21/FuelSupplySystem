USE [master]
GO
/****** Object:  Database [FuelSupplySystem]    Script Date: 10/23/2016 11:33:12 AM ******/
CREATE DATABASE [FuelSupplySystem] ON  PRIMARY 
( NAME = N'FuelSupplySystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\FuelSupplySystem.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FuelSupplySystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\FuelSupplySystem_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FuelSupplySystem] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FuelSupplySystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FuelSupplySystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [FuelSupplySystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FuelSupplySystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FuelSupplySystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FuelSupplySystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FuelSupplySystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FuelSupplySystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FuelSupplySystem] SET  MULTI_USER 
GO
ALTER DATABASE [FuelSupplySystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FuelSupplySystem] SET DB_CHAINING OFF 
GO
USE [FuelSupplySystem]
GO
/****** Object:  StoredProcedure [dbo].[Fetch_CreditHistory]    Script Date: 10/23/2016 11:33:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Fetch_CreditHistory]
	@KeyCustomerId as int, @StartTime as datetime, @EndTime as datetime, @UserId as int, 
	@CustomerId as int, @PaymentId as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
   
   If @KeyCustomerId > 0
   Begin
				SELECT        Id, Time, FuelStationId, CustomerId, UserId, PaymentType, CreditAmount							
				FROM            CreditHistory
				WHERE      time > @StartTime AND time < @EndTime AND  (CustomerId = @KeyCustomerId OR (CustomerId IN
				(SELECT        Id
                FROM            Customer
                WHERE        (KeyCustomerId = @KeyCustomerId)))) 
	End

	Else if @CustomerId > 0
	Begin
				SELECT        Id, Time, FuelStationId, CustomerId, UserId, PaymentType, CreditAmount
				FROM            CreditHistory
				WHERE      time > @StartTime AND time < @EndTime AND  CustomerId = @CustomerId
	End

	Else if @UserId > 0
	Begin
				SELECT        Id, Time, FuelStationId, CustomerId, UserId, PaymentType, CreditAmount
				FROM            CreditHistory
				WHERE      time > @StartTime AND time < @EndTime AND  UserId = @UserId
	End

	Else if @PaymentId > 0
	Begin
				SELECT        Id, Time, FuelStationId, CustomerId, UserId, PaymentType, CreditAmount
				FROM            CreditHistory
				WHERE      time > @StartTime AND time < @EndTime AND  PaymentType = @PaymentId
	End

	Else 
	Begin
				SELECT        Id, Time, FuelStationId, CustomerId, UserId, PaymentType, CreditAmount
				FROM            CreditHistory
				WHERE      time > @StartTime AND time < @EndTime 
	End
END
GO
/****** Object:  StoredProcedure [dbo].[Fetch_CreditHistoryBy_KeyCustomer]    Script Date: 10/23/2016 11:33:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Fetch_CreditHistoryBy_KeyCustomer]
	@KeyCustomerId as int, @StartTime as datetime, @EndTime as datetime, @UserId as int, 
	@CustomerId as int, @PaymentId as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
   
   If @KeyCustomerId > 0
   Begin
				SELECT        Id, Time, FuelStationId, CustomerId, UserId, PaymentType, CreditAmount							
				FROM            CreditHistory
				WHERE      time > @StartTime AND time < @EndTime AND  (CustomerId = @KeyCustomerId OR (CustomerId IN
				(SELECT        Id
                FROM            Customer
                WHERE        (KeyCustomerId = @KeyCustomerId)))) 
	End

	Else if @CustomerId > 0
	Begin
				SELECT        Id, Time, FuelStationId, CustomerId, UserId, PaymentType, CreditAmount
				FROM            CreditHistory
				WHERE      time > @StartTime AND time < @EndTime AND  CustomerId = @CustomerId
	End

	Else if @UserId > 0
	Begin
				SELECT        Id, Time, FuelStationId, CustomerId, UserId, PaymentType, CreditAmount
				FROM            CreditHistory
				WHERE      time > @StartTime AND time < @EndTime AND  UserId = @UserId
	End

	Else if @PaymentId > 0
	Begin
				SELECT        Id, Time, FuelStationId, CustomerId, UserId, PaymentType, CreditAmount
				FROM            CreditHistory
				WHERE      time > @StartTime AND time < @EndTime AND  PaymentType = @PaymentId
	End

	Else 
	Begin
				SELECT        Id, Time, FuelStationId, CustomerId, UserId, PaymentType, CreditAmount
				FROM            CreditHistory
				WHERE      time > @StartTime AND time < @EndTime 
	End
END
GO
/****** Object:  StoredProcedure [dbo].[Fetch_FuelHistory]    Script Date: 10/23/2016 11:33:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[Fetch_FuelHistory]
	@KeyCustomerId as int, @StartTime as date, @EndTime as date, @UserId as int, 
	@CustomerId as int, @FuelId as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
   
   If @KeyCustomerId > 0
   Begin
				SELECT        Id, Time, FuelStationId, CustomerId, UserId, FuelType, FuelVolume, FuelAmount, KeyCustomerId							
				FROM            FuelHistory
				WHERE      time > @StartTime AND time < @EndTime AND  (CustomerId = @KeyCustomerId OR (CustomerId IN
				(SELECT        Id
                FROM            Customer
                WHERE        (KeyCustomerId = @KeyCustomerId)))) 
	End

	Else if @CustomerId > 0
	Begin
				SELECT        Id, Time, FuelStationId, CustomerId, UserId, FuelType, FuelVolume, FuelAmount, KeyCustomerId
				FROM            FuelHistory
				WHERE      time > @StartTime AND time < @EndTime AND  CustomerId = @CustomerId
	End

	Else if @UserId > 0
	Begin
				SELECT        Id, Time, FuelStationId, CustomerId, UserId, FuelType, FuelVolume, FuelAmount, KeyCustomerId
				FROM            FuelHistory
				WHERE      time > @StartTime AND time < @EndTime AND  UserId = @UserId
	End

	Else if @FuelId > 0
	Begin
				SELECT        Id, Time, FuelStationId, CustomerId, UserId, FuelType, FuelVolume, FuelAmount, KeyCustomerId
				FROM            FuelHistory
				WHERE      time > @StartTime AND time < @EndTime AND  FuelType = @FuelId
	End

	Else 
	Begin
				SELECT        Id, Time, FuelStationId, CustomerId, UserId, FuelType, FuelVolume, FuelAmount, KeyCustomerId
				FROM            FuelHistory
				WHERE      time > @StartTime AND time < @EndTime 
	End
END
GO
/****** Object:  Table [dbo].[CreditHistory]    Script Date: 10/23/2016 11:33:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Time] [datetime] NULL,
	[FuelStationId] [int] NULL,
	[CustomerId] [int] NULL,
	[UserId] [int] NULL,
	[PaymentType] [int] NULL,
	[CreditAmount] [decimal](18, 2) NULL,
	[KeyCustomerId] [int] NULL,
 CONSTRAINT [PK_CreditHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 10/23/2016 11:33:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](10) NULL,
	[CustomerType] [int] NULL,
	[KeyCustomerId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[Pincode] [nvarchar](10) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[PaymentType] [int] NULL,
	[FuelLimitType] [int] NULL,
	[PaymentLimit] [decimal](10, 2) NULL,
	[AvailablePay] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomerFingerPrint]    Script Date: 10/23/2016 11:33:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerFingerPrint](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[Policy] [nvarchar](10) NULL,
	[FingerPrint] [nvarchar](3000) NULL,
	[FingerPrintType] [int] NULL,
 CONSTRAINT [PK_CustomerFingerPrint] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomerType]    Script Date: 10/23/2016 11:33:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_CustomerType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FingerPrintType]    Script Date: 10/23/2016 11:33:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FingerPrintType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_FingerPrintType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FuelHistory]    Script Date: 10/23/2016 11:33:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FuelHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Time] [datetime] NULL,
	[FuelStationId] [int] NULL,
	[CustomerId] [int] NULL,
	[UserId] [int] NULL,
	[FuelType] [int] NULL,
	[FuelVolume] [decimal](18, 2) NULL,
	[FuelAmount] [decimal](18, 2) NULL,
	[KeyCustomerId] [int] NULL,
	[KeyCustomerName] [nvarchar](50) NULL,
	[CustomerName] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
 CONSTRAINT [PK_FuelHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FuelLimitType]    Script Date: 10/23/2016 11:33:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FuelLimitType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_FuelLimitType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FuelStation]    Script Date: 10/23/2016 11:33:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FuelStation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](10) NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[Pincode] [nvarchar](10) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
 CONSTRAINT [PK_FuelStation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FuelType]    Script Date: 10/23/2016 11:33:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FuelType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_FuelType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentType]    Script Date: 10/23/2016 11:33:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_PaymentType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 10/23/2016 11:33:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](100) NULL,
	[Name] [nvarchar](50) NULL,
	[UserType] [int] NULL,
	[Address] [nvarchar](100) NULL,
	[Pincode] [nvarchar](10) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[ContactNo] [nvarchar](20) NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserType]    Script Date: 10/23/2016 11:33:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[CreditHistory] ON 

GO
INSERT [dbo].[CreditHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [PaymentType], [CreditAmount], [KeyCustomerId]) VALUES (1, CAST(0x0000A642016636F1 AS DateTime), 1, 5, 1, 2, CAST(2000.00 AS Decimal(18, 2)), NULL)
GO
INSERT [dbo].[CreditHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [PaymentType], [CreditAmount], [KeyCustomerId]) VALUES (2, CAST(0x0000A6420167613F AS DateTime), 1, 3, 1, 1, CAST(1000.00 AS Decimal(18, 2)), NULL)
GO
INSERT [dbo].[CreditHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [PaymentType], [CreditAmount], [KeyCustomerId]) VALUES (3, CAST(0x0000A64201676BD1 AS DateTime), 1, 5, 1, 2, CAST(5000.00 AS Decimal(18, 2)), NULL)
GO
INSERT [dbo].[CreditHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [PaymentType], [CreditAmount], [KeyCustomerId]) VALUES (4, CAST(0x0000A66A0166D8C3 AS DateTime), 1, 1, 1, 2, CAST(500.00 AS Decimal(18, 2)), NULL)
GO
INSERT [dbo].[CreditHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [PaymentType], [CreditAmount], [KeyCustomerId]) VALUES (5, CAST(0x0000A66A016C5C31 AS DateTime), 1, 1, 1, 2, CAST(500.00 AS Decimal(18, 2)), NULL)
GO
INSERT [dbo].[CreditHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [PaymentType], [CreditAmount], [KeyCustomerId]) VALUES (6, CAST(0x0000A66B01861CA3 AS DateTime), 1, 1, 1, 2, CAST(500.00 AS Decimal(18, 2)), NULL)
GO
INSERT [dbo].[CreditHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [PaymentType], [CreditAmount], [KeyCustomerId]) VALUES (10, CAST(0x0000A68601570EA3 AS DateTime), 1, 1, 1, 1, CAST(500.00 AS Decimal(18, 2)), NULL)
GO
INSERT [dbo].[CreditHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [PaymentType], [CreditAmount], [KeyCustomerId]) VALUES (11, CAST(0x0000A686016046DB AS DateTime), 1, 1, 1, 1, CAST(2000.00 AS Decimal(18, 2)), NULL)
GO
INSERT [dbo].[CreditHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [PaymentType], [CreditAmount], [KeyCustomerId]) VALUES (12, CAST(0x0000A68B00B2B597 AS DateTime), 1, 1, 1, 1, CAST(100.00 AS Decimal(18, 2)), NULL)
GO
SET IDENTITY_INSERT [dbo].[CreditHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

GO
INSERT [dbo].[Customer] ([Id], [Code], [CustomerType], [KeyCustomerId], [Name], [Address], [Pincode], [City], [State], [Country], [PaymentType], [FuelLimitType], [PaymentLimit], [AvailablePay]) VALUES (1, N'C001', 1, NULL, N'Pratik Parikh', N'Gota', N'382481', N'Ahemdabad', N'Gujurat', N'India', 1, 2, CAST(10500.00 AS Decimal(10, 2)), CAST(1665.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Customer] ([Id], [Code], [CustomerType], [KeyCustomerId], [Name], [Address], [Pincode], [City], [State], [Country], [PaymentType], [FuelLimitType], [PaymentLimit], [AvailablePay]) VALUES (2, N'C002', 2, 1, N'Chirag Shah', N'Satellite', N'380015', N'Ahemdabad', N'Gujarat', N'India', 2, 2, CAST(5000.00 AS Decimal(10, 2)), CAST(5000.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Customer] ([Id], [Code], [CustomerType], [KeyCustomerId], [Name], [Address], [Pincode], [City], [State], [Country], [PaymentType], [FuelLimitType], [PaymentLimit], [AvailablePay]) VALUES (3, N'C003', 2, 1, N'Krishna Parikh', N'Calefornia', N'380015', N'Calefornia', N'Calefornia', N'United State', 1, 2, CAST(16000.00 AS Decimal(10, 2)), CAST(14550.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Customer] ([Id], [Code], [CustomerType], [KeyCustomerId], [Name], [Address], [Pincode], [City], [State], [Country], [PaymentType], [FuelLimitType], [PaymentLimit], [AvailablePay]) VALUES (5, N'C004', 2, 1, N'Test User 2', NULL, NULL, NULL, NULL, NULL, 2, NULL, CAST(20000.00 AS Decimal(10, 2)), CAST(12200.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Customer] ([Id], [Code], [CustomerType], [KeyCustomerId], [Name], [Address], [Pincode], [City], [State], [Country], [PaymentType], [FuelLimitType], [PaymentLimit], [AvailablePay]) VALUES (6, N'C005', 1, 1, N'Test Customer 2 Test', NULL, NULL, NULL, NULL, NULL, 1, NULL, CAST(500000.00 AS Decimal(10, 2)), CAST(500000.00 AS Decimal(10, 2)))
GO
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[CustomerFingerPrint] ON 

GO
INSERT [dbo].[CustomerFingerPrint] ([ID], [CustomerID], [Policy], [FingerPrint], [FingerPrintType]) VALUES (1, 1, N'9', N'mspZFqfpp09VQRPST1CBGM5QV0EU1lBegQzU0GTBDNNPPQEPzlBEARLR0TWBEszROcEV19FEARXYP3uBDzxAgAEPNMB', 1)
GO
INSERT [dbo].[CustomerFingerPrint] ([ID], [CustomerID], [Policy], [FingerPrint], [FingerPrintType]) VALUES (2, 2, N'9', N'mspZVneBpLwwwQvPQTNBC9RDM0ER3i1VgQNFsFpBAkSjZEEKPCNswQk2rz4BB0pEaMEP4pZIwR8FGGbBCS6rdEEKNEJHwQ', 2)
GO
INSERT [dbo].[CustomerFingerPrint] ([ID], [CustomerID], [Policy], [FingerPrint], [FingerPrintType]) VALUES (3, 1, N'9', N'TS5TUzIxAAAEbXEECAUHCc7QAAAcbGkBAAAAhJAtkG21AOIPegABANZizQCXAPMNvACBbd8OaQCAAJsNmW0JAd0PhgDSAVNi1gAKAfUPtgBWbd0PrgBHAKwN421bAO0NmgD/AF5gUgAvAVUMmQA2bV0KUAA+ARMCe22yAFQPfwBaANpizQCWAPQNqwDpbVoPXACLABsNpW0QAVYPjACdANli/QCmAIIBZgBMbeYN0ABRAK0P8m1jAPUGRwDdAd9kSgAoAdoMYAAmbXgMqABTAY4JmG3ZAOIPzwB4APFijwD2AGIPQQD9beMPygB4AC8O8m2fAAQO8gBCAPxj+gCUAHkHBABJbeQP8gAOAccPIW2NAFsLiwD6AUpiNwApAUIMggAybMoC7QBEAYgMJfxDBAoQeYFf8HKYTQBOCMoC9IDdbhsAMYCFgI+C0OZIfg2LEX/kA+4ZkPcHog8baANK746ElIclhXQSrZUkhiIKUXT48v2OyX+VjHWAi4JH7ioHSA91gKT+fejseTF15Y2cA8KRzQBKDMoC/Ho2lfOLhYCaAH8C3BagA9L0LQNI+3LppYO9CbKMUYNF4QAPpYQpfmMBJWXT/R6P3QDAmtXmxYfxEF0AnCNO+z8Szv3z9yb39u1XATYSSgaPAneclYNBhkYMUYMx57QH1vjW+JuGpOpRjzUOsYAE9roVXXexgEX5YHwtECMABQ/DtjqvIxMe/yN/TgUr+gOUPGlZeSFqEfelFYDw+07nW2oHdLA1S0t2IDjEAilzNg8AkBRXOmpQNcA1DQCDFZL/fybAwHEGAJvTVsSSZwcApRZT/cH6YAF2GFyF/pz/Rm4BtxxgwArFZRo6/FxVxAYABCBmrMHC/gUAy+FkxlMLANMpYGSAWR5t3i5mwcDD6MBdCcH/wUV+ksAARV5WPwkAWzWZwPsJWAQA4zVgOv4LbZc7XlbBUjpVU2oBnD5cZGHcAPcuZVlVZXBkBf/7rcT/fxcArYxkxZOCwjJdwHE6wccuFgDOUGvBBXvEr1dYQ4PDwMwA1Dhj/8DBwMC5CwQdWVxkVcDABwwEjl9pwlRzwk0EBIJlcIMaAQqvd/uvw1lndP/Aj8H6yYILAQ1yervAxq1zBgENe3pAwg1ta4RX/cJYlwYFe5WTosATADiVeRPAd8B2wcE4wVJnAcWYaWt+khUFfZmAw8BncUT/xa1VhQkA/KW/dWOsCgD3rXqABcHErXcEAPezd5kMBBK0WsL9wv4F/8SoVhIA97t6B/6SBMM2wcFLEcX6xxd1w3HBwEUFaBVt9sx6hcKIBFDErVgJAPfVd7bExKzACAD333q7w29oAWrvXsFawwB0nFtYwQcAjD1ifwIMARD8hogGfIGsBBEQA4OAzBDvY3abwYIHEFsSXhlgBxCjFFM6wceS/gYQhxhTgMANffYZg6eNwsIQ/UmCw8TDwsTAEARDNXQGEPgxScbCxQUQ7TdrscgQizrRwv7+wcA4wPuQwf4MAPfxv4hzr2c=', 1)
GO
INSERT [dbo].[CustomerFingerPrint] ([ID], [CustomerID], [Policy], [FingerPrint], [FingerPrintType]) VALUES (4, 3, N'9', N'TeFTUzIxAAAEoqEECAUHCc7QAAAco2kBAAAAhE80kqLMADEPdwANAD2txQCsAMIHfQCUoj0GwwD1ABEFtqJ3AE4GTAB9AEytXACKAEsPUQBsolkMxwBtAOgGnqIeAZENUgCqANegpgAtAYQNoABSouUFyQA5AbAK6aIpAfUCKQDkATqmMwA0AfQCYQDJoikPjQDjAOoPw6LSANMHYQADADqtyADsANgKGQCTokcKvAB2AN4G76K8AFAPtQCpAGKkswBgAGEK+QCcok4HBAHFAIkOXKJgAFALugCNAH2puwBBAfQNxQEao/ADtgBVAbsCk6KgADcPugBnALirxACgAL4JAgCPokEGtwD+AHcDpaJuAOYOvgDHAeGkkgARAaMLjwDhokYPfQBiAJwMSKIBAUcI+QBWAEStkQBJAGcIpQBIouEFYQA2AbsPZ6I3AFwCQQD5AfqpUAihBtb4CP5Kqm8NgAdhCKuIPV6AgWmBhP848BWGMTcA68VplA7iW3sD5Xum/Tf+Oda0Ah74uRQQycFAnPARDc7juITZKYvwnfUdk2PhTKUAlLGPeYCj+Z5TeAX6gvsCtE1i4yb4ofg9uUp43Vq7+GJVIQLzCPJb6P7yATatgAbZWJ/6AAf99qAG1aisijUL5M/Jt01SPAfiDpp/rRtE7dEHVP9hFkR+ZqsjBQd8wf8qDd+pgHwVbAFykBpKtztOeYDVeehz+V/+fyMPzY0bCbJdT302/Y4LeQPhXoCBPAcE/6yFsaaEhe0m5M5w4LV8MJCtmZWB9TZpsnAODRQGIQ/yfaEXCv4DMQJ4dwZW+AOfCGvLeP+KUcoBVhB2EPcKyF1khvGSfoBCjLOZaH5ieHIP3Ax6IifBCmm5IP77D4I0AQINGpzBAGi/ccLBBgDP4QkvXQUAejF0xQYLBB4oBv/9VP4E/PimAVA+YsPByACz63uIiXVZEcWLTtOSdMLBVUwEEAQxSu3/+zMvO/8xYhUAi09kwASFxtHBwf5e/sQ6BgTndlBMwwQAolLkmwgAyDQQ/4UwEKJoV+bB/f87wf9iMkFG/MIExTx89FYOAK5iaWeGb2F1DgB7ZWJXwftg/md8BhBn3RrHXYgIAJNqXlXCeacBtG1pogbFpmpL/PkgAwBPs1PFpwHLhjD7LM0AqtpSwsLDwcEFBwQReknAxMDDOwQFt4I9WgoAXEhMaPJYDQD6lDc4Szhd/yYFANmZg/5ApAHemT0q/8MAPzhRwMBZBgCFnE1cwEYJAJWj+IXHYPv/BgCbo/KAxqsBTLxJWDM6BQRIwEwpNAEQBENFYv1AMf/8weYxX+L+/v7///wE+cVZ/v///f7ABf/7XP7+//4ZAdDBOV0x//tLPUA7/ihiQwsBAMhM+P37XfwfEwEGyYNA+Vw6/v77/y46wQ2iZco9wIDDUAYE1809wv+dBcWXypP+xPwEAKYVKcZZBQBJ5UMk2gDNSdL+wcGQw7ChxmfCeIN7wcIGwQqzDQteK/8/BSv7oRAQH3fABNUAKpWCDBDYQ3r4XcTADRA1NfT+BPrG9IoEEM8495IFFa4tHvg4CxBwUIRdXf9dww==', 1)
GO
INSERT [dbo].[CustomerFingerPrint] ([ID], [CustomerID], [Policy], [FingerPrint], [FingerPrintType]) VALUES (5, 3, N'9', N'TWlTUzIxAAAEKikECAUHCc7QAAAcK2kBAAAAhNcssyrPAOgHsgDHATAl3ADjADgIJQDIKn8G4AD7AHAG2iqqABEHgwBmAOog6gCxAJEMBwAqKx8GagAaAYMPeCorATwPzQC+AAEnBwEbAU4MowBAK0QPvgAzAMMPqSrEAFYO0gApADEuqACnAGgLTgCvKmEK0gCpAL0NbSrUANkPcQB3AF0jkgAoATkPpQCwKt0IVwD7AIsPfCqMAGMLvQCEAagi3AB8AH4NIQBUK4kJkgAtADYJlirIAFYPywB7AGcs2QD7ALUGsADrKkgP3wAJAXwBkiqYAG4L1gDdAbYotQAuAS0OugCXKl4L0gAsAWsC5CoqAdADqgCCASAhxwBXAZsBQgA7KnAKOJd9h9uVQqg3AmaHrQTIvoGsSEqdHtHviQIV1myB9JuBgBSzsLlIkIGBgYC4G0U95PBdcK11nA7i038DQQbh+mf5eaK6jK/1ooIDl7O2GwkCAycNxxf3vLIDSHOlAyPwNdZ8gG2Btf6TFJIiwXf59c0D1WtRPBTr1ZNe/beBoVYMbh37xf6XDWo8oITaBZLy4wRaIwd3OAZNduv0pbtIiYWBhXiKCEdGc+y2uabeIIWSxbbzJQPi+Pdx6cQ05oF/zAI8/Hmq5HOq/IIHSARxLhwIaRGBgegKodIYBf3lVRBEgaEhvf5ZcC3yFOBJupz9LSRdIeu0VVMs+3KIlfIbFHpS5IELblsTlm77QVn6IDkBATMYAS0BiCbw/j86CAS/MfrBNV8DxY01XcAEAIM+d1cMBKFB8Cs3VP7AAIBuanIRARBUSnt068DBwcF7w8oBFE6WiVqDccLXARRFkcDC/8CDvcHFW8AHAMh7gAdn+ywBz38DMP/WARRVm8NiwmzAmcGP6BIBEIqTYkxycunAwFwHAHRLa4PowwcAdpJksXcOKp2X8P7/LzlTCisOopp8wsEFi4g6AaWncMN8BcL76UXAwcEHAGin9NT8/jMGANFtgJnpCACPqe3A68AuLgGHq2SLD8WlqE5odIQ/wgTFibRIw8AEAG6ym8LEIwFytlrCZacEBHe7VmYMAI4PWsasUHQIAKrKmZCHIgGSzVN8wKoHBMrMicXFxIXCALjk5/74/SMNxbXQecCIwMHAwwD/xikBZtlPwAzF8eyHxcXGxsSSB/4CKvLtQP/+PskA/d87/S7/9Pw7//o+AfjBnsT+DYaIqmb/iAQAzyo6wNYEARDwQ1nGAHDYTcEEARb4hUoHKlX9U8IGAJ/9SOr+cAgQsAT4h8aNBBAYCFOIyRDXP6z//vz79T/6LiwRaBxMXcDAEGk2QsD/xAQQVCw+UgUQmCw0YMEQBAQ2jQQQei6GfwA6gS49cAYQBjAg6MasBRD6KBbDxOkQAPjTpsIHxKHoaMP/fw==', 1)
GO
INSERT [dbo].[CustomerFingerPrint] ([ID], [CustomerID], [Policy], [FingerPrint], [FingerPrintType]) VALUES (6, 3, N'9', N'Sp5TUzIxAAAD3eMECAUHCc7QAAAb3GkBAAAAgwAijN3TACALewAFAFnWZwDFAFMNoAC33WANuwCwAOAPzd2vACUPTwBnAGXSWQAjAa4OBgA73AwCRwA5AekM9t2FABsHmAD8AJPceQDaAC8PtADm3TEPYQDnAPgPvd3CADMPqgDfAQXSzAACAfIDWgAy3BECnAA+AdwBmd1oAIkOsgCXARLZIQAyATEHSgDx3bAJqAC7AO0Jnt2vAIYLlwBaAITRQwDsAEUPiAAP3C8O4AC2AOEPV92IAHAPXwCAASHS6gAxAd4EYwBJ3Q8E1fIBy2dwnte8+m0v7fTg+lL6XBA1Cr3/nAGWKMT+2QFtAXD35ic4B0YXgYLohwlQz/z+/RoKrA8yXt/+t/rH+gr7FlsyCDMLqAOAEhYKKQPN9GKAhAwyzr/4CRHdDuwTzifL7p769u8S5rjLuftFDnoF9QuF1TsWyny3+P4GDSVLBBoCWgaChzTak5BigHKEOKRG22j2rQAdYDtgKt0nYA5mNgirGTTJgIIG6SrzKAHNLOcBWQ5DEIIf8iYXdRP3HtKK7gwSGILChLuLmYbZO+EgNwECIt44BN2TFiKZRQTFYBogKgQATSz69wQDazQQWwYA24mPw7YEADgxVv4B/A7dVjUAP2I2jxADmkEASv7D/DjDwx1F/wgAq0rVZcPlEQBHVvf+Ok9JksA1DwBGSzhLwSP//2RGCQBTaooddoURADdmNS/8IzZH/0QMAFOAgITAwcHBiATF+YDOJhIAMHjxOsD9kT//PcBlCsWVhtsq/1fACACXh3OxwIELAFqIMv79HP1dWgsAVEhtch/+wsNlEQD9fvPy/jVXRMD/wwCbQI2IwQ8ATmRwkRzAdMP/ehXFlqBbeIzBwsF6BcLDHsBZEgCepNUp/CP//P/3/P87wP/+DQBPpmnCRcDBHcN0DgCXq0yIlR7AbsIWANB3IPzr/P3//P/6Ov//I8P+//3//skAZm5qw8LAg44FCQOwsvf+wP/8O/z+zwG/tBwxLDn7KR39/yYDAONzIMPZAZi4fWwOxWW6v5d+nsFuBcXguvTAMgQQ/AiJKQ3NxjF9/Ef+9R7+0QFjxVqJwgDAgiIEAMDFK/7ZAwMe93r/DABoDVDCI8XEwsDDwj/ABd121tP89/49BgND1qTIzLEHxWDpnsSAwgcAQjRJkVQCART8Ov/iEK7BB5vEw8PEsYzCHP7CwsDDwbLCw1XB/8LE/v9UBhPsHDfBwz8E1QAz6XoEEJ80CYcEE/k0MYwDEEb5MMPeEUs8Iv0D1Z890cADEJtBMQA=', 1)
GO
INSERT [dbo].[CustomerFingerPrint] ([ID], [CustomerID], [Policy], [FingerPrint], [FingerPrintType]) VALUES (7, 2, N'9', N'TEhTUzIxAAAFCwwECAUHCc7QAAAdCmkBAAAAhDYxpAt8APAPZwC6ANsGdwDMANgPGQCtC/4P4AB5AD4PTguNANQNyQCUAHEF5gDNAAoPpwBMC90L0wDvAPEOjAsRAUAP+AAqACIEsAAYAOkOXgARC2gMewAOAKILVgswAUAJ9QCCAdsAcwCfAN4PowC7C1sPegDUAJEPwQtbAPgPzACkAHAF0gDZAAwOpADaC1UPwABFADIP9gttAHUN4QAsACAFwgAhAO8NWwAcCisPjQASACIM7wsKAVMN3wDZAdEHwwA7AfcOawDCC+8PnABZADUPwgvEAPsPWQAHAN8EUAB+AE8NigBoC9oOQwC3AJ8PnAs8AOYP5ACRAP8HZgADAUwP8gBIC1wJKwBYABgNXgsgANsOcQDKAOMAQQAcANoPHwAwCnYNTgFz+L59IJk/BBIM1IaAgQiM3wW29kcEqIovg0sBfYSe+Lf4ZXSwfEUA4QIE5wcbXwL6AiKD+BAIIXPhURrS9T7v4A2v1Nr1rf3kghsOnIQhgqIEUYMgiiMAE/UrA/IbO2NSDC/p8oSTAfsMmIBFhUoIHX6YCNP8XIekh4QBrXTEAPl/melw2mQNHAMRf+J8dITECruEdX61f18CyPv8GWUCJQkT+RuA6O53NefjZIAgihwDhYECLCMxgIr0Xg7fgYKvJy7GkPam5gueiA/LCpP6sfIBDlfwnIs4gt6IgXwodDB54IzC/O4I/II3Cu+L1fum8pt+CIDwh7GHRg0r+YIGhIBKhiKDQIC/DMaSLQOJ91d1nYigAwEPrQMqfZr+gILRpiKYY6BbpauoACBGAceaJJUHACwAVsA7hA0LOwBcwsBDBwMFWAFiwgMAWsVX+gwBbABXZUrBAH0LYXYRAJEAp21iyfzBVWIRAFgAYcj/WcFdwMCdwAYLqgBmwAwAfABsjFDAwHcUAA8Abo5swGXCUlg6CQVOAF7DRsFVyQBkCFZEwcFVbsYAyghj/w8A1gyuwGvKwf9iwY4OxXsKacBf/8NAwJgGBZMUYsBiCAD4Il/K/lPCFQDm4XdZycD/eMCE/p5xAQu8JW1+DQArLHJscMBnwBIANDV4yf7DWcJawAVwxQ4B9Tx6XAjF+kd8wcE6wwUAOUp4dAYAOVBT/AfB+soXAMpSd8GqacXJWcIyxMFBwAD+XHtYDQEDXL/AxY59dBEAv12xwcTJZ37/XVUJxcxncYLBwWkJAc9kf1FxwA0A7224w1f0jcH/wgQByW54fgUBEHh9ZdQA3nF2wHaQhP6nwR8KF4eQxMDAB27H9cPBiVVcwTuFCwoRi4PAcIcEhMcOAUiRV8A7yAEVmIFiwm7AkNEA8pF8wHd1eVIEcsUIAW+lWsATxfekdnXCwcHCwQTBxcnBwf9/FAAysIOLkHjCwMDBBMLE9cUDAEa6VjoGBfy3g3nBBgCmv1tgwwYAaMJanMAXC/a9gP/DwzrAwI/AicA+BAAPxv81CwDgzYCOWMPEAQF31lZraZUEBXbWUGUEANcZCfvKBABg4VNZzQDZ7I3ExMbEp8YA4ewb/goQ+SObO/n0/BsGAMftp8fBxc0MAOLtIjj8+/H8/cD8GxPF99uEwMOPw8LDAcPDyMfEwsfDBcXT9yb6+vwGEPr8XPvL+sADELtYQ/wAGhQBPf8fBdVmAEdmBhBrB0kFfAMbiRVAc8EI1fc3a/4qHwMQAPQwxQ==', 1)
GO
INSERT [dbo].[CustomerFingerPrint] ([ID], [CustomerID], [Policy], [FingerPrint], [FingerPrintType]) VALUES (8, 2, N'9', N'TSxTUzIxAAAEb3AECAUHCc7QAAAcbmkBAAAAhJIpj2+2AOUPvgC8APFgaQCjANUNqwCEb+AP7ACIAL4Od2/1AFcP5gCuAPhg4AAAARAMiQBvb+IP1wAcAZUMvG8uAOgMnQDsAOphyAAwAc0NbwBdbhoGvwCeAD0Pc2/VAGUOwQAtAPFg2ADoAAEPegBYb+0P9ADQAMMPaG9iAOQLXQCkAGdhhQA4AOIPKgAUbigMxQAzAK4Mo283ATAPlwDbAFtl5ABQAXIFRgCRb+QNbwCVAJwNa2/XAOUOWgAPAFhhggBfAOMLOwClbwcGeABbAKYL/G/yAA8PcwDeAUpgmAApAUIPYwAvb3EOawAxABoP5m86AdENUgEzgTZ9TAvO/X77RHyen3f13Yld/wf8uu8vdJeI5nQ39G5lv4AWE/oLUOkhfVwDiYGWAHMGjekcKwJApXzkeSWQnYI8k50LSH7BkC+dIu0b2YZRTmp2ffcXkIJvCGZ/jPam5pfqxA61nY/01ftegmPzspbfBUcAhYDkgJFtJICJgRUD7AeNmHuE+RnF2+Mcpeuj/KJ+8fGPZYp6TXNB8X58DKG2yMYmHY0+BM8QeesodeV5kIL0itL/OHoy+PaK6YMRbR+CIozHAO4PKOyEgJUD3QN76AZ/8/pCDkcjmBXKmUZ0mIYZiWgRqZDXf5OA6WGEB2bvAHbjuq8I5T8FbV4ehQUAjc1gxTUKAHoMXGc7UWRgAaUMZMDAB/53HFgKAGwNWgXAxCx0BQCTDl4F//tqAa8QYsFJxgC+fWHACABdFJvB+q/AhggAxhWnaMQBCACXH2BkkcEHb9kiaf8LABspYq9SwVLCDgBgLGKv/sLB/8P/O2vFfgHXOWnC/gRaYa7AVv8FADMLTFxpAbQ0aWvDwwDHWmhZwAgAZ/NexK9SwQoA10WswU6vaAUA70VwBcL6fAHnU3RtwqfBxipDwMALAP6denSuXXMKAHpepWRSCBEBAGF9wgT/xRlywsBwBwCYZGQ9aAkAf2ReBV3ENgcBBXJ9WgcUBWd6ff/+mYVGZMaRZBQBCYSABf7H72bBwcD/w/rBDW/qiH3BwXepCwVni31qwINuwgEL/nzAwMJZB8VrkjXBVMESAQldfVive3TBw/zCpREElaN9wMHBwE6MMqz+AxBUF1cHFwV/r4ZvwWrCB3TEAmXE/hYBEHODxK7Ae8LCwcEG//ut/sB0wxUB1cCCF4STwcP/wAfB+qxeFAEQx4MFb8XjwMHBwMLAocIBb1fLXsDBwcAAX6JWURMBEM5DfsXjgsB+bwYAsNZkHMASARDciQSBma6CwMHBhQfEEOfmbocFAN3lOC8Db9Tpd8HFfMAA2YQB/zsEACw7TPisCABy9lfAoYsOb/b4jI3DwgeQDH/rDZPExsQArRR/9gGMwsGpAcLBrsnEwv/EA9XUFQnGBBDjEyI++wF/3hct+hgD1dsbL/gFEHIgTwRuAH+XLEmHBBBeLEQGAhArOE/AwxCuVizCsA==', 1)
GO
INSERT [dbo].[CustomerFingerPrint] ([ID], [CustomerID], [Policy], [FingerPrint], [FingerPrintType]) VALUES (9, 1, N'9', N'SuxTUzIxAAADr7EECAUHCc7QAAAbrmkBAAAAg1Igd6/0ABYHaQANAAagjgASAS0PYgADriIPpgC2AEwOPa8wAUkPqACEAUimVACDAPgP2QA0rkgL7AAkAWEFe69XAHoOYADHAQOslwAAASQPgwD7r+sPowDQAEIOr68qATELugDlAaairQCWAIkNkQBXrsoEIwBLAQ8IFK9/ANYLGQCzAOqnhgDMAIYOXgDXrwsOcQCzAMMPp6/DABUOIAAZAOOgmQCRAAkNcAA8rjoGpABTAYkDFK9EAckKVACZAPul1Bp69Y7qjAKBL7LwJQk6DLf+3q/H+IqBPHf4B0UvswGagWKC9ROCrvweSgK39qKChi9ogTIBYwGOa1jYg4VjhjNzrO4StI7VXQCt+S8RpkCW1DMMaH54dL6sNI19+pnqhHUhw4eFgYFCAId3YSk+AUMHSAOsgZkude6v45fjJRagXIrzCX4BA0eAZylI9QWGyQJbgFxSMY+xkMENevi0TJbfgYGqgmr1w1z0DqHz/QHUE/2xzQOFgKaDR4HJrfoTgAIgB+U1Aq0MHXQJAFOBdMHXwXcIAGlGssLB4cEGADBLaQR4Ca+EUIOCwf+uAwPZV3rBDgCVn4PAb8FxwVLCwcoApMeNkU5zwXzKAJzCiMNqeMFuQw0DMHKDeIT/wqMJA/iD9Dj/QRDFx5E8wv6Dc8NqshMDYp6WfsNahARvebsB1quQwMAH/8LXgnSJwAQAsbIA/g0ApLSJbAXBwm7AiRQA17RWXHPRdsP/w8BbwgBvGXt3iQQAdH0DPakBq7oP/8CVFQNywZZ7eMHBBIWRb8NOBgCowcr+w88GAKfGF1gFwACvacn9/RcAIdGfbnlyi3/CiDpvEq+l0on+wcFSdX3bCgCd1A/+Ov/C8SkGAB7hWrvABa919w/+/kDJAERW4/39Kv81BQ4DPv2TwaCgwgX+dacRmQEg/8AEUAu/pgEe//9fBwgT8wJkosIuCdVkB6z9/CwwBxBTBCf3SxoQ+gaXnkqA2o3CwIt0wsgQiKGfwsDHw8RRYga+FA49VQQQVhQlgQcQYhVXw5kFE8EXHvskBhBKFipS/iIEEQ8WjH8Gv2kYUMFdBNVtHp8mGhDuH6S9UsIug4jBwXjAtQQTxSJDNwMQr+gpw6sRPzNJVRjV6T0zSnTCwISfB5DB/hsQE1LD/ztd/J41/v/+/0SJXBq/5jGgwFjBvYbBa8DBwoNzwMsQRfjNwVP8+Pw///lv/A==', 1)
GO
INSERT [dbo].[CustomerFingerPrint] ([ID], [CustomerID], [Policy], [FingerPrint], [FingerPrintType]) VALUES (10, 3, N'9', N'SllTUzIxAAADGh0ECAUHCc7QAAAbG2kBAAAAg8cashrLAOwNswBqAPMVxQCtAG4PFACNGukPqQAhAa8PWxquAOQI6ACwAGUXtAA/AWEPLgBHG/wOqwDhAK4MpBqqAG8PlABoAO0ViAAOAWgPWQAdG+gP7ACCADcNmho6AWMP9QCmAOIRQAAwAdsCewDmGu4OygCxAC4PvRoJAfAPugC6AOUVYACfAGUOdAAyG+gPqgBcAKIOmBpKANwH4YKc/8HmHYONhJUG6YaOmpQDMQeK+C99o5lEgW4IcYFa+5xtTHT1hnqBEHimGdP4C54TF96XHmfggkZ/IINIgRYZjIGWALYG8IItG04EoIM1gnADQpb/DFoQDX/gB+Jl+H7u8P/6UnesYZ73GYVCBBcBL2QY/vYB7oiXAfkQsANKgtN4tIEkYyb7zYhFgcyLvpRLgkODyXYW+8jtsRK5ACAzxAHDD70MAMk5ZATA/tplWAsAtzunPMDlWcAPANM8m/9wNWz+wMEIAFg/XefAUv4OAN76YMPaWkRYwgQAS0FodxAA6kJiwQX/w9rA/V1bVAbF8UpEwUYHAPpNm8Es5QsAp15kRAVWwx4B+HBiPBDF8Wt8/sFFVsH9B1ULGud4acFTPcMA9JZqUv8LALZBaWdC/lcSAPWFtcL+2MBz/8DAWwXDwx8B+JVwdATF951qaAYA96dwBP9/CgH3sW1SWqHB/dtnCQClrGY7wHNADgDCrmtzll3C2cHDBwD3vLRidwoBxbVmwFMERMLlxYkQAPfFtf/C28DBRFbAgcAA9NR1cwoA99exwMLawX7ADgD3JXdseHX+cw0A9yx3w23D/sHC/WjNAJjxaMFCcgUAMvB32msFAPf3dAVqBQr1AXp+wQjV9wlnisJoAxC5yWb8HxGHDWnAXMMQ9Al8hMMGEIrXYlLaBxD3HIDCSQUTvSFxwmQKEDIkg9iGwIcGEKnhZlnaCRD3LH3CB4b8HRH3M3rD/wZ1BgoBMDRwBRBpNWfbYQQQlTlmrgQTgjxeSwYQ9/50hdoCELNEYsHBEPlWgqY=', 1)
GO
INSERT [dbo].[CustomerFingerPrint] ([ID], [CustomerID], [Policy], [FingerPrint], [FingerPrintType]) VALUES (11, 3, N'9', N'SspTUzIxAAADiY4ECAUHCc7QAAAbiGkBAAAAg7QfqImjAF8NoQBoAN2EoQBrAGgMSADRiWQP+AC3ADgPu4n3AOcPngA7AGOG7wA9AG8NWwAfiWIHhwARAJAEy4lKATkDrwBJAOuHsQBxAOoKVAByiegNqgDiAKoOyIlNAOQPXQCjAGGH5QA9AOkOGwAtiWEOigAcABAHlIkkAVcPvQBjAOiHxABwAOYLBQDKiesPnwDiACoOu4lAAOAPVACzAOGCuwD+AGAPKAAGiPsP6wAZABICvok2AU4PgYDoderzgICZ93b5TIAqCUuBOXoy+DeL+J4SF8sH0IqEeg4CFHshA+FzVYuym8+ZnZGxEoOHYwo89k939e4i5wBS4IpZA8b84ISWjNgHiYCaAHMCkwo4imIQMQZM/3UMdIEreSP/XX9KAQAHUXhB8jyKYwqsdcv38vr7CwhwII0ND0IE3P31iGMF9nxWATv+kwowjtWTtf5Lg51ydIGf+5t/HXqiis/wD50TFiKvAvPnafLxXRYD+/B7DkDMeCA6xAHwkOcMALAAXvnDR9APAMAAZsMEQcNJwT3BEQDXxV5udsP+Q1j+W8gAyohfwVtLwF3XAOGIW8HA/cHB/cL+SMFUwAgAjMdcPdIHAIEDacIFZQSJ6wRWR0AGxf8X1WP9BwCHFqVMSo0ABxxeSgrFnB3pVVhWEQDn2l5SzFnAVVcSADw8XXdbwML/wP4EVj2OAd0yYFtawAD0xWpcEQDsP6NFbMlqwP//BQA9Vm5JcQcA919trm8TifZqa2L+w5RdV44B93VtwMAEbACJW2lkwAkAZG5hymX/EAD3f6hZw0tGwMDARQzFwnbtZVXA/1MGxfeL+WvACgD3kbVrctARAPebdHC9wMNIQlsRAPekscFvSVj+wHI+CcWnoOlEPv8FAPdpdHiMAfezcMDABw8Dfrx3hGz+WgXAw4AB98Z6w24Ewf2HAffPesKGoWRYjAGL0mldBsX32/STwAUAjtanVQaJ9uB6wnYIxajg4nliDQD36LLD/EpywlzBDAAy8HcBc3TBCwD3PHGA5mYIALj8YAVkao8Bmf5ka//AEJ2JXUUIELsCmf/C/8EGEPgIegGEBpk2FEzBbgjV9xL9woJ+BhD333DAEQQQ+SRwxGQDE21J1sAEEJfiV22MEQAxNHgD1bk53sIDEL87UwYDEp88Q8I=', 1)
GO
INSERT [dbo].[CustomerFingerPrint] ([ID], [CustomerID], [Policy], [FingerPrint], [FingerPrintType]) VALUES (12, 3, N'9', N'TXdTUzIxAAAENDsECAUHCc7QAAAcNWkBAAAAhNklfTS8AFQOfQBWAMo5lgCCANsNdgDTNE4PPQDfABwIcjRVAOQP4wADAOk7VwASAc4BRAAoNU4GhAAkABMPYjQ1Ab4FtwDUAOI9EwBGAbkCXADENFsOpQChABkPkzTfAE4PfwCqANM6WwBfANsPZgABNUcPygBZACkPwzQVAToPxQD4AOU73wAXASQPUgAVNOMKXgBNAeAKiTTEANoOgwBBAEs5rADMANYPjgDuNEoB0wB8ACsPbTRUAFwPJwC/ANc8IwBrAE4LmgApNc0FbQAkABYPpTQOAGIK3wD4ARE3PH4y/PKLRICawHf5IY1CBMsM2E+oA9L1xZESDpOzGYvRDw4IwqEXLioT5QfiEq+GoaezhZcsLQZI95ZBZfcRm7lz2IYaMM4Ez5e/72rvnLc8hmoM9oSTATYn5HcZeykDJIlZw8f8jYGW+HMGSjrv+gv4TQ8PAf8nTBra9nMkHPdTyEaD4e6eEi7jnLcAD578KWvbWDvsmYM1itmPRIApQeh51ItBftSL1UFvfhf7SgU+FxcjIXuNgUqFpISfz5p7ZXwrduL30CdoA/aorf5a8OPEmYMhfoaN6/IT6ArTn+cAnsUgODUDVR+jDwC+AFj1wj3AVlXCwQCANFZZDwCMAZ/C+vXBVsE+bQTFmgVnRggAsAFTBT9aPwFrAlNz/gf9xmAXALsCV8LoSnrLZf/B/sNdwAAv2FHA/8MLAJMEV2NqRQsAXwWVwFD0wP99CACOzFfFZzwUAMQOYARSXfRpXVvCAxDYKEj3EACgEFrAO4fAyMHAwMH/wI8JBHIQV8DAW3TPAM0lW8BRcsAUxdQVaMBEwGRpWob/DzTaGmDBwMH6fgc04yJkwQUA9CNa9lgXAOcraa5wccrBwMBlwIsEBhRzF0N2wwoAKDNvXVjBwA4A8v9p+/XDVsB4wMHBAPFzdXsaAPdOscJi9mfBwMHASYfB+/HC/8YWAQOSgJf1eX5nP8JUzABsbVvAwcD/wZ4KBTZhg8VxwMFHCQTOZXBs/8HBBAUFM2dwYhUBC6hw+vdMccDCwP8FdMT0wQ4BFnmGAXjEpIX9FQEQfrJ4xfSRwsFawVGoCQRLhlBV/sI1xhApHELCCgEQh7/DZrLDEwEQkHe7hsT2/5RqWxAAMqB09oDCf8HBwK8PFI9GvVfAOEE6Mgc0o6dXwA4AMrFponbBwf/CdssAcYtWwMH/h8STwsc8AXu/UP/AosAONJfDV8BvQgUNBM3CcJuAxFEHDATDzGvDksKLawgEw9hpxcGlwsEAKOJH/MEKALMdSfr2d8BpBwCTJFDE9VQGAJnhTAX/czEB9+RcxMQBBBR0FEnE/gQQ4CVCpQcQoglDUgUDFAIpQP8FEMfdPcZmAhDfGivDwxAGFUfFwIQ=', 1)
GO
INSERT [dbo].[CustomerFingerPrint] ([ID], [CustomerID], [Policy], [FingerPrint], [FingerPrintType]) VALUES (13, 1, N'9', N'TW5TUzIxAAAELTAECAUHCc7QAAAcLGkBAAAAhNAony3RAFUPggA4AEki0QDLAPYPcQAaLDAP6ADNAMYP7i39ADsLywC4APEicwBCAUgO6QD4LdMFoABYAesH7i1CAXwLSQCNAVQoWQBLAN8IswAXLd0NmADGABoPjS20AGIPogDRAUUiaACsAFwPGgD5LTwLWwARAZQPsy1BARwP8gDuAdkk2ABGAfwMuwBbLeEPBwEsARACOy1BAdEHyAD4AO8igwDWAFcPuQCzLeIPlwCXACEP5S34AEUL8AAiABYmkQB2AOQNwAGyLe0FYwA+AY8Oey1TATkIMQDZAVEvywBaAPUPSQBFLd4KiQAWABsO5FagA9b09vqLDqbSjPaq57+wIPGKODb2AQ6x86cZ0NfBAxEqSgE+Exc+xABdEbb6L3zHrOYDMhTa9SLpea3QouqpzIPkCyontwObAXcHaAHX0Pr/1I9FfsyLnK5AhkoMVREf9msJOHoy/FME3flEKhwvpv8eB64Iwt0j7W4hpQk0YgbMfIAO4WYhAP92LTcDLfkSWd7ZyK6viWJ+1fvSD9PWIAcRe+J0XYMxp9eM7oRWAT8DlCRJDhk2udoIziU+QAfWAVsFP+nj8Ab6QQSp/5cR6dxTDeLx5oSbhiYqSA/KAfsTMwBmLlIFKQXiA8YD3+w7W8QgNwHHKhu7DAB7AV7EOGXEecPAAwChApL9Dy1tBVbB/cE6ZFsjAY0FXsBiBVdY0ggAYQhewobAYT8BuAhgRcIH/cRwwcBb/wYAewxe0HUQAKkNZpnBxe/9XFjABwCKEGZVbwYAxhVmBcB0JAHOF17+wK7AxT4B1xxiWGWZXMTSVAQAhB1etAoE8yVmwF3AasYA7gRswRgA7jK1XFhYNsBa/oxzzADwEXbCamcNACJJcO/DUMDAfsDMAGxoW1DAUg0ATEdedVdMgRgA5550Y1nDVl1dcUPdAO5OdnjBwcFtB8BVdsDBXQkA9696xKZ5DAD3cHoEwMB9ecAEAPd2slsdLAt8emJ6cQTC+UbD/2t0CAC1hV5qUhgBD4aABX5z78F2X1V0wNwBEriWx1/C/sIHwnfvWcB0/3ILxBCcroVqw3wKAdWih+zAe8N2FQHVqodadMGLfsH/s8MMLWarXMBKicgAb4JW/8DAYsKBwxAsE7GAaYzBBMGAcWgNAIe3YgdZ+3HAbBMBEL5DhMetf8L/h8HBwwEW64eEwQ4A4wp3n62CwKAMAJgUV8RAaGIMAJ3TlV9cW8EMAILXUwVaxX52CQD424YHw8DswJ0MAPfkRsGt6cPEw8bECMX38L7Fxq7FBAAf+1eOBgDe/kDBBdW2KBGDAUxZBdVcEn3+VwYQoheFwMTtxAMQFxhJBAMUAx5Q/wgQt9owx+7HwZEGEPeDbfrWLQQQ01iM3w==', 1)
GO
INSERT [dbo].[CustomerFingerPrint] ([ID], [CustomerID], [Policy], [FingerPrint], [FingerPrintType]) VALUES (14, 1, N'9', N'TZ5TUzIxAAAE3eMECAUHCc7QAAAc3GkBAAAAhAAvct3lAIAPfAB5AArSdwAUAVYNdQCk3RkLugCdAFQL2t0FASAPowDyAVLeiAA9AaYEJgCh3RkOSAAwAf0K/93cAJUIeACjAHvT8QCEAJkOVQBQ3YoPswA3AFwP5N1AAKEBhgA5AIDSbwAKAV8OkQD43WUPiAAiAf0LsN0rATQM6AASABfQnAB9AI4NawA/3EYHQAAiAYsOkN1LAXwB9gBwAJ3cSABFAbgG5gA23FUPKwBFAY4EO91IAAMPiQDoAInSrgDUAJQPbQAV3CsPSwDaALQPc90jATAOwgDmASnSiAA1AcMEkwCJ3XgP0wAlAewPK90KAV0PawCGAbTbugBFAToJ/gA63DICxABcAF0PON1VAHkPXQDwAIDRKP9KIsp2f5KDo96e0PtpKIAiWFYXjm8BZHs/e5bar/nC9ysM/RMV+ITemOb9LBRTgV87gfKL9exsBY1ceIWq/kd0L/QeLmIJOoEnAEIDWtGvAir/FgK3D0rOQwprCBcLrCeh2lQzRAtVM8jfNtnT9Y4KyQhAdeE7OAiZ7bHhvIE6oLr0HnqmB9P/zC5UDAkVXRi8H90uqNixuuEqQII+VCcGXYSJgY9sWdRAIxoIJRgg+TJJyY9mgZOH6wb6Km8Lr323BkaKgSZz/Mb1Rg3f82PLDNxR+AHTBPhd2bPo/B59jSxK4yAub7cD3fwX9CLUSBMW+nIKDwzGMU/54fTd5zgLZaG86C39yvpfAkvQ0Xvq9xf/o4HyJl8PHqRydWZTJOEAAoUkqgjFkwTHWsBXCQCmxRdVHf7+/AsAhMEQ+bVyQQ8AdQbSwMQdwf9Vwf/D9AcEtQ0TU2UQAJgQFyLCMl1DZAXFUB3NahMAQCAJgsLE8P7//YfAUckAMfQCPcE+WATFMTXdUxIAKDz38z48hkvCCQAhRj9URR0GAEFJ/S+XFgTIU/f///3DO8BGIkHAwMB7/8EAOIh7ZAwAl1fWR18f/lsWABFmMUX65f/B/0ZYwgVEDd12ZoZpa8DJAHG2By9AXcEMxQpyJ8FXZft3F8UKhy3/wEw9UTOhwVnTAQyM9P9T+//7HVUHAFSOgLpvCt0HmfBEQf8HMvnQAbGeEDJwO8D4JsEIALGkGjprxiMZAAir7TeB/vvlOf9c/8JC3AAEZOo4wMA4wDhLQLlawAUAf7rJwDXXAXe8hnzCBWYd3QG/9MFGVwX+OiND/8DBwMHyBQSiwRBFGQAAC+T5IsBAVP//MQRRWZILALXZHluoNAPdSNx0wv+L3gAFPuc4wDX/NQXB+SLBwP//wcAE/jDRAXXkhpLCOsJiHBgAAe7mPf1B+iLAOExYVwbFU/i7hcAOAIT8Q5vGH//DwzX/GcUA+gEqO8A7Q/2WWE7bEeEHIMDABf0NzWoJacPCxAbEodARcwsA/CA++/wg+BsGEC4Np3vGzBGkDqDCwGXCxRzCwVbAwwbVaxSDw8SzBRB212aq2hGsEx7+/QREAM1/FRP8+xTVAB4HP/7AwP/+7f5AIkoEED4jUEgFFB4nK/9ZBRASJyAdNgUQ0igpjwMUaiow/xgQBPfXOpb+Nf3+//4FU/qRBRAgNFrDOAQU+DdPNBIQWpKa+yH7/v3A/fw7/vgj//0m', 1)
GO
SET IDENTITY_INSERT [dbo].[CustomerFingerPrint] OFF
GO
SET IDENTITY_INSERT [dbo].[CustomerType] ON 

GO
INSERT [dbo].[CustomerType] ([Id], [Name]) VALUES (1, N'Key Customer')
GO
INSERT [dbo].[CustomerType] ([Id], [Name]) VALUES (2, N'Driver')
GO
SET IDENTITY_INSERT [dbo].[CustomerType] OFF
GO
SET IDENTITY_INSERT [dbo].[FingerPrintType] ON 

GO
INSERT [dbo].[FingerPrintType] ([ID], [Name]) VALUES (1, N'Left Hand First Finger')
GO
INSERT [dbo].[FingerPrintType] ([ID], [Name]) VALUES (2, N'Left Hand Middle Finger')
GO
INSERT [dbo].[FingerPrintType] ([ID], [Name]) VALUES (3, N'Left Hand  Thumb')
GO
INSERT [dbo].[FingerPrintType] ([ID], [Name]) VALUES (4, N'Left Hand Third Finger')
GO
INSERT [dbo].[FingerPrintType] ([ID], [Name]) VALUES (5, N'Left Hand Last Finger')
GO
INSERT [dbo].[FingerPrintType] ([ID], [Name]) VALUES (6, N'Right Hand First Finger')
GO
INSERT [dbo].[FingerPrintType] ([ID], [Name]) VALUES (7, N'Right Hand Middle Finger')
GO
INSERT [dbo].[FingerPrintType] ([ID], [Name]) VALUES (8, N'Right Hand  Thumb')
GO
INSERT [dbo].[FingerPrintType] ([ID], [Name]) VALUES (9, N'Right Hand Third Finger')
GO
INSERT [dbo].[FingerPrintType] ([ID], [Name]) VALUES (10, N'RIght Hand Last Finger')
GO
SET IDENTITY_INSERT [dbo].[FingerPrintType] OFF
GO
SET IDENTITY_INSERT [dbo].[FuelHistory] ON 

GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (1, CAST(0x0000A6430097F02A AS DateTime), 1, 5, 1, 1, CAST(12.00 AS Decimal(18, 2)), CAST(800.00 AS Decimal(18, 2)), 1, N'Pratik Parikh', N'Test User 2', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (2, CAST(0x0000A65301873440 AS DateTime), 1, 3, 2, 1, CAST(5.00 AS Decimal(18, 2)), CAST(450.00 AS Decimal(18, 2)), 1, N'Pratik Parikh', N'Johnson', N'Chirag Shah')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (3, CAST(0x0000A6530187548A AS DateTime), 1, 1, 2, 1, CAST(10.00 AS Decimal(18, 2)), CAST(2000.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Chirag Shah')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (4, CAST(0x0000A668013EF1BA AS DateTime), 1, 1, 1, 1, CAST(5.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (5, CAST(0x0000A66801415405 AS DateTime), 1, 1, 1, 1, CAST(8.00 AS Decimal(18, 2)), CAST(1500.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (6, CAST(0x0000A6680141E1E5 AS DateTime), 1, 1, 1, 1, CAST(5.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (7, CAST(0x0000A6690102BCF2 AS DateTime), 1, 1, 1, 1, CAST(4.00 AS Decimal(18, 2)), CAST(240.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (8, CAST(0x0000A6690102CF45 AS DateTime), 1, 1, 1, 1, CAST(4.00 AS Decimal(18, 2)), CAST(224.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (9, CAST(0x0000A669010AE661 AS DateTime), 1, 1, 1, 1, CAST(10.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (10, CAST(0x0000A66A01601B9D AS DateTime), 1, 1, 1, 1, CAST(10.00 AS Decimal(18, 2)), CAST(620.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (11, CAST(0x0000A66A016403CF AS DateTime), 1, 1, 1, 1, CAST(5.00 AS Decimal(18, 2)), CAST(300.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (12, CAST(0x0000A66A016516A2 AS DateTime), 1, 1, 1, 1, CAST(5.00 AS Decimal(18, 2)), CAST(300.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (13, CAST(0x0000A66A01659DEF AS DateTime), 1, 1, 1, 1, CAST(5.00 AS Decimal(18, 2)), CAST(350.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (14, CAST(0x0000A66A0166B7A7 AS DateTime), 1, 1, 1, 1, CAST(1.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (15, CAST(0x0000A66B0183DC12 AS DateTime), 1, 1, 1, 1, CAST(10.00 AS Decimal(18, 2)), CAST(450.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (16, CAST(0x0000A66D0102AF51 AS DateTime), 1, 1, 1, 1, CAST(2.00 AS Decimal(18, 2)), CAST(120.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (17, CAST(0x0000A66D016EA45B AS DateTime), 1, 1, 1, 1, CAST(1.00 AS Decimal(18, 2)), CAST(65.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (18, CAST(0x0000A673016B58F3 AS DateTime), 1, 1, 1, 1, CAST(1.00 AS Decimal(18, 2)), CAST(60.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (19, CAST(0x0000A6860156FDFC AS DateTime), 1, 1, 1, 1, CAST(50.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (20, CAST(0x0000A68601572510 AS DateTime), 1, 1, 1, 1, CAST(2.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (21, CAST(0x0000A68601573A6A AS DateTime), 1, 1, 1, 1, CAST(1.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (22, CAST(0x0000A686015934DB AS DateTime), 1, 1, 1, 1, CAST(2.00 AS Decimal(18, 2)), CAST(120.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (23, CAST(0x0000A6860160CFC0 AS DateTime), 1, 1, 1, 2, CAST(10.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (24, CAST(0x0000A6860162B5A3 AS DateTime), 1, 1, 2, 1, CAST(5.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Chirag Shah')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (25, CAST(0x0000A6860164E238 AS DateTime), 1, 1, 1, 1, CAST(12.00 AS Decimal(18, 2)), CAST(30.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Pratik Parikh')
GO
INSERT [dbo].[FuelHistory] ([Id], [Time], [FuelStationId], [CustomerId], [UserId], [FuelType], [FuelVolume], [FuelAmount], [KeyCustomerId], [KeyCustomerName], [CustomerName], [UserName]) VALUES (26, CAST(0x0000A6A701481546 AS DateTime), 1, 1, 4, 1, CAST(1.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), NULL, NULL, N'Pratik Parikh', N'Admin')
GO
SET IDENTITY_INSERT [dbo].[FuelHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[FuelLimitType] ON 

GO
INSERT [dbo].[FuelLimitType] ([Id], [Name]) VALUES (1, N'PerDay')
GO
INSERT [dbo].[FuelLimitType] ([Id], [Name]) VALUES (2, N'PerMonth')
GO
INSERT [dbo].[FuelLimitType] ([Id], [Name]) VALUES (3, N'PerYear')
GO
SET IDENTITY_INSERT [dbo].[FuelLimitType] OFF
GO
SET IDENTITY_INSERT [dbo].[FuelStation] ON 

GO
INSERT [dbo].[FuelStation] ([Id], [Code], [Name], [Address], [Pincode], [City], [State], [Country]) VALUES (1, N'FS001', N'Fuel Station 1', N'Gota Cross Road', N'382481', N'Ahemdabad', N'Gujurat', N'India')
GO
SET IDENTITY_INSERT [dbo].[FuelStation] OFF
GO
SET IDENTITY_INSERT [dbo].[FuelType] ON 

GO
INSERT [dbo].[FuelType] ([Id], [Name]) VALUES (1, N'Petrol')
GO
INSERT [dbo].[FuelType] ([Id], [Name]) VALUES (2, N'Diesel')
GO
SET IDENTITY_INSERT [dbo].[FuelType] OFF
GO
SET IDENTITY_INSERT [dbo].[PaymentType] ON 

GO
INSERT [dbo].[PaymentType] ([Id], [Name]) VALUES (1, N'Credit')
GO
INSERT [dbo].[PaymentType] ([Id], [Name]) VALUES (2, N'Cash')
GO
SET IDENTITY_INSERT [dbo].[PaymentType] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([Id], [Code], [Name], [UserType], [Address], [Pincode], [City], [State], [Country], [ContactNo], [UserName], [Password]) VALUES (1, N'U001', N'Pratik Parikh', 1, N'Icb Floora
Opp silver nest
Gota', N'382481', N'Ahemdabad', N'Gujurat', N'India', N'9662490722', N'pratik.parikh', N'admin')
GO
INSERT [dbo].[User] ([Id], [Code], [Name], [UserType], [Address], [Pincode], [City], [State], [Country], [ContactNo], [UserName], [Password]) VALUES (2, N'U002', N'Chirag Shah', 2, N'Satellite', N'380015', N'Ahemdabad', N'Gujurat', N'India', N'9998856124', N'chirag.shah', N'admin')
GO
INSERT [dbo].[User] ([Id], [Code], [Name], [UserType], [Address], [Pincode], [City], [State], [Country], [ContactNo], [UserName], [Password]) VALUES (3, N'U003', N'Test 1', 1, N'Gota', N'382481', N'Ahemdabad', N'Gujarat', N'India', N'99985411', N'test', N'admin')
GO
INSERT [dbo].[User] ([Id], [Code], [Name], [UserType], [Address], [Pincode], [City], [State], [Country], [ContactNo], [UserName], [Password]) VALUES (4, N'U004', N'Admin', 1, NULL, NULL, NULL, NULL, NULL, NULL, N'admin', N'admin')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserType] ON 

GO
INSERT [dbo].[UserType] ([Id], [Name]) VALUES (1, N'Admin')
GO
INSERT [dbo].[UserType] ([Id], [Name]) VALUES (2, N'Operator')
GO
SET IDENTITY_INSERT [dbo].[UserType] OFF
GO
/****** Object:  Index [AK_CreditHistory_Time]    Script Date: 10/23/2016 11:33:13 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [AK_CreditHistory_Time] ON [dbo].[CreditHistory]
(
	[Time] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [NonClusteredIndex-20160628-214326-CreditHistory]    Script Date: 10/23/2016 11:33:13 AM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160628-214326-CreditHistory] ON [dbo].[CreditHistory]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [NonClusteredIndex-20160628-214336-CreditHistory]    Script Date: 10/23/2016 11:33:13 AM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160628-214336-CreditHistory] ON [dbo].[CreditHistory]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AK_FuelHistory_Time]    Script Date: 10/23/2016 11:33:13 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [AK_FuelHistory_Time] ON [dbo].[FuelHistory]
(
	[Time] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [NonClusteredIndex-20160628-214326]    Script Date: 10/23/2016 11:33:13 AM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160628-214326] ON [dbo].[FuelHistory]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [NonClusteredIndex-20160628-214336]    Script Date: 10/23/2016 11:33:13 AM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20160628-214336] ON [dbo].[FuelHistory]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CreditHistory]  WITH CHECK ADD  CONSTRAINT [FK_CreditHistory_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[CreditHistory] CHECK CONSTRAINT [FK_CreditHistory_Customer]
GO
ALTER TABLE [dbo].[CreditHistory]  WITH CHECK ADD  CONSTRAINT [FK_CreditHistory_FuelStation] FOREIGN KEY([FuelStationId])
REFERENCES [dbo].[FuelStation] ([Id])
GO
ALTER TABLE [dbo].[CreditHistory] CHECK CONSTRAINT [FK_CreditHistory_FuelStation]
GO
ALTER TABLE [dbo].[CreditHistory]  WITH CHECK ADD  CONSTRAINT [FK_CreditHistory_PaymentType] FOREIGN KEY([PaymentType])
REFERENCES [dbo].[PaymentType] ([Id])
GO
ALTER TABLE [dbo].[CreditHistory] CHECK CONSTRAINT [FK_CreditHistory_PaymentType]
GO
ALTER TABLE [dbo].[CreditHistory]  WITH CHECK ADD  CONSTRAINT [FK_CreditHistory_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[CreditHistory] CHECK CONSTRAINT [FK_CreditHistory_User]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Customer] FOREIGN KEY([KeyCustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Customer]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_CustomerType] FOREIGN KEY([CustomerType])
REFERENCES [dbo].[CustomerType] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_CustomerType]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_FuelLimitType] FOREIGN KEY([FuelLimitType])
REFERENCES [dbo].[FuelLimitType] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_FuelLimitType]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_PaymentType] FOREIGN KEY([PaymentType])
REFERENCES [dbo].[PaymentType] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_PaymentType]
GO
ALTER TABLE [dbo].[CustomerFingerPrint]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFingerPrint_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[CustomerFingerPrint] CHECK CONSTRAINT [FK_CustomerFingerPrint_Customer]
GO
ALTER TABLE [dbo].[CustomerFingerPrint]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFingerPrint_FingerPrintType] FOREIGN KEY([FingerPrintType])
REFERENCES [dbo].[FingerPrintType] ([ID])
GO
ALTER TABLE [dbo].[CustomerFingerPrint] CHECK CONSTRAINT [FK_CustomerFingerPrint_FingerPrintType]
GO
ALTER TABLE [dbo].[FuelHistory]  WITH CHECK ADD  CONSTRAINT [FK_FuelHistory_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[FuelHistory] CHECK CONSTRAINT [FK_FuelHistory_Customer]
GO
ALTER TABLE [dbo].[FuelHistory]  WITH CHECK ADD  CONSTRAINT [FK_FuelHistory_FuelStation] FOREIGN KEY([FuelStationId])
REFERENCES [dbo].[FuelStation] ([Id])
GO
ALTER TABLE [dbo].[FuelHistory] CHECK CONSTRAINT [FK_FuelHistory_FuelStation]
GO
ALTER TABLE [dbo].[FuelHistory]  WITH CHECK ADD  CONSTRAINT [FK_FuelHistory_FuelType] FOREIGN KEY([FuelType])
REFERENCES [dbo].[FuelType] ([Id])
GO
ALTER TABLE [dbo].[FuelHistory] CHECK CONSTRAINT [FK_FuelHistory_FuelType]
GO
ALTER TABLE [dbo].[FuelHistory]  WITH CHECK ADD  CONSTRAINT [FK_FuelHistory_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[FuelHistory] CHECK CONSTRAINT [FK_FuelHistory_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserType] FOREIGN KEY([UserType])
REFERENCES [dbo].[UserType] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserType]
GO
USE [master]
GO
ALTER DATABASE [FuelSupplySystem] SET  READ_WRITE 
GO
