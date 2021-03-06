USE [DISHBILL]
GO
/****** Object:  Table [dbo].[AreaList]    Script Date: 1/3/2020 4:54:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AreaList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ManagerId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[EditedDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[USERS]    Script Date: 1/3/2020 4:54:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IsActivated] [bit] NOT NULL,
	[Avatar] [nvarchar](max) NULL,
	[CompanyName] [nvarchar](max) NULL,
	[CompanyLogo] [nvarchar](max) NULL,
	[AreaId] [int] NULL,
	[FathersName] [nvarchar](max) NULL,
	[MobileNumber] [nvarchar](50) NULL,
	[Password] [nvarchar](max) NULL,
	[EncPassword] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[House] [nvarchar](max) NULL,
	[Road] [nvarchar](max) NULL,
	[PostOffice] [nvarchar](max) NULL,
	[Thana] [nvarchar](max) NULL,
	[Zila] [nvarchar](max) NULL,
	[NIDNo] [nvarchar](max) NULL,
	[ReferenceName] [nvarchar](max) NULL,
	[ReferenceMobile] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedId] [bigint] NULL,
 CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[AreaList] ON 

INSERT [dbo].[AreaList] ([Id], [Name], [ManagerId], [CreatedDate], [EditedDate]) VALUES (6, N'Dhaka', 1, CAST(N'2020-01-02 23:22:32.860' AS DateTime), NULL)
INSERT [dbo].[AreaList] ([Id], [Name], [ManagerId], [CreatedDate], [EditedDate]) VALUES (7, N'Mymenshing', 1, CAST(N'2020-01-02 23:30:01.797' AS DateTime), NULL)
INSERT [dbo].[AreaList] ([Id], [Name], [ManagerId], [CreatedDate], [EditedDate]) VALUES (8, N'Comilla', 1, CAST(N'2020-01-02 23:59:08.450' AS DateTime), NULL)
INSERT [dbo].[AreaList] ([Id], [Name], [ManagerId], [CreatedDate], [EditedDate]) VALUES (9, N'Barisal', 1, CAST(N'2020-01-03 00:09:38.430' AS DateTime), NULL)
INSERT [dbo].[AreaList] ([Id], [Name], [ManagerId], [CreatedDate], [EditedDate]) VALUES (10, N'Sylet', 1, CAST(N'2020-01-03 00:11:35.387' AS DateTime), NULL)
INSERT [dbo].[AreaList] ([Id], [Name], [ManagerId], [CreatedDate], [EditedDate]) VALUES (11, N'Rajshahi', 1, CAST(N'2020-01-03 00:12:01.773' AS DateTime), NULL)
INSERT [dbo].[AreaList] ([Id], [Name], [ManagerId], [CreatedDate], [EditedDate]) VALUES (12, N'Vola', 1, CAST(N'2020-01-03 00:12:16.263' AS DateTime), NULL)
INSERT [dbo].[AreaList] ([Id], [Name], [ManagerId], [CreatedDate], [EditedDate]) VALUES (13, N'wrtwrtwrt', 1, CAST(N'2020-01-03 00:14:06.080' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[AreaList] OFF
SET IDENTITY_INSERT [dbo].[USERS] ON 

INSERT [dbo].[USERS] ([Id], [Name], [IsActivated], [Avatar], [CompanyName], [CompanyLogo], [AreaId], [FathersName], [MobileNumber], [Password], [EncPassword], [Email], [House], [Road], [PostOffice], [Thana], [Zila], [NIDNo], [ReferenceName], [ReferenceMobile], [CreatedDate], [CreatedId]) VALUES (1, N'Abdur Rahim', 1, NULL, NULL, NULL, 0, N'Abdul Karim', N'01711111111', N'123', N'PV0yzuraq6f+DRhS5iehlQ==', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[USERS] OFF
/****** Object:  StoredProcedure [dbo].[GetAreaNameListByManagerId]    Script Date: 1/3/2020 4:54:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAreaNameListByManagerId](
	@mId bigint
)
AS 
BEGIN 

	select * from [dbo].[AreaList] where [ManagerId]=@mId
	
END



GO
