USE [CPILUNION]
GO


CREATE TABLE [dbo].[CTI_tb_PersonalDelegacion](
	[NidMatricula] [numeric](11, 0) NOT NULL,
	[SidEmpresa] [nchar](4) NOT NULL,
	[SidDelegacion] [nchar](6) NOT NULL,
	[SidSubDelegacion] [nchar](6) NOT NULL,
	[FchInicio] [datetime] NOT NULL,
	[Fchfin] [datetime] NULL,
	[EsOrigen] [bit] NOT NULL,
	[SidUsrAlta] [nvarchar](50) NOT NULL,
	[FchUsrAlta] [datetime] NOT NULL,
	[SidUsrModif] [nvarchar](50) NOT NULL,
	[FchUsrModif] [datetime] NULL,
	[Ts] [timestamp] NOT NULL,
	[id] [decimal](18, 0) NOT NULL,
	[Idcontrato] [numeric](18, 0) NULL,
	[Estado] [char](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CTI_tb_PersonalDelegacion] ADD  DEFAULT ((0)) FOR [NidMatricula]
GO

ALTER TABLE [dbo].[CTI_tb_PersonalDelegacion] ADD  DEFAULT ((0)) FOR [SidDelegacion]
GO

ALTER TABLE [dbo].[CTI_tb_PersonalDelegacion] ADD  DEFAULT ((0)) FOR [SidSubDelegacion]
GO

ALTER TABLE [dbo].[CTI_tb_PersonalDelegacion] ADD  DEFAULT ((1)) FOR [EsOrigen]
GO

ALTER TABLE [dbo].[CTI_tb_PersonalDelegacion] ADD  DEFAULT ('') FOR [SidUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_PersonalDelegacion] ADD  DEFAULT (getdate()) FOR [FchUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_PersonalDelegacion] ADD  DEFAULT ('') FOR [SidUsrModif]
GO

ALTER TABLE [dbo].[CTI_tb_PersonalDelegacion] ADD  DEFAULT ('N') FOR [Estado]
GO

ALTER TABLE [dbo].[CTI_tb_PersonalDelegacion]  WITH CHECK ADD FOREIGN KEY([NidMatricula])
REFERENCES [dbo].[CTI_tb_Personal] ([NidMatricula])
ON UPDATE CASCADE
ON DELETE CASCADE
GO