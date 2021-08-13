USE CPILUNION
GO
CREATE TABLE [dbo].[CTI_tb_SubDelegacion](
	[SidEmpresa] [nchar](4) NOT NULL,
	[SidDelegacion] [nchar](6) NOT NULL,
	[SidSubDelegacion] [nchar](6) NOT NULL,
	[StrSubDelegacion] [char](100) NOT NULL,
	[SidUsrAlta] [char](50) NOT NULL,
	[FchUsrAlta] [datetime] NOT NULL,
	[SidUsrModif] [char](50) NOT NULL,
	[FchUsrModif] [datetime] NULL,
	[Ts] [timestamp] NOT NULL,
	[CodigoExterno] [varchar](50) NULL,
 CONSTRAINT [PK_CTI_tb_SubDelegacion_1] PRIMARY KEY CLUSTERED 
(
	[SidEmpresa] ASC,
	[SidDelegacion] ASC,
	[SidSubDelegacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CTI_tb_SubDelegacion] ADD  CONSTRAINT [DF_CTI_tb_SubDelegacion_SidEmpresa]  DEFAULT ('') FOR [SidEmpresa]
GO

ALTER TABLE [dbo].[CTI_tb_SubDelegacion] ADD  CONSTRAINT [DF_CTI_tb_SubDelegacion_SidDelegacion]  DEFAULT ((0)) FOR [SidDelegacion]
GO

ALTER TABLE [dbo].[CTI_tb_SubDelegacion] ADD  CONSTRAINT [DF_CTI_tb_SubDelegacion_SidSubDelegacion]  DEFAULT ((0)) FOR [SidSubDelegacion]
GO

ALTER TABLE [dbo].[CTI_tb_SubDelegacion] ADD  CONSTRAINT [DF_CTI_tb_SubDelegacion_StrSubDelegacion]  DEFAULT ('') FOR [StrSubDelegacion]
GO

ALTER TABLE [dbo].[CTI_tb_SubDelegacion] ADD  CONSTRAINT [DF_CTI_tb_SubDelegacion_SidUsrAlta]  DEFAULT ('') FOR [SidUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_SubDelegacion] ADD  CONSTRAINT [DF_CTI_tb_SubDelegacion_FchUsrAlta]  DEFAULT (getdate()) FOR [FchUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_SubDelegacion] ADD  CONSTRAINT [DF_CTI_tb_SubDelegacion_SidUsrModif]  DEFAULT ('') FOR [SidUsrModif]
GO




