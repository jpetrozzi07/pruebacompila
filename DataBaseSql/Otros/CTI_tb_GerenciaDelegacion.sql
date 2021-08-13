CREATE TABLE [dbo].[CTI_tb_GerenciaDelegacion](
	[SidEmpresa] [char](4) NOT NULL,
	[SidGerencia] [char](2) NOT NULL,
	[SidDelegacion] [char](6) NOT NULL
) ON [PRIMARY]

ALTER TABLE [dbo].[CTI_tb_GerenciaDelegacion] ADD [SidUsrAlta] [varchar](50) NOT NULL
ALTER TABLE [dbo].[CTI_tb_GerenciaDelegacion] ADD [FchUsrAlta] [datetime] NOT NULL
ALTER TABLE [dbo].[CTI_tb_GerenciaDelegacion] ADD [SidUsrModif] [varchar](50) NOT NULL
ALTER TABLE [dbo].[CTI_tb_GerenciaDelegacion] ADD [FchUsrModif] [datetime] NULL
ALTER TABLE [dbo].[CTI_tb_GerenciaDelegacion] ADD [Ts] [timestamp] NOT NULL

ALTER TABLE [dbo].[CTI_tb_GerenciaDelegacion] ADD  CONSTRAINT [PK_CTI_tb_GerenciaDelegacion_1] PRIMARY KEY CLUSTERED 
(
	[SidEmpresa] ASC,
	[SidDelegacion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CTI_tb_GerenciaDelegacion] ADD  CONSTRAINT [DF_CTI_tb_GerenciaDelegacion_SidUsrAlta]  DEFAULT ('') FOR [SidUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_GerenciaDelegacion] ADD  CONSTRAINT [DF_CTI_tb_GerenciaDelegacion_FchUsrAlta]  DEFAULT (getdate()) FOR [FchUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_GerenciaDelegacion] ADD  CONSTRAINT [DF_CTI_tb_GerenciaDelegacion_SidUsrModif]  DEFAULT ('') FOR [SidUsrModif]
GO


