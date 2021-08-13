USE CPILUNION
GO
INSERT INTO [CPILUNION].[dbo].[CTI_tb_LineaNegocio]
           ([SidEmpresa]
           ,[SidLinea]
           ,[StrDescripcion]
           ,[SidUsrAlta]
           ,[FchUsrAlta]
           ,[SidUsrModif]
           ,[FchUsrModif]
           ,[SidEnlace]
           ,[Defecto])
SELECT [SidEmpresa]
      ,[SidLinea]
      ,[StrDescripcion]
      ,[SidUsrAlta]
      ,[FchUsrAlta]
      ,[SidUsrModif]
      ,[FchUsrModif]
      ,[SidEnlace]
      ,[Defecto]
  FROM [CPILUNION].[dbo].[CPLineaNegocio]
GO




