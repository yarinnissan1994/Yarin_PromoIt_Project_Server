using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    internal class Products_post_new_campaign_product : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("Products_post_new_campaign_product Command Was called");
            try
            {
                object data = new Product();
                data = System.Text.Json.JsonSerializer.Deserialize<Product>((string)arg[2]);
                MainManager.Instance.ProductsM.SendNewCampaignProductToDB((Product)data, (string)arg[1]);
                return ("success", null);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while posting new campaign product: ", ex);
                return ("BadRequestObjectResult", "Error while posting new campaign product: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
