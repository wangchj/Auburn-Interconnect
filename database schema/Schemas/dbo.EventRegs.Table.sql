USE [interconnect]
GO
/****** Object:  Table [dbo].[EventRegs]    Script Date: 04/18/2012 11:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventRegs](
	[eventId] [int] NOT NULL,
	[userId] [int] NOT NULL,
	[headCount] [int] NOT NULL,
	[canDrive] [bit] NOT NULL,
	[vehicleCap] [int] NOT NULL,
 CONSTRAINT [PK_EventRegs] PRIMARY KEY CLUSTERED 
(
	[eventId] ASC,
	[userId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventRegs]  WITH CHECK ADD  CONSTRAINT [FK_EventRegs_Events] FOREIGN KEY([eventId])
REFERENCES [dbo].[Events] ([eventId])
GO
ALTER TABLE [dbo].[EventRegs] CHECK CONSTRAINT [FK_EventRegs_Events]
GO
ALTER TABLE [dbo].[EventRegs]  WITH CHECK ADD  CONSTRAINT [FK_EventRegs_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([uid])
GO
ALTER TABLE [dbo].[EventRegs] CHECK CONSTRAINT [FK_EventRegs_Users]
GO
ALTER TABLE [dbo].[EventRegs] ADD  DEFAULT ((1)) FOR [headCount]
GO
ALTER TABLE [dbo].[EventRegs] ADD  DEFAULT ((0)) FOR [canDrive]
GO
ALTER TABLE [dbo].[EventRegs] ADD  DEFAULT ((0)) FOR [vehicleCap]
GO
