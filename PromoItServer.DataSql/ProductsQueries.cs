using PromoItServer.model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Utilities_CS;

namespace PromoItServer.DataSql
{
    public class ProductsQueries : BaseDataSql
    {
        public ProductsQueries(Log Logger) : base(Logger) { }
        public void NewCampaignProductQuery(Product data, string userEmail)
        {
            Log.LogEvent("NewCampaignProductQuery function was called");
            string insertCampaignProductQuery = "declare @BC_code int\r\nselect @BC_code = (select Code from Buisness_Companies where Email='" + userEmail + "')\r\ninsert into Products values ('" + data.Name + "', '" + data.Description + "', @Price, @UnitsInStock, @BC_code, @CampaignCode, '" + data.MyImage + "')";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                    { "@Price", data.Price},
                    { "@UnitsInStock", data.UnitsInStock},
                    { "@CampaignCode", data.CampaignCode},
            };
            PromoItServer.DAL.SqlFunctions.WriteWithValuesToDB(insertCampaignProductQuery, parameters);
        }
        public DataTable GetCampaignProductsQuery()
        {
            Log.LogEvent("GetCampaignProductsQuery function was called");
            string CampaignsProductsQuery = "select P.*, BC.Name as BC_Name, BC.Email as BC_Email from Products as P\r\ninner join Buisness_Companies as BC\r\non P.BC_code = BC.Code";
            return PromoItServer.DAL.SqlFunctions.ReadTableFromDB(CampaignsProductsQuery);
        }
        public DataTable GetOrdersQuery()
        {
            Log.LogEvent("GetOrdersQuery function was called");
            string OrdersQuery = "select O.*, SA.Name as SA_Name, BC.Name as BC_Name, C.Name as Campaign_Name, P.Name as Product_Name, \r\nSA.Address as Activist_Address, SA.Phone_Number as Activist_Phone,\r\nSA.Email as Activist_Email, C.Email as Campaign_Email \r\nfrom Orders as O\r\ninner join Social_Activist as SA on O.SA_code = SA.Code\r\ninner join Buisness_Companies as BC on O.BC_code = BC.Code\r\ninner join Campaigns as C on O.Campaign_code = C.Code\r\ninner join Products as P on O.Product_code = P.Code";
            return PromoItServer.DAL.SqlFunctions.ReadTableFromDB(OrdersQuery);
        }
        public void ApproveOrderShippedQuery(string orderCode)
        {
            Log.LogEvent("ApproveOrderShippedQuery function was called");
            string updateQuery = "update Orders set is_Sent = 1 where Code = '" + orderCode + "'";
            PromoItServer.DAL.SqlFunctions.WriteToDB(updateQuery);
        }
        public void UpdatedCampaignProductQuery(Product data)
        {
            Log.LogEvent("UpdatedCampaignProductQuery function was called");
            string insertCampaignProductQuery = "update Products set Name='"+data.Name+"', Description='"+data.Description+ "', Price=@Price, Units_In_Stock=@UnitsInStock, Image='"+data.MyImage+"' where Code="+data.Code;
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                    { "@Price", data.Price},
                    { "@UnitsInStock", data.UnitsInStock},
            };
            PromoItServer.DAL.SqlFunctions.WriteWithValuesToDB(insertCampaignProductQuery, parameters);
        }
        public void SendDonateDetailsQuery(Order order, int Quantity)
        {
            Log.LogEvent("SendDonateDetailsQuery function was called");
            string UpdateDonateDetailsQuery = "insert into Orders values (@SA_code, @BC_code, @Campaign_code, @Product_code, getdate(), 0)\r\nupdate Products set Units_In_Stock = Units_In_Stock - 1 where Code = @Product_code";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                    { "@SA_code", order.SACode},
                    { "@BC_code", order.BCCode},
                    { "@Campaign_code", order.CampaignCode},
                    { "@Product_code", order.ProductCode},
            };
            for (int i = 0; i < Quantity; i++)
            {
                PromoItServer.DAL.SqlFunctions.WriteWithValuesToDB(UpdateDonateDetailsQuery, parameters);
            }
        }
    }
}
