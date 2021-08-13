using DataAccessLayer.Models.BaseClasses;
using DataAccessLayer.Models.DBEntities;
using DataAccessLayer.Models.DBEntities.BaseClasses;
using DataAccessLayer.Models.DBEntities.CentrosCoste;
using DataAccessLayer.Models.DBEntities.Cliente;
using DataAccessLayer.Models.DBEntities.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskIlu14Cti.Classes.EqualityComparers
{

    public class ClienteComparer : IEqualityComparer<ClienteKeys>
    {
        public bool Equals(ClienteKeys x, ClienteKeys y)
        {
            return (x.SidCliente == y.SidCliente && x.SidEmpresa == y.SidEmpresa);
        }

        public int GetHashCode(ClienteKeys obj)
        {
            return (obj.SidCliente.Trim() + obj.SidEmpresa.Trim()).GetHashCode();
        }
    }

    public class ServicioComparer : IEqualityComparer<ServicioKeys>
    {
        public bool Equals(ServicioKeys x, ServicioKeys y)
        {
            return (x.SidCliente == y.SidCliente && x.SidEmpresa == y.SidEmpresa && x.SidServicio == y.SidServicio);
        }

        public int GetHashCode(ServicioKeys obj)
        {
            return (obj.SidCliente.Trim() + obj.SidEmpresa.Trim() + obj.SidServicio).GetHashCode();
        }
    }
    public class CentrosCosteComparer : IEqualityComparer<CentrosCosteKeys>
    {
        public bool Equals(CentrosCosteKeys x, CentrosCosteKeys y)
        {
            return (x.SidCliente == y.SidCliente && x.SidEmpresa == y.SidEmpresa && x.SidCentroCoste == y.SidCentroCoste);
        }

        public int GetHashCode(CentrosCosteKeys obj)
        {
            return (obj.SidCliente.Trim() + obj.SidEmpresa.Trim() + obj.SidCentroCoste).GetHashCode();
        }
    }

    public class PersonalComparer : IEqualityComparer<PersonalKeys>
    {
        public bool Equals(PersonalKeys x, PersonalKeys y)
        {
            return (x.NidMatricula == y.NidMatricula);
        }

        public int GetHashCode(PersonalKeys obj)
        {
            return obj.NidMatricula.GetHashCode();
        }
    }
    public class ContratoComparer : IEqualityComparer<ContratoKeys>
    {
        public bool Equals(ContratoKeys x, ContratoKeys y)
        {
            return (x.SidCliente == y.SidCliente && x.SidEmpresa == y.SidEmpresa && x.SidContrato == y.SidContrato);
        }

        public int GetHashCode(ContratoKeys obj)
        {
            return (obj.SidCliente.Trim() + obj.SidEmpresa.Trim() + obj.SidContrato).GetHashCode();
        }
    }

    public class PersonalContratolComparer : IEqualityComparer<PersonalContratoKeys>
    {
        public bool Equals(PersonalContratoKeys x, PersonalContratoKeys y)
        {
            return (x.IdContrato == y.IdContrato && x.NidMatricula == y.NidMatricula );
        }

        public int GetHashCode(PersonalContratoKeys obj)
        {
            return (obj.IdContrato.ToString()+ obj.NidMatricula.ToString()).GetHashCode();
        }
    }
    public class PersonalDelegacionComparer : IEqualityComparer<PersonalDelegacionKeys>
    {
        public bool Equals(PersonalDelegacionKeys x, PersonalDelegacionKeys y)
        {
            return (x.id == y.id);
        }

        public int GetHashCode(PersonalDelegacionKeys obj)
        {
            return obj.id.ToString().GetHashCode();
        }
    }

    public class ServicioDelegacionComparer : IEqualityComparer<ServicioDelegacionKeys>
    {
        public bool Equals(ServicioDelegacionKeys x, ServicioDelegacionKeys y)
        {
            return (x.Id == y.Id);
        }

        public int GetHashCode(ServicioDelegacionKeys obj)
        {
            return obj.Id.ToString().GetHashCode();
        }
    }

    public class ServicioPersonalComparer : IEqualityComparer<ServicioPersonalKeys>
    {
        public bool Equals(ServicioPersonalKeys x, ServicioPersonalKeys y)
        {
            return (x.Fchfin==y.Fchfin && x.Fchfin==y.FchInicio && x.IdContrato==y.IdContrato && x.NidMatricula == y.NidMatricula 
                    && x.SecuencialContrato==y.SecuencialContrato && x.SidCliente==y.SidCliente && x.SidEmpresa == y.SidEmpresa && x.SidServicio == y.SidServicio);
        }

        public int GetHashCode(ServicioPersonalKeys obj)
        {
            return (obj.Fchfin.ToString() + obj.FchInicio.ToString() + obj.IdContrato.ToString() + obj.NidMatricula.ToString() + obj.SecuencialContrato.ToString() + obj.SidCliente + obj.SidEmpresa + obj.SidServicio).GetHashCode();
        }
    }
}
