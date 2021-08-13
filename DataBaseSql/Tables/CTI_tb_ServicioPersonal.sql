USE [CPILUNION]
GO

CREATE TABLE [dbo].[CTI_tb_ServicioPersonal] (
    [SidEmpresa]         NCHAR (4)       NOT NULL,
    [SidCliente]         NVARCHAR (12)   DEFAULT ((0)) NOT NULL,
    [SidServicio]        NCHAR (20)      NOT NULL,
    [NidMatricula]       NUMERIC (11)    DEFAULT ((0)) NOT NULL,
    [SidUsrAlta]         VARCHAR (50)    DEFAULT ('') NOT NULL,
    [FchUsrAlta]         DATETIME        DEFAULT (getdate()) NOT NULL,
    [SidUsrModif]        VARCHAR (50)    DEFAULT ('') NOT NULL,
    [FchUsrModif]        DATETIME        NULL,
    [Ts]                 ROWVERSION      NOT NULL,
    [IdContrato]         NUMERIC (18)    NOT NULL,
    [SecuencialContrato] NUMERIC (3)     NOT NULL,
    [TipoAsignacion]     CHAR (1)        DEFAULT ('P') NOT NULL,
    [FchInicio]          DATETIME        NOT NULL,
    [Fchfin]             DATETIME        NULL,
    [Estado]       CHAR           NOT NULL DEFAULT('N'),
    PRIMARY KEY CLUSTERED ([SidEmpresa] ASC, [SidCliente] ASC, [SidServicio] ASC, [NidMatricula] ASC, [IdContrato] ASC, [SecuencialContrato] ASC, [FchInicio] ASC),
    FOREIGN KEY ([SidEmpresa], [SidCliente], [SidServicio]) REFERENCES [dbo].[CTI_tb_Servicio] ([SidEmpresa], [SidCliente], [SidServicio]) ON DELETE CASCADE ON UPDATE CASCADE
);


