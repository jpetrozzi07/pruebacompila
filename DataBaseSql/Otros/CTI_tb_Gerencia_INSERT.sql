USE CPILUNION
GO
INSERT INTO [CPILUNION].[dbo].[CTI_tb_Gerencia]
           ([SidEmpresa]
           ,[SidGerencia]
           ,[StrGerencia]
           ,[SidUsrAlta]
           ,[FchUsrAlta]
           ,[SidUsrModif]
           ,[FchUsrModif])
SELECT [SidEmpresa]
      ,[SidGerencia]
      ,[StrGerencia]
      ,[SidUsrAlta]
      ,[FchUsrAlta]
      ,[SidUsrModif]
      ,[FchUsrModif]
  FROM [CPILUNION].[dbo].[CPGerencia]
GO




