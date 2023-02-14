using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class Products_post_donate_details : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("Products_post_donate_details Command Was called");
            try
            {
                object data = new Order();
                data = System.Text.Json.JsonSerializer.Deserialize<Order>((string)arg[2]);
                MainManager.Instance.ProductsM.SendDonateDetailsToDB((Order)data, int.Parse((string)arg[1]));
                return ("success", null);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while posting donate details: ", ex);
                return ("BadRequestObjectResult", "Error while posting donate details: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
