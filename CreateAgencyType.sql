/****** Object:  Table [dbo].[AgencyType]    Script Date: 6/2/2023 1:39:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AgencyType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[SortOrder] [int] NULL,
	[AllCertificates] [bit] NOT NULL,
	[IndustryTypeId] [int] NULL,
 CONSTRAINT [PK_AgencyType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AgencyType] ADD  CONSTRAINT [DF__AgencyTyp__AllCe__3B0BC30C]  DEFAULT ((1)) FOR [AllCertificates]
GO

ALTER TABLE [dbo].[AgencyType]  WITH NOCHECK ADD  CONSTRAINT [FK_AgencyType_IndustryType] FOREIGN KEY([IndustryTypeId])
REFERENCES [dbo].[IndustryType] ([Id])
GO

ALTER TABLE [dbo].[AgencyType] CHECK CONSTRAINT [FK_AgencyType_IndustryType]
GO


