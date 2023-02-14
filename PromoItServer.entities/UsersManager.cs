using PromoItServer.DataSql;
using PromoItServer.model;
using System.Data;
using System.Threading.Tasks;
using Utilities_CS;

namespace PromoItServer.entities
{
    public class UsersManager : BaseEntity
    {
        public UsersManager(Log Logger) : base(Logger) { usersQ = new UsersQueries(Logger); }
        UsersQueries usersQ;
        public DataTable getUserInfoFromDB(string Email, string Role)
        {
            Log.LogEvent("getUserInfoFromDB function was called");
            return usersQ.GetUserInfoQuery(Email, Role);
        }
        public bool? getPenddingFromDB(string email)
        {
            Log.LogEvent("getPenddingFromDB function was called");
            return usersQ.getPenddingQuery(email);
        }
        public void SendUserToDB(object data)
        {
            Log.LogEvent("SendUserToDB function was called");
            usersQ.ConvertUserToSqlQuery(data);
        }
        public DataTable GetPenddingListFromDB()
        {
            Log.LogEvent("GetPenddingListFromDB function was called");
            return usersQ.GetPenddingListQuery();
        }
        public DataTable GetSADonationsFromDB(string SACode)
        {
            Log.LogEvent("GetSADonationsFromDB function was called");
            return usersQ.GetSADonationsQuery(SACode);
        }
        public void ApproveUserInDB(string userCode)
        {
            Log.LogEvent("ApproveUserInDB function was called");
            usersQ.ApproveUserQuery(userCode);
        }
        public void SendUserMessageToDB(UserMessage data)
        {
            Log.LogEvent("SendUserMessageToDB function was called");
            usersQ.SendUserMessageQuery(data);
        }
        public void SendMoneyStatusToDB(decimal moneyStatus, int SACode)
        {
            Log.LogEvent("SendMoneyStatusToDB function was called");
            usersQ.SendMoneyStatusQuery(moneyStatus, SACode);
        }
    }
}
