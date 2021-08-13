INSERT INTO CTI_tb_PersonalDelegacion 
(
	[NidMatricula],
	[SidEmpresa] ,
	[SidDelegacion] ,
	[SidSubDelegacion] ,
	[FchInicio] ,
	[Fchfin] ,
	[EsOrigen] ,
	[SidUsrAlta],
	[FchUsrAlta],
	[SidUsrModif] ,
	[FchUsrModif] ,
	[Ts] ,
	[id] ,
	[Idcontrato],
	[Estado] 
)

SELECT
	pc.[NidMatricula],
	pc.[SidEmpresa] ,
	pc.[SidDelegacion] ,
	pc.[SidSubDelegacion] ,
	pc.[FchInicio] ,
	pc.[Fchfin] ,
	pc.[EsOrigen] ,
	pc.[SidUsrAlta],
	pc.[FchUsrAlta],
	pc.[SidUsrModif] ,
	pc.[FchUsrModif] ,
	pc.[Ts] ,
	pc.[id] ,
	pc.[Idcontrato],
    ('N')

FROM CPPersonalContrato pc
INNER JOIN CPPersonal p on pc.nidmatricula = p.nidmatricula
