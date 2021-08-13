USE CPILUNION
CREATE TABLE [dbo].[CTI_tb_Paises](
	[SidPais] [char](3) NOT NULL,
	[StrPais] [varchar](50) NOT NULL,
	[CodigoOficial] [char](3) NOT NULL,
	[ISO3166] [varchar](2) NULL,
	[ISO31661] [varchar](3) NULL,
 CONSTRAINT [PK_CTI_tb_Paises] PRIMARY KEY CLUSTERED 
(
	[SidPais] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CTI_tb_Paises] ADD  CONSTRAINT [DF_CTI_tb_Paises_SidPais]  DEFAULT ('') FOR [SidPais]
GO

ALTER TABLE [dbo].[CTI_tb_Paises] ADD  CONSTRAINT [DF_CTI_tb_Paises_StrPais]  DEFAULT ('') FOR [StrPais]
GO


