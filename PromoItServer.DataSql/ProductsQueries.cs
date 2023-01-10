using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.DataSql
{
    public class ProductsQueries
    {
        public void NewCampaignProductQuery(Product data, string userEmail)
        {
            string insertCampaignProductQuery = "declare @BC_code int\r\nselect @BC_code = (select Code from Buisness_Companies where Email='" + userEmail + "')\r\ninsert into Products values ('" + data.Name + "', '" + data.Description + "', @Price, @UnitsInStock, @BC_code, @CampaignCode, '" + data.MyImage + "')";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                    { "@Price", data.Price},
                    { "@UnitsInStock", data.UnitsInStock},
                    { "@CampaignCode", data.CampaignCode},
            };
            PromoItServer.DAL.SqlFunctions.WriteWithValuesToDB(insertCampaignProductQuery, parameters);
        }
    }
}
