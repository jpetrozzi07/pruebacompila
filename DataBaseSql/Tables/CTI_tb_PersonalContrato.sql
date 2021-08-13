USE [CPILUNION]
GO

CREATE TABLE [dbo].[CTI_tb_PersonalContrato] (
    [NidMatricula]           NUMERIC (11)     DEFAULT ((0)) NOT NULL,
    [IdContrato]             NUMERIC (11)     DEFAULT ((0)) NOT NULL,
    [SidEmpresa]             NCHAR (4)       DEFAULT ('') NOT NULL,
    [SidTipoContrato]        NCHAR (5)      NULL,
    [FchAlta]                DATETIME       NOT NULL,
    [FchBaja]                DATETIME       NULL,
    [SidConvenio]            NCHAR (6)      NOT NULL,
    [BolNominaProrrateada]   CHAR (1)         DEFAULT ('N') NULL,
    [NidCentroTrabajo]       DECIMAL (6)    NULL,
    [NumPorcentajeIRPF]      DECIMAL (6, 2)   DEFAULT ((0)) NULL,
    [FchAntiguedad]          DATETIME       NULL,
    [SidMotivoBaja]          CHAR (2)       NULL,
    [SidUsrAlta]             NVARCHAR (50)    DEFAULT ('') NOT NULL,
    [FchUsrAlta]             DATETIME         DEFAULT (getdate()) NOT NULL,
    [SidUsrModif]            NVARCHAR (50)    DEFAULT ('') NOT NULL,
    [FchUsrModif]            DATETIME       NULL,
    [Ts]                     ROWVERSION     NOT NULL,
    [Secuencial]             NUMERIC (6)      DEFAULT ((0)) NOT NULL,
    [BolFiniquitado]         BIT              DEFAULT ((0)) NOT NULL,
    [Id]                     NUMERIC (18)   NOT NULL,
    [NidOcupacion]           CHAR (8)         DEFAULT ('') NULL,
    [StrCCC]                 CHAR (20)      NULL,
    [SidFormaPagoNomina]     CHAR (2)       NULL,
    [bolprovisional]         BIT              DEFAULT ('0') NULL,
    [BolEntregaMaterial]     BIT              DEFAULT ((0)) NULL,
    [BolCotiza]              BIT            NULL,
    [StrIBAN]                NVARCHAR (50)    DEFAULT ('') NULL,
    [StrBIC]                 CHAR (11)        DEFAULT ('') NOT NULL,
    [SidRegimen]             NCHAR (4)        DEFAULT ('0111') NULL,
    [BolFirmaHC]             INT              DEFAULT ((0)) NOT NULL,
    [NumDiasPreaviso]        INT              DEFAULT ('0') NULL,
    [StrModalidadCotizacion] VARCHAR (1)    NULL,
    [FchFinVacaciones]       DATETIME       NULL,
    [StrCampoConfigurable1]  VARCHAR (100)  NULL,
    [StrCampoConfigurable2]  VARCHAR (100)  NULL,
    [StrCampoConfigurable3]  VARCHAR (100)  NULL,
    [StrCampoConfigurable4]  VARCHAR (100)  NULL,
    [StrCampoConfigurable5]  VARCHAR (100)  NULL,
    [StrPreaviso]            CHAR (1)         DEFAULT ('E') NOT NULL,
    [AfiAlta]                VARCHAR (1)    NULL,
    [AfiBaja]                VARCHAR (1)    NULL,
    [CodigoEnlace]           NVARCHAR (100) NULL,
    [NidTipoAntiguedad]      INT            NULL,
    [Estado]                 CHAR   NOT NULL,
      PRIMARY KEY CLUSTERED ([NidMatricula] ASC, [IdContrato] ASC),
      FOREIGN KEY ([NidMatricula]) REFERENCES [dbo].[CTI_tb_Personal] ([NidMatricula]) ON DELETE CASCADE ON UPDATE CASCADE
);


ALTER TABLE [dbo].[CTI_tb_PersonalContrato] ADD  CONSTRAINT [DF_CTI_tb_PersonalContrato_Estado]  DEFAULT ('N') FOR [Estado]
GO

