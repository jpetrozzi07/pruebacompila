INSERT INTO [CPILUNION].[dbo].[CTI_tb_Servicio]
           ([SidEmpresa]
           ,[SidCliente]
           ,[SidContrato]
           ,[SidServicio]
           ,[StrServicio]
           ,[SidActividad]
           ,[StrContacto]
           ,[Numero]
           ,[StrDireccion]
           ,[StrPoblacion]
           ,[StrProvincia]
           ,[StrTelefono1]
           ,[StrTelefono2]
           ,[StrTelefonoControlMovil]
           ,[FchAlta]
           ,[FchBaja]
           ,[SidCentroCoste]
           ,[SidLineaNegocio]
           ,[SidUsrAlta]
           ,[FchUsrAlta]
           ,[SidUsrModif]
           ,[FchUsrModif])
SELECT DISTINCT s.[SidEmpresa]
      ,s.[SidCliente]
      ,s.[SidContrato]
      ,s.[SidServicio]
      ,s.[StrServicio]
      ,s.[SidActividad]
      ,s.[StrContacto]
      ,s.[Numero]
      ,s.[StrDireccion]
      ,s.[StrPoblacion]
      ,s.[StrProvincia]
      ,s.[StrTelefono1]
      ,s.[StrTelefono2]
      ,s.[StrTelefonoControlMovil]
      ,s.[FchAlta]
      ,s.[FchBaja]
      ,s.[SidCentroCoste]
      ,s.[SidLineaNegocio]
      ,s.[SidUsrAlta]
      ,s.[FchUsrAlta]
      ,s.[SidUsrModif]
      ,s.[FchUsrModif]
  FROM [CPILUNION].[dbo].[CPServicio] s
  INNER JOIN [dbo].[CTI_tb_Cliente] cl on s.SidEmpresa=cl.SidEmpresa and s.SidCliente=cl.Sidcliente
  INNER JOIN [dbo].[CTI_tb_Contrato] ct on ct.SidEmpresa=s.SidEmpresa and ct.SidCliente=s.Sidcliente and ct.SidContrato=s.SidContrato
  INNER JOIN [dbo].[CTI_tb_CentrosCoste] cc on cc.SidEmpresa=s.SidEmpresa and cc.SidCliente=s.Sidcliente and cc.SidCentroCoste=s.SidCentroCoste
  WHERE GETDATE() between s.FchAlta and ISNULL(s.fchbaja,GETDATE())
  
GO


