USE CPILUNION
GO
INSERT INTO [CPILUNION].[dbo].[CTI_tb_SubDelegacion]
           ([SidEmpresa]
           ,[SidDelegacion]
           ,[SidSubDelegacion]
           ,[StrSubDelegacion]
           ,[SidUsrAlta]
           ,[FchUsrAlta]
           ,[SidUsrModif]
           ,[FchUsrModif]
           ,[CodigoExterno])
SELECT [SidEmpresa]
      ,[SidDelegacion]
      ,[SidSubDelegacion]
      ,[StrSubDelegacion]
      ,[SidUsrAlta]
      ,[FchUsrAlta]
      ,[SidUsrModif]
      ,[FchUsrModif]
      ,[CodigoExterno]
  FROM [CPILUNION].[dbo].[CPSubDelegacion]
GO




