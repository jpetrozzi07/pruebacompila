using DataAccessLayer.Models.DBEntities.BaseClasses;
using DataAccessLayer.Models.DBEntities.Cliente;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("CTI_tb_Cliente")]
public class CTI_tb_Cliente : ClienteKeys
{
    [MaxLength(150)]
    public string SnmCliente { get; set; }

    [MaxLength(50)]
    public string StrCIF { get; set; }

    [MaxLength(150)]
    public string StrDireccion { get; set; }

    [MaxLength(5)]
    public string SidMunicipio { get; set; }

    [MaxLength(2)]
    public string SidProvincia { get; set; }

    [MaxLength(3)]
    public string SidPais { get; set; }

    [MaxLength(20)]
    public string CodPostal { get; set; }

    [MaxLength(15)]
    public string StrTelefono1 { get; set; }

    [MaxLength(15)]
    public string StrTelefono2 { get; set; }

    [MaxLength(15)]
    public string StrFax { get; set; }

    [MaxLength(100)]
    public string StrContacto { get; set; }

    [MaxLength(100)]
    public string StrEmail { get; set; }

    [MaxLength(1)]
    public string TipoCliente { get; set; }

    public DateTime FchAlta { get; set; }
    public DateTime? FchBaja { get; set; }

    [MaxLength(50)]
    public string SidUsrAlta { get; set; }

    public DateTime FchUsrAlta { get; set; }

    [MaxLength(50)]
    public string SidUsrModif { get; set; }

    public DateTime? FchUsrModif { get; set; }

    [MaxLength(1)]
    public string Estado { get; set; }

    [Timestamp]
    public byte[] Ts { get; set; }
}