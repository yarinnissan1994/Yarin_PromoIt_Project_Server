using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using Utilities_CS;

namespace PromoItServer.DAL
{
    public class SqlFunctions
    {
        public static Log Log;
        public static string connectionString;
        public static void LogInit(Log Logger)
        {
            Log = Logger;
        }
        public static void ConnectionStringInit(string ConnectionString)
        {
            Log.LogEvent("ConnectionStringInit function was called");
            connectionString = ConnectionString;
        }

        public delegate object SetDataReader_delegate(SqlDataReader reader);
        public static object ReadFromDB(string SqlQuery, SetDataReader_delegate Ptrfunc)
        {
            Log.LogEvent("ReadFromDB function was called");
            object retHash = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(SqlQuery, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            retHash = Ptrfunc(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Log.LogException("An error occurred on ReadFromDB function: ", ex);
            }

            return retHash;
        }
        public static int WriteToDB(string SqlQuery)
        {
            Log.LogEvent("WriteToDB function was called");
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(SqlQuery, connection))
                    {
                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Log.LogException("An error occurred on WriteToDB function: ", ex);
            }

            return rowsAffected;
        }
        public static void WriteWithValuesToDB(string query, Dictionary<string, object> parameters)
        {
            Log.LogEvent("WriteWithValuesToDB function was called");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        foreach (KeyValuePair<string, object> param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Log.LogException("An error occurred on WriteWithValuesToDB function: ", ex);
            }
        }
        public static object ReadScalarFromDB(string SqlQuery)
        {
            Log.LogEvent("ReadScalarFromDB function was called");
            object answer = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(SqlQuery, connection))
                    {
                        connection.Open();

                        answer = command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                Log.LogException("An error occurred on ReadScalarFromDB function: ", ex);
            }
            return answer;
        }
        public static DataTable ReadTableFromDB(string sqlQuery)
        {
            Log.LogEvent("ReadTableFromDB function was called");
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connectionString);

                DataTable table = new DataTable();

                adapter.Fill(table);

                return table;
            }
            catch (SqlException ex)
            {
                Log.LogException("An error occurred on ReadTableFromDB function: ", ex);
            }
            return null;
        }

    }
}
