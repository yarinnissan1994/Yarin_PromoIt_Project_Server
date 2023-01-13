using PromoItServer.DataSql;
using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities
{
    public class UsersManager
    {
        public DataTable getUserInfoFromDB(string Email, string Role)
        {
            UsersQueries usersQ = new UsersQueries();
            return usersQ.GetUserInfoQuery(Email, Role);
        }
        public bool? getPenddingFromDB(string email)
        {
            UsersQueries usersQ = new UsersQueries();
            return usersQ.getPenddingQuery(email);
        }
        public void SendUserToDB(object data)
        {
            UsersQueries usersQ = new UsersQueries();
            usersQ.ConvertUserToSqlQuery(data);
        }
        public DataTable GetPenddingListFromDB()
        {
            UsersQueries usersQ = new UsersQueries();
            return usersQ.GetPenddingListQuery();
        }
        public void ApproveUserInDB(string userCode)
        {
            UsersQueries usersQ = new UsersQueries();
            usersQ.ApproveUserQuery(userCode);
        }
        public void SendUserMessageToDB(UserMessage data)
        {
            UsersQueries usersQ = new UsersQueries();
            usersQ.SendUserMessageQuery(data);
        }
        public void SendMoneyStatusToDB(decimal moneyStatus, int SACode)
        {
            UsersQueries usersQ = new UsersQueries();
            usersQ.SendMoneyStatusQuery(moneyStatus, SACode);
        }
    }
}
