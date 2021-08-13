USE CPILUNION
GO
INSERT INTO [CPILUNION].[dbo].[CTI_tb_Municipios]
           ([SidPais]
           ,[SidProvincia]
           ,[SidMunicipio]
           ,[StrMunicipio]
           ,[SidUsrAlta]
           ,[FchUsrAlta]
           ,[SidUsrModif]
           ,[FchUsrModif])
SELECT [SidPais]
      ,[SidProvincia]
      ,[SidMunicipio]
      ,[StrMunicipio]
      ,[SidUsrAlta]
      ,[FchUsrAlta]
      ,[SidUsrModif]
      ,[FchUsrModif]
  FROM [CPILUNION].[dbo].[CPMunicipios]
GO


GO


