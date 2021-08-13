using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.DBEntities
{
    [Table("CTI_tb_ServicioDelegacion")]
    public class CTI_tb_ServicioDelegacion
    {
        [MaxLength(4)]
        public string SidEmpresa { get; set; }
        [MaxLength(12)]
        public string SidCliente { get; set; }
        [MaxLength(20)]
        public string Sidcontrato { get; set; }
        [MaxLength(20)]
        public string sidservicio { get; set; }
        [MaxLength(6)]
        public string SidDelegacion { get; set; }
        [MaxLength(6)]
        public string SidSubdelegacion { get; set; }
        public DateTime FchInicio { get; set; }
        public DateTime? FchFin { get; set; }
        [MaxLength(50)]
        public string SidUsrAlta { get; set; }
        public DateTime FchUsrAlta { get; set; }
        [MaxLength(50)]
        public string SidUsrModif { get; set; }
        public DateTime? FchUsrModif { get; set; }
        [Timestamp]
        public byte[] Ts { get; set; }
        [Key]
        public decimal Id { get; set; }
        [MaxLength(1)]
        public string Estado { get; set; }
    }
}
