USE CPILUNION
CREATE TABLE [dbo].[CTI_tb_Provincias](
	[SidPais] [char](3) NOT NULL,
	[SidProvincia] [char](2) NOT NULL,
	[StrProvincia] [nvarchar](100) NOT NULL,
	[SidComunidad] [varchar](2) NULL,
 CONSTRAINT [PK_CTI_tb_Provincias] PRIMARY KEY CLUSTERED 
(
	[SidPais] ASC,
	[SidProvincia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



