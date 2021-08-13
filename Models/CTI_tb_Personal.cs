using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


    [Table("CTI_tb_Personal")]
    public class CTI_tb_Personal
    {
        [Key]
        public decimal NidMatricula { get; set; }
        [MaxLength(50)]
        public string StrNombre { get; set; }
        [MaxLength(50)]
        public string StrApellido1 { get; set; }
        [MaxLength(50)]
        public string StrApellido2 { get; set; }
        [MaxLength(50)]
        public string StrDni { get; set; }
        [MaxLength(50)]
        public string StrPoblacion { get; set; }
        [MaxLength(50)]
        public string StrProvincia { get; set; }
        [MaxLength(100)]
        public string StrDireccion { get; set; }
        [MaxLength(50)]
        public string StrCP { get; set; }
        [MaxLength(1)]
        public string StrSexo { get; set; }
        [MaxLength(255)]
        public string Strfotografia { get; set; }
        [MaxLength(50)]
        public string StrTelefono1 { get; set; }
        [MaxLength(50)]
        public string StrTelefono2 { get; set; }
        [MaxLength(15)]
        public string StrTelefonoControlMovil { get; set; }
        [MaxLength(150)]
        public string Email { get; set; }
        [MaxLength(2)]
        public string NumSS1 { get; set; }
        [MaxLength(8)]
        public string NumSS2 { get; set; }
        [MaxLength(2)]
        public string NumSS3 { get; set; }
        [MaxLength(20)]
        public string StrCCC { get; set; }
        public bool? BolProvisional { get; set; }
        public DateTime? FchNacimiento { get; set; }
        [MaxLength(50)]
        public string SidUsrAlta { get; set; }
        public DateTime FchUsrAlta { get; set; }
        [MaxLength(50)]
        public string SidUsrModif { get; set; }
        public DateTime? FchUsrModif { get; set; }
        [MaxLength(3)]
        public string SidPais { get; set; }
        [MaxLength(5)]
        public string sidMunicipio { get; set; }
        [MaxLength(2)]
        public string SidProvincia { get; set; }
        [MaxLength(50)]
        public string strPais { get; set; }
        [MaxLength(2)]
        public string SidSitFamiliar { get; set; }
        [MaxLength(10)]
        public string StrDNIConyuge { get; set; }
        [MaxLength(1)]
        public string SidDiscapacitado { get; set; }
        [MaxLength(3)]
        public string NidGradoDiscapacidad { get; set; }
        [MaxLength(1)]
        public string BolMovReducida { get; set; }
        [MaxLength(1)]
        public string StrResCeMe { get; set; }
        [MaxLength(1)]
        public string SidSitLaboral { get; set; }
        [MaxLength(1)]
        public string SidMovGeografica { get; set; }
        [MaxLength(1)]
        public string SidProActLaboral { get; set; }
        [Timestamp]
        public byte[] Ts { get; set; }
        [MaxLength(2)]
        public string StrRegularizaIRPF { get; set; }
        public decimal? NumPensionConyuge { get; set; }
        public decimal? NumAnualidadHijos { get; set; }
        [MaxLength(1)]
        public string StrPagoVivienda { get; set; }
        [MaxLength(2)]
        public string SidFormaPagoNomina { get; set; }
        [MaxLength(1)]
        public string StrPagoViviendaAntes2011 { get; set; }
        [MaxLength(1000)]
        public string StrObservaciones { get; set; }
        [MaxLength(100)]
        public string CodigoEnlace { get; set; }
        [MaxLength(3)]
        public string SidNacionalidad { get; set; }
        [MaxLength(1)]
        public string SidTipoDiscapacidad { get; set; }
        public DateTime? FchDiscapacidad { get; set; }
        [MaxLength(10)]
        public string PerImpCambioResidencia { get; set; }
        public DateTime? FchRevocacion { get; set; }
        [MaxLength(1)]
        public string SidTipoIdentificador { get; set; }
        [MaxLength(1)]
        public string Estado { get; set; }
    }
