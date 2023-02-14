using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class User_get_pendding : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("User_get_pendding Command Was called");
            try
            {
                string responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.UsersM.getPenddingFromDB((string)arg[1]));
                return ("OkObjectResult", responseMessage);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while getting pennding: ", ex);
                return ("BadRequestObjectResult", "Error while getting pennding: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
