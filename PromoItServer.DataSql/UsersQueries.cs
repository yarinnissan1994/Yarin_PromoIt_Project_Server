using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.DataSql
{
    public class UsersQueries
    {
        public bool? getPenddingQuery(string email)
        {
            string isAprovedQuery = "select Is_Aproved from Register_Applications where Email like '" + email + "'";
            bool? isPendding = (bool?)PromoItServer.DAL.SqlFunctions.ReadScalarFromDB(isAprovedQuery);
            return isPendding;
        }
        public void ConvertUserToSqlQuery(object data)
        {
            if (data is NonProfitOrganization)
            {
                NonProfitOrganization NPOUser = (NonProfitOrganization)data;
                string registerQuery = "insert into Register_Applications values ('" + NPOUser.Name + "', '" + NPOUser.Email + "', 'NPO', 0)";
                PromoItServer.DAL.SqlFunctions.WriteToDB(registerQuery);

                string addToNPOQuery = "insert into Non_Profit_Organizations values ('" + NPOUser.Name + "', '" + NPOUser.Email + "', '" + NPOUser.WebsiteURL + "', '" + NPOUser.MyImage + "')";
                PromoItServer.DAL.SqlFunctions.WriteToDB(addToNPOQuery);
            }
            else if (data is BuisnessCompany)
            {
                BuisnessCompany BCUser = (BuisnessCompany)data;
                string registerQuery = "insert into Register_Applications values ('" + BCUser.Name + "', '" + BCUser.Email + "', 'BC', 0)";
                PromoItServer.DAL.SqlFunctions.WriteToDB(registerQuery);

                string addToBCQuery = "insert into Buisness_Companies values ('" + BCUser.Name + "', '" + BCUser.Email + "', '" + BCUser.MyImage + "')";
                PromoItServer.DAL.SqlFunctions.WriteToDB(addToBCQuery);
            }
            else
            {
                SocialActivist SAUser = (SocialActivist)data;
                string registerQuery = "insert into Register_Applications values ('" + SAUser.Name + "', '" + SAUser.Email + "', 'SA', 0)";
                PromoItServer.DAL.SqlFunctions.WriteToDB(registerQuery);

                string addToBCQuery = "insert into Social_Activist values ('" + SAUser.Name + "', '" + SAUser.Email + "', '" + SAUser.Address + "', '" + SAUser.PhoneNumber + "', @MoneyStatus, '" + SAUser.MyImage + "')";
                decimal money = 0;
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@MoneyStatus", money},
                };
                PromoItServer.DAL.SqlFunctions.WriteWithValuesToDB(addToBCQuery, parameters);
            }
        }
        public DataTable GetPenddingListQuery()
        {
            string penddingListQuery = "select * from Register_Applications where Is_Aproved = 0";
            return PromoItServer.DAL.SqlFunctions.ReadTableFromDB(penddingListQuery);
        }
        public void ApproveUserQuery(string userCode)
        {
            string updateQuery = "update Register_Applications set Is_Aproved = 1 where Code = '" + userCode + "'";
            PromoItServer.DAL.SqlFunctions.WriteToDB(updateQuery);
        }
        public void SendUserMessageQuery(UserMessage data)
        {
            string sqlQuery = "insert into Contact_Us values('" + data.Name + "','" + data.Message + "','" + data.Phone + "','" + data.Email + "',getdate())";
            PromoItServer.DAL.SqlFunctions.WriteToDB(sqlQuery);
        }
    }
}
