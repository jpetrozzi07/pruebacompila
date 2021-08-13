USE [CPILUNION]
GO

CREATE TABLE [dbo].[CTI_tb_Cliente](
	[SidEmpresa] [nchar](4) NOT NULL,
	[SidCliente] [nvarchar](12) NOT NULL,
	[SnmCliente] [nvarchar](150) NOT NULL,
	[StrCIF] [nvarchar](50) NOT NULL,
	[StrDireccion] [nvarchar](150) NOT NULL,
	[SidMunicipio] [nchar](5) NOT NULL,
	[SidProvincia] [char](2) NOT NULL,
	[SidPais] [char](3) NOT NULL,
	[CodPostal] [nvarchar](20) NULL,
	[StrTelefono1] [nvarchar](15) NOT NULL,
	[StrTelefono2] [nvarchar](15) NOT NULL,
	[StrFax] [nvarchar](15) NOT NULL,
	[StrContacto] [nvarchar](100) NOT NULL,
	[StrEmail] [nvarchar](100) NOT NULL,
	[TipoCliente] [char](1) NOT NULL,
	[FchAlta] [datetime] NOT NULL,
	[FchBaja] [datetime] NULL,
	[SidUsrAlta] [nvarchar](50) NOT NULL,
	[FchUsrAlta] [datetime] NOT NULL,
	[SidUsrModif] [nvarchar](50) NOT NULL,
	[FchUsrModif] [datetime] NULL,
	[Ts] [timestamp] NOT NULL,
    [Estado] [char](1) NULL
 CONSTRAINT [PK_CTI_tb_Cliente] PRIMARY KEY CLUSTERED 
(
	[SidEmpresa] ASC,
	[SidCliente] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_SidEmpresa]  DEFAULT ('') FOR [SidEmpresa]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_SidCliente]  DEFAULT ((0)) FOR [SidCliente]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_SnmCliente]  DEFAULT ('') FOR [SnmCliente]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_StrCIF]  DEFAULT ('') FOR [StrCIF]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_StrDireccion]  DEFAULT ('') FOR [StrDireccion]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_SidMunicipio]  DEFAULT ('0') FOR [SidMunicipio]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_SidProvincia]  DEFAULT ('0') FOR [SidProvincia]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_SidPais]  DEFAULT ('') FOR [SidPais]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_CodPostal]  DEFAULT ('') FOR [CodPostal]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_StrTelefono1]  DEFAULT ('') FOR [StrTelefono1]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_StrTelefono2]  DEFAULT ('') FOR [StrTelefono2]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_StrFax]  DEFAULT ('') FOR [StrFax]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_StrContacto]  DEFAULT ('') FOR [StrContacto]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_StrEmail]  DEFAULT ('') FOR [StrEmail]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_SidUSrAlta]  DEFAULT ('') FOR [SidUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_FchUSrAlta]  DEFAULT (getdate()) FOR [FchUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_SidUsrModif]  DEFAULT ('') FOR [SidUsrModif]
GO

ALTER TABLE [dbo].[CTI_tb_Cliente] ADD  CONSTRAINT [DF_CTI_tb_Cliente_Estado]  DEFAULT ('N') FOR [Estado]
GO

