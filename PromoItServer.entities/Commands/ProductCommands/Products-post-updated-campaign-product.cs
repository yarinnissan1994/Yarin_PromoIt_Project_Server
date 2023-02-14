using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class Products_post_updated_campaign_product : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("Products_post_updated_campaign_product Command Was called");
            try
            {
                object data = new Product();
                data = System.Text.Json.JsonSerializer.Deserialize<Product>((string)arg[2]);
                MainManager.Instance.ProductsM.SendUpdatedCampaignProductToDB((Product)data);
                return ("success", null);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while posting updated campaign product: ", ex);
                return ("BadRequestObjectResult", "Error while posting updated campaign product: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
