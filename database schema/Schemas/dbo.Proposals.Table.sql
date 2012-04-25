USE [interconnect]
GO
/****** Object:  Table [dbo].[Proposals]    Script Date: 04/18/2012 11:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Proposals](
	[proposalId] [int] IDENTITY(1,1) NOT NULL,
	[createTime] [datetime] NOT NULL,
	[creatorId] [int] NOT NULL,
	[title] [varchar](50) NOT NULL,
	[content] [text] NOT NULL,
	[approved] [bit] NOT NULL,
	[declined] [bit] NOT NULL,
 CONSTRAINT [PK_Proposals] PRIMARY KEY CLUSTERED 
(
	[proposalId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Proposals] ADD  CONSTRAINT [DF_Proposals_approved]  DEFAULT ((0)) FOR [approved]
GO
ALTER TABLE [dbo].[Proposals] ADD  CONSTRAINT [DF_Proposals_declined]  DEFAULT ((0)) FOR [declined]
GO
