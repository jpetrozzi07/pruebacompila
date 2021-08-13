using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


    [Table("CTI_tb_Contrato")]
    public class CTI_tb_Contrato
    {
        [Key]
        [Column(Order = 1)]
        [MaxLength(4)]
        public string SidEmpresa { get; set; }
        [Key]
        [Column(Order = 2)]
        [MaxLength(12)]
        public string SidCliente { get; set; }
        [Key]
        [Column(Order = 3)]
        [MaxLength(20)]
        public string SidContrato { get; set; }
        [MaxLength(100)]
        public string StrContrato { get; set; }
        public DateTime FchAlta { get; set; }
        public DateTime? FchBaja { get; set; }
        [MaxLength(50)]
        public string StrCuenta { get; set; }
        [MaxLength(50)]
        public string SidUsrAlta { get; set; }
        public DateTime FchUsrAlta { get; set; }
        [MaxLength(50)]
        public string SidUsrModif { get; set; }
        public DateTime? FchUsrModif { get; set; }
        [Timestamp]
        public byte[] Ts { get; set; }
        [MaxLength(10)]
        public string SidOferta { get; set; }
        public decimal? NumSecContrato { get; set; }
        [MaxLength(6)]
        public string SidLineaNegocio { get; set; }
        [MaxLength(50)]
        public string Nivel { get; set; }
        [MaxLength(50)]
        public string CodigoExterno { get; set; }
        [MaxLength(50)]
        public string SidCodGestor { get; set; }
        [MaxLength(50)]
        public string SidCodSupervisor { get; set; }
        [MaxLength(50)]
        public string SidCodJefeServicio { get; set; }
        [MaxLength(1)]
        public string Estado { get; set; }
    }

