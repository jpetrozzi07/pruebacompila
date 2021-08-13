using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.DBEntities
{
    [Table("CTI_tb_ServicioPersonal")]
    public class CTI_tb_ServicioPersonal
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
        public string SidServicio { get; set; }
        [Key]
        [Column(Order = 4)]
        public decimal NidMatricula { get; set; }
        [MaxLength(50)]
        public string SidUsrAlta { get; set; }
        public DateTime FchUsrAlta { get; set; }
        [MaxLength(50)]
        public string SidUsrModif { get; set; }
        public DateTime? FchUsrModif { get; set; }
        [Timestamp]
        public byte[] Ts { get; set; }
        [Key]
        [Column(Order = 5)]
        public decimal IdContrato { get; set; }
        [Key]
        [Column(Order = 6)]
        public decimal SecuencialContrato { get; set; }
        [MaxLength(1)]
        public string TipoAsignacion { get; set; }
        [Key]
        [Column(Order = 7)]
        public DateTime FchInicio { get; set; }
        public DateTime? Fchfin { get; set; }
        [MaxLength(1)]
        public string Estado { get; set; }
    }
}
