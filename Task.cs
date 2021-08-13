using DataAccessLayer.Classes;
using DataAccessLayer.Context;
using DataAccessLayer.Models.BaseClasses;
using DataAccessLayer.Models.DBEntities;
using DataAccessLayer.Models.DBEntities.CentrosCoste;
using DataAccessLayer.Models.DBEntities.Cliente;
using DataAccessLayer.Models.DBEntities.Personal;
using DataAccessLayer.Models.DBEntities.Servicios;
using DataAccessLayer.Models.Empresa;
using DataAccessLayer.Models.Procesos;
using KVPImporter.Common.Interfaces;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using TaskIlu14Cti.Classes.EqualityComparers;
using TaskIlu14Cti.DataBase;

namespace TaskIlu14Cti
{
    public class Task : ITask
    {

        #region variables

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static LogImportacionService logImportacionService;
        private static CPApiProcesosLanzados Proceso;
        public static string IdEmpresa;

        public const string FICHERO = "CTI";
        public const string ERROR = "CON ERRORES";
        public const string NO_ERROR = "PROCESADO";

        public enTaskStatus TaskStatus { get; set; }
        public string AssemblyName { get; set; }
        public string Name { get; set; }
        public string AssemblyParams { get; set; }

        #endregion

        #region events

        public void Init()
        {
            throw new NotImplementedException();
        }

        public bool Run(string[] parameters)
        {

            string process_ini = string.Empty;
            string process_end = string.Empty;

            var watch = Stopwatch.StartNew();
            DbContext Context = new ApiContext("ControlPresenciaConnectionString");
            logImportacionService = new LogImportacionService(new DbRepository(Context));

            Proceso = GetIdProceso(Context);
            IdEmpresa = string.Empty;

            Log.Info($"Process Start...  : {DateTime.Now}");
            process_ini = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $" - Inicio proceso CTI - ", Fichero = FICHERO, SidEmpresa = IdEmpresa });

            // Get max TS for all destination Tables
            string maxTs = DataBaseFunctions.GetMaxTs(Context);
            maxTs = $"0x{maxTs}";
            List<bool> errors = new List<bool>();

            string log_detail = string.Empty; // Aquí guardo el log por proceso

            //#01# Process Personal
            errors.Add(ProcessPersonal("CPPersonal", "CTI_tb_Personal", maxTs, Context, ref log_detail));

            //#02# Process PersonalContrato
            errors.Add(ProcessPersonalContrato("CPPersonalContrato", "CTI_tb_PersonalContrato", maxTs, Context, ref log_detail));

            //#03# Process PersonalDelegacion
            errors.Add(ProcessPersonalDelegacion("CPPersonalDelegacion", "CTI_tb_PersonalDelegacion", maxTs, Context, ref log_detail));

            //#04# Process Clientes
            errors.Add(ProcessClientes("CPCliente", "CTI_tb_Cliente", maxTs, Context, ref log_detail));

            //#05# Process Contrato
            errors.Add(ProcessContrato("CPContrato", "CTI_tb_Contrato", maxTs, Context, ref log_detail));

            //#06# Process CentrosCoste
            errors.Add(ProcessCentrosCoste("CPCentrosCoste", "CTI_tb_CentrosCoste", maxTs, Context, ref log_detail));

            //#07# Process Servicios
            errors.Add(ProcessServicios("CPServicio", "CTI_tb_Servicio", maxTs, Context, ref log_detail));

            //#08# Process ServicioDelegacion
            errors.Add(ProcessServicioDelegacion("CPServicioDelegacion", "CTI_tb_ServicioDelegacion", maxTs, Context, ref log_detail));

            //#09# Process ServicioPersonal
            errors.Add(ProcessServicioPersonal("CPServicioPersonal", "CTI_tb_ServicioPersonal", maxTs, Context, ref log_detail));

            string errorMesagge = errors.Any(o => o) ? "Finalizado con Errores" : string.Empty;

            var rootAppender = ((Hierarchy)LogManager.GetRepository()).Root.Appenders.OfType<FileAppender>().FirstOrDefault();

