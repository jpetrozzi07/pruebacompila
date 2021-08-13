USE CPILUNION
CREATE TABLE [dbo].[CTI_tb_Municipios](
	[SidPais] [char](3) NOT NULL,
	[SidProvincia] [char](2) NOT NULL,
	[SidMunicipio] [nchar](5) NOT NULL,
	[StrMunicipio] [nvarchar](50) NOT NULL,
	[SidUsrAlta] [nvarchar](50) NOT NULL,
	[FchUsrAlta] [datetime] NOT NULL,
	[SidUsrModif] [nvarchar](50) NULL,
	[FchUsrModif] [datetime] NULL,
	[Ts] [timestamp] NOT NULL,
 CONSTRAINT [PK_CTI_tb_Municipios] PRIMARY KEY CLUSTERED 
(
	[SidPais] ASC,
	[SidProvincia] ASC,
	[SidMunicipio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CTI_tb_Municipios] ADD  CONSTRAINT [DF_CTI_tb_Municipios_SidMunicipio]  DEFAULT ('0') FOR [SidMunicipio]
GO

ALTER TABLE [dbo].[CTI_tb_Municipios] ADD  CONSTRAINT [DF_CTI_tb_Municipios_StrMunicipio]  DEFAULT ('') FOR [StrMunicipio]
GO



