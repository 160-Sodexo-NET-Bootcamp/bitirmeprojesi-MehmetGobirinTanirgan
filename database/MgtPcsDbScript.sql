USE [master]
GO
/****** Object:  Database [MgtPcsDb]    Script Date: 18.02.2022 22:58:04 ******/
CREATE DATABASE [MgtPcsDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MgtPcsDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQL2019\MSSQL\DATA\MgtPcsDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MgtPcsDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQL2019\MSSQL\DATA\MgtPcsDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MgtPcsDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MgtPcsDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MgtPcsDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MgtPcsDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MgtPcsDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MgtPcsDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MgtPcsDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [MgtPcsDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MgtPcsDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MgtPcsDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MgtPcsDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MgtPcsDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MgtPcsDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MgtPcsDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MgtPcsDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MgtPcsDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MgtPcsDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MgtPcsDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MgtPcsDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MgtPcsDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MgtPcsDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MgtPcsDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MgtPcsDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MgtPcsDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MgtPcsDb] SET RECOVERY FULL 
GO
ALTER DATABASE [MgtPcsDb] SET  MULTI_USER 
GO
ALTER DATABASE [MgtPcsDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MgtPcsDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MgtPcsDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MgtPcsDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MgtPcsDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MgtPcsDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MgtPcsDb', N'ON'
GO
ALTER DATABASE [MgtPcsDb] SET QUERY_STORE = OFF
GO
USE [MgtPcsDb]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 18.02.2022 22:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[Id] [uniqueidentifier] NOT NULL,
	[BrandName] [nvarchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedById] [nvarchar](200) NULL,
	[UpdatedBy] [nvarchar](200) NULL,
	[UpdatedById] [nvarchar](200) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 18.02.2022 22:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [uniqueidentifier] NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[CategoryLevel] [int] NOT NULL,
	[LeftBorder] [int] NOT NULL,
	[RightBorder] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedById] [nvarchar](200) NULL,
	[UpdatedBy] [nvarchar](200) NULL,
	[UpdatedById] [nvarchar](200) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colors]    Script Date: 18.02.2022 22:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ColorName] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedById] [nvarchar](200) NULL,
	[UpdatedBy] [nvarchar](200) NULL,
	[UpdatedById] [nvarchar](200) NULL,
 CONSTRAINT [PK_Colors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Emails]    Script Date: 18.02.2022 22:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emails](
	[Id] [uniqueidentifier] NOT NULL,
	[EmailType] [tinyint] NOT NULL,
	[EmailStatus] [tinyint] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[ReceiverId] [uniqueidentifier] NOT NULL,
	[AttemptCount] [tinyint] NOT NULL,
	[MailResponse] [nvarchar](200) NULL,
	[MailResponseDate] [datetime2](7) NULL,
	[IsMoved] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedById] [nvarchar](200) NULL,
	[UpdatedBy] [nvarchar](200) NULL,
	[UpdatedById] [nvarchar](200) NULL,
 CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FailedEmails]    Script Date: 18.02.2022 22:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FailedEmails](
	[Id] [uniqueidentifier] NOT NULL,
	[EmailId] [uniqueidentifier] NOT NULL,
	[FinalStatus] [tinyint] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedById] [nvarchar](200) NULL,
	[UpdatedBy] [nvarchar](200) NULL,
	[UpdatedById] [nvarchar](200) NULL,
 CONSTRAINT [PK_FailedEmails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LockoutRecords]    Script Date: 18.02.2022 22:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LockoutRecords](
	[Id] [uniqueidentifier] NOT NULL,
	[LockoutBegin] [datetime2](7) NOT NULL,
	[LockoutUntil] [datetime2](7) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedById] [nvarchar](200) NULL,
	[UpdatedBy] [nvarchar](200) NULL,
	[UpdatedById] [nvarchar](200) NULL,
 CONSTRAINT [PK_LockedoutUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Offers]    Script Date: 18.02.2022 22:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offers](
	[Id] [uniqueidentifier] NOT NULL,
	[OfferStatus] [tinyint] NOT NULL,
	[OfferPercentage] [tinyint] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[ProductOwnerId] [uniqueidentifier] NOT NULL,
	[OffererId] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedById] [nvarchar](200) NULL,
	[UpdatedBy] [nvarchar](200) NULL,
	[UpdatedById] [nvarchar](200) NULL,
 CONSTRAINT [PK_Offers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 18.02.2022 22:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderPrice] [decimal](8, 2) NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[PaymentTypeId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedById] [nvarchar](200) NULL,
	[UpdatedBy] [nvarchar](200) NULL,
	[UpdatedById] [nvarchar](200) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentTypes]    Script Date: 18.02.2022 22:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentTypes](
	[Id] [int] NOT NULL,
	[PaymentMethod] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedById] [nvarchar](200) NULL,
	[UpdatedBy] [nvarchar](200) NULL,
	[UpdatedById] [nvarchar](200) NULL,
 CONSTRAINT [PK_PaymentTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductImages]    Script Date: 18.02.2022 22:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductImages](
	[Id] [uniqueidentifier] NOT NULL,
	[ImageUrl] [nvarchar](1000) NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedById] [nvarchar](200) NULL,
	[UpdatedBy] [nvarchar](200) NULL,
	[UpdatedById] [nvarchar](200) NULL,
 CONSTRAINT [PK_ProductImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 18.02.2022 22:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductName] [varchar](100) NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[Price] [decimal](8, 2) NOT NULL,
	[ProductOwnerId] [uniqueidentifier] NOT NULL,
	[IsOfferable] [bit] NOT NULL,
	[IsSold] [bit] NOT NULL,
	[UsageCondition] [tinyint] NOT NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[BrandId] [uniqueidentifier] NULL,
	[ColorId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedById] [nvarchar](200) NULL,
	[UpdatedBy] [nvarchar](200) NULL,
	[UpdatedById] [nvarchar](200) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 18.02.2022 22:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Firstname] [nvarchar](50) NOT NULL,
	[Lastname] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[EmailAddress] [nvarchar](100) NOT NULL,
	[RefreshToken] [nvarchar](100) NULL,
	[HashedPassword] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[LastLoginIP] [nvarchar](50) NULL,
	[RefreshTokenExpireDate] [datetime2](7) NULL,
	[Role] [nvarchar](30) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[FailedLoginAttemptCount] [int] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[DailyLockoutCount] [tinyint] NOT NULL,
	[PermaBlockEnabled] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](200) NULL,
	[CreatedById] [nvarchar](200) NULL,
	[UpdatedBy] [nvarchar](200) NULL,
	[UpdatedById] [nvarchar](200) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'c96239ae-2a8b-4c90-db13-08d9f2827a75', N'Asus', 0, CAST(N'2022-02-18T02:00:46.4428422' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'8054a8de-98c4-4c7f-db14-08d9f2827a75', N'Apple', 0, CAST(N'2022-02-18T02:00:52.2384060' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'44adb276-aea0-420f-db15-08d9f2827a75', N'Samsung', 0, CAST(N'2022-02-18T02:00:56.4967967' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'48d8f660-9e56-4617-db16-08d9f2827a75', N'Huawei', 0, CAST(N'2022-02-18T02:01:01.5956674' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'd45ba8a1-82e1-471e-db17-08d9f2827a75', N'Xiaomi', 0, CAST(N'2022-02-18T02:01:07.3336343' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'd15ed868-8af3-4819-db18-08d9f2827a75', N'Oppo', 0, CAST(N'2022-02-18T02:01:11.5849459' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'126609d0-7509-4491-db19-08d9f2827a75', N'Monster', 0, CAST(N'2022-02-18T02:01:16.3063213' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'b2e52411-d166-4d64-db1a-08d9f2827a75', N'MSI', 0, CAST(N'2022-02-18T02:01:40.5556079' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'8131a541-c4c6-44ae-db1b-08d9f2827a75', N'Dell', 0, CAST(N'2022-02-18T02:02:10.9143892' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'859956b9-e989-43e7-db1c-08d9f2827a75', N'HP', 0, CAST(N'2022-02-18T02:02:14.5206083' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'765085fe-f5f6-4471-db1d-08d9f2827a75', N'Lenovo', 0, CAST(N'2022-02-18T02:02:51.4945297' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'8c0c1108-cf76-462f-db1e-08d9f2827a75', N'Acer', 0, CAST(N'2022-02-18T02:03:21.6425098' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'd8d41135-1e8a-4336-db1f-08d9f2827a75', N'Casper', 0, CAST(N'2022-02-18T02:03:26.4525715' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'ab137d25-73c3-4b3a-db20-08d9f2827a75', N'Toshiba', 0, CAST(N'2022-02-18T02:03:46.9741175' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'6df2fd97-9092-4643-db21-08d9f2827a75', N'LG', 0, CAST(N'2022-02-18T02:04:19.2074290' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'847b3227-ddcf-486d-db22-08d9f2827a75', N'Philips', 0, CAST(N'2022-02-18T02:04:25.0599162' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'b04420b8-8a5d-466e-db23-08d9f2827a75', N'Logitech', 0, CAST(N'2022-02-18T02:04:30.2280601' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'd62d3dad-bee3-4c53-db24-08d9f2827a75', N'Steelseries', 0, CAST(N'2022-02-18T02:04:38.9771238' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'787fe04d-58cb-47da-db25-08d9f2827a75', N'NZXT', 0, CAST(N'2022-02-18T02:04:46.7808030' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'3a055a4e-9d35-456d-db26-08d9f2827a75', N'Hyperx', 0, CAST(N'2022-02-18T02:05:19.0169766' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'3b3df4a2-00f4-4e8a-db27-08d9f2827a75', N'JBL', 0, CAST(N'2022-02-18T02:05:41.7449526' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'603e9bf5-5cde-4d08-db28-08d9f2827a75', N'Razer', 0, CAST(N'2022-02-18T02:05:53.9639757' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'db516b43-6db3-47f8-db29-08d9f2827a75', N'Sony', 0, CAST(N'2022-02-18T02:06:15.2897124' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'9e75ad00-5a44-4976-db2a-08d9f2827a75', N'Kingston', 0, CAST(N'2022-02-18T02:06:39.5609091' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'525ea719-d68b-4654-db2b-08d9f2827a75', N'Sandisk', 0, CAST(N'2022-02-18T02:06:45.8621326' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'31821667-5edc-4e63-db2c-08d9f2827a75', N'Corsair', 0, CAST(N'2022-02-18T02:06:57.2781193' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Brands] ([Id], [BrandName], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'4fc551b6-1371-424f-db2d-08d9f2827a75', N'Sennheiser', 0, CAST(N'2022-02-18T02:07:14.0804928' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
GO
INSERT [dbo].[Categories] ([Id], [CategoryName], [CategoryLevel], [LeftBorder], [RightBorder], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'08e304a4-1e4d-409f-8f33-08d9f28171c0', N'Electronics', 1, 0, 19, 0, CAST(N'2022-02-18T01:53:22.3464298' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryName], [CategoryLevel], [LeftBorder], [RightBorder], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'ab754b9a-b6f7-4c16-8f34-08d9f28171c0', N'Computers', 2, 1, 12, 0, CAST(N'2022-02-18T01:53:44.9367324' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryName], [CategoryLevel], [LeftBorder], [RightBorder], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'3029de27-e9c0-4341-8f35-08d9f28171c0', N'Desktop Computers', 3, 2, 3, 0, CAST(N'2022-02-18T01:54:02.5812568' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryName], [CategoryLevel], [LeftBorder], [RightBorder], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'6400c3d0-ffd7-46a2-8f36-08d9f28171c0', N'Notebooks', 3, 4, 5, 0, CAST(N'2022-02-18T01:54:18.6393521' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryName], [CategoryLevel], [LeftBorder], [RightBorder], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'71f602cf-0959-4ae1-8f37-08d9f28171c0', N'Tablets', 3, 6, 7, 0, CAST(N'2022-02-18T01:54:32.6699024' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryName], [CategoryLevel], [LeftBorder], [RightBorder], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'cd578caa-4fcd-4fba-8f38-08d9f28171c0', N'Computer Parts', 3, 8, 9, 0, CAST(N'2022-02-18T01:54:57.2884686' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryName], [CategoryLevel], [LeftBorder], [RightBorder], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'43ae237f-0c51-473b-8f39-08d9f28171c0', N'Computer Accessories', 3, 10, 11, 0, CAST(N'2022-02-18T01:55:13.7246222' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryName], [CategoryLevel], [LeftBorder], [RightBorder], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'bb9da29c-ea47-4b57-8f3a-08d9f28171c0', N'Phones', 2, 13, 18, 0, CAST(N'2022-02-18T01:55:33.4508208' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryName], [CategoryLevel], [LeftBorder], [RightBorder], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'87551007-d65b-401f-8f3b-08d9f28171c0', N'Android Mobile Phone', 3, 14, 15, 0, CAST(N'2022-02-18T01:56:05.4304316' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CategoryName], [CategoryLevel], [LeftBorder], [RightBorder], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'f054f9b2-1ba7-4d54-8f3c-08d9f28171c0', N'IOS Mobile Phones', 3, 16, 17, 0, CAST(N'2022-02-18T01:56:20.4865428' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Colors] ON 

INSERT [dbo].[Colors] ([Id], [ColorName], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (1, N'Black', CAST(N'2022-02-18T01:57:09.8333607' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Colors] ([Id], [ColorName], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (2, N'White', CAST(N'2022-02-18T01:57:14.6997973' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Colors] ([Id], [ColorName], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (3, N'Gray', CAST(N'2022-02-18T01:57:19.2929960' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Colors] ([Id], [ColorName], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (4, N'Green', CAST(N'2022-02-18T01:57:23.2223563' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Colors] ([Id], [ColorName], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (5, N'Yellow', CAST(N'2022-02-18T01:57:27.5178820' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Colors] ([Id], [ColorName], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (6, N'Blue', CAST(N'2022-02-18T01:57:31.6883229' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Colors] ([Id], [ColorName], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (7, N'Purple', CAST(N'2022-02-18T01:57:36.9000257' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Colors] ([Id], [ColorName], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (8, N'Orange', CAST(N'2022-02-18T01:57:41.5619326' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Colors] ([Id], [ColorName], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (9, N'Red', CAST(N'2022-02-18T01:57:45.6157203' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Colors] ([Id], [ColorName], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (10, N'Pink', CAST(N'2022-02-18T01:58:53.3756264' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Colors] ([Id], [ColorName], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (11, N'Golden', CAST(N'2022-02-18T01:59:03.3989102' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Colors] ([Id], [ColorName], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (12, N'Piano Black', CAST(N'2022-02-18T01:59:11.5204977' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Colors] ([Id], [ColorName], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (13, N'Brown', CAST(N'2022-02-18T01:59:28.6180511' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Colors] ([Id], [ColorName], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (14, N'Teal', CAST(N'2022-02-18T01:59:35.3604849' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Colors] ([Id], [ColorName], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (15, N'Silver', CAST(N'2022-02-18T01:59:44.1760955' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Colors] OFF
GO
INSERT [dbo].[Emails] ([Id], [EmailType], [EmailStatus], [IsDeleted], [ReceiverId], [AttemptCount], [MailResponse], [MailResponseDate], [IsMoved], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'80724cea-92da-4c09-b3cb-08d9f280dfaa', 1, 2, 0, N'496ce0b0-e481-422a-250d-08d9f280df95', 1, N'2.0.0 OK  1645148961 5sm1718962ejl.211 - gsmtp', CAST(N'2022-02-18T01:49:22.3369074' AS DateTime2), 0, CAST(N'2022-02-18T01:49:17.2998129' AS DateTime2), CAST(N'2022-02-18T01:49:22.5367686' AS DateTime2), N'Initial Create', N'Initial Create', N'Email Service', N'Email Service')
INSERT [dbo].[Emails] ([Id], [EmailType], [EmailStatus], [IsDeleted], [ReceiverId], [AttemptCount], [MailResponse], [MailResponseDate], [IsMoved], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'af5b3c2f-d311-4f2e-b3cc-08d9f280dfaa', 2, 2, 0, N'496ce0b0-e481-422a-250d-08d9f280df95', 1, N'2.0.0 OK  1645150206 ed11sm3842542edb.81 - gsmtp', CAST(N'2022-02-18T02:10:07.0680147' AS DateTime2), 0, CAST(N'2022-02-18T02:10:05.5694346' AS DateTime2), CAST(N'2022-02-18T02:10:07.1272144' AS DateTime2), N'Initial Create', N'Initial Create', N'Email Service', N'Email Service')
INSERT [dbo].[Emails] ([Id], [EmailType], [EmailStatus], [IsDeleted], [ReceiverId], [AttemptCount], [MailResponse], [MailResponseDate], [IsMoved], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'860f67fb-3d01-4571-bbbb-08d9f30e2429', 2, 2, 0, N'496ce0b0-e481-422a-250d-08d9f280df95', 1, N'2.0.0 OK  1645209634 l19sm5286315edb.87 - gsmtp', CAST(N'2022-02-18T18:40:35.7443391' AS DateTime2), 0, CAST(N'2022-02-18T18:40:31.5092274' AS DateTime2), CAST(N'2022-02-18T18:40:35.9061196' AS DateTime2), N'Initial Create', N'Initial Create', N'Email Worker Service', N'ScanEmailTableJob')
INSERT [dbo].[Emails] ([Id], [EmailType], [EmailStatus], [IsDeleted], [ReceiverId], [AttemptCount], [MailResponse], [MailResponseDate], [IsMoved], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'7588c65e-6742-48c4-bbbc-08d9f30e2429', 2, 2, 0, N'496ce0b0-e481-422a-250d-08d9f280df95', 1, N'2.0.0 OK  1645209730 l2sm4840092eds.28 - gsmtp', CAST(N'2022-02-18T18:42:11.5233254' AS DateTime2), 0, CAST(N'2022-02-18T18:42:09.4086182' AS DateTime2), CAST(N'2022-02-18T18:42:11.5740294' AS DateTime2), N'Initial Create', N'Initial Create', N'Email Worker Service', N'ScanEmailTableJob')
INSERT [dbo].[Emails] ([Id], [EmailType], [EmailStatus], [IsDeleted], [ReceiverId], [AttemptCount], [MailResponse], [MailResponseDate], [IsMoved], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'b576549b-f8be-469a-71d3-08d9f30eb958', 2, 2, 0, N'496ce0b0-e481-422a-250d-08d9f280df95', 1, N'2.0.0 OK  1645209883 z14sm5067269edc.62 - gsmtp', CAST(N'2022-02-18T18:44:44.1157247' AS DateTime2), 0, CAST(N'2022-02-18T18:44:41.8433256' AS DateTime2), CAST(N'2022-02-18T18:44:44.2698078' AS DateTime2), N'Initial Create', N'Initial Create', N'Email Worker Service', N'ScanEmailTableJob')
INSERT [dbo].[Emails] ([Id], [EmailType], [EmailStatus], [IsDeleted], [ReceiverId], [AttemptCount], [MailResponse], [MailResponseDate], [IsMoved], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'365efd3f-1697-457f-71d4-08d9f30eb958', 2, 2, 0, N'496ce0b0-e481-422a-250d-08d9f280df95', 1, N'2.0.0 OK  1645209982 l2sm4844123eds.28 - gsmtp', CAST(N'2022-02-18T18:46:23.6355435' AS DateTime2), 0, CAST(N'2022-02-18T18:46:21.6373412' AS DateTime2), CAST(N'2022-02-18T18:46:23.6855372' AS DateTime2), N'Initial Create', N'Initial Create', N'Email Worker Service', N'ScanEmailTableJob')
INSERT [dbo].[Emails] ([Id], [EmailType], [EmailStatus], [IsDeleted], [ReceiverId], [AttemptCount], [MailResponse], [MailResponseDate], [IsMoved], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'66a9aea0-eeca-425c-71d5-08d9f30eb958', 3, 2, 0, N'496ce0b0-e481-422a-250d-08d9f280df95', 1, N'2.0.0 OK  1645210062 m25sm2556766ejl.38 - gsmtp', CAST(N'2022-02-18T18:47:43.0421211' AS DateTime2), 0, CAST(N'2022-02-18T18:47:41.5304217' AS DateTime2), CAST(N'2022-02-18T18:47:43.0911880' AS DateTime2), N'Initial Create', N'Initial Create', N'Email Worker Service', N'ScanEmailTableJob')
INSERT [dbo].[Emails] ([Id], [EmailType], [EmailStatus], [IsDeleted], [ReceiverId], [AttemptCount], [MailResponse], [MailResponseDate], [IsMoved], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'eaf25039-b873-4932-e78b-08d9f313743d', 1, 2, 0, N'13376cc2-322a-4026-65b8-08d9f313743d', 1, N'2.0.0 OK  1645211915 q19sm2506791ejm.74 - gsmtp', CAST(N'2022-02-18T19:18:36.1763101' AS DateTime2), 0, CAST(N'2022-02-18T19:18:33.0773459' AS DateTime2), CAST(N'2022-02-18T19:18:36.3202841' AS DateTime2), N'Initial Create', N'Initial Create', N'Email Worker Service', N'ScanEmailTableJob')
GO
INSERT [dbo].[LockoutRecords] ([Id], [LockoutBegin], [LockoutUntil], [UserId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'793f7d40-a93e-4416-1aa5-08d9f283c7be', CAST(N'2022-02-18T02:10:04.9256069' AS DateTime2), CAST(N'2022-02-18T02:11:04.9253575' AS DateTime2), N'496ce0b0-e481-422a-250d-08d9f280df95', 0, CAST(N'2022-02-18T02:10:05.5694371' AS DateTime2), NULL, N'Initial Create', N'Initial Create', NULL, NULL)
INSERT [dbo].[LockoutRecords] ([Id], [LockoutBegin], [LockoutUntil], [UserId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'c9d98753-dbec-4e3e-bfbb-08d9f30e245a', CAST(N'2022-02-18T18:40:31.1233858' AS DateTime2), CAST(N'2022-02-18T18:41:31.1232416' AS DateTime2), N'496ce0b0-e481-422a-250d-08d9f280df95', 0, CAST(N'2022-02-18T18:40:31.5093889' AS DateTime2), NULL, N'Initial Create', N'Initial Create', NULL, NULL)
INSERT [dbo].[LockoutRecords] ([Id], [LockoutBegin], [LockoutUntil], [UserId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'8d6557d6-bf72-451d-bfbc-08d9f30e245a', CAST(N'2022-02-18T18:42:09.3398728' AS DateTime2), CAST(N'2022-02-18T18:43:09.3398682' AS DateTime2), N'496ce0b0-e481-422a-250d-08d9f280df95', 0, CAST(N'2022-02-18T18:42:09.4086718' AS DateTime2), NULL, N'Initial Create', N'Initial Create', NULL, NULL)
INSERT [dbo].[LockoutRecords] ([Id], [LockoutBegin], [LockoutUntil], [UserId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'73d15441-86c3-4fe6-91b9-08d9f30eb98c', CAST(N'2022-02-18T18:44:41.4073904' AS DateTime2), CAST(N'2022-02-18T18:45:41.4072804' AS DateTime2), N'496ce0b0-e481-422a-250d-08d9f280df95', 0, CAST(N'2022-02-18T18:44:41.8435719' AS DateTime2), NULL, N'Initial Create', N'Initial Create', NULL, NULL)
INSERT [dbo].[LockoutRecords] ([Id], [LockoutBegin], [LockoutUntil], [UserId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'3fbf7736-9505-401f-91ba-08d9f30eb98c', CAST(N'2022-02-18T18:46:21.6256754' AS DateTime2), CAST(N'2022-02-18T18:47:21.6256737' AS DateTime2), N'496ce0b0-e481-422a-250d-08d9f280df95', 0, CAST(N'2022-02-18T18:46:21.6374057' AS DateTime2), NULL, N'Initial Create', N'Initial Create', NULL, NULL)
GO
INSERT [dbo].[Offers] ([Id], [OfferStatus], [OfferPercentage], [ProductId], [ProductOwnerId], [OffererId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'4077d823-e99a-4280-53a0-08d9f3157c33', 1, 20, N'73dea965-ac4d-4be7-44d6-08d9f312c8d3', N'496ce0b0-e481-422a-250d-08d9f280df95', N'13376cc2-322a-4026-65b8-08d9f313743d', 0, CAST(N'2022-02-18T19:33:05.4259744' AS DateTime2), NULL, N'Mehmet Tanirgan', N'13376cc2-322a-4026-65b8-08d9f313743d', NULL, NULL)
GO
INSERT [dbo].[ProductImages] ([Id], [ImageUrl], [ProductId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'1a8f8a82-abcc-4c76-bca9-08d9f312c8e8', N'http://res.cloudinary.com/dt107fl3n/image/upload/v1645211623/vazh7ayewbg42jfckyhz.png', N'73dea965-ac4d-4be7-44d6-08d9f312c8d3', 0, CAST(N'2022-02-18T19:13:45.6233501' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[ProductImages] ([Id], [ImageUrl], [ProductId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'70623d04-95d8-4ad7-bcaa-08d9f312c8e8', N'http://res.cloudinary.com/dt107fl3n/image/upload/v1645211872/ywv04qtduqijlfsgxmlj.jpg', N'5f180cc9-728b-446e-44d7-08d9f312c8d3', 0, CAST(N'2022-02-18T19:17:53.3496086' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[ProductImages] ([Id], [ImageUrl], [ProductId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'0bc00e05-95fb-425f-bcab-08d9f312c8e8', N'http://res.cloudinary.com/dt107fl3n/image/upload/v1645212267/wt1kcswahgmijswfn41o.webp', N'7fbee3bb-2a1c-4e09-44d8-08d9f312c8d3', 0, CAST(N'2022-02-18T19:24:28.4134557' AS DateTime2), NULL, N'Mehmet Tanirgan', N'13376cc2-322a-4026-65b8-08d9f313743d', NULL, NULL)
INSERT [dbo].[ProductImages] ([Id], [ImageUrl], [ProductId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'e3b0e79a-2a4b-4fc5-bcac-08d9f312c8e8', N'http://res.cloudinary.com/dt107fl3n/image/upload/v1645212511/cp9rhogn94fb1wvxb0sx.jpg', N'4820b70d-d554-4670-44d9-08d9f312c8d3', 0, CAST(N'2022-02-18T19:28:34.3558351' AS DateTime2), NULL, N'Mehmet Tanirgan', N'13376cc2-322a-4026-65b8-08d9f313743d', NULL, NULL)
INSERT [dbo].[ProductImages] ([Id], [ImageUrl], [ProductId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'b509dd7c-3b0f-4c41-bcad-08d9f312c8e8', N'http://res.cloudinary.com/dt107fl3n/image/upload/v1645212512/ietl5pts4wdf7cxydf59.webp', N'4820b70d-d554-4670-44d9-08d9f312c8d3', 0, CAST(N'2022-02-18T19:28:34.3558364' AS DateTime2), NULL, N'Mehmet Tanirgan', N'13376cc2-322a-4026-65b8-08d9f313743d', NULL, NULL)
INSERT [dbo].[ProductImages] ([Id], [ImageUrl], [ProductId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'b7575af5-80c1-46c9-bcae-08d9f312c8e8', N'http://res.cloudinary.com/dt107fl3n/image/upload/v1645212513/ljh6lqbvwhxtwcxhcfxx.webp', N'4820b70d-d554-4670-44d9-08d9f312c8d3', 0, CAST(N'2022-02-18T19:28:34.3558369' AS DateTime2), NULL, N'Mehmet Tanirgan', N'13376cc2-322a-4026-65b8-08d9f313743d', NULL, NULL)
GO
INSERT [dbo].[Products] ([Id], [ProductName], [Description], [Price], [ProductOwnerId], [IsOfferable], [IsSold], [UsageCondition], [CategoryId], [BrandId], [ColorId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'73dea965-ac4d-4be7-44d6-08d9f312c8d3', N'iPhone 13 Pro', N'iPhone 13 Pro 256 GB Sierra Blue', CAST(999.00 AS Decimal(8, 2)), N'496ce0b0-e481-422a-250d-08d9f280df95', 1, 0, 1, N'f054f9b2-1ba7-4d54-8f3c-08d9f28171c0', N'8054a8de-98c4-4c7f-db14-08d9f2827a75', 6, 0, CAST(N'2022-02-18T19:13:45.6233320' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Products] ([Id], [ProductName], [Description], [Price], [ProductOwnerId], [IsOfferable], [IsSold], [UsageCondition], [CategoryId], [BrandId], [ColorId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'5f180cc9-728b-446e-44d7-08d9f312c8d3', N'HUAWEI MateBook D15', N'HUAWEI MateBook D15 Windows 10 Home i3 - 8GB + 256GB', CAST(550.00 AS Decimal(8, 2)), N'496ce0b0-e481-422a-250d-08d9f280df95', 0, 0, 2, N'6400c3d0-ffd7-46a2-8f36-08d9f28171c0', N'48d8f660-9e56-4617-db16-08d9f2827a75', 3, 0, CAST(N'2022-02-18T19:17:53.3495582' AS DateTime2), NULL, N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95', NULL, NULL)
INSERT [dbo].[Products] ([Id], [ProductName], [Description], [Price], [ProductOwnerId], [IsOfferable], [IsSold], [UsageCondition], [CategoryId], [BrandId], [ColorId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'7fbee3bb-2a1c-4e09-44d8-08d9f312c8d3', N'SteelSeries Arctis Pro Wireless Hi-Res Gaming Headset', N'SteelSeries Arctis Pro Wireless Hi-Res Gaming Headset', CAST(450.00 AS Decimal(8, 2)), N'13376cc2-322a-4026-65b8-08d9f313743d', 1, 0, 1, N'43ae237f-0c51-473b-8f39-08d9f28171c0', N'd62d3dad-bee3-4c53-db24-08d9f2827a75', 1, 0, CAST(N'2022-02-18T19:24:28.4134537' AS DateTime2), NULL, N'Mehmet Tanirgan', N'13376cc2-322a-4026-65b8-08d9f313743d', NULL, NULL)
INSERT [dbo].[Products] ([Id], [ProductName], [Description], [Price], [ProductOwnerId], [IsOfferable], [IsSold], [UsageCondition], [CategoryId], [BrandId], [ColorId], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'4820b70d-d554-4670-44d9-08d9f312c8d3', N'Dell Alienware M17-6L75W16256N i7-9750H 16GB 256GB SSD 6GB RTX2060 17.3 Windows 10 AWM17-6L75W16256N', N'Dell Alienware M17-6L75W16256N i7-9750H 16GB 256GB SSD 6GB RTX2060 17.3 Windows 10 AWM17-6L75W16256N', CAST(450.00 AS Decimal(8, 2)), N'13376cc2-322a-4026-65b8-08d9f313743d', 0, 0, 2, N'6400c3d0-ffd7-46a2-8f36-08d9f28171c0', N'8131a541-c4c6-44ae-db1b-08d9f2827a75', 3, 0, CAST(N'2022-02-18T19:28:34.3558302' AS DateTime2), NULL, N'Mehmet Tanirgan', N'13376cc2-322a-4026-65b8-08d9f313743d', NULL, NULL)
GO
INSERT [dbo].[Users] ([Id], [Firstname], [Lastname], [PhoneNumber], [EmailAddress], [RefreshToken], [HashedPassword], [PasswordSalt], [LastLoginIP], [RefreshTokenExpireDate], [Role], [EmailConfirmed], [FailedLoginAttemptCount], [LockoutEnabled], [DailyLockoutCount], [PermaBlockEnabled], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'496ce0b0-e481-422a-250d-08d9f280df95', N'Mehmet Gobirin', N'Tanirgan', N'5000000000', N'mehmettanirgan1993@gmail.com', N'PT5x6ewxGrXmmg2HTxrW97uI4jXzIZXwIb2mvsZPrX9HWuMVN+UXtbubHP8btYnFw2b43nCIPxJvfvSosIOFww==', 0xACBBDAA9B5AE980BEF84C9AFADD6BFBA61EE5B682274A6B8734319EE8CE838D7, 0x1291A6F64EF073C4792EACCCED5C0196, NULL, CAST(N'2022-02-18T20:18:28.5058597' AS DateTime2), N'Admin', 0, 18, 0, 3, 0, 0, CAST(N'2022-02-18T01:49:17.2996301' AS DateTime2), CAST(N'2022-02-18T19:13:28.5062982' AS DateTime2), N'Initial Create', N'Initial Create', N'Mehmet Gobirin Tanirgan', N'496ce0b0-e481-422a-250d-08d9f280df95')
INSERT [dbo].[Users] ([Id], [Firstname], [Lastname], [PhoneNumber], [EmailAddress], [RefreshToken], [HashedPassword], [PasswordSalt], [LastLoginIP], [RefreshTokenExpireDate], [Role], [EmailConfirmed], [FailedLoginAttemptCount], [LockoutEnabled], [DailyLockoutCount], [PermaBlockEnabled], [IsDeleted], [CreatedDate], [UpdatedDate], [CreatedBy], [CreatedById], [UpdatedBy], [UpdatedById]) VALUES (N'13376cc2-322a-4026-65b8-08d9f313743d', N'Mehmet', N'Tanirgan', N'5000000001', N'mehmet.g.tanirgan@gmail.com', N'nLnzYYCSiacn2dE+CyUNcdjPDq3tgFNhsVaq39xZjojyCBfmVCkwK1YLIuACgpEOUKh+xrqdO/I/ofq0Om0vug==', 0xCFD6EE81D4745A3500D8492E0AED0D00CC6BB0311BF4D7C72D77FB41D81A65E1, 0xBD3D508C2CF1CD60D53B2A7603CEF636, NULL, CAST(N'2022-02-18T20:36:00.4018354' AS DateTime2), N'User', 0, 0, 0, 0, 0, 0, CAST(N'2022-02-18T19:18:33.0773278' AS DateTime2), CAST(N'2022-02-18T19:31:00.4024980' AS DateTime2), N'Initial Create', N'Initial Create', NULL, NULL)
GO
USE [master]
GO
ALTER DATABASE [MgtPcsDb] SET  READ_WRITE 
GO
