using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("CTI_tb_Servicio")]
public class CTI_tb_Servicio
{
    [Key]
    [Column(Order = 1)]
    public string SidEmpresa { get; set; }
    [Key]
    [Column(Order = 2)]
    [MaxLength(12)]
    public string SidCliente { get; set; }
    [MaxLength(20)]
    public string SidContrato { get; set; }
    [Key]
    [Column(Order = 3)]
    [MaxLength(20)]
    public string SidServicio { get; set; }
    [MaxLength(150)]
    public string StrServicio { get; set; }
    [MaxLength(8)]
    public string SidActividad { get; set; }
    [MaxLength(100)]
    public string StrContacto { get; set; }
    public decimal Numero { get; set; }
    [MaxLength(100)]
    public string StrDireccion { get; set; }
    [MaxLength(50)]
    public string StrPoblacion { get; set; }
    [MaxLength(50)]
    public string StrProvincia { get; set; }
    [MaxLength(15)]
    public string StrTelefono1 { get; set; }
    [MaxLength(50)]
    public string StrTelefono2 { get; set; }
    [MaxLength(50)]
    public string StrTelefonoControlMovil { get; set; }
    public DateTime FchAlta { get; set; }
    public DateTime? FchBaja { get; set; }
    [MaxLength(20)]
    public string SidCentroCoste { get; set; }
    [MaxLength(6)]
    public string SidLineaNegocio { get; set; }
    [MaxLength(50)]
    public string SidUsrAlta { get; set; }
    public DateTime FchUsrAlta { get; set; }
    [MaxLength(50)]
    public string SidUsrModif { get; set; }
    public DateTime? FchUsrModif { get; set; }
    [Timestamp]
    public byte[] Ts { get; set; }
    [MaxLength(1)]
    public string Estado { get; set; }
}
