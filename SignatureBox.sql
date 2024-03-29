USE [SignatureBox]
GO
/****** Object:  Table [dbo].[MySignatureTable]    Script Date: 6/28/2019 10:14:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MySignatureTable](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[SignatureHolder] [varchar](50) NULL,
	[SignatureBase64] [varchar](max) NULL,
 CONSTRAINT [PK_MySignatureTable] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
