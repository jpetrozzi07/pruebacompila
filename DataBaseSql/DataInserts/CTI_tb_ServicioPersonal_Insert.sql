INSERT INTO [CTI_tb_ServicioPersonal]
(
    [SidEmpresa],
    [SidCliente],
    [SidServicio],
    [NidMatricula],
    [SidUsrAlta],
    [FchUsrAlta],
    [SidUsrModif],
    [FchUsrModif],
    [IdContrato] ,
    [SecuencialContrato],
    [TipoAsignacion],
    [FchInicio],
    [Fchfin] ,
    [Estado] 
)

SELECT DISTINCT
    cp.[SidEmpresa],
    cp.[SidCliente],
    cp.[SidServicio],
    cp.[NidMatricula],
    cp.[SidUsrAlta],
    cp.[FchUsrAlta],
    cp.[SidUsrModif],
    cp.[FchUsrModif],
    cp.[IdContrato] ,
    cp.[SecuencialContrato],
    cp.[TipoAsignacion],
    cp.[FchInicio],
    cp.[Fchfin] ,
    'N'  

FROM [CPServicioPersonal] cp
INNER JOIN CPServicio s on cp.[SidEmpresa]=s.[SidEmpresa] AND cp.[SidCliente]=s.[SidCliente] AND cp.[SidServicio]=s.[SidServicio]
INNER JOIN [dbo].[CPCliente] cl on s.SidEmpresa=cl.SidEmpresa and s.SidCliente=cl.Sidcliente
INNER JOIN [dbo].CPContrato ct on ct.SidEmpresa=s.SidEmpresa and ct.SidCliente=s.Sidcliente and ct.SidContrato=s.SidContrato
INNER JOIN [dbo].CPCentrosCoste cc on cc.SidEmpresa=s.SidEmpresa and cc.SidCliente=s.Sidcliente and cc.SidCentroCoste=s.SidCentroCoste
WHERE GETDATE() between s.FchAlta and ISNULL(s.fchbaja,GETDATE()) 
   AND   cl.SidEmpresa<>'' and cl.SidCliente<>'999999' and GETDATE() between cl.FchAlta and ISNULL(cl.FchBaja,GETDATE()) and cl.SidEmpresa in (select SidEmpresa from CPEmpresa where BolActivo=1) 
   AND	  GETDATE() between ct.FchAlta and ISNULL(ct.fchbaja,GETDATE()) 
   AND	  GETDATE() between cc.FchAlta and ISNULL(cc.fchbaja,GETDATE())
