USE [CPILUNION]
GO
CREATE TABLE [dbo].[CTI_tb_CentrosCoste](
	[SidEmpresa] [nchar](4) NOT NULL,
	[SidCliente] [nvarchar](12) NOT NULL,
	[SidCentroCoste] [nvarchar](20) NOT NULL,
	[StrCentroCoste] [nvarchar](150) NOT NULL,
	[Direccion] [nvarchar](150) NOT NULL,
	[Pais] [char](3) NOT NULL,
	[SidMunicipio] [nchar](5) NOT NULL,
	[Poblacion] [nvarchar](50) NOT NULL,
	[SidProvincia] [char](2) NOT NULL,
	[Provincia] [nvarchar](50) NOT NULL,
	[StrCP] [nvarchar](5) NOT NULL,
	[StrContacto] [nvarchar](100) NOT NULL,
	[StrTelefono] [nvarchar](15) NOT NULL,
	[SidUsrAlta] [nvarchar](50) NOT NULL,
	[FchUsrAlta] [datetime] NOT NULL,
	[SidUsrModif] [nvarchar](50) NULL,
	[FchUsrModif] [datetime] NULL,
	[Ts] [timestamp] NOT NULL,
	[FchAlta] [datetime] NULL,
	[FchBaja] [datetime] NULL,
	[siddelegacion] [nchar](6) NULL,
	[Estado] [char](1) NULL
 CONSTRAINT [PK_CTI_tb_CentrosCoste] PRIMARY KEY CLUSTERED 
(
	[SidEmpresa] ASC,
	[SidCliente] ASC,
	[SidCentroCoste] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_SidEmpresa]  DEFAULT ('') FOR [SidEmpresa]
GO

ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_SidCliente]  DEFAULT ('') FOR [SidCliente]
GO

ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_SidCentroCoste]  DEFAULT ('') FOR [SidCentroCoste]
GO

ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_StrCentroCoste]  DEFAULT ('') FOR [StrCentroCoste]
GO

ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_Direccion]  DEFAULT ('') FOR [Direccion]
GO

ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_Pais]  DEFAULT ('') FOR [Pais]
GO

ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_SidMunicipio]  DEFAULT ('0') FOR [SidMunicipio]
GO

ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_Poblacion]  DEFAULT ('') FOR [Poblacion]
GO

ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_SidProvincia]  DEFAULT ('0') FOR [SidProvincia]
GO

ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_Provincia]  DEFAULT ('') FOR [Provincia]
GO

ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_StrCP]  DEFAULT ('') FOR [StrCP]
GO

ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_StrContacto]  DEFAULT ('') FOR [StrContacto]
GO

ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_StrTelefono]  DEFAULT ('') FOR [StrTelefono]
GO

ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_SidUsrAlta]  DEFAULT ('') FOR [SidUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_FchUsrAlta]  DEFAULT (getdate()) FOR [FchUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_SidUsrModif]  DEFAULT ('') FOR [SidUsrModif]
GO
ALTER TABLE [dbo].[CTI_tb_CentrosCoste] ADD  CONSTRAINT [DF_CTI_tb_CentrosCoste_Estado]  DEFAULT ('N') FOR [Estado]
GO

