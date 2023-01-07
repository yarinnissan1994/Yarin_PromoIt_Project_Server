using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.DAL
{
    public class SqlFunctions
    {
        public static string connectionString;
        public static void ConnectionStringInit(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        public delegate object SetDataReader_delegate(SqlDataReader reader);
        public static object ReadFromDB(string SqlQuery, SetDataReader_delegate Ptrfunc)
        {
            object retHash = null;

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
            return retHash;
        }
        public static int WriteToDB(string SqlQuery)
        {
            int rowsAffected;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(SqlQuery, connection))
                {
                    connection.Open();

                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }
        public static void WriteWithValuesToDB(string query, Dictionary<string, object> parameters)
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
        public static object ReadScalarFromDB(string SqlQuery)
        {
            object answer;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(SqlQuery, connection))
                {
                    connection.Open();

                    answer = command.ExecuteScalar();
                }
            }
            return answer;
        }
        public static DataTable ReadTableFromDB(string sqlQuery)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connectionString);

            DataTable table = new DataTable();

            adapter.Fill(table);

            return table;
        }
    }
}
