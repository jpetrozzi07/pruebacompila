USE CPILUNION
GO
CREATE TABLE [dbo].[CTI_tb_LineaNegocio](
	[SidEmpresa] [nchar](4) NOT NULL,
	[SidLinea] [nchar](6) NOT NULL,
	[StrDescripcion] [nvarchar](150) NOT NULL,
	[SidUsrAlta] [varchar](50) NOT NULL,
	[FchUsrAlta] [datetime] NOT NULL,
	[SidUsrModif] [varchar](50) NOT NULL,
	[FchUsrModif] [datetime] NULL,
	[Ts] [timestamp] NOT NULL,
	[SidEnlace] [varchar](6) NOT NULL,
	[Defecto] [int] NOT NULL,
 CONSTRAINT [PK_CTI_tb_LineaNegocio] PRIMARY KEY CLUSTERED 
(
	[SidEmpresa] ASC,
	[SidLinea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CTI_tb_LineaNegocio] ADD  CONSTRAINT [DF_CTI_tb_LineaNegocio_SidEmpresa]  DEFAULT ('') FOR [SidEmpresa]
GO

ALTER TABLE [dbo].[CTI_tb_LineaNegocio] ADD  CONSTRAINT [DF_CTI_tb_LineaNegocio_SidLinea]  DEFAULT ('') FOR [SidLinea]
GO

ALTER TABLE [dbo].[CTI_tb_LineaNegocio] ADD  CONSTRAINT [DF_CTI_tb_LineaNegocio_StrDescripcion]  DEFAULT ('') FOR [StrDescripcion]
GO

ALTER TABLE [dbo].[CTI_tb_LineaNegocio] ADD  CONSTRAINT [DF_CTI_tb_LineaNegocio_SidUsrAlta]  DEFAULT ('') FOR [SidUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_LineaNegocio] ADD  CONSTRAINT [DF_CTI_tb_LineaNegocio_FchUsrAlta]  DEFAULT (getdate()) FOR [FchUsrAlta]
GO

ALTER TABLE [dbo].[CTI_tb_LineaNegocio] ADD  CONSTRAINT [DF_CTI_tb_LineaNegocio_SidUsrModif]  DEFAULT ('') FOR [SidUsrModif]
GO

ALTER TABLE [dbo].[CTI_tb_LineaNegocio] ADD  CONSTRAINT [DF_CTI_tb_LineaNegocio_Sidenlace]  DEFAULT ('') FOR [SidEnlace]
GO

ALTER TABLE [dbo].[CTI_tb_LineaNegocio] ADD  CONSTRAINT [DF_CTI_tb_LineaNegocio_Defecto]  DEFAULT ((0)) FOR [Defecto]
GO


