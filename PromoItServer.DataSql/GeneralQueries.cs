using PromoItServer.model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Utilities_CS;

namespace PromoItServer.DataSql
{
    public class GeneralQueries : BaseDataSql
    {
        public GeneralQueries(Log Logger, ConfigClass Config) : base(Logger) { ConfigPtr = Config; }

        ConfigClass ConfigPtr { get; set; }
        public void LogInit()
        {
            PromoItServer.DAL.SqlFunctions.LogInit(Log);
        }
        public void ConnectionInit()
        {
            Log.LogEvent("ConnectionInit function was called");
            PromoItServer.DAL.SqlFunctions.ConnectionStringInit("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=PromoIt;Data Source=localhost\\sqlexpress");
        }

        public void ConfigInit()
        {
            Log.LogEvent("ConfigInit function was called");
            ConfigPtr.Auth0BearerCode = (string)DAL.SqlFunctions.ReadScalarFromDB("select Value from Configuration_Table where Name like 'Auth0BearerCode'");
            ConfigPtr.TwitterBearerCode = (string)DAL.SqlFunctions.ReadScalarFromDB("select Value from Configuration_Table where Name like 'TwitterBearerCode'");
            ConfigPtr.ConsumerKey = (string)DAL.SqlFunctions.ReadScalarFromDB("select Value from Configuration_Table where Name like 'ConsumerKey'");
            ConfigPtr.ConsumerKeySecret = (string)DAL.SqlFunctions.ReadScalarFromDB("select Value from Configuration_Table where Name like 'ConsumerKeySecret'");
            ConfigPtr.AccessKey = (string)DAL.SqlFunctions.ReadScalarFromDB("select Value from Configuration_Table where Name like 'AccessKey'");
            ConfigPtr.AccessKeySecret = (string)DAL.SqlFunctions.ReadScalarFromDB("select Value from Configuration_Table where Name like 'AccessKeySecret'");
        }

    }
}

        //static ConfigClass ReadConfigFromDb(SqlDataReader reader)
        //{
        //    Log.LogEvent("ReadConfigFromDb function was called");
        //    ConfigClass config = new ConfigClass();
        //    config.Auth0BearerCode = reader.GetString(1);
        //    config.TwitterBearerCode = reader.GetString(2);
        //    config.ConsumerKey = reader.GetString(3);
        //    config.ConsumerKeySecret = reader.GetString(4);
        //    config.AccessKey = reader.GetString(5);
        //    config.AccessKeySecret = reader.GetString(6);
        //    return config;
        //}
        //public static ConfigClass GetConfigQuery()
        //{
        //    Log.LogEvent("GetConfigQuery function was called");
        //    string SqlQuery = "select * from Configuration_Table";
        //    ConfigClass config = (ConfigClass)PromoItServer.DAL.SqlFunctions.ReadFromDB(SqlQuery, ReadConfigFromDb);
        //    return config;
        //}