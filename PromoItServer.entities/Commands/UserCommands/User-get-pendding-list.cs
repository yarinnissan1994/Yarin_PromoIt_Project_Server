using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class User_get_pendding_list : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("User_get_pendding_list Command Was called");
            try
            {
                string responseMessage = JsonConvert.SerializeObject(MainManager.Instance.UsersM.GetPenddingListFromDB());
                return ("OkObjectResult", responseMessage);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while getting pendding list: ", ex);
                return ("BadRequestObjectResult", "Error while getting pendding list: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
