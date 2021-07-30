using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace L.Util
{
    public static class DbOperator
    {
        static readonly Type EnumType = typeof(System.Enum);
        private static string ConnectionString { get; set; }//= ConfigurationManager.AppSettings["DbConnectionString"];
        private static Type TypeOfConnection { get; set; }
        private static Type TypeOfCommand { get; set; }
        private static Type TypeOfParamter { get; set; }

        public static void Initialize(Type connectionType, Type commandType, Type paramterType,string connectionString)
        {
            TypeOfConnection = connectionType;
            TypeOfCommand = commandType;
            TypeOfParamter = paramterType;
            ConnectionString = connectionString;
        }

        private static DbConnection Connection
        {
            get
            {
                var connection = Activator.CreateInstance(TypeOfConnection,new []{ ConnectionString }) as DbConnection;
                connection.Open();
                return connection;
            }
        }

        public static DbCommand CreateCommand(string procedureName, IEnumerable<KeyValuePair<string, object>> paramters)
        {
            var command = Activator.CreateInstance(TypeOfCommand, new[] { ConnectionString }) as DbCommand;
            command.Connection = Connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;
            if (paramters == null) return command;
            foreach (var paramter in paramters)
            {
                var paramterCmd = Activator.CreateInstance(TypeOfParamter, new[] {paramter.Key, paramter.Value}) as DbParameter;
                command.Parameters.Add(paramterCmd);
            }
            return command;
        }

        public static object ExecuteScalarOnce(this DbCommand cmd)
        {
            using (cmd.Connection)
            {
                var ret = cmd.ExecuteScalar();
                return ret;
            }
        }

        public static DbDataReader ExecuteReaderOnce(this DbCommand cmd)
        {
            using (cmd.Connection)
            {
                var ret = cmd.ExecuteReader();
                return ret;
            }
        }

        public static int ExecuteNonQueryOnce(this DbCommand cmd)
        {
            using (cmd.Connection)
            {
                var ret = cmd.ExecuteNonQuery();
                return ret;
            }
        }

        public static List<T> ExecuteEntitiesOnce<T>(this DbCommand cmd) where T : new()
        {
            using (cmd.Connection)
            {
                var ret = cmd.ExecuteReader().GetEntities<T>();
                return ret;
            }
        }

        static List<T> GetEntities<T>(this DbDataReader reader) where T : new()
        {
            var listT = new List<T>();
            using (reader)
            {
                while (reader.Read())
                {
                    var inst = new T();
                    var properties = inst.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    foreach (var pi in properties)
                    {
                        try
                        {
                            var obj = reader[pi.Name];
                            if (obj == DBNull.Value || obj == null) continue;
                            var si = pi.GetSetMethod();
                            if (si == null) continue;
                            if (pi.PropertyType.IsSubclassOf(EnumType)) obj = Enum.Parse(pi.PropertyType, $"{obj}");
                            pi.SetValue(inst, obj, null);
                        }
                        catch{ }
                    }
                    listT.Add(inst);
                }
            }
            return listT;
        }
    }
}
