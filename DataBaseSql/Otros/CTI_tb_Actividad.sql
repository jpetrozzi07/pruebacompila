USE CPILUNION
GO
CREATE TABLE [dbo].[CTI_tb_Actividad](
	[SidEmpresa] [nchar](4) NOT NULL,
	[SidActividad] [nchar](8) NOT NULL,
	[StrActividad] [nvarchar](100) NOT NULL,
	[SidUsrAlta] [nvarchar](50) NOT NULL,
	[FchUSrAlta] [datetime] NOT NULL,
	[SidUsrModif] [nvarchar](50) NOT NULL,
	[FchUsrModif] [datetime] NULL,
	[Ts] [timestamp] NOT NULL,
	[CodigoExterno] [varchar](20) NOT NULL,
 CONSTRAINT [PK_CTI_tb_Actividad] PRIMARY KEY CLUSTERED 
(
	[SidEmpresa] ASC,
	[SidActividad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CTI_tb_Actividad] ADD  CONSTRAINT [DF_CTI_tb_Actividad_SidEmpresa]  DEFAULT ('') FOR [SidEmpresa]
GO

ALTER TABLE [dbo].[CTI_tb_Actividad] ADD  CONSTRAINT [DF_CTI_tb_Actividad_SidActividad]  DEFAULT ('') FOR [SidActividad]
GO

ALTER TABLE [dbo].[CTI_tb_Actividad] ADD  CONSTRAINT [DF_CTI_tb_Actividad_StrActividad]  DEFAULT ('') FOR [StrActividad]
GO

ALTER TABLE [dbo].[CTI_tb_Actividad] ADD  CONSTRAINT [DF_CTI_tb_Actividad_SidUsrAlta]  DEFAULT ('') FOR [SidUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_Actividad] ADD  CONSTRAINT [DF_CTI_tb_Actividad_FchUSrAlta]  DEFAULT (getdate()) FOR [FchUSrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_Actividad] ADD  CONSTRAINT [DF_CTI_tb_Actividad_SidUsrModif]  DEFAULT ('') FOR [SidUsrModif]
GO

ALTER TABLE [dbo].[CTI_tb_Actividad] ADD  CONSTRAINT [DF_CTI_tb_Actividad_CodigoExterno]  DEFAULT ('') FOR [CodigoExterno]
GO


