using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    internal class Products_post_order_shipped : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("Products_post_order_shipped Command Was called");
            try
            {
                MainManager.Instance.ProductsM.ApproveOrderShippedInDB((string)arg[1]);
                return ("success", null);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while posting order shipped: ", ex);
                return ("BadRequestObjectResult", "Error while posting order shipped: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
