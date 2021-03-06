USE [MyStoreDb]
GO
/****** Object:  User [USER_DEV]    Script Date: 29/6/2020 18:47:04 ******/
CREATE USER [USER_DEV] FOR LOGIN [dev] WITH DEFAULT_SCHEMA=[db_datareader]
GO
/****** Object:  User [USER_DEVELOPER]    Script Date: 29/6/2020 18:47:04 ******/
CREATE USER [USER_DEVELOPER] FOR LOGIN [developer] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  DatabaseRole [Executer]    Script Date: 29/6/2020 18:47:05 ******/
CREATE ROLE [Executer]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 29/6/2020 18:47:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Application] [nvarchar](50) NOT NULL,
	[Logged] [datetime] NOT NULL,
	[Level] [nvarchar](50) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Logger] [nvarchar](250) NULL,
	[Callsite] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 29/6/2020 18:47:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ProductCategoryId] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[ProductBrandId] [int] NOT NULL,
	[Stock] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 29/6/2020 18:47:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ProductCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductoBrand]    Script Date: 29/6/2020 18:47:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductoBrand](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [ProductCategoryId], [Price], [ProductBrandId], [Stock]) VALUES (13, N'Gaseosa 1l', 3, 90, 2, 500)
INSERT [dbo].[Product] ([Id], [Name], [ProductCategoryId], [Price], [ProductBrandId], [Stock]) VALUES (14, N'Lavandina', 2, 70, 2, 300)
INSERT [dbo].[Product] ([Id], [Name], [ProductCategoryId], [Price], [ProductBrandId], [Stock]) VALUES (16, N'Jabon', 1, 70, 4, 455)
INSERT [dbo].[Product] ([Id], [Name], [ProductCategoryId], [Price], [ProductBrandId], [Stock]) VALUES (17, N'Dentrifico', 4, 110, 6, 2424)
INSERT [dbo].[Product] ([Id], [Name], [ProductCategoryId], [Price], [ProductBrandId], [Stock]) VALUES (19, N'Gaseosa 2l', 3, 140, 3, 234)
INSERT [dbo].[Product] ([Id], [Name], [ProductCategoryId], [Price], [ProductBrandId], [Stock]) VALUES (21, N'Desinfectante', 1, 190, 5, 1250)
INSERT [dbo].[Product] ([Id], [Name], [ProductCategoryId], [Price], [ProductBrandId], [Stock]) VALUES (22, N'Lavaplatos', 1, 145, 8, 40)
INSERT [dbo].[Product] ([Id], [Name], [ProductCategoryId], [Price], [ProductBrandId], [Stock]) VALUES (24, N'Afeitadora Premium', 4, 550, 7, 45)
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[ProductCategory] ON 

INSERT [dbo].[ProductCategory] ([Id], [Description]) VALUES (1, N'Limpieza')
INSERT [dbo].[ProductCategory] ([Id], [Description]) VALUES (2, N'Comida')
INSERT [dbo].[ProductCategory] ([Id], [Description]) VALUES (3, N'Bebida')
INSERT [dbo].[ProductCategory] ([Id], [Description]) VALUES (4, N'Higiene')
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF
SET IDENTITY_INSERT [dbo].[ProductoBrand] ON 

INSERT [dbo].[ProductoBrand] ([Id], [Description]) VALUES (1, N'Ala')
INSERT [dbo].[ProductoBrand] ([Id], [Description]) VALUES (2, N'Pepsi')
INSERT [dbo].[ProductoBrand] ([Id], [Description]) VALUES (3, N'Coca')
INSERT [dbo].[ProductoBrand] ([Id], [Description]) VALUES (4, N'Dave')
INSERT [dbo].[ProductoBrand] ([Id], [Description]) VALUES (5, N'CIF')
INSERT [dbo].[ProductoBrand] ([Id], [Description]) VALUES (6, N'Colgate')
INSERT [dbo].[ProductoBrand] ([Id], [Description]) VALUES (7, N'Gillette')
INSERT [dbo].[ProductoBrand] ([Id], [Description]) VALUES (8, N'Magistral')
SET IDENTITY_INSERT [dbo].[ProductoBrand] OFF
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductCategories] FOREIGN KEY([ProductCategoryId])
REFERENCES [dbo].[ProductCategory] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Products_ProductCategories]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductoBrands] FOREIGN KEY([ProductBrandId])
REFERENCES [dbo].[ProductoBrand] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Products_ProductoBrands]
GO