            string filename = rootAppender != null ? rootAppender.File : string.Empty;

            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $" - Fin actualización CTI fichero log generado:  {filename} ", Fichero = FICHERO, SidEmpresa = IdEmpresa });
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $" - Fin actualización CTI Tiempo:  {watch.ElapsedMilliseconds / 1000} segundos. {errorMesagge} ", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = errors.Any(o => o) ? ERROR : NO_ERROR });

            watch.Stop();
            Log.Info($"Process End: {DateTime.Now} Took: {(watch.ElapsedMilliseconds / 1000) / 60} min. ");
            process_end = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            UpdateProccess(Proceso, Context);

            if (errorMesagge != string.Empty)
            {
                // Envío un email con el informe de LOG - Parámetros en App.config
                string subject = "Información de Importación CTI - ILUNION";
                string process_info = $"Inicio: {process_ini} - Fin: {process_end}. Tiempo: {(watch.ElapsedMilliseconds / 1000) / 60} min. <br/>";
                string message = $"Detalle del Proceso <br/> {log_detail} {process_info}";

                var result = KVP.Util.EMail.EMail.SendMail(subject, message); // envía el email
            }

            return true;
        }

        #endregion

        #region methods process

        /// <summary>
        /// # 01 - CPPersonal --> CTI_tb_Personal
        /// </summary>
        /// <param name="sourceTableName"></param>
        /// <param name="destinationTableName"></param>
        /// <param name="maxTs"></param>
        /// <param name="context"></param>
        /// <param name="log_detail"></param>
        /// <returns>True.Error / False.OK</returns>
        private static bool ProcessPersonal(string sourceTableName, string destinationTableName, string maxTs, DbContext context, ref string log_detail)
        {

            // Log Description
            log_detail = $"{log_detail}# 01 - CTI Personal <br/>";

            var watch = Stopwatch.StartNew();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = "# 01 - Inicio actualización CTI Personal", Fichero = FICHERO, SidEmpresa = IdEmpresa });
            string result = string.Empty;
            List<string> sqlCommands = new List<string>();

            try
            {
                List<CPPersonal> sourceEntities = context.Database.SqlQuery<CPPersonal>($"SELECT * FROM  {sourceTableName} WHERE Ts >{maxTs}").ToList();
                List<CTI_tb_Personal> destinationEntities = context.Database.SqlQuery<CTI_tb_Personal>($"SELECT * FROM {destinationTableName} Where Estado <> 'D' OR Estado is null").ToList();

                List<PersonalKeys> sourceKeys = sourceEntities.Select(o => new PersonalKeys() { NidMatricula = o.NidMatricula }).ToList();
                List<PersonalKeys> destinationKeys = destinationEntities.Select(o => new PersonalKeys() { NidMatricula = o.NidMatricula }).ToList();

                // Modified
                var toUpdate = destinationKeys.Intersect(sourceKeys, new PersonalComparer()).ToList();
                if (toUpdate.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateSql(toUpdate, sourceEntities, destinationTableName, context));

                // New
                var toInsert = sourceKeys.Except(destinationKeys, new PersonalComparer()).ToList();
                if (toInsert.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateInsertSql<CPPersonal, PersonalKeys>(toInsert, sourceEntities, destinationTableName, context));

                // Deleted
                var toDeleteKeys = context.Database.SqlQuery<PersonalKeys>($"SELECT * FROM  {sourceTableName}Hco WHERE Tipo ='D' ").ToList();
                var toDelete = destinationKeys.Intersect(toDeleteKeys, new PersonalComparer()).ToList();
                if (toDelete.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateDeletedSql(toDelete, destinationTableName, context));

                result = DataBaseUtilities.SendSqlCommands(destinationTableName, context, sqlCommands) ? "OK" : "ERROR";
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"Error en actualizacion de {destinationTableName}", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = ERROR });

                // Log Description
                log_detail = $"{log_detail}Error en actualizacion de {destinationTableName}. <br/>";
                log_detail = $"{log_detail}{ex.Message} <br/>";
                log_detail = $"{log_detail}<br/>";

                return true;
            }

            watch.Stop();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"# 01 - Fin actualización CTI {destinationTableName} con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg.", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = NO_ERROR });

            // Log Description
            log_detail = $"{log_detail}       Fin CTI Personal con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg. <br/>";
            log_detail = $"{log_detail}<br/>";

            Log.Info($"Process {destinationTableName} End. Took: {watch.ElapsedMilliseconds / 1000} secs. ");

            if (result == "OK")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// # 02 - CPPersonalContrato --> CTI_tb_PersonalContrato
        /// </summary>
        /// <param name="sourceTableName"></param>
        /// <param name="destinationTableName"></param>
        /// <param name="maxTs"></param>
        /// <param name="context"></param>
        /// <param name="log_detail"></param>
        /// <returns>True.Error / False.OK</returns>
        private static bool ProcessPersonalContrato(string sourceTableName, string destinationTableName, string maxTs, DbContext context, ref string log_detail)
        {

            // Log Description
            log_detail = $"{log_detail}# 02 - CTI Personal-Contrato <br/>";

            var watch = Stopwatch.StartNew();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = "# 02 - Inicio actualización CTI Personal-Contrato", Fichero = FICHERO, SidEmpresa = IdEmpresa });
            string result = string.Empty;
            List<string> sqlCommands = new List<string>();

            try
            {
                List<CPPersonalContrato> sourceEntities = context.Database.SqlQuery<CPPersonalContrato>($"SELECT * FROM  {sourceTableName} pc" +
                                                                                                $" INNER JOIN CPPersonal p on pc.nidmatricula = p.nidmatricula" +
                                                                                                $" WHERE pc.Ts >{maxTs}").ToList();
                List<CTI_tb_PersonalContrato> destinationEntities = context.Database.SqlQuery<CTI_tb_PersonalContrato>($"SELECT * FROM {destinationTableName} Where Estado <> 'D' OR Estado is null").ToList();

                List<PersonalContratoKeys> sourceKeys = sourceEntities.Select(o => new PersonalContratoKeys() { NidMatricula = o.NidMatricula, IdContrato = o.IdContrato }).ToList();
                List<PersonalContratoKeys> destinationKeys = destinationEntities.Select(o => new PersonalContratoKeys() { NidMatricula = o.NidMatricula, IdContrato = o.IdContrato }).ToList();

                // Modified
                var toUpdate = destinationKeys.Intersect(sourceKeys, new PersonalContratolComparer()).ToList();
                if (toUpdate.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateSql(toUpdate, sourceEntities, destinationTableName, context));

                // New
                var toInsert = sourceKeys.Except(destinationKeys, new PersonalContratolComparer()).ToList();
                if (toInsert.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateInsertSql<CPPersonalContrato, PersonalContratoKeys>(toInsert, sourceEntities, destinationTableName, context));

                //Deleted
                var toDeleteKeys = context.Database.SqlQuery<PersonalContratoKeys>($"SELECT * FROM  {sourceTableName}Hco WHERE TipoUD ='D' ").ToList();
                var toDelete = destinationKeys.Intersect(toDeleteKeys, new PersonalContratolComparer()).ToList();
                if (toDelete.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateDeletedSql(toDelete, destinationTableName, context));

                result = DataBaseUtilities.SendSqlCommands(destinationTableName, context, sqlCommands) ? "OK" : "ERROR";
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"Error en actualizacion de  {destinationTableName}", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = ERROR });

                // Log Description
                log_detail = $"{log_detail}Error en actualizacion de {destinationTableName}. <br/>";
                log_detail = $"{log_detail}{ex.Message} <br/>";
                log_detail = $"{log_detail}<br/>";

                return true;
            }

            watch.Stop();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"# 02 - Fin actualización CTI {destinationTableName} con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg.", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = NO_ERROR });

            // Log Description
            log_detail = $"{log_detail}       Fin CTI Personal-Contrato con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg. <br/>";
            log_detail = $"{log_detail}<br/>";

            Log.Info($"Process {destinationTableName} End. Took: {watch.ElapsedMilliseconds / 1000} secs. ");

            if (result == "OK")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// # 03 - CPPersonalDelegacion --> CTI_tb_PersonalDelegacion
        /// </summary>
        /// <param name="sourceTableName"></param>
        /// <param name="destinationTableName"></param>
        /// <param name="maxTs"></param>
        /// <param name="context"></param>
        /// <param name="log_detail"></param>
        /// <returns>True.Error / False.OK</returns>
        private static bool ProcessPersonalDelegacion(string sourceTableName, string destinationTableName, string maxTs, DbContext context, ref string log_detail)
        {
            // Log Description
            log_detail = $"{log_detail}# 03 - CTI Personal-Delegacion <br/>";

            var watch = Stopwatch.StartNew();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = "# 03 - Inicio actualización CTI Personal-Delegacion", Fichero = FICHERO, SidEmpresa = IdEmpresa });

            string result = string.Empty;
            List<string> sqlCommands = new List<string>();

            try
            {
                List<CPPersonalDelegacion> sourceEntities = context.Database.SqlQuery<CPPersonalDelegacion>($"SELECT * FROM  {sourceTableName} cpd" +
                                                                                                    $" INNER JOIN cpPersonal cpp ON cpp.nidmatricula = cpd.nidmatricula" +
                                                                                                    $" WHERE cpd.Ts >{maxTs}").ToList();

                List<CTI_tb_PersonalDelegacion> destinationEntities = context.Database.SqlQuery<CTI_tb_PersonalDelegacion>($"SELECT * FROM {destinationTableName} Where Estado <> 'D' OR Estado is null").ToList();

                List<PersonalDelegacionKeys> sourceKeys = sourceEntities.Select(o => new PersonalDelegacionKeys() { id = o.id }).ToList();
                List<PersonalDelegacionKeys> destinationKeys = destinationEntities.Select(o => new PersonalDelegacionKeys() { id = o.id }).ToList();

                // Modified
                var toUpdate = destinationKeys.Intersect(sourceKeys, new PersonalDelegacionComparer()).ToList();
                if (toUpdate.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateSql(toUpdate, sourceEntities, destinationTableName, context));

                // New
                var toInsert = sourceKeys.Except(destinationKeys, new PersonalDelegacionComparer()).ToList();
                if (toInsert.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateInsertSql<CPPersonalDelegacion, PersonalDelegacionKeys>(toInsert, sourceEntities, destinationTableName, context));

                //Deleted
                var toDeleteKeys = context.Database.SqlQuery<PersonalDelegacionKeys>($"SELECT * FROM  {sourceTableName}Hco WHERE Tipo ='D' ").ToList();
                var toDelete = destinationKeys.Intersect(toDeleteKeys, new PersonalDelegacionComparer()).ToList();
                if (toDelete.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateDeletedSql(toDelete, destinationTableName, context));

                result = DataBaseUtilities.SendSqlCommands(destinationTableName, context, sqlCommands) ? "OK" : "ERROR";
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"Error en actualizacion de  {destinationTableName}", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = ERROR });

                // Log Description
                log_detail = $"{log_detail}Error en actualizacion de {destinationTableName}. <br/>";
                log_detail = $"{log_detail}{ex.Message} <br/>";
                log_detail = $"{log_detail}<br/>";

                return true;
            }

            watch.Stop();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"# 03 - Fin actualización CTI {destinationTableName} con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg.", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = NO_ERROR });

            // Log Description
            log_detail = $"{log_detail}       Fin CTI Personal-Delegacion con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg. <br/>";
            log_detail = $"{log_detail}<br/>";

            Log.Info($"Process {destinationTableName} End. Took: {watch.ElapsedMilliseconds / 1000} secs. ");

            if (result == "OK")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// # 04 - CPCliente --> CTI_tb_Cliente
        /// </summary>
        /// <param name="sourceTableName"></param>
        /// <param name="destinationTableName"></param>
        /// <param name="maxTs"></param>
        /// <param name="context"></param>
        /// <param name="log_detail"></param>
        /// <returns>True.Error / False.OK</returns>
        private static bool ProcessClientes(string sourceTableName, string destinationTableName, string maxTs, DbContext context, ref string log_detail)
        {

            // Log Description
            log_detail = $"{log_detail}# 04 - CTI Clientes <br/>";

            var watch = Stopwatch.StartNew();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = "# 04 - Inicio actualización CTI Clientes", Fichero = FICHERO, SidEmpresa = IdEmpresa });

            string result = string.Empty;
            List<string> sqlCommands = new List<string>();


            try
            {
                List<CPCliente> sourceEntities = context.Database.SqlQuery<CPCliente>($"SELECT * FROM  {sourceTableName} " +
                                                                                $" WHERE SidEmpresa<>'' and SidCliente<>'999999' and GETDATE() between FchAlta and ISNULL(FchBaja,GETDATE())" +
                                                                                $" AND  Ts >{maxTs}").ToList();
                List<CTI_tb_Cliente> destinationEntities = context.Database.SqlQuery<CTI_tb_Cliente>($"SELECT * FROM {destinationTableName} WHERE Estado <> 'D' OR Estado is null").ToList();

                List<ClienteKeys> sourceKeys = sourceEntities.Select(o => new ClienteKeys() { SidCliente = o.SidCliente, SidEmpresa = o.SidEmpresa }).ToList();

                List<ClienteKeys> destinationKeys = destinationEntities.Select(o => new ClienteKeys() { SidCliente = o.SidCliente, SidEmpresa = o.SidEmpresa }).ToList();

                // Modified
                var toUpdate = destinationKeys.Intersect(sourceKeys, new ClienteComparer()).ToList();
                if (toUpdate.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateSql(toUpdate, sourceEntities, destinationTableName, context));

                // New
                var toInsert = sourceKeys.Except(destinationKeys, new ClienteComparer()).ToList();
                if (toInsert.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateInsertSql<CPCliente, ClienteKeys>(toInsert, sourceEntities, destinationTableName, context));

                //Deleted
                var toDeleteKeys = context.Database.SqlQuery<ClienteKeys>($"SELECT * FROM  {sourceTableName}Hco WHERE TipoUD='D' ").ToList();
                var toDelete = destinationKeys.Intersect(toDeleteKeys, new ClienteComparer()).ToList();
                if (toDelete.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateDeletedSql(toDelete, destinationTableName, context));

                result = DataBaseUtilities.SendSqlCommands(destinationTableName, context, sqlCommands) ? "OR" : "ERROR";
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"Error en actualizacion de  {destinationTableName}", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = ERROR });

                // Log Description
                log_detail = $"{log_detail}Error en actualizacion de {destinationTableName}. <br/>";
                log_detail = $"{log_detail}{ex.Message} <br/>";
                log_detail = $"{log_detail}<br/>";

                return true;
            }

            watch.Stop();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"# 04 - Fin actualización CTI {destinationTableName} con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg.", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = NO_ERROR });

            // Log Description
            log_detail = $"{log_detail}       Fin CTI Clientes con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg. <br/>";
            log_detail = $"{log_detail}<br/>";

            Log.Info($"Process {destinationTableName} End. Took: {watch.ElapsedMilliseconds / 1000} secs. ");

            if (result == "OK")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// # 05 - CPContrato --> CTI_tb_Contrato
        /// </summary>
        /// <param name="sourceTableName"></param>
        /// <param name="destinationTableName"></param>
        /// <param name="maxTs"></param>
        /// <param name="context"></param>
        /// <param name="log_detail"></param>
        /// <returns>True.Error / False.OK</returns>
        private static bool ProcessContrato(string sourceTableName, string destinationTableName, string maxTs, DbContext context, ref string log_detail)
        {
            // Log Description
            log_detail = $"{log_detail}# 05 - CTI Contrato <br/>";

            var watch = Stopwatch.StartNew();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = "# 05 - Inicio actualización CTI Contrato", Fichero = FICHERO, SidEmpresa = IdEmpresa });
            string result = string.Empty;
            List<string> sqlCommands = new List<string>();


            try
            {
                List<CPContrato> sourceEntities = context.Database.SqlQuery<CPContrato>($"SELECT * FROM  {sourceTableName} ct " +
                                                                                $" INNER JOIN CPCliente cl on ct.SidEmpresa=cl.sidempresa and ct.SidCliente=cl.sidcliente " +
                                                                                $" WHERE ct.Ts >{maxTs}  AND (GETDATE() between ct.FchAlta and ISNULL(ct.fchbaja,GETDATE()))" +
                                                                                $" AND cl.SidEmpresa<>'' and cl.SidCliente<>'999999'" +
                                                                                $" AND (GETDATE() between cl.FchAlta and ISNULL(cl.fchbaja,GETDATE()))").ToList();
                List<CTI_tb_Contrato> destinationEntities = context.Database.SqlQuery<CTI_tb_Contrato>($"SELECT * FROM {destinationTableName} Where Estado <> 'D' OR Estado is null").ToList();

                List<ContratoKeys> sourceKeys = sourceEntities.Select(o => new ContratoKeys() { SidEmpresa = o.SidEmpresa, SidCliente = o.SidCliente, SidContrato = o.SidContrato }).ToList();
                List<ContratoKeys> destinationKeys = destinationEntities.Select(o => new ContratoKeys() { SidEmpresa = o.SidEmpresa, SidCliente = o.SidCliente, SidContrato = o.SidContrato }).ToList();

                // Modified
                var toUpdate = destinationKeys.Intersect(sourceKeys, new ContratoComparer()).ToList();
                if (toUpdate.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateSql(toUpdate, sourceEntities, destinationTableName, context));

                // New
                var toInsert = sourceKeys.Except(destinationKeys, new ContratoComparer()).ToList();
                if (toInsert.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateInsertSql<CPContrato, ContratoKeys>(toInsert, sourceEntities, destinationTableName, context));

                //Deleted
                var toDeleteKeys = context.Database.SqlQuery<ContratoKeys>($"SELECT * FROM  {sourceTableName}Hco WHERE TipoUD ='D' ").ToList();
                var toDelete = destinationKeys.Intersect(toDeleteKeys, new ContratoComparer()).ToList();
                if (toDelete.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateDeletedSql(toDelete, destinationTableName, context));

                result = DataBaseUtilities.SendSqlCommands(destinationTableName, context, sqlCommands) ? "OK" : "ERROR";
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"Error en actualizacion de  {destinationTableName}", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = ERROR });

                // Log Description
                log_detail = $"{log_detail}Error en actualizacion de {destinationTableName}. <br/>";
                log_detail = $"{log_detail}{ex.Message} <br/>";
                log_detail = $"{log_detail}<br/>";

                return true;
            }

            watch.Stop();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"# 05 - Fin actualización CTI {destinationTableName} con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg.", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = NO_ERROR });

            // Log Description
            log_detail = $"{log_detail}       Fin CTI Personal con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg. <br/>";
            log_detail = $"{log_detail}<br/>";

            Log.Info($"Process {destinationTableName} End. Took: {watch.ElapsedMilliseconds / 1000} secs. ");

            if (result == "OK")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// # 06 - CPCentrosCoste --> CTI_tb_CentrosCoste
        /// </summary>
        /// <param name="sourceTableName"></param>
        /// <param name="destinationTableName"></param>
        /// <param name="maxTs"></param>
        /// <param name="context"></param>
        /// <param name="log_detail"></param>
        /// <returns>True.Error / False.OK</returns>
        private static bool ProcessCentrosCoste(string sourceTableName, string destinationTableName, string maxTs, DbContext context, ref string log_detail)
        {
            // Log Description
            log_detail = $"{log_detail}# 06 - CTI Centros Coste <br/>";

            var watch = Stopwatch.StartNew();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = "# 06 - Inicio actualización CTI Centros Coste", Fichero = FICHERO, SidEmpresa = IdEmpresa });
            string result = string.Empty;
            List<string> sqlCommands = new List<string>();

            try
            {
                List<CPCentrosCoste> sourceEntities = context.Database.SqlQuery<CPCentrosCoste>($"SELECT * FROM  {sourceTableName} WHERE (Ts >{maxTs}) AND ( GETDATE() between FchAlta and ISNULL(fchbaja,GETDATE()))").ToList();
                List<CTI_tb_CentrosCoste> destinationEntities = context.Database.SqlQuery<CTI_tb_CentrosCoste>($"SELECT * FROM {destinationTableName} Where Estado <> 'D' OR Estado is null").ToList();

                List<CentrosCosteKeys> sourceKeys = sourceEntities.Select(o => new CentrosCosteKeys() { SidEmpresa = o.SidEmpresa, SidCliente = o.SidCliente, SidCentroCoste = o.SidCentroCoste }).ToList();
                List<CentrosCosteKeys> destinationKeys = destinationEntities.Select(o => new CentrosCosteKeys() { SidEmpresa = o.SidEmpresa, SidCliente = o.SidCliente, SidCentroCoste = o.SidCentroCoste }).ToList();

                // Modified
                var toUpdate = destinationKeys.Intersect(sourceKeys, new CentrosCosteComparer()).ToList();
                if (toUpdate.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateSql(toUpdate, sourceEntities, destinationTableName, context));

                // New
                var toInsert = sourceKeys.Except(destinationKeys, new CentrosCosteComparer()).ToList();
                if (toInsert.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateInsertSql<CPCentrosCoste, CentrosCosteKeys>(toInsert, sourceEntities, destinationTableName, context));

                //Deleted
                var toDeleteKeys = context.Database.SqlQuery<CentrosCosteKeys>($"SELECT * FROM  {sourceTableName}Hco WHERE TipoUD ='D' ").ToList();
                var toDelete = destinationKeys.Intersect(toDeleteKeys, new CentrosCosteComparer()).ToList();
                if (toDelete.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateDeletedSql(toDelete, destinationTableName, context));

                result = DataBaseUtilities.SendSqlCommands(destinationTableName, context, sqlCommands) ? "OK" : "ERROR";
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"Error en actualizacion de  {destinationTableName}", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = ERROR });

                // Log Description
                log_detail = $"{log_detail}Error en actualizacion de {destinationTableName}. <br/>";
                log_detail = $"{log_detail}{ex.Message} <br/>";
                log_detail = $"{log_detail}<br/>";

                return true;
            }

            watch.Stop();
            Log.Info($"Process {destinationTableName} End. Took: {watch.ElapsedMilliseconds / 1000} secs. ");

            // Log Description
            log_detail = $"{log_detail}       Fin CTI Centros Coste con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg. <br/>";
            log_detail = $"{log_detail}<br/>";

            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"# 06 - Fin actualización CTI {destinationTableName} con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg.", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = NO_ERROR });

            if (result == "OK")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// # 07 - CPServicio --> CTI_tb_Servicio
        /// </summary>
        /// <param name="sourceTableName"></param>
        /// <param name="destinationTableName"></param>
        /// <param name="maxTs"></param>
        /// <param name="context"></param>
        /// <param name="log_detail"></param>
        /// <returns>True.Error / False.OK</returns>
        private static bool ProcessServicios(string sourceTableName, string destinationTableName, string maxTs, DbContext context, ref string log_detail)
        {
            // Log Description
            log_detail = $"{log_detail}# 07 - CTI Servicio <br/>";

            var watch = Stopwatch.StartNew();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = "# 07 - Inicio actualización CTI Servicio", Fichero = FICHERO, SidEmpresa = IdEmpresa });

            string result = string.Empty;
            List<string> sqlCommands = new List<string>();

            try
            {
                List<CPServicio> sourceEntities = context.Database.SqlQuery<CPServicio>($"SELECT * FROM  {sourceTableName} s " +
                                                                                $" INNER JOIN [dbo].[CPCliente] cl on s.SidEmpresa=cl.SidEmpresa and s.SidCliente=cl.Sidcliente" +
                                                                                $" INNER JOIN [dbo].CPContrato ct on ct.SidEmpresa=s.SidEmpresa and ct.SidCliente=s.Sidcliente and ct.SidContrato=s.SidContrato" +
                                                                                $" INNER JOIN [dbo].CPCentrosCoste cc on cc.SidEmpresa=s.SidEmpresa and cc.SidCliente=s.Sidcliente and cc.SidCentroCoste=s.SidCentroCoste" +
                                                                                $" WHERE s.Ts >{maxTs} AND" +
                                                                                $" GETDATE() between s.FchAlta and ISNULL(s.fchbaja,GETDATE()) AND " +
                                                                                $" GETDATE() between ct.FchAlta and ISNULL(ct.fchbaja,GETDATE()) AND" +
                                                                                $" cl.SidEmpresa<>'' and cl.SidCliente<>'999999' AND" +
                                                                                $" GETDATE() between cl.FchAlta and ISNULL(cl.FchBaja,GETDATE()) AND" +
                                                                                $" GETDATE() between cc.FchAlta and ISNULL(cc.fchbaja,GETDATE())").ToList();


                List<CTI_tb_Servicio> destinationEntities = context.Database.SqlQuery<CTI_tb_Servicio>($"SELECT * FROM {destinationTableName} Where Estado <> 'D' OR Estado is null").ToList();

                List<ServicioKeys> sourceKeys = sourceEntities.Select(o => new ServicioKeys() { SidEmpresa = o.SidEmpresa, SidCliente = o.SidCliente, SidServicio = o.SidServicio }).ToList();
                List<ServicioKeys> destinationKeys = destinationEntities.Select(o => new ServicioKeys() { SidEmpresa = o.SidEmpresa, SidCliente = o.SidCliente, SidServicio = o.SidServicio }).ToList();

                // Modified
                var toUpdate = destinationKeys.Intersect(sourceKeys, new ServicioComparer()).ToList();
                if (toUpdate.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateSql(toUpdate, sourceEntities, destinationTableName, context));

                // New
                var toInsert = sourceKeys.Except(destinationKeys, new ServicioComparer()).ToList();
                if (toInsert.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateInsertSql<CPServicio, ServicioKeys>(toInsert, sourceEntities, destinationTableName, context));

                //Deleted
                var toDeleteKeys = context.Database.SqlQuery<ServicioKeys>($"SELECT * FROM  {sourceTableName}Hco WHERE Tipo='D' ").ToList();
                var toDelete = destinationKeys.Intersect(toDeleteKeys, new ServicioComparer()).ToList();
                if (toDelete.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateDeletedSql(toDelete, destinationTableName, context));


                result = DataBaseUtilities.SendSqlCommands(destinationTableName, context, sqlCommands) ? "OK" : "ERROR";
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"Error en actualizacion de  {destinationTableName}", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = ERROR });

                // Log Description
                log_detail = $"{log_detail}Error en actualizacion de {destinationTableName}. <br/>";
                log_detail = $"{log_detail}{ex.Message} <br/>";
                log_detail = $"{log_detail}<br/>";

                return true;
            }

            watch.Stop();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"# 07 - Fin actualización CTI {destinationTableName} con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg.", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = NO_ERROR });

            // Log Description
            log_detail = $"{log_detail}       Fin CTI Servicio con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg. <br/>";
            log_detail = $"{log_detail}<br/>";

            Log.Info($"Process {destinationTableName} End. Took: {watch.ElapsedMilliseconds / 1000} secs. ");

            if (result == "OK")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// # 08 - CPServicioDelegacion --> CTI_tb_ServicioDelegacion
        /// </summary>
        /// <param name="sourceTableName"></param>
        /// <param name="destinationTableName"></param>
        /// <param name="maxTs"></param>
        /// <param name="context"></param>
        /// <param name="log_detail"></param>
        /// <returns>True.Error / False.OK</returns>
        private static bool ProcessServicioDelegacion(string sourceTableName, string destinationTableName, string maxTs, DbContext context, ref string log_detail)
        {
            // Log Description
            log_detail = $"{log_detail}# 08 - CTI Servicio-Delegacion <br/>";

            var watch = Stopwatch.StartNew();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = "# 08 - Inicio actualización CTI Servicio-Delegacion", Fichero = FICHERO, SidEmpresa = IdEmpresa });

            string result = string.Empty;
            List<string> sqlCommands = new List<string>();

            try
            {
                List<CPServicioDelegacion> sourceEntities = context.Database.SqlQuery<CPServicioDelegacion>($"SELECT * FROM  {sourceTableName} sd " +
                                                                                                    $" INNER JOIN CPDelegacion d ON sd.SidDelegacion = d.SidDelegacion and sd.SidEmpresa=d.SidEmpresa" +
                                                                                                    $" INNER JOIN CPServicio s on s.SidEmpresa=sd.SidEmpresa and s.SidCliente= sd.SidCliente and s.SidServicio=sd.sidservicio" +
                                                                                                    $" INNER JOIN [dbo].[CPCliente] cl on s.SidEmpresa=cl.SidEmpresa and s.SidCliente=cl.Sidcliente" +
                                                                                                    $" INNER JOIN [dbo].CPContrato ct on ct.SidEmpresa=s.SidEmpresa and ct.SidCliente=s.Sidcliente and ct.SidContrato=s.SidContrato" +
                                                                                                    $" INNER JOIN [dbo].CPCentrosCoste cc on cc.SidEmpresa=s.SidEmpresa and cc.SidCliente=s.Sidcliente and cc.SidCentroCoste=s.SidCentroCoste" +
                                                                                                    $" WHERE GETDATE() between s.FchAlta and ISNULL(s.FchBaja,GETDATE()) " +
                                                                                                    $" AND cl.SidEmpresa<>'' and cl.SidCliente<>'999999' and GETDATE() between cl.FchAlta and ISNULL(cl.FchBaja,GETDATE()) " +
                                                                                                    $" AND  GETDATE() between cc.FchAlta and ISNULL(cc.fchbaja,GETDATE())" +
                                                                                                    $" AND sd.Ts >{maxTs}").ToList();

                List<CTI_tb_ServicioDelegacion> destinationEntities = context.Database.SqlQuery<CTI_tb_ServicioDelegacion>($"SELECT * FROM {destinationTableName} WHERE Estado <> 'D' OR Estado is null").ToList();

                List<ServicioDelegacionKeys> sourceKeys = sourceEntities.Select(o => new ServicioDelegacionKeys() { Id = o.Id }).ToList();
                List<ServicioDelegacionKeys> destinationKeys = destinationEntities.Select(o => new ServicioDelegacionKeys() { Id = o.Id }).ToList();

                // Modified
                var toUpdate = destinationKeys.Intersect(sourceKeys, new ServicioDelegacionComparer()).ToList();
                if (toUpdate.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateSql(toUpdate, sourceEntities, destinationTableName, context));

                // New
                var toInsert = sourceKeys.Except(destinationKeys, new ServicioDelegacionComparer()).ToList();
                if (toInsert.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateInsertSql<CPServicioDelegacion, ServicioDelegacionKeys>(toInsert, sourceEntities, destinationTableName, context));

                //Deleted
                var toDeleteKeys = context.Database.SqlQuery<ServicioDelegacionKeys>($"SELECT * FROM  {sourceTableName}Hco WHERE TipoUD='D' ").ToList();
                var toDelete = destinationKeys.Intersect(toDeleteKeys, new ServicioDelegacionComparer()).ToList();
                if (toDelete.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateDeletedSql(toDelete, destinationTableName, context));

                result = DataBaseUtilities.SendSqlCommands(destinationTableName, context, sqlCommands) ? "OK" : "ERROR";
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"Error en actualizacion de  {destinationTableName}", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = ERROR });

                // Log Description
                log_detail = $"{log_detail}Error en actualizacion de {destinationTableName}. <br/>";
                log_detail = $"{log_detail}{ex.Message} <br/>";
                log_detail = $"{log_detail}<br/>";

                return true;
            }

            watch.Stop();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"# 08 - Fin actualización CTI {destinationTableName} con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg.", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = NO_ERROR });

            // Log Description
            log_detail = $"{log_detail}       Fin CTI Servicio-Delegacion con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg. <br/>";
            log_detail = $"{log_detail}<br/>";

            Log.Info($"Process {destinationTableName} End. Took: {watch.ElapsedMilliseconds / 1000} secs. ");

            if (result == "OK")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// # 09 - CPServicioPersonal --> CTI_tb_ServicioPersonal
        /// </summary>
        /// <param name="sourceTableName"></param>
        /// <param name="destinationTableName"></param>
        /// <param name="maxTs"></param>
        /// <param name="context"></param>
        /// <param name="log_detail"></param>
        /// <returns>True.Error / False.OK</returns>
        private static bool ProcessServicioPersonal(string sourceTableName, string destinationTableName, string maxTs, DbContext context, ref string log_detail)
        {
            // Log Description
            log_detail = $"{log_detail}# 09 - CTI Servicio-Personal <br/>";

            var watch = Stopwatch.StartNew();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = "# 09 - Inicio actualización CTI Servicio-Personal", Fichero = FICHERO, SidEmpresa = IdEmpresa });

            string result = string.Empty;
            List<string> sqlCommands = new List<string>();

            try
            {
                List<CPServicioPersonal> sourceEntities = context.Database.SqlQuery<CPServicioPersonal>($"SELECT * FROM  {sourceTableName} cp" +
                                                                                                        $" INNER JOIN CPServicio s on cp.[SidEmpresa]=s.[SidEmpresa] AND cp.[SidCliente]=s.[SidCliente] AND cp.[SidServicio]=s.[SidServicio]" +
                                                                                                        $" INNER JOIN [dbo].[CPCliente] cl on s.SidEmpresa=cl.SidEmpresa and s.SidCliente=cl.Sidcliente" +
                                                                                                        $" INNER JOIN [dbo].CPContrato ct on ct.SidEmpresa=s.SidEmpresa and ct.SidCliente=s.Sidcliente and ct.SidContrato=s.SidContrato" +
                                                                                                        $" INNER JOIN [dbo].CPCentrosCoste cc on cc.SidEmpresa=s.SidEmpresa and cc.SidCliente=s.Sidcliente and cc.SidCentroCoste=s.SidCentroCoste" +
                                                                                                        $" WHERE GETDATE() between s.FchAlta and ISNULL(s.fchbaja,GETDATE()) " +
                                                                                                        $" AND cl.SidEmpresa<>'' and cl.SidCliente<>'999999' and GETDATE() between cl.FchAlta and ISNULL(cl.FchBaja,GETDATE()) " +
                                                                                                        $" AND GETDATE() between ct.FchAlta and ISNULL(ct.fchbaja,GETDATE()) " +
                                                                                                        $" AND GETDATE() between cc.FchAlta and ISNULL(cc.fchbaja,GETDATE())" +
                                                                                                        $" AND cp.Ts >{maxTs}").ToList();
                List<CTI_tb_ServicioPersonal> destinationEntities = context.Database.SqlQuery<CTI_tb_ServicioPersonal>($"SELECT * FROM {destinationTableName} Where Estado <> 'D' OR Estado is null").ToList();

                List<ServicioPersonalKeys> sourceKeys = sourceEntities.Select(o => new ServicioPersonalKeys()
                {
                    Fchfin = o.Fchfin,
                    FchInicio = o.FchInicio,
                    IdContrato = o.IdContrato,
                    NidMatricula = o.NidMatricula,
                    SecuencialContrato = o.SecuencialContrato,
                    SidCliente = o.SidCliente,
                    SidEmpresa = o.SidEmpresa,
                    SidServicio = o.SidServicio
                }).ToList();

                List<ServicioPersonalKeys> destinationKeys = destinationEntities.Select(o => new ServicioPersonalKeys()
                {
                    Fchfin = o.Fchfin,
                    FchInicio = o.FchInicio,
                    IdContrato = o.IdContrato,
                    NidMatricula = o.NidMatricula,
                    SecuencialContrato = o.SecuencialContrato,
                    SidCliente = o.SidCliente,
                    SidEmpresa = o.SidEmpresa,
                    SidServicio = o.SidServicio
                }).ToList();

                // Modified
                var toUpdate = destinationKeys.Intersect(sourceKeys, new ServicioPersonalComparer()).ToList();
                if (toUpdate.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateSql(toUpdate, sourceEntities, destinationTableName, context));

                // New
                var toInsert = sourceKeys.Except(destinationKeys, new ServicioPersonalComparer()).ToList();
                if (toInsert.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateInsertSql<CPServicioPersonal, ServicioPersonalKeys>(toInsert, sourceEntities, destinationTableName, context));

                //Deleted
                var toDeleteKeys = context.Database.SqlQuery<ServicioPersonalKeys>($"SELECT * FROM  {sourceTableName}Hco WHERE Tipo='D' ").ToList();
                var toDelete = destinationKeys.Intersect(toDeleteKeys, new ServicioPersonalComparer()).ToList();
                if (toDelete.Any()) sqlCommands.AddRange(DataBaseUtilities.GenerateUpdateDeletedSql(toDelete, destinationTableName, context));

                result = DataBaseUtilities.SendSqlCommands(destinationTableName, context, sqlCommands) ? "OK" : "ERROR";
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"Error en actualizacion de  {destinationTableName}", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = ERROR });

                // Log Description
                log_detail = $"{log_detail}Error en actualizacion de {destinationTableName}. <br/>";
                log_detail = $"{log_detail}{ex.Message} <br/>";
                log_detail = $"{log_detail}<br/>";

                return true;
            }

            watch.Stop();
            logImportacionService.Add(new CPLogImportacion() { Fecha = DateTime.Now, Comentario = $"# 09 - Fin actualización CTI {destinationTableName} con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg.", Fichero = FICHERO, SidEmpresa = IdEmpresa, CodError = NO_ERROR });

            // Log Description
            log_detail = $"{log_detail}       Fin CTI Servicio-Personal con {sqlCommands.Count} operaciones en DB y resultado : {result} en {watch.ElapsedMilliseconds / 1000} seg. <br/>";
            log_detail = $"{log_detail}<br/>";

            Log.Info($"Process {destinationTableName} End. Took: {watch.ElapsedMilliseconds / 1000} secs. ");

            if (result == "OK")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
               
        #endregion

        #region methods others

        private static string GetIdEmpresa(DbContext context)
        {

            CPEmpresaService empresaService = new CPEmpresaService(new DbRepository(context));
            List<CPEmpresa> empresas = empresaService.GetAllActive().ToList();
            return empresas.Any() ? empresas.FirstOrDefault().SidEmpresa : string.Empty;

        }

        private CPApiProcesosLanzados GetIdProceso(DbContext context)
        {
            CPApiProcesosLanzados entity = new CPApiProcesosLanzados();
            entity.FechaLanzamiento = DateTime.Now;
            entity.Mensaje = "Proceso Exportacion CTI";
            entity.Metodo = 2;

            CPApiProcesosLanzadosService cPApiProcesosLanzadosService = new CPApiProcesosLanzadosService(new DbRepository(context));
            return cPApiProcesosLanzadosService.GetNewProcessId(entity);
        }

        private void UpdateProccess(CPApiProcesosLanzados proceso, DbContext context)
        {
            CPApiProcesosLanzados entity = new CPApiProcesosLanzados();
            entity.FechaLanzamiento = proceso.FechaLanzamiento;
            entity.Estado = 2;
            entity.Mensaje = "Proceso finalizado";
            entity.Metodo = proceso.Metodo;
            CPApiProcesosLanzadosService cPApiProcesosLanzadosService = new CPApiProcesosLanzadosService(new DbRepository(context));
            cPApiProcesosLanzadosService.Add(entity);

        }

        #endregion
    }
}
