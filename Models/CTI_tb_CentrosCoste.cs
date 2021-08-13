using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("CTI_tb_CentrosCoste")]
public class CTI_tb_CentrosCoste
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
    public string SidCentroCoste { get; set; }
    [MaxLength(150)]
    public string StrCentroCoste { get; set; }
    [MaxLength(150)]
    public string Direccion { get; set; }
    [MaxLength(3)]
    public string Pais { get; set; }
    [MaxLength(5)]
    public string SidMunicipio { get; set; }
    [MaxLength(50)]
    public string Poblacion { get; set; }
    [MaxLength(2)]
    public string SidProvincia { get; set; }
    [MaxLength(50)]
    public string Provincia { get; set; }
    [MaxLength(5)]
    public string StrCP { get; set; }
    [MaxLength(100)]
    public string StrContacto { get; set; }
    [MaxLength(15)]
    public string StrTelefono { get; set; }
    [MaxLength(50)]
    public string SidUsrAlta { get; set; }
    public DateTime FchUsrAlta { get; set; }
    [MaxLength(50)]
    public string SidUsrModif { get; set; }
    public DateTime? FchUsrModif { get; set; }
    [Timestamp]
    public byte[] Ts { get; set; }
    public DateTime? FchAlta { get; set; }
    public DateTime? FchBaja { get; set; }
    [MaxLength(6)]
    public string siddelegacion { get; set; }
    [MaxLength(1)]
    public string Estado { get; set; }
}
