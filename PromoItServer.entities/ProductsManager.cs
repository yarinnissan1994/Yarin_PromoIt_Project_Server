using PromoItServer.DataSql;
using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Data;
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
        public DataTable GetCampaignProductsFromDB()
        {
            ProductsQueries productsQ = new ProductsQueries();
            return productsQ.GetCampaignProductsQuery();
        }
        public DataTable GetOrdersFromDB()
        {
            ProductsQueries productsQ = new ProductsQueries();
            return productsQ.GetOrdersQuery();
        }
        public void ApproveOrderShippedInDB(string orderCode)
        {
            ProductsQueries productsQ = new ProductsQueries();
            productsQ.ApproveOrderShippedQuery(orderCode);
        }
        public void SendUpdatedCampaignProductToDB(Product data)
        {
            ProductsQueries productsQ = new ProductsQueries();
            productsQ.UpdatedCampaignProductQuery(data);
        }
        public void SendDonateDetailsToDB(Order order, int Quantity)
        {
            ProductsQueries productsQ = new ProductsQueries();
            productsQ.SendDonateDetailsQuery(order, Quantity);
        }
    }
}
