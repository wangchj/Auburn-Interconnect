USE [interconnect]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 04/18/2012 11:20:41 ******/
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([uid], [pwd], [fname], [lname], [email], [phone], [admin]) VALUES (1, 0xBA7816BF8F01CFEA414140DE5DAE2223B00361A396177A9CB410FF61F20015AD, N'Jeff', N'Wang', N'cjw39@hotmail.com', 3343333333, 1)
INSERT [dbo].[Users] ([uid], [pwd], [fname], [lname], [email], [phone], [admin]) VALUES (2, 0xBA7816BF8F01CFEA414140DE5DAE2223B00361A396177A9CB410FF61F20015AD, N'John', N'Doe', N'john@doe.com', 3333333333, 0)
INSERT [dbo].[Users] ([uid], [pwd], [fname], [lname], [email], [phone], [admin]) VALUES (3, 0xBA7816BF8F01CFEA414140DE5DAE2223B00361A396177A9CB410FF61F20015AD, N'Ann', N'Bach', N'annbach@auburn.edu', NULL, 0)
INSERT [dbo].[Users] ([uid], [pwd], [fname], [lname], [email], [phone], [admin]) VALUES (4, 0xBA7816BF8F01CFEA414140DE5DAE2223B00361A396177A9CB410FF61F20015AD, N'Alice', N'Doe', N'alice@doe.com', 3333333333, 0)
INSERT [dbo].[Users] ([uid], [pwd], [fname], [lname], [email], [phone], [admin]) VALUES (5, 0xBA7816BF8F01CFEA414140DE5DAE2223B00361A396177A9CB410FF61F20015AD, N'Jason', N'Doe', N'jason@doe.com', 3456789022, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
