using app_ingenieria_ufinet.Data;
using System.Data.Common;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace app_ingenieria_ufinet.Utils
{
    public interface IDatabaseUtils
    {
        List<T> ExecuteStoredProc<T>(string storedProcedureName, Dictionary<string, object> procParams) where T : class;
    }

    public class DatabaseUtils : IDatabaseUtils
    {
        private readonly DataContext _context;

        public DatabaseUtils(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Execute procedure from database using it's name and params that is protected from the SQL injection attacks.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedureName">Name of the procedure that should be executed.</param>
        /// <param name="procParams">Dictionary of params that procedure takes. </param>
        /// <returns>List of objects that are mapped in T type, returned by procedure.</returns>
        public List<T> ExecuteStoredProc<T>(string storedProcedureName, Dictionary<string, object> procParams) where T : class
        {
            DbConnection conn = this._context.Database.GetDbConnection();
            try
            {
                if (conn.State != ConnectionState.Open)
                conn.Open();

                using (DbCommand command = conn.CreateCommand())
                {
                    command.CommandText = storedProcedureName;
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (KeyValuePair<string, object> procParam in procParams)
                    {
                        DbParameter param = command.CreateParameter();
                        param.ParameterName = procParam.Key;
                        param.Value = procParam.Value;
                        command.Parameters.Add(param);
                    }

                    DbDataReader reader = command.ExecuteReader();
                    List<T> objList = new List<T>();
                    IEnumerable<PropertyInfo> props = typeof(T).GetRuntimeProperties();
                    Dictionary<string, DbColumn> colMapping = reader.GetColumnSchema()
                        .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
                        .ToDictionary(key => key.ColumnName.ToLower());

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            T obj = Activator.CreateInstance<T>();
                            foreach (PropertyInfo prop in props)
                            {
                                object val =
                                    reader.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                                prop.SetValue(obj, val == DBNull.Value ? null : val);
                            }
                            objList.Add(obj);
                        }
                    }
                    reader.Dispose();

                    return objList;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message, e.InnerException);
            }
            finally
            {
                conn.Close();
            }

            return null; // default state
        }
    }
}
