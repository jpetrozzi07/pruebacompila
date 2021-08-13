USE CPILUNION
GO
CREATE TABLE [dbo].[CTI_tb_Gerencia](
	[SidEmpresa] [char](4) NOT NULL,
	[SidGerencia] [char](2) NOT NULL,
	[StrGerencia] [char](100) NOT NULL,
	[SidUsrAlta] [varchar](50) NOT NULL,
	[FchUsrAlta] [datetime] NOT NULL,
	[SidUsrModif] [varchar](50) NOT NULL,
	[FchUsrModif] [datetime] NULL,
	[Ts] [timestamp] NOT NULL,
 CONSTRAINT [PK_CTI_tb_Gerencia] PRIMARY KEY CLUSTERED 
(
	[SidEmpresa] ASC,
	[SidGerencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CTI_tb_Gerencia] ADD  CONSTRAINT [DF_CTI_tb_Gerencia_StrGerencia]  DEFAULT ('') FOR [StrGerencia]
GO

ALTER TABLE [dbo].[CTI_tb_Gerencia] ADD  CONSTRAINT [DF_CTI_tb_Gerencia_SidUsrAlta]  DEFAULT ('') FOR [SidUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_Gerencia] ADD  CONSTRAINT [DF_CTI_tb_Gerencia_FchUsrAlta]  DEFAULT (getdate()) FOR [FchUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_Gerencia] ADD  CONSTRAINT [DF_CTI_tb_Gerencia_SidUsrModif]  DEFAULT ('') FOR [SidUsrModif]
GO


