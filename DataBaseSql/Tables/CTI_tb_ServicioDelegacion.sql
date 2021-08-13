USE [ILUNION]
GO

CREATE TABLE [dbo].[CTI_tb_ServicioDelegacion] (
    [SidEmpresa]       NCHAR (4)     DEFAULT ('') NOT NULL,
    [SidCliente]       NVARCHAR (12) DEFAULT ((0)) NOT NULL,
    [Sidcontrato]      NCHAR (20)    DEFAULT ('') NOT NULL,
    [sidservicio]      NCHAR (20)    DEFAULT ('') NOT NULL,
    [SidDelegacion]    NCHAR (6)     DEFAULT ((0)) NOT NULL,
    [SidSubdelegacion] NCHAR (6)     DEFAULT ((0)) NOT NULL,
    [FchInicio]        DATETIME      NOT NULL,
    [FchFin]           DATETIME      NULL,
    [SidUsrAlta]       CHAR (50)     DEFAULT ('') NOT NULL,
    [FchUsrAlta]       DATETIME      DEFAULT (getdate()) NOT NULL,
    [SidUsrModif]      CHAR (50)     DEFAULT ('') NOT NULL,
    [FchUsrModif]      DATETIME      NULL,
    [Ts]               ROWVERSION    NOT NULL,
    [Id]               NUMERIC (18)  NOT NULL,
    [Estado]       CHAR           NOT NULL DEFAULT('N'),

    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([SidEmpresa], [SidDelegacion]) REFERENCES [dbo].[CTI_tb_Delegacion] ([SidEmpresa], [SidDelegacion]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([SidEmpresa], [SidCliente], [sidservicio]) REFERENCES [dbo].[CTI_tb_Servicio] ([SidEmpresa], [SidCliente], [SidServicio]) ON DELETE CASCADE ON UPDATE CASCADE
);

