INSERT INTO [CPILUNION].[dbo].[CTI_tb_Actividad]
           ([SidEmpresa]
           ,[SidActividad]
           ,[StrActividad]
           ,[SidUsrAlta]
           ,[FchUSrAlta]
           ,[SidUsrModif]
           ,[FchUsrModif]
           ,[CodigoExterno])
SELECT [SidEmpresa]
      ,[SidActividad]
      ,[StrActividad]
      ,[SidUsrAlta]
      ,[FchUSrAlta]
      ,[SidUsrModif]
      ,[FchUsrModif]
      ,[CodigoExterno]
FROM [CPILUNION].dbo.[CPActividad]
WHERE SidEmpresa<>''


