USE [CPILUNION]
GO
CREATE TABLE [dbo].[CTI_tb_Servicio](
	[SidEmpresa] [nchar](4) NOT NULL,
	[SidCliente] [nvarchar](12) NOT NULL,
	[SidContrato] [nchar](20) NOT NULL,
	[SidServicio] [nchar](20) NOT NULL,
	[StrServicio] [nvarchar](150) NOT NULL,
	[SidActividad] [nchar](8) NOT NULL,
	[StrContacto] [nvarchar](100) NOT NULL,
	[Numero] [numeric](6, 0) NOT NULL,
	[StrDireccion] [nvarchar](100) NOT NULL,
	[StrPoblacion] [nvarchar](50) NOT NULL,
	[StrProvincia] [nvarchar](50) NOT NULL,
	[StrTelefono1] [nvarchar](15) NOT NULL,
	[StrTelefono2] [nvarchar](50) NOT NULL,
	[StrTelefonoControlMovil] [nvarchar](50) NOT NULL,
	[FchAlta] [datetime] NOT NULL,
	[FchBaja] [datetime] NULL,
	[SidCentroCoste] [nvarchar](20) NOT NULL,
	[SidLineaNegocio] [nchar](6) NULL,
	[SidUsrAlta] [nvarchar](50) NOT NULL,
	[FchUsrAlta] [datetime] NOT NULL,
	[SidUsrModif] [nvarchar](50) NOT NULL,
	[FchUsrModif] [datetime] NULL,
	[Ts] [timestamp] NOT NULL,
    [Estado] [char](1) NULL
 CONSTRAINT [PK_CTI_tb_Servicio_1] PRIMARY KEY CLUSTERED 
(
	[SidEmpresa] ASC,
	[SidCliente] ASC,
	[SidServicio] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CTI_tb_Servicio]  WITH NOCHECK ADD  CONSTRAINT [FK_CTI_tb_Servicio_Actividad1] FOREIGN KEY([SidEmpresa], [SidActividad])
REFERENCES [dbo].[CTI_tb_Actividad] ([SidEmpresa], [SidActividad])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] CHECK CONSTRAINT [FK_CTI_tb_Servicio_Actividad1]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio]  WITH NOCHECK ADD  CONSTRAINT [FK_CTI_tb_Servicio_CentrosCoste] FOREIGN KEY([SidEmpresa], [SidCliente], [SidCentroCoste])
REFERENCES [dbo].[CTI_tb_CentrosCoste] ([SidEmpresa], [SidCliente], [SidCentroCoste])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] CHECK CONSTRAINT [FK_CTI_tb_Servicio_CentrosCoste]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio]  WITH NOCHECK ADD  CONSTRAINT [FK_CTI_tb_Servicio_Contrato1] FOREIGN KEY([SidEmpresa], [SidCliente], [SidContrato])
REFERENCES [dbo].[CTI_tb_Contrato] ([SidEmpresa], [SidCliente], [SidContrato])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] CHECK CONSTRAINT [FK_CTI_tb_Servicio_Contrato1]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio]  WITH NOCHECK ADD  CONSTRAINT [FK_CTI_tb_Servicio_LineaNegocio] FOREIGN KEY([SidEmpresa], [SidLineaNegocio])
REFERENCES [dbo].[CTI_tb_LineaNegocio] ([SidEmpresa], [SidLinea])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] CHECK CONSTRAINT [FK_CTI_tb_Servicio_LineaNegocio]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_SidEmpresa]  DEFAULT ('') FOR [SidEmpresa]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_SidCliente]  DEFAULT ((0)) FOR [SidCliente]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_SidContrato]  DEFAULT ('') FOR [SidContrato]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_SidServicio]  DEFAULT ('') FOR [SidServicio]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_StrServicio]  DEFAULT ('') FOR [StrServicio]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_SidActividad]  DEFAULT ('') FOR [SidActividad]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_StrContacto]  DEFAULT ('') FOR [StrContacto]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_Numero]  DEFAULT ((1)) FOR [Numero]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_StrDireccion]  DEFAULT ('') FOR [StrDireccion]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_StrPoblacion]  DEFAULT ('') FOR [StrPoblacion]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_StrProvincia]  DEFAULT ('') FOR [StrProvincia]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_StrTelefono1]  DEFAULT ('') FOR [StrTelefono1]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_StrTelefono2]  DEFAULT ('') FOR [StrTelefono2]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_StrTelefonoControlMovil]  DEFAULT ('') FOR [StrTelefonoControlMovil]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_SidCentroCoste]  DEFAULT ('') FOR [SidCentroCoste]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_SidUsrAlta]  DEFAULT ('') FOR [SidUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_FchUsrAlta]  DEFAULT (getdate()) FOR [FchUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_SidUsrModif]  DEFAULT ('') FOR [SidUsrModif]
GO

ALTER TABLE [dbo].[CTI_tb_Servicio] ADD  CONSTRAINT [DF_CTI_tb_Servicio_Estado]  DEFAULT ('N') FOR [Estado]
GO

