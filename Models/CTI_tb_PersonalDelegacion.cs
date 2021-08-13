using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


    [Table("CTI_tb_PersonalDelegacion")]
    public class CTI_tb_PersonalDelegacion
    {
        public decimal NidMatricula { get; set; }
        [MaxLength(4)]
        public string SidEmpresa { get; set; }
        [MaxLength(6)]
        public string SidDelegacion { get; set; }
        [MaxLength(6)]
        public string SidSubDelegacion { get; set; }
        public DateTime FchInicio { get; set; }
        public DateTime? Fchfin { get; set; }
        public bool EsOrigen { get; set; }
        [MaxLength(50)]
        public string SidUsrAlta { get; set; }
        public DateTime FchUsrAlta { get; set; }
        [MaxLength(50)]
        public string SidUsrModif { get; set; }
        public DateTime? FchUsrModif { get; set; }
        [Timestamp]
        public byte[] Ts { get; set; }
        [Key]
        public decimal id { get; set; }
        public decimal? Idcontrato { get; set; }
        [MaxLength(1)]
        public string Estado { get; set; }
    }

