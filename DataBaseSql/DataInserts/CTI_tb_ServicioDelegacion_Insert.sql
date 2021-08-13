INSERT INTO [CTI_tb_ServicioDelegacion]  
(   [SidEmpresa],
    [SidCliente],  
    [Sidcontrato]   ,
    [sidservicio]     ,
    [SidDelegacion]   ,
    [SidSubdelegacion] ,
    [FchInicio]    ,
    [FchFin]       ,
    [SidUsrAlta]   ,
    [FchUsrAlta]   ,
    [SidUsrModif]  ,
    [FchUsrModif]  ,
    [Id]       ,
    [Estado] 
    )
SELECT
    sd.[SidEmpresa] ,
    sd.[SidCliente]  ,
    sd.[Sidcontrato]   ,
    sd.[sidservicio]     ,
    sd.[SidDelegacion]   ,
    sd.[SidSubdelegacion] ,
    sd.[FchInicio]    ,
    sd.[FchFin]       ,
    sd.[SidUsrAlta]   ,
    sd.[FchUsrAlta]   ,
    sd.[SidUsrModif]  ,
    sd.[FchUsrModif]  ,
    sd.[Id]       ,
    'N' 
from CPServicioDelegacion sd
inner join CPDelegacion d on sd.SidDelegacion = d.SidDelegacion and sd.SidEmpresa=d.SidEmpresa
inner join CPServicio s on s.SidEmpresa=sd.SidEmpresa and s.SidCliente= sd.SidCliente and s.SidServicio=sd.sidservicio
INNER JOIN [dbo].[CPCliente] cl on s.SidEmpresa=cl.SidEmpresa and s.SidCliente=cl.Sidcliente
INNER JOIN [dbo].CPContrato ct on ct.SidEmpresa=s.SidEmpresa and ct.SidCliente=s.Sidcliente and ct.SidContrato=s.SidContrato
INNER JOIN [dbo].CPCentrosCoste cc on cc.SidEmpresa=s.SidEmpresa and cc.SidCliente=s.Sidcliente and cc.SidCentroCoste=s.SidCentroCoste
where GETDATE() between s.FchAlta and ISNULL(s.FchBaja,GETDATE())
     AND cl.SidEmpresa<>'' and cl.SidCliente<>'999999' and GETDATE() between cl.FchAlta and ISNULL(cl.FchBaja,GETDATE()) and cl.SidEmpresa in (select SidEmpresa from CPEmpresa where BolActivo=1) 
	 AND  GETDATE() between cc.FchAlta and ISNULL(cc.fchbaja,GETDATE())

