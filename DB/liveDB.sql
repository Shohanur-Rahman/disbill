USE [DB_A4507A_dishbill]
GO
/****** Object:  Table [dbo].[AreaList]    Script Date: 1/12/2020 6:33:26 PM ******/
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
/****** Object:  Table [dbo].[USERS]    Script Date: 1/12/2020 6:33:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[RoleId] [int] NOT NULL,
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

INSERT [dbo].[AreaList] ([Id], [Name], [ManagerId], [CreatedDate], [EditedDate]) VALUES (1, N'-- এরিয়া সিলেক্ট করুন --', 0, CAST(N'2020-01-12 13:41:10.947' AS DateTime), NULL)
INSERT [dbo].[AreaList] ([Id], [Name], [ManagerId], [CreatedDate], [EditedDate]) VALUES (2, N'উত্তরা', 4, CAST(N'2020-01-12 13:53:10.383' AS DateTime), NULL)
INSERT [dbo].[AreaList] ([Id], [Name], [ManagerId], [CreatedDate], [EditedDate]) VALUES (3, N' আব্দুল্লাহপুর', 4, CAST(N'2020-01-12 13:53:31.193' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[AreaList] OFF
SET IDENTITY_INSERT [dbo].[USERS] ON 

INSERT [dbo].[USERS] ([Id], [Name], [RoleId], [IsActivated], [Avatar], [CompanyName], [CompanyLogo], [AreaId], [FathersName], [MobileNumber], [Password], [EncPassword], [Email], [House], [Road], [PostOffice], [Thana], [Zila], [NIDNo], [ReferenceName], [ReferenceMobile], [CreatedDate], [CreatedId]) VALUES (1, N'Abdur Rahim', 1, 1, NULL, NULL, NULL, 0, N'Abdul Karim', N'01711111111', N'123', N'PV0yzuraq6f+DRhS5iehlQ==', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[USERS] ([Id], [Name], [RoleId], [IsActivated], [Avatar], [CompanyName], [CompanyLogo], [AreaId], [FathersName], [MobileNumber], [Password], [EncPassword], [Email], [House], [Road], [PostOffice], [Thana], [Zila], [NIDNo], [ReferenceName], [ReferenceMobile], [CreatedDate], [CreatedId]) VALUES (2, N'asdfasdf', 3, 1, NULL, NULL, NULL, NULL, N'asdfadsf', N'asadfs', N'$z1kg3', N'JHc2O2LyRC7zWQbUltvhfA==', N'asdfasdf@asdf.sfg', N'asdfadsf', N'asdfads', N'fadf', N'adfadfs', N'adsfa', N'sdadsfa', N'adsf', N'asdf', CAST(N'2020-01-10 18:50:03.790' AS DateTime), 1)
INSERT [dbo].[USERS] ([Id], [Name], [RoleId], [IsActivated], [Avatar], [CompanyName], [CompanyLogo], [AreaId], [FathersName], [MobileNumber], [Password], [EncPassword], [Email], [House], [Road], [PostOffice], [Thana], [Zila], [NIDNo], [ReferenceName], [ReferenceMobile], [CreatedDate], [CreatedId]) VALUES (3, N'Shohanur Rahman', 3, 1, NULL, NULL, NULL, NULL, N'asdfadsf', N'01894751837', N'ReNRWj', N'f87cEsu+/fAWAIEjhXkJFA==', N'asdfasdf@asdf.sfg', N'asdfadsf', N'asdfads', N'fadf', N'adfadfs', N'adsfa', N'sdadsfa', N'adsf', N'asdf', CAST(N'2020-01-10 19:54:25.260' AS DateTime), 1)
INSERT [dbo].[USERS] ([Id], [Name], [RoleId], [IsActivated], [Avatar], [CompanyName], [CompanyLogo], [AreaId], [FathersName], [MobileNumber], [Password], [EncPassword], [Email], [House], [Road], [PostOffice], [Thana], [Zila], [NIDNo], [ReferenceName], [ReferenceMobile], [CreatedDate], [CreatedId]) VALUES (4, N'Shakib Al Hasan', 3, 1, N'/uploads/user/01-2020/2020-01-11-e9322b4f-7ca2-4add-8800-626457c5e99a-14380135_699033556929588_8977708589985069736_o.jpg', N'dghdh', N'/uploads/company/01-2020/2020-01-11-63eaebcb-04b4-4a2e-b6fe-2174fbc49caf-1.jpg', NULL, N'Father', N'01755555555', N'Test123', N'1AADtB0G/umddeA1Bj+iwg==', N'shakib75@gmail.com', N'127', N'7, Gulshan, Dhaka', N'Banani', N'Banani', N'Dhaka', N'234234234', N'sdfgsdf', N'gsfgsfg', CAST(N'2020-01-10 23:26:06.087' AS DateTime), 1)
INSERT [dbo].[USERS] ([Id], [Name], [RoleId], [IsActivated], [Avatar], [CompanyName], [CompanyLogo], [AreaId], [FathersName], [MobileNumber], [Password], [EncPassword], [Email], [House], [Road], [PostOffice], [Thana], [Zila], [NIDNo], [ReferenceName], [ReferenceMobile], [CreatedDate], [CreatedId]) VALUES (5, N'Fahim Numan', 3, 1, NULL, NULL, NULL, NULL, N'mizanur rahman', N'01782496285', N'bWnxlx', N'c17eD9IfzUQNkqqYCu+Yeg==', N'fahiomnuman87@gmail.com', N'50', N'60', N'2200', N'fulbaria', N'mymensingh', N'353252367844', N'rajuvhai', N'01716641599', CAST(N'2020-01-10 22:44:31.357' AS DateTime), 1)
INSERT [dbo].[USERS] ([Id], [Name], [RoleId], [IsActivated], [Avatar], [CompanyName], [CompanyLogo], [AreaId], [FathersName], [MobileNumber], [Password], [EncPassword], [Email], [House], [Road], [PostOffice], [Thana], [Zila], [NIDNo], [ReferenceName], [ReferenceMobile], [CreatedDate], [CreatedId]) VALUES (6, N'Nasir Hossain', 4, 1, NULL, NULL, NULL, 2, N'Pare na', N'01845454545', N'Uo8vQV', N'dyWfzLlQc68tO5NhXUHZjw==', N'parena@gmail.com', N'854', N'#8, Gulshan 2, Block E', N'Gulshan', N'Gulshan', N'Dhaka', N'987458', NULL, NULL, CAST(N'2020-01-12 14:10:39.483' AS DateTime), 4)
INSERT [dbo].[USERS] ([Id], [Name], [RoleId], [IsActivated], [Avatar], [CompanyName], [CompanyLogo], [AreaId], [FathersName], [MobileNumber], [Password], [EncPassword], [Email], [House], [Road], [PostOffice], [Thana], [Zila], [NIDNo], [ReferenceName], [ReferenceMobile], [CreatedDate], [CreatedId]) VALUES (7, N'Abdul Barik', 4, 1, NULL, NULL, NULL, 2, N'sfdgsdf', N'01711111112', N'xRm&d4', N'7sQlXDTPZOgBhybbmM3f8A==', N'sfdgsfg@sdf.sfg', N'asdfasd', N'asdfasd', N'asd', N'asdf', N'adf', N'adf', N'adsf', N'asdf', CAST(N'2020-01-12 16:57:36.427' AS DateTime), 4)
INSERT [dbo].[USERS] ([Id], [Name], [RoleId], [IsActivated], [Avatar], [CompanyName], [CompanyLogo], [AreaId], [FathersName], [MobileNumber], [Password], [EncPassword], [Email], [House], [Road], [PostOffice], [Thana], [Zila], [NIDNo], [ReferenceName], [ReferenceMobile], [CreatedDate], [CreatedId]) VALUES (8, N'Mominul hoque', 4, 1, NULL, NULL, NULL, 2, N'sfgsfg', N'01711111121', N'maYh#d', N'PCJ3eUy3Bnv+GVEy6KcbQQ==', N'sfgsfg@sfdg.ert', N'sfdgs', N'gsf', N'sfgsfd', N'sfgsfg', N'sfgsg', N'sfg', N'sfg', N'sfg', CAST(N'2020-01-12 17:03:42.407' AS DateTime), 4)
INSERT [dbo].[USERS] ([Id], [Name], [RoleId], [IsActivated], [Avatar], [CompanyName], [CompanyLogo], [AreaId], [FathersName], [MobileNumber], [Password], [EncPassword], [Email], [House], [Road], [PostOffice], [Thana], [Zila], [NIDNo], [ReferenceName], [ReferenceMobile], [CreatedDate], [CreatedId]) VALUES (9, N'Junayed Siddique', 4, 1, NULL, NULL, NULL, 2, N'Siddique', N'01711111131', N'dEvODX', N'ZS9N//vioE/z9qnSwQ81nQ==', N'parena1@gmail.com', N'asdfasdf', N'adsasdf', N'adsfasdf', N'adsfasdf', N'asdf', N'asdfasdf', N'adsfads', N'adsf', CAST(N'2020-01-12 17:05:11.307' AS DateTime), 4)
SET IDENTITY_INSERT [dbo].[USERS] OFF
/****** Object:  StoredProcedure [dbo].[Get_AreaName_ListBy_ManagerId_With_Default]    Script Date: 1/12/2020 6:33:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_AreaName_ListBy_ManagerId_With_Default](
	@mId bigint
)
AS 
BEGIN 

	select * from [dbo].[AreaList] where [ManagerId]=@mId or id=1
	
END




GO
/****** Object:  StoredProcedure [dbo].[Get_User_By_Mobile_Number]    Script Date: 1/12/2020 6:33:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_User_By_Mobile_Number](
	@mobileNumber nvarchar(100)
)
AS 
BEGIN 

	select * from [dbo].[USERS] where [MobileNumber]=@mobileNumber
	
END




GO
/****** Object:  StoredProcedure [dbo].[GetAreaNameListByManagerId]    Script Date: 1/12/2020 6:33:28 PM ******/
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
/****** Object:  StoredProcedure [dbo].[Manager_Get_Collector_List_By_managerId]    Script Date: 1/12/2020 6:33:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Manager_Get_Collector_List_By_managerId]
(
	@mngId as bigint
)
AS 
BEGIN 

	select * from [dbo].[USERS] where [RoleId]= 4 and [CreatedId]=@mngId
	
END




GO
/****** Object:  StoredProcedure [dbo].[Super_Admin_Get_Manager_List]    Script Date: 1/12/2020 6:33:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Super_Admin_Get_Manager_List]
AS 
BEGIN 

	select * from [dbo].[USERS] where [RoleId]= 3
	
END




GO
