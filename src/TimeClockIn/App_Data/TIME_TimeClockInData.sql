USE [TimeClockIn]
SET IDENTITY_INSERT [dbo].[EmployeeClockIn] ON 

INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (1, N'adedamola@splasherstech.com', N'VGG Isheri (Splashers Tech)', CAST(N'2016-04-08 08:55:23.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (3, N'segun@venturegardengroup.com', N'VGG Oregun', CAST(N'2016-04-08 07:23:10.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (4, N'dolapo@cloudtech.ng', N'Site', CAST(N'2016-04-07 14:49:11.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (5, N'cecilia.makinde@avitech.com', N'VGG EAN Hangar', CAST(N'2016-04-02 08:00:00.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (7, N'dd@dd.com', N'VGG Oregun', CAST(N'2016-04-12 09:09:20.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (8, N'segun@vgg.com', N'VGG Oregun', CAST(N'2016-04-12 11:01:15.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (9, N'briola.amanda@splasherstech.com', N'VGG Isheri (Splashers Tech)', CAST(N'2016-04-12 11:26:52.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (10, N'ddee@vgg.com', N'VGG Oregun', CAST(N'2016-04-12 11:37:51.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (11, N'Ghandi@vgg.com', N'VGG EAN Hangar', CAST(N'2016-04-12 11:44:35.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (12, N'adedamola@splasherstech.com', N'Site', CAST(N'2016-04-12 11:57:04.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (13, N'adedamola@splasherstech.com', N'Site', CAST(N'2016-04-12 12:03:04.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (14, N'adedamola@splasherstech.com', N'Site', CAST(N'2016-04-12 12:11:26.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (15, N'adedamola@splasherstech.com', N'Site', CAST(N'2016-04-12 12:19:19.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (16, N'segun.dada@splasherstech.com', N'Site', CAST(N'2016-04-12 12:24:49.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (17, N'dd@dd.com', N'Site', CAST(N'2016-04-12 12:35:45.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (18, N'shola.nelson@vgg.com', N'Home', CAST(N'2016-04-12 12:44:11.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (19, N'dd@dd.com', N'Home', CAST(N'2016-04-12 12:53:15.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (20, N'deji.karim@vgg.com', N'Home', CAST(N'2016-04-13 09:14:44.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (23, N'bibidot@vgg.com', N'VGG EAN Hangar', CAST(N'2014-04-13 00:00:00.000' AS DateTime))
INSERT [dbo].[EmployeeClockIn] ([id], [EmployeeUserId], [LocationName], [ClockInDateTime]) VALUES (24, N'sugar@vgg.com', N'VGG Oregun', CAST(N'2016-04-14 15:28:51.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[EmployeeClockIn] OFF
SET IDENTITY_INSERT [dbo].[EmployeeLocationDetails] ON 

INSERT [dbo].[EmployeeLocationDetails] ([id], [EmployeeClockInId], [Latitude], [Longitude], [Address], [LocationName]) VALUES (1, 15, CAST(6.3428839 AS Decimal(12, 7)), CAST(3.5323994 AS Decimal(12, 7)), N'17A, Ojo Ramoni Street, Victoria Island, Lagos, Nigeria', N'The View Hotel')
INSERT [dbo].[EmployeeLocationDetails] ([id], [EmployeeClockInId], [Latitude], [Longitude], [Address], [LocationName]) VALUES (2, 17, CAST(6.3400000 AS Decimal(12, 7)), CAST(3.5300000 AS Decimal(12, 7)), N'17A, Ojo Ramoni Street, Victoria Island, Lagos, Nigeria', N'The View Hotel')
INSERT [dbo].[EmployeeLocationDetails] ([id], [EmployeeClockInId], [Latitude], [Longitude], [Address], [LocationName]) VALUES (3, 18, CAST(6.8700000 AS Decimal(12, 7)), CAST(3.9800000 AS Decimal(12, 7)), N'18, Era Road, Surulere, Lagos, Nigeria', N'Home')
INSERT [dbo].[EmployeeLocationDetails] ([id], [EmployeeClockInId], [Latitude], [Longitude], [Address], [LocationName]) VALUES (4, 19, CAST(6.8764320 AS Decimal(12, 7)), CAST(3.9874432 AS Decimal(12, 7)), N'18, Era Road, Surulere, Lagos, Nigeria', N'Home')
INSERT [dbo].[EmployeeLocationDetails] ([id], [EmployeeClockInId], [Latitude], [Longitude], [Address], [LocationName]) VALUES (5, 20, CAST(6.8764320 AS Decimal(12, 7)), CAST(3.9874432 AS Decimal(12, 7)), N'18, Era Road, Surulere, Lagos, Nigeria', N'Home')
SET IDENTITY_INSERT [dbo].[EmployeeLocationDetails] OFF
SET IDENTITY_INSERT [dbo].[Location] ON 

INSERT [dbo].[Location] ([id], [LocationName], [Latitude], [Longitude], [Address]) VALUES (1, N'VGG EAN Hangar', CAST(6.6102499 AS Decimal(12, 7)), CAST(3.3243022 AS Decimal(12, 7)), N'2nd Floor, EAN Hangar Building, beside Caverton Helicopters, Off FAAN Transit Camp Road, Murtala Mohammed International Airport, Ikeja, Lagos Nigeria')
INSERT [dbo].[Location] ([id], [LocationName], [Latitude], [Longitude], [Address]) VALUES (2, N'VGG Oregun', CAST(6.5997161 AS Decimal(12, 7)), CAST(3.3721780 AS Decimal(12, 7)), N'Plot E, Ziatech Road, Beside Diamond Bank, Opposite Tantalizers, Ikosi Road, Oregun, Ikeja, Lagos Nigeria')
INSERT [dbo].[Location] ([id], [LocationName], [Latitude], [Longitude], [Address]) VALUES (3, N'VGG Isheri (Splashers Tech)', CAST(6.6450144 AS Decimal(12, 7)), CAST(3.3899548 AS Decimal(12, 7)), N'7, Association Avenue, Harmony Villas, Isheri, Ojodu Berger, Lagos Nigeria')
SET IDENTITY_INSERT [dbo].[Location] OFF
