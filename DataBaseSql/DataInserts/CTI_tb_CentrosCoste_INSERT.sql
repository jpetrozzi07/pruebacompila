INSERT INTO CTI_tb_CentrosCoste
(
     [SidEmpresa],
     [SidCliente]	 ,
     [SidCentroCoste],
     [StrCentroCoste],
     [Direccion]	 ,
     [Pais]			 ,
     [SidMunicipio]	 ,
     [Poblacion]	 ,
     [SidProvincia]	 ,
     [Provincia]	 ,
     [StrCP]		 ,
     [StrContacto]	 ,
     [StrTelefono]	 ,
     [SidUsrAlta]	 ,
     [FchUsrAlta]	 ,
     [SidUsrModif]	 ,
     [FchUsrModif]	 ,
     [FchAlta]		 ,
     [FchBaja]		 ,
     [siddelegacion] 
)
SELECT cc.[SidEmpresa]
      ,cc.[SidCliente]
      ,cc.[SidCentroCoste]
      ,cc.[StrCentroCoste]
      ,cc.[Direccion]
      ,cc.[Pais]
      ,cc.[SidMunicipio]
      ,cc.[Poblacion]
      ,cc.[SidProvincia]
      ,cc.[Provincia]
      ,cc.[StrCP]
      ,cc.[StrContacto]
      ,cc.[StrTelefono]
      ,cc.[SidUsrAlta]
      ,cc.[FchUsrAlta]
      ,cc.[SidUsrModif]
      ,cc.[FchUsrModif]
      ,cc.[FchAlta]
      ,cc.[FchBaja]
      ,cc.[siddelegacion]
  FROM [dbo].[CPCentrosCoste] cc
  INNER JOIN dbo.CTI_tb_Cliente cl on cc.SidEmpresa=cl.sidempresa and cc.SidCliente=cl.sidcliente
  where GETDATE() between cc.FchAlta and ISNULL(cc.fchbaja,GETDATE())
  AND   cc.Ts > (

select max(t.id) from (
                            select max(Ts) id from [CTI_tb_Cliente] UNION
                            select max(Ts) id from [CTI_tb_Contrato] UNION
                            select max(Ts) id from [CTI_tb_Servicio] UNION
                            select max(Ts) id from [CTI_tb_ServicioDelegacion] UNION
                            select max(Ts) id from [CTI_tb_ServicioPersonal] UNION
                            select max(Ts) id from [CTI_tb_Personal] UNION
                            select max(Ts) id from [CTI_tb_PersonalContrato] UNION
                            select max(Ts) id from [CTI_tb_PersonalDelegacion] UNION  
							select max(Ts) id from [CTI_tb_CentrosCoste]  ) t )