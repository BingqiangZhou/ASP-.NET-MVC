
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/10/2018 10:26:06
-- Generated from EDMX file: E:\C#\ASP\Learing\MVCLearning\Chapter1_Control\Model\Models\ModelEntity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MVC];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Commodity]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Commodity];
GO
IF OBJECT_ID(N'[dbo].[PurchaseOrders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PurchaseOrders];
GO
IF OBJECT_ID(N'[dbo].[SellOrders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SellOrders];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Commodities'
CREATE TABLE [dbo].[Commodities] (
    [CommodityId] nvarchar(32)  NOT NULL,
    [CommodityName] nvarchar(32)  NOT NULL,
    [CommodityPrice] int  NOT NULL,
    [CommodityAmount] int  NOT NULL,
    [CommodityImage] nvarchar(50)  NULL
);
GO

-- Creating table 'PurchaseOrders'
CREATE TABLE [dbo].[PurchaseOrders] (
    [PurchaseOrderId] nvarchar(32)  NOT NULL,
    [CommodityId] nvarchar(32)  NOT NULL,
    [CommodityAmount] int  NOT NULL
);
GO

-- Creating table 'SellOrders'
CREATE TABLE [dbo].[SellOrders] (
    [OrderId] nvarchar(32)  NOT NULL,
    [CommodityID] nvarchar(32)  NOT NULL,
    [CommodityAmount] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Email] nvarchar(32)  NOT NULL,
    [Name] nvarchar(16)  NOT NULL,
    [Password] nvarchar(15)  NOT NULL,
    [Status] tinyint  NOT NULL,
    [Rank] tinyint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CommodityId] in table 'Commodities'
ALTER TABLE [dbo].[Commodities]
ADD CONSTRAINT [PK_Commodities]
    PRIMARY KEY CLUSTERED ([CommodityId] ASC);
GO

-- Creating primary key on [PurchaseOrderId] in table 'PurchaseOrders'
ALTER TABLE [dbo].[PurchaseOrders]
ADD CONSTRAINT [PK_PurchaseOrders]
    PRIMARY KEY CLUSTERED ([PurchaseOrderId] ASC);
GO

-- Creating primary key on [OrderId] in table 'SellOrders'
ALTER TABLE [dbo].[SellOrders]
ADD CONSTRAINT [PK_SellOrders]
    PRIMARY KEY CLUSTERED ([OrderId] ASC);
GO

-- Creating primary key on [Email] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Email] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------