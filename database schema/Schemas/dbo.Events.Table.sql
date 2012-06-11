USE [interconnect]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 06/10/2012 21:52:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Events](
	[eventId] [int] IDENTITY(1,1) NOT NULL,
	[creatorId] [int] NOT NULL,
	[proposalId] [int] NULL,
	[createTime] [datetime] NOT NULL,
	[approved] [bit] NOT NULL,
	[declined] [bit] NOT NULL,
	[guestLimit] [int] NULL,
	[hostOrg] [varchar](255) NULL,
	[hostName] [varchar](255) NOT NULL,
	[hostEmail] [varchar](255) NOT NULL,
	[hostPhone] [bigint] NOT NULL,
	[eventName] [varchar](255) NOT NULL,
	[descr] [text] NOT NULL,
	[startTime] [datetime] NOT NULL,
	[endTime] [datetime] NOT NULL,
	[regDeadline] [datetime] NOT NULL,
	[location] [varchar](255) NOT NULL,
	[meetLocation] [varchar](255) NOT NULL,
	[meetTime] [datetime] NOT NULL,
	[transportation] [text] NOT NULL,
	[requestDrivers] [bit] NOT NULL,
	[costs] [text] NULL,
	[equipment] [text] NULL,
	[food] [text] NULL,
	[other] [text] NULL,
 CONSTRAINT [PK__Events__2DC7BD090519C6AF] PRIMARY KEY CLUSTERED 
(
	[eventId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Users] FOREIGN KEY([creatorId])
REFERENCES [dbo].[Users] ([uid])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Users]
GO
ALTER TABLE [dbo].[Events] ADD  CONSTRAINT [DF__Events__approved__07020F21]  DEFAULT ((0)) FOR [approved]
GO
ALTER TABLE [dbo].[Events] ADD  CONSTRAINT [DF__Events__declined__07F6335A]  DEFAULT ((0)) FOR [declined]
GO
ALTER TABLE [dbo].[Events] ADD  CONSTRAINT [DF_Events_regDeadline]  DEFAULT (((2012)-(2))-(11)) FOR [regDeadline]
GO
ALTER TABLE [dbo].[Events] ADD  CONSTRAINT [DF__Events__requestD__08EA5793]  DEFAULT ((0)) FOR [requestDrivers]
GO
