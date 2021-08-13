USE CPILUNION
GO
INSERT INTO [CPILUNION].[dbo].[CTI_tb_Provincias]
           ([SidPais]
           ,[SidProvincia]
           ,[StrProvincia]
           ,[SidComunidad])
SELECT [SidPais]
      ,[SidProvincia]
      ,[StrProvincia]
      ,[SidComunidad]
  FROM [CPILUNION].[dbo].[CPProvincias]
GO


GO


