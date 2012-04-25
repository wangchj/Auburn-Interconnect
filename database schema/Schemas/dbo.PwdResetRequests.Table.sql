USE [interconnect]
GO
/****** Object:  Table [dbo].[PwdResetRequests]    Script Date: 04/18/2012 11:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PwdResetRequests](
	[email] [varchar](100) NULL,
	[resetcode] [varchar](10) NULL,
	[requestDate] [date] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
