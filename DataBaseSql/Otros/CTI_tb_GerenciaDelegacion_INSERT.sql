USE CPILUNION
GO
INSERT INTO [CPILUNION].[dbo].[CTI_tb_GerenciaDelegacion]
           ([SidEmpresa]
           ,[SidGerencia]
           ,[SidDelegacion]
           ,[SidUsrAlta]
           ,[FchUsrAlta]
           ,[SidUsrModif]
           ,[FchUsrModif])
SELECT [SidEmpresa]
      ,[SidGerencia]
      ,[SidDelegacion]
      ,[SidUsrAlta]
      ,[FchUsrAlta]
      ,[SidUsrModif]
      ,[FchUsrModif]
  FROM [CPILUNION].[dbo].[CPGerenciaDelegacion]
GO



