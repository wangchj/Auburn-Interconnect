USE [interconnect]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 04/18/2012 11:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[uid] [int] IDENTITY(1,1) NOT NULL,
	[pwd] [binary](32) NOT NULL,
	[fname] [varchar](50) NOT NULL,
	[lname] [varchar](50) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[phone] [bigint] NOT NULL,
	[admin] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[uid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [admin]
GO
