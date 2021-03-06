CREATE DATABASE
hair_salon
GO
USE [hair_salon]
GO
/****** Object:  Table [dbo].[clients]    Script Date: 12/9/2016 9:47:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[client_name] [varchar](255) NULL,
	[client_details] [varchar](255) NULL,
	[stylist_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[stylists]    Script Date: 12/9/2016 9:47:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stylists](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[stylist_name] [varchar](255) NULL,
	[stylist_details] [varchar](255) NULL
) ON [PRIMARY]

GO
