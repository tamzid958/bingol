USE [master]
GO
/****** Object:  Database [bingol]    Script Date: 1/26/2021 2:08:39 AM ******/
CREATE DATABASE [bingol]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'bingol', FILENAME = N'D:\SQL\MSSQL15.MSSQLSERVER\MSSQL\DATA\bingol.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'bingol_log', FILENAME = N'D:\SQL\MSSQL15.MSSQLSERVER\MSSQL\DATA\bingol_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [bingol] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [bingol].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [bingol] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [bingol] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [bingol] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [bingol] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [bingol] SET ARITHABORT OFF 
GO
ALTER DATABASE [bingol] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [bingol] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [bingol] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [bingol] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [bingol] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [bingol] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [bingol] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [bingol] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [bingol] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [bingol] SET  DISABLE_BROKER 
GO
ALTER DATABASE [bingol] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [bingol] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [bingol] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [bingol] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [bingol] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [bingol] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [bingol] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [bingol] SET RECOVERY FULL 
GO
ALTER DATABASE [bingol] SET  MULTI_USER 
GO
ALTER DATABASE [bingol] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [bingol] SET DB_CHAINING OFF 
GO
ALTER DATABASE [bingol] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [bingol] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [bingol] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [bingol] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'bingol', N'ON'
GO
ALTER DATABASE [bingol] SET QUERY_STORE = OFF
GO
USE [bingol]
GO
/****** Object:  User [NT AUTHORITY\SYSTEM]    Script Date: 1/26/2021 2:08:39 AM ******/
CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
/****** Object:  Schema [bingol]    Script Date: 1/26/2021 2:08:39 AM ******/
CREATE SCHEMA [bingol]
GO
/****** Object:  Table [bingol].[optiongroups]    Script Date: 1/26/2021 2:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [bingol].[optiongroups](
	[OptionGroupID] [int] IDENTITY(4,1) NOT NULL,
	[OptionGroupName] [varchar](50) NULL,
 CONSTRAINT [PK_optiongroups_OptionGroupID] PRIMARY KEY CLUSTERED 
(
	[OptionGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [bingol].[options]    Script Date: 1/26/2021 2:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [bingol].[options](
	[OptionID] [int] IDENTITY(9,1) NOT NULL,
	[OptionName] [varchar](50) NULL,
 CONSTRAINT [PK_options_OptionID] PRIMARY KEY CLUSTERED 
(
	[OptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [bingol].[orderdetails]    Script Date: 1/26/2021 2:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [bingol].[orderdetails](
	[DetailID] [int] IDENTITY(1,1) NOT NULL,
	[DetailOrderID] [int] NOT NULL,
	[DetailProductID] [int] NOT NULL,
	[DetailName] [varchar](250) NOT NULL,
	[DetailPrice] [real] NOT NULL,
	[DetailSKU] [varchar](50) NOT NULL,
	[DetailQuantity] [int] NOT NULL,
 CONSTRAINT [PK_orderdetails_DetailID] PRIMARY KEY CLUSTERED 
(
	[DetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [bingol].[orders]    Script Date: 1/26/2021 2:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [bingol].[orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderUserID] [int] NOT NULL,
	[OrderAmount] [real] NOT NULL,
	[OrderShipName] [varchar](100) NOT NULL,
	[OrderShipAddress] [varchar](100) NOT NULL,
	[OrderShipAddress2] [varchar](100) NOT NULL,
	[OrderCity] [varchar](50) NOT NULL,
	[OrderState] [varchar](50) NOT NULL,
	[OrderZip] [varchar](20) NOT NULL,
	[OrderCountry] [varchar](50) NOT NULL,
	[OrderPhone] [varchar](20) NOT NULL,
	[OrderFax] [varchar](20) NOT NULL,
	[OrderShipping] [real] NOT NULL,
	[OrderTax] [real] NOT NULL,
	[OrderEmail] [varchar](100) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[OrderShipped] [smallint] NOT NULL,
	[OrderTrackingNumber] [varchar](80) NULL,
 CONSTRAINT [PK_orders_OrderID] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [bingol].[productcategories]    Script Date: 1/26/2021 2:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [bingol].[productcategories](
	[CategoryID] [int] IDENTITY(7,1) NOT NULL,
	[CategoryName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_productcategories_CategoryID] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [bingol].[productoptions]    Script Date: 1/26/2021 2:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [bingol].[productoptions](
	[ProductOptionID] [bigint] IDENTITY(9,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[OptionID] [int] NOT NULL,
	[OptionPriceIncrement] [float] NULL,
	[OptionGroupID] [int] NOT NULL,
 CONSTRAINT [PK_productoptions_ProductOptionID] PRIMARY KEY CLUSTERED 
(
	[ProductOptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [bingol].[products]    Script Date: 1/26/2021 2:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [bingol].[products](
	[ProductID] [int] IDENTITY(991,1) NOT NULL,
	[ProductSKU] [varchar](50) NOT NULL,
	[ProductName] [varchar](100) NOT NULL,
	[ProductPrice] [real] NOT NULL,
	[ProductWeight] [real] NOT NULL,
	[ProductCartDesc] [varchar](250) NOT NULL,
	[ProductShortDesc] [varchar](1000) NOT NULL,
	[ProductLongDesc] [varchar](max) NOT NULL,
	[ProductThumb] [varchar](100) NOT NULL,
	[ProductImage] [varchar](100) NOT NULL,
	[ProductCategoryID] [int] NULL,
	[ProductUpdateDate] [datetime] NOT NULL,
	[ProductStock] [real] NULL,
	[ProductLive] [smallint] NULL,
	[ProductUnlimited] [smallint] NULL,
	[ProductLocation] [varchar](250) NULL,
 CONSTRAINT [PK_products_ProductID] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [bingol].[reviews]    Script Date: 1/26/2021 2:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [bingol].[reviews](
	[ReviewID] [int] NOT NULL,
	[ReviewRating] [int] NULL,
	[ReviewProductID] [int] NOT NULL,
	[ReviewUserID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [bingol].[users]    Script Date: 1/26/2021 2:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [bingol].[users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserEmail] [varchar](500) NULL,
	[UserPassword] [varchar](500) NULL,
	[UserFirstName] [varchar](50) NULL,
	[UserLastName] [varchar](50) NULL,
	[UserCity] [varchar](90) NULL,
	[UserState] [varchar](20) NULL,
	[UserZip] [varchar](12) NULL,
	[UserEmailVerified] [smallint] NULL,
	[UserRegistrationDate] [datetime] NULL,
	[UserVerificationCode] [varchar](20) NULL,
	[UserIP] [varchar](50) NULL,
	[UserPhone] [varchar](20) NULL,
	[UserFax] [varchar](20) NULL,
	[UserCountry] [varchar](20) NULL,
	[UserAddress] [varchar](100) NULL,
	[UserAddress2] [varchar](50) NULL,
	[UserType] [smallint] NOT NULL,
 CONSTRAINT [PK_users_UserID] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [bingol].[wishlists]    Script Date: 1/26/2021 2:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [bingol].[wishlists](
	[WishlistID] [int] NOT NULL,
	[Wishlist] [smallint] NULL,
	[WishlistProductID] [int] NOT NULL,
	[WishlistUserID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[WishlistID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [bingol].[optiongroups] ADD  DEFAULT (NULL) FOR [OptionGroupName]
GO
ALTER TABLE [bingol].[options] ADD  DEFAULT (NULL) FOR [OptionName]
GO
ALTER TABLE [bingol].[orders] ADD  DEFAULT (getdate()) FOR [OrderDate]
GO
ALTER TABLE [bingol].[orders] ADD  DEFAULT ((0)) FOR [OrderShipped]
GO
ALTER TABLE [bingol].[orders] ADD  DEFAULT (NULL) FOR [OrderTrackingNumber]
GO
ALTER TABLE [bingol].[productoptions] ADD  DEFAULT (NULL) FOR [OptionPriceIncrement]
GO
ALTER TABLE [bingol].[products] ADD  DEFAULT (NULL) FOR [ProductCategoryID]
GO
ALTER TABLE [bingol].[products] ADD  DEFAULT (getdate()) FOR [ProductUpdateDate]
GO
ALTER TABLE [bingol].[products] ADD  DEFAULT (NULL) FOR [ProductStock]
GO
ALTER TABLE [bingol].[products] ADD  DEFAULT ((0)) FOR [ProductLive]
GO
ALTER TABLE [bingol].[products] ADD  DEFAULT ((1)) FOR [ProductUnlimited]
GO
ALTER TABLE [bingol].[products] ADD  DEFAULT (NULL) FOR [ProductLocation]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT (NULL) FOR [UserEmail]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT (NULL) FOR [UserPassword]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT (NULL) FOR [UserFirstName]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT (NULL) FOR [UserLastName]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT (NULL) FOR [UserCity]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT (NULL) FOR [UserState]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT (NULL) FOR [UserZip]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT ((0)) FOR [UserEmailVerified]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT (getdate()) FOR [UserRegistrationDate]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT (NULL) FOR [UserVerificationCode]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT (NULL) FOR [UserIP]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT (NULL) FOR [UserPhone]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT (NULL) FOR [UserFax]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT (NULL) FOR [UserCountry]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT (NULL) FOR [UserAddress]
GO
ALTER TABLE [bingol].[users] ADD  DEFAULT (NULL) FOR [UserAddress2]
GO
ALTER TABLE [bingol].[orderdetails]  WITH CHECK ADD  CONSTRAINT [FK_orderdetails_DetailOrderID] FOREIGN KEY([DetailOrderID])
REFERENCES [bingol].[orders] ([OrderID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [bingol].[orderdetails] CHECK CONSTRAINT [FK_orderdetails_DetailOrderID]
GO
ALTER TABLE [bingol].[orderdetails]  WITH CHECK ADD  CONSTRAINT [FK_orderdetails_DetailProductID] FOREIGN KEY([DetailProductID])
REFERENCES [bingol].[products] ([ProductID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [bingol].[orderdetails] CHECK CONSTRAINT [FK_orderdetails_DetailProductID]
GO
ALTER TABLE [bingol].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_OrderUserID] FOREIGN KEY([OrderUserID])
REFERENCES [bingol].[users] ([UserID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [bingol].[orders] CHECK CONSTRAINT [FK_orders_OrderUserID]
GO
ALTER TABLE [bingol].[productoptions]  WITH CHECK ADD  CONSTRAINT [FK_productoptions_OptionGroupID] FOREIGN KEY([OptionGroupID])
REFERENCES [bingol].[optiongroups] ([OptionGroupID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [bingol].[productoptions] CHECK CONSTRAINT [FK_productoptions_OptionGroupID]
GO
ALTER TABLE [bingol].[productoptions]  WITH CHECK ADD  CONSTRAINT [FK_productoptions_optionID] FOREIGN KEY([OptionID])
REFERENCES [bingol].[options] ([OptionID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [bingol].[productoptions] CHECK CONSTRAINT [FK_productoptions_optionID]
GO
ALTER TABLE [bingol].[productoptions]  WITH CHECK ADD  CONSTRAINT [FK_productoptions_ProductID] FOREIGN KEY([ProductID])
REFERENCES [bingol].[products] ([ProductID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [bingol].[productoptions] CHECK CONSTRAINT [FK_productoptions_ProductID]
GO
ALTER TABLE [bingol].[products]  WITH CHECK ADD  CONSTRAINT [FK_product_ProductCategoryID] FOREIGN KEY([ProductCategoryID])
REFERENCES [bingol].[productcategories] ([CategoryID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [bingol].[products] CHECK CONSTRAINT [FK_product_ProductCategoryID]
GO
ALTER TABLE [bingol].[reviews]  WITH CHECK ADD FOREIGN KEY([ReviewProductID])
REFERENCES [bingol].[products] ([ProductID])
GO
ALTER TABLE [bingol].[reviews]  WITH CHECK ADD FOREIGN KEY([ReviewUserID])
REFERENCES [bingol].[users] ([UserID])
GO
ALTER TABLE [bingol].[wishlists]  WITH CHECK ADD FOREIGN KEY([WishlistProductID])
REFERENCES [bingol].[products] ([ProductID])
GO
ALTER TABLE [bingol].[wishlists]  WITH CHECK ADD FOREIGN KEY([WishlistUserID])
REFERENCES [bingol].[users] ([UserID])
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'bingol.optiongroups' , @level0type=N'SCHEMA',@level0name=N'bingol', @level1type=N'TABLE',@level1name=N'optiongroups'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'bingol.options' , @level0type=N'SCHEMA',@level0name=N'bingol', @level1type=N'TABLE',@level1name=N'options'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'bingol.orderdetails' , @level0type=N'SCHEMA',@level0name=N'bingol', @level1type=N'TABLE',@level1name=N'orderdetails'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'bingol.orders' , @level0type=N'SCHEMA',@level0name=N'bingol', @level1type=N'TABLE',@level1name=N'orders'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'bingol.productcategories' , @level0type=N'SCHEMA',@level0name=N'bingol', @level1type=N'TABLE',@level1name=N'productcategories'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'bingol.productoptions' , @level0type=N'SCHEMA',@level0name=N'bingol', @level1type=N'TABLE',@level1name=N'productoptions'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'bingol.products' , @level0type=N'SCHEMA',@level0name=N'bingol', @level1type=N'TABLE',@level1name=N'products'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'bingol.users' , @level0type=N'SCHEMA',@level0name=N'bingol', @level1type=N'TABLE',@level1name=N'users'
GO
USE [master]
GO
ALTER DATABASE [bingol] SET  READ_WRITE 
GO
