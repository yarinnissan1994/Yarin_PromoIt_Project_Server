using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class User_get_user_info : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("User_get_user_info Command Was called");
            try
            {
                string responseMessage = JsonConvert.SerializeObject(MainManager.Instance.UsersM.getUserInfoFromDB((string)arg[1], (string)arg[2]));
                return ("OkObjectResult", responseMessage);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while getting user info: ", ex);
                return ("BadRequestObjectResult", "Error while getting user info: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
