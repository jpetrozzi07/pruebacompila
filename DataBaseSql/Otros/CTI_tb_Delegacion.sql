USE CPILUNION
GO
CREATE TABLE [dbo].[CTI_tb_Delegacion](
	[SidEmpresa] [nchar](4) NOT NULL,
	[SidDelegacion] [nchar](6) NOT NULL,
	[StrDelegacion] [char](100) NOT NULL,
	[UsoHorario] [int] NOT NULL,
	[SidPais] [nvarchar](3) NULL,
	[SidProvincia] [nvarchar](2) NULL,
	[SidMunicipio] [nvarchar](5) NULL,
	[SidCP] [nvarchar](5) NULL,
	[StrDireccion] [nvarchar](150) NULL,
	[StrTelefono1] [nvarchar](15) NULL,
	[StrTelefono2] [nvarchar](15) NULL,
	[StrFax] [nvarchar](15) NULL,
	[StrEmail] [nvarchar](50) NULL,
	[SidUsrAlta] [nvarchar](50) NULL,
	[FchUsrAlta] [datetime] NULL,
	[SidUsrModif] [nvarchar](50) NOT NULL,
	[FchUsrModif] [datetime] NULL,
	[Ts] [timestamp] NOT NULL,
	[CodigoExterno] [varchar](50) NULL,
	[SidGerente] [varchar](50) NULL,
 CONSTRAINT [PK_CTI_tb_Delegacion_1] PRIMARY KEY CLUSTERED 
(
	[SidEmpresa] ASC,
	[SidDelegacion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CTI_tb_Delegacion] ADD  CONSTRAINT [DF_CTI_tb_Delegacion_SidEmpresa]  DEFAULT ('') FOR [SidEmpresa]
GO

ALTER TABLE [dbo].[CTI_tb_Delegacion] ADD  CONSTRAINT [DF_CTI_tb_Delegacion_SidDelegacion]  DEFAULT ((0)) FOR [SidDelegacion]
GO

ALTER TABLE [dbo].[CTI_tb_Delegacion] ADD  CONSTRAINT [DF_CTI_tb_Delegacion_StrDelegacion]  DEFAULT ('') FOR [StrDelegacion]
GO

ALTER TABLE [dbo].[CTI_tb_Delegacion] ADD  CONSTRAINT [DF_CTI_tb_Delegacion_UsoHorario]  DEFAULT ((1)) FOR [UsoHorario]
GO

ALTER TABLE [dbo].[CTI_tb_Delegacion] ADD  CONSTRAINT [DF_CTI_tb_Delegacion_SidUsrAlta]  DEFAULT ('') FOR [SidUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_Delegacion] ADD  CONSTRAINT [DF_CTI_tb_Delegacion_FchUsrAlta]  DEFAULT (getdate()) FOR [FchUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_Delegacion] ADD  CONSTRAINT [DF_CTI_tb_Delegacion_SidUsrModif]  DEFAULT ('') FOR [SidUsrModif]
GO
