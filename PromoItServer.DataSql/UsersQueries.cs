using PromoItServer.model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Utilities_CS;

namespace PromoItServer.DataSql
{
    public class UsersQueries : BaseDataSql
    {
        public UsersQueries(Log Logger) : base(Logger) { }
        public DataTable GetUserInfoQuery(string Email, string Role)
        {
            Log.LogEvent("GetUserInfoQuery function was called");
            string penddingListQuery;
            if (Role == "NPO")
            {
                penddingListQuery = "select * from Non_Profit_Organizations where Email = '" + Email + "'";
            }
            else if(Role == "BC")
            {
                penddingListQuery = "select * from Buisness_Companies where Email = '" + Email + "'";
            }
            else
            {
                penddingListQuery = "select * from Social_Activist where Email = '" + Email + "'";
            }
            return PromoItServer.DAL.SqlFunctions.ReadTableFromDB(penddingListQuery);
        }
        public bool? getPenddingQuery(string email)
        {
            Log.LogEvent("getPenddingQuery function was called");
            string isAprovedQuery = "select Is_Aproved from Register_Applications where Email like '" + email + "'";
            bool? isPendding = (bool?)PromoItServer.DAL.SqlFunctions.ReadScalarFromDB(isAprovedQuery);
            return isPendding;
        }
        public void ConvertUserToSqlQuery(object data)
        {
            Log.LogEvent("ConvertUserToSqlQuery function was called");
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

                string addToBCQuery = "insert into Social_Activist values ('" + SAUser.Name + "', '" + SAUser.Email + "', '" + SAUser.Address + "', '" + SAUser.PhoneNumber + "', @MoneyStatus, '" + SAUser.MyImage + "', '" + SAUser.TwitterName + "')";
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
            Log.LogEvent("GetPenddingListQuery function was called");
            string penddingListQuery = "select * from Register_Applications where Is_Aproved = 0";
            return PromoItServer.DAL.SqlFunctions.ReadTableFromDB(penddingListQuery);
        }
        public DataTable GetSADonationsQuery(string SACode)
        {
            Log.LogEvent("GetSADonationsQuery function was called");
            string SADonationsQuery = "SELECT Products.Name as 'Product_Name', Products.Price as 'Price', COUNT(Orders.Code) as 'Donation_Count', \r\nProducts.Price*COUNT(Orders.Code) as 'Total_Price', Campaigns.Name as 'Campaign_Name'\r\nFROM Orders\r\nJOIN Products ON Products.Code = Orders.Product_code\r\nJOIN Campaigns ON Orders.Campaign_code = Campaigns.Code\r\nWHERE Orders.SA_code = "+ SACode + "\r\nGROUP BY Products.Name, Products.Price, Campaigns.Name;";
            return PromoItServer.DAL.SqlFunctions.ReadTableFromDB(SADonationsQuery);
        }
        public void ApproveUserQuery(string userCode)
        {
            Log.LogEvent("ApproveUserQuery function was called");
            string updateQuery = "update Register_Applications set Is_Aproved = 1 where Code = '" + userCode + "'";
            PromoItServer.DAL.SqlFunctions.WriteToDB(updateQuery);
        }
        public void SendUserMessageQuery(UserMessage data)
        {
            Log.LogEvent("SendUserMessageQuery function was called");
            string sqlQuery = "insert into Contact_Us values('" + data.Name + "','" + data.Message + "','" + data.Phone + "','" + data.Email + "',getdate())";
            PromoItServer.DAL.SqlFunctions.WriteToDB(sqlQuery);
        }
        public void SendMoneyStatusQuery(decimal moneyStatus, int SACode)
        {
            Log.LogEvent("SendMoneyStatusQuery function was called");
            string updateQuery = "update Social_Activist set Money_Status = @MoneyStatus where Code = @SACode";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                    { "@MoneyStatus", moneyStatus},
                    { "@SACode", SACode},
            };
            PromoItServer.DAL.SqlFunctions.WriteWithValuesToDB(updateQuery, parameters);
        }
    }
}
