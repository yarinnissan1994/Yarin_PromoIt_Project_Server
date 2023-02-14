using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class Products_get_orders : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("Products_get_orders Command Was called");
            try
            {
                string responseMessage = JsonConvert.SerializeObject(MainManager.Instance.ProductsM.GetOrdersFromDB());
                return ("OkObjectResult", responseMessage);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while getting orders: ", ex);
                return ("BadRequestObjectResult", "Error while getting orders: " + ex.Message);
            }
        }

        public void Init() { }
    }
}
