using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    internal class Products_get_products : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("Products_get_products Command Was called");
            try
            {
                string responseMessage = JsonConvert.SerializeObject(MainManager.Instance.ProductsM.GetCampaignProductsFromDB());
                return ("OkObjectResult", responseMessage);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while getting products: ", ex);
                return ("BadRequestObjectResult", "Error while getting products: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
