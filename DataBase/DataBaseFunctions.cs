using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TaskIlu14Cti.DataBase
{
    public static class DataBaseFunctions
    {

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public static string GetMaxTs(DbContext ctx)
        {

            string sql = @"select max(t.id) from (
                            select max(Ts) id from [CTI_tb_Cliente] UNION
                            select max(Ts) id from [CTI_tb_Contrato] UNION
                            select max(Ts) id from [CTI_tb_Servicio] UNION
                            select max(Ts) id from [CTI_tb_ServicioDelegacion] UNION
                            select max(Ts) id from [CTI_tb_ServicioPersonal] UNION
                            select max(Ts) id from [CTI_tb_Personal] UNION
                            select max(Ts) id from [CTI_tb_PersonalContrato] UNION
                            select max(Ts) id from [CTI_tb_PersonalDelegacion] UNION  
							select max(Ts) id from [CTI_tb_CentrosCoste]  ) t ";
            try
            {
                var result=  ctx.Database.SqlQuery<Byte[]>(sql).FirstOrDefault();
                return BitConverter.ToString(result).Replace("-","");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            return null;
        }



    }
}
