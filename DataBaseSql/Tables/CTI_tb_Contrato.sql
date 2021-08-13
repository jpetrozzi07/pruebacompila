USE [CPILUNION]
GO

CREATE TABLE [dbo].[CTI_tb_Contrato](
	[SidEmpresa] [nchar](4) NOT NULL,
	[SidCliente] [nvarchar](12) NOT NULL,
	[SidContrato] [nchar](20) NOT NULL,
	[StrContrato] [nvarchar](100) NOT NULL,
	[FchAlta] [datetime] NOT NULL,
	[FchBaja] [datetime] NULL,
	[StrCuenta] [nvarchar](50) NOT NULL,
	[SidUsrAlta] [nvarchar](50) NOT NULL,
	[FchUsrAlta] [datetime] NOT NULL,
	[SidUsrModif] [nvarchar](50) NOT NULL,
	[FchUsrModif] [datetime] NULL,
	[Ts] [timestamp] NOT NULL,
	[SidOferta] [varchar](10) NULL,
	[NumSecContrato] [numeric](4, 0) NULL,
	[SidLineaNegocio] [varchar](6) NULL,
	[Nivel] [nvarchar](50) NULL,
	[CodigoExterno] [varchar](50) NULL,
	[SidCodGestor] [varchar](50) NULL,
	[SidCodSupervisor] [varchar](50) NULL,
	[SidCodJefeServicio] [varchar](50) NULL,
    [Estado] [char](1) NULL
 CONSTRAINT [PK_CTI_tb_Contrato] PRIMARY KEY CLUSTERED 
(
	[SidEmpresa] ASC,
	[SidCliente] ASC,
	[SidContrato] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [dbo].[CTI_tb_Contrato]  WITH NOCHECK ADD  CONSTRAINT [FK_CTI_tb_Contrato_Cliente] FOREIGN KEY([SidEmpresa], [SidCliente])
REFERENCES [dbo].[CTI_tb_Cliente] ([SidEmpresa], [SidCliente])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CTI_tb_Contrato] CHECK CONSTRAINT [FK_CTI_tb_Contrato_Cliente]
GO

ALTER TABLE [dbo].[CTI_tb_Contrato] ADD  CONSTRAINT [DF_CTI_tb_Contrato_SidEmpresa]  DEFAULT ('') FOR [SidEmpresa]
GO

ALTER TABLE [dbo].[CTI_tb_Contrato] ADD  CONSTRAINT [DF_CTI_tb_Contrato_SidCliente]  DEFAULT ((0)) FOR [SidCliente]
GO

ALTER TABLE [dbo].[CTI_tb_Contrato] ADD  CONSTRAINT [DF_CTI_tb_Contrato_SidContrato]  DEFAULT ('') FOR [SidContrato]
GO

ALTER TABLE [dbo].[CTI_tb_Contrato] ADD  CONSTRAINT [DF_CTI_tb_Contrato_StrContrato]  DEFAULT ('') FOR [StrContrato]
GO

ALTER TABLE [dbo].[CTI_tb_Contrato] ADD  CONSTRAINT [DF_CTI_tb_Contrato_StrCuenta]  DEFAULT ('') FOR [StrCuenta]
GO

ALTER TABLE [dbo].[CTI_tb_Contrato] ADD  CONSTRAINT [DF_CTI_tb_Contrato_SidUsrAlta]  DEFAULT ('') FOR [SidUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_Contrato] ADD  CONSTRAINT [DF_CTI_tb_Contrato_FchUsrAlta]  DEFAULT (getdate()) FOR [FchUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_Contrato] ADD  CONSTRAINT [DF_CTI_tb_Contrato_SidUsrModif]  DEFAULT ('') FOR [SidUsrModif]
GO

ALTER TABLE [dbo].[CTI_tb_Contrato] ADD  CONSTRAINT [DF_CTI_tb_Contrato_Estado]  DEFAULT ('N') FOR [Estado]
GO

