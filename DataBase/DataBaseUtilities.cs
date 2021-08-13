using DataAccessLayer.DbUtils;
using DataAccessLayer.DbUtils.SchemaEntities;
using TaskIlu14Cti.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using log4net;
using System.Reflection;
using System.Diagnostics;

namespace TaskIlu14Cti.DataBase
{
    public static class DataBaseUtilities
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static List<string> GenerateInsertSql<TEntity, TKeys>(List<TKeys> ToInsertKeys, List<TEntity> sourceEntities, string destinationTableName, DbContext context) where TEntity : class where TKeys : class
        {

            List<DBSchemaColumns> fields = SqlQueryUtils.GetTableFields(destinationTableName, context).Where(o => o.Data_Type != "timestamp").ToList();
            List<string> keyFields = SqlQueryUtils.GetPrimaryKeys(destinationTableName, context);

            List<string> insertCommands = new List<string>();
            

            foreach (var entity in sourceEntities)
            {
                List<string> whereNotExists = new List<string>();

                string sql = $"INSERT INTO {destinationTableName} ";
                sql += $"({string.Join(",", fields.Select(o => o.Column_Name).ToArray())}) VALUES ";

                List<string> values = new List<string>();
                foreach (DBSchemaColumns f in fields)
                {
                    string fieldValue = ExtractFieldValue(entity, f);

                    if (f.Column_Name == "Estado")
                        fieldValue = $"'N'";
                    values.Add(fieldValue);
                    if (keyFields.Contains(f.Column_Name))
                    {
                        whereNotExists.Add($"{f.Column_Name} = {fieldValue}");
                    }
                }
                sql += $"({string.Join(",", values.ToArray())})";


                string sqlIfNotExists = $"IF NOT EXISTS (SELECT * FROM {destinationTableName} WHERE ({string.Join(" AND ",whereNotExists.ToArray())}))";
                
                insertCommands.Add($"{sqlIfNotExists} {sql}");
            }

            return insertCommands;
        }

        public static List<string> GenerateUpdateSql<TEntity, TKeys>(List<TKeys> toupdateKeys, List<TEntity> sourceEntities, string destinationTableName, DbContext context, string status = "M") where TEntity : class where TKeys : class
        {
            List<string> keyFields = typeof(TKeys).GetProperties().Select(o => o.Name).ToList();
            List<DBSchemaColumns> fields = SqlQueryUtils.GetTableFields(destinationTableName, context).Where(o => o.Data_Type != "timestamp").ToList();

            List<string> updateCommands = new List<string>();

            foreach (var entity in sourceEntities)
            {
                string sql = $"UPDATE {destinationTableName} SET ";
                List<string> sqlSets = new List<string>();
                List<string> conditions = new List<string>();

                foreach (DBSchemaColumns f in fields)
                {
                    string fieldValue = ExtractFieldValue(entity, f);

                    if (f.Column_Name == "Estado")
                        fieldValue = $"'{status}'";


                    if (keyFields.Contains(f.Column_Name))
                    {
                        string condition = $"{f.Column_Name} = {fieldValue}";
                        conditions.Add(condition);
                        continue;
                    }

                    string sqlSet = $"{f.Column_Name} = {fieldValue}";
                    sqlSets.Add(sqlSet);
                }

                sql += string.Join(" , ", sqlSets.ToArray());
                sql += $" WHERE ";

                sql += $"({string.Join(" AND ", conditions.ToArray())})";

                updateCommands.Add(sql);
            }

            return updateCommands;
        }

        private static string ExtractFieldValue<TEntity>(TEntity entitiy, DBSchemaColumns f) where TEntity : class
        {
            object value = EntitiesUtils.GetPropValue(entitiy, f.Column_Name);
            string fieldValue = string.Empty;

            if (value == null)
                fieldValue = "Null";
            else if (value is string)
                fieldValue = $"'{value.ToString()}'";
            else if (value is DateTime)
                fieldValue = $"'{((DateTime)value).ToString()}'";
            else if (value is decimal)
            {
                fieldValue = $"{value.ToString().Replace(',','.')}";
            }
            else if (value is Boolean)
            {
                int val = (bool)value ? 1 : 0;
                fieldValue = $"{val}";
            }

            else
                fieldValue = value.ToString();
            return fieldValue;
        }

        public static List<string> GenerateUpdateDeletedSql<TKeys>(List<TKeys> toupdateKeys, string destinationTableName, DbContext context) where TKeys : class
        {
            List<string> updateCommands = new List<string>();

            foreach (var entity in toupdateKeys)
            {
                string sql = $"UPDATE {destinationTableName} SET ESTADO ='D' WHERE ";
                List<string> whereSql = new List<string>();
                foreach (var property in entity.GetType().GetProperties())
                {
                    whereSql.Add($"({property.Name} = {EntitiesUtils.GetPropValueSqlString(entity, property.Name)})");
                }

                sql += $" {string.Join(" AND ", whereSql.ToArray())}";
                updateCommands.Add(sql);
            }

            return updateCommands;
        }

        public static bool ExecuteQuerys(List<string> sqlCommands, DbContext context, bool forceRollBack = false)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                string actualCommand = string.Empty;
                try
                {
                    foreach (string command in sqlCommands)
                    {
                        actualCommand = command;
                        Log.Debug(actualCommand);
                        int result = context.Database.ExecuteSqlCommand(command);
                        context.SaveChanges();
                        Log.Debug($"Command: {actualCommand}");
                        Log.Debug($"{result} rows affected");
                    }
                    if (!forceRollBack)
                        dbContextTransaction.Commit();
                    else dbContextTransaction.Rollback();
                    return true;
                }
                catch (Exception e)
                {

                    dbContextTransaction.Rollback();
                    Log.Error($"{actualCommand}",e);
                    return false;
                }
            }
        }

        public static bool SendSqlCommands(string destinationTableName, DbContext context, List<string> sqlCommands)
        {
            bool result = false;

            var watch = Stopwatch.StartNew();

            if (sqlCommands.Any())
            {
                if (DataBaseUtilities.ExecuteQuerys(sqlCommands, context))
                {
                    Log.Info($"Update for {destinationTableName} Ok.");
                    result = true;
                }
            }
            else
            {
                Log.Info($"No updates for {destinationTableName} Ok.");
                result = true;
            }

            Log.Info($"Send {sqlCommands.Count} commands takes {watch.ElapsedMilliseconds / 1000} secs.");
            return result;
        }
    }
}
