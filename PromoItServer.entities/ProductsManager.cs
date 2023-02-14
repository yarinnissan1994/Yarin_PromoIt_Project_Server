using PromoItServer.DataSql;
using PromoItServer.model;
using System.Data;
using System.Threading.Tasks;
using Utilities_CS;

namespace PromoItServer.entities
{
    public class ProductsManager : BaseEntity
    {
        public ProductsManager(Log Logger) : base(Logger) { productsQ = new ProductsQueries(Logger); }
        ProductsQueries productsQ;
        public void SendNewCampaignProductToDB(Product data, string userEmail)
        {
            Log.LogEvent("SendNewCampaignProductToDB function was called");
            productsQ.NewCampaignProductQuery(data, userEmail);
        }
        public DataTable GetCampaignProductsFromDB()
        {
            Log.LogEvent("GetCampaignProductsFromDB function was called");
            return productsQ.GetCampaignProductsQuery();
        }
        public DataTable GetOrdersFromDB()
        {
            Log.LogEvent("GetOrdersFromDB function was called");
            return productsQ.GetOrdersQuery();
        }
        public void ApproveOrderShippedInDB(string orderCode)
        {
            Log.LogEvent("ApproveOrderShippedInDB function was called");
            productsQ.ApproveOrderShippedQuery(orderCode);
        }
        public void SendUpdatedCampaignProductToDB(Product data)
        {
            Log.LogEvent("SendUpdatedCampaignProductToDB function was called");
            productsQ.UpdatedCampaignProductQuery(data);
        }
        public void SendDonateDetailsToDB(Order order, int Quantity)
        {
            Log.LogEvent("SendDonateDetailsToDB function was called");
            productsQ.SendDonateDetailsQuery(order, Quantity);
        }
    }
}
