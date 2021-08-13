using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


    [Table("CTI_tb_PersonalContrato")]
    public class CTI_tb_PersonalContrato
    {
        [Key]
        [Column(Order = 1)]
        public decimal NidMatricula { get; set; }
        [Key]
        [Column(Order = 2)]
        public decimal IdContrato { get; set; }
        [MaxLength(4)]
        public string SidEmpresa { get; set; }
        [MaxLength(5)]
        public string SidTipoContrato { get; set; }
        public DateTime FchAlta { get; set; }
        public DateTime? FchBaja { get; set; }
        [MaxLength(6)]
        public string SidConvenio { get; set; }
        [MaxLength(1)]
        public string BolNominaProrrateada { get; set; }
        public decimal? NidCentroTrabajo { get; set; }
        public decimal? NumPorcentajeIRPF { get; set; }
        public DateTime? FchAntiguedad { get; set; }
        [MaxLength(2)]
        public string SidMotivoBaja { get; set; }
        [MaxLength(50)]
        public string SidUsrAlta { get; set; }
        public DateTime FchUsrAlta { get; set; }
        [MaxLength(50)]
        public string SidUsrModif { get; set; }
        public DateTime? FchUsrModif { get; set; }
        [Timestamp]
        public byte[] Ts { get; set; }
        public decimal Secuencial { get; set; }
        public bool BolFiniquitado { get; set; }
        public decimal Id { get; set; }
        [MaxLength(8)]
        public string NidOcupacion { get; set; }
        [MaxLength(20)]
        public string StrCCC { get; set; }
        [MaxLength(2)]
        public string SidFormaPagoNomina { get; set; }
        public bool? bolprovisional { get; set; }
        public bool? BolEntregaMaterial { get; set; }
        public bool? BolCotiza { get; set; }
        [MaxLength(50)]
        public string StrIBAN { get; set; }
        [MaxLength(11)]
        public string StrBIC { get; set; }
        [MaxLength(4)]
        public string SidRegimen { get; set; }
        public int BolFirmaHC { get; set; }
        public int? NumDiasPreaviso { get; set; }
        [MaxLength(1)]
        public string StrModalidadCotizacion { get; set; }
        public DateTime? FchFinVacaciones { get; set; }
        [MaxLength(100)]
        public string StrCampoConfigurable1 { get; set; }
        [MaxLength(100)]
        public string StrCampoConfigurable2 { get; set; }
        [MaxLength(100)]
        public string StrCampoConfigurable3 { get; set; }
        [MaxLength(100)]
        public string StrCampoConfigurable4 { get; set; }
        [MaxLength(100)]
        public string StrCampoConfigurable5 { get; set; }
        [MaxLength(1)]
        public string StrPreaviso { get; set; }
        [MaxLength(1)]
        public string AfiAlta { get; set; }
        [MaxLength(1)]
        public string AfiBaja { get; set; }
        [MaxLength(100)]
        public string CodigoEnlace { get; set; }
        public int? NidTipoAntiguedad { get; set; }
        [MaxLength(1)]
        public string Estado { get; set; }
    }
