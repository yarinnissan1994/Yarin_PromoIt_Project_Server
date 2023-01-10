using PromoItServer.DataSql;
using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities
{
    public class ProductsManager
    {
        public void SendNewCampaignProductToDB(Product data, string userEmail)
        {
            ProductsQueries productsQ = new ProductsQueries();
            productsQ.NewCampaignProductQuery(data, userEmail);
        }
    }
}
