USE CPILUNION
GO
INSERT INTO [CPILUNION].[dbo].[CTI_tb_Delegacion]
           ([SidEmpresa]
           ,[SidDelegacion]
           ,[StrDelegacion]
           ,[UsoHorario]
           ,[SidPais]
           ,[SidProvincia]
           ,[SidMunicipio]
           ,[SidCP]
           ,[StrDireccion]
           ,[StrTelefono1]
           ,[StrTelefono2]
           ,[StrFax]
           ,[StrEmail]
           ,[SidUsrAlta]
           ,[FchUsrAlta]
           ,[SidUsrModif]
           ,[FchUsrModif]
           ,[CodigoExterno]
           ,[SidGerente])
SELECT [SidEmpresa]
      ,[SidDelegacion]
      ,[StrDelegacion]
      ,[UsoHorario]
      ,[SidPais]
      ,[SidProvincia]
      ,[SidMunicipio]
      ,[SidCP]
      ,[StrDireccion]
      ,[StrTelefono1]
      ,[StrTelefono2]
      ,[StrFax]
      ,[StrEmail]
      ,[SidUsrAlta]
      ,[FchUsrAlta]
      ,[SidUsrModif]
      ,[FchUsrModif]
      ,[CodigoExterno]
      ,[SidGerente]
  FROM [CPILUNION].[dbo].[CPDelegacion]
GO



