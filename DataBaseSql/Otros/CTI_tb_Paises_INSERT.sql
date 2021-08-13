USE CPILUNION
GO
INSERT INTO [CPILUNION].[dbo].[CTI_tb_Paises]
           ([SidPais]
           ,[StrPais]
           ,[CodigoOficial]
           ,[ISO3166]
           ,[ISO31661])
SELECT [SidPais]
      ,[StrPais]
      ,[CodigoOficial]
      ,[ISO3166]
      ,[ISO31661]
  FROM [CPILUNION].[dbo].[CPPaises]
GO


GO


