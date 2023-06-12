/****** Object:  Table [dbo].[DebtType]    Script Date: 6/2/2023 1:40:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DebtType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[AllCertificates] [bit] NOT NULL,
 CONSTRAINT [PK_DebtType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DebtType] ADD  CONSTRAINT [DF__DebtType__AllCer__4E1E9780]  DEFAULT ((1)) FOR [AllCertificates]
GO


