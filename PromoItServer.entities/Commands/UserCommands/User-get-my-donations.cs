using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class User_get_my_donations : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("User_get_my_donations Command Was called");
            try
            {
                string responseMessage = JsonConvert.SerializeObject(MainManager.Instance.UsersM.GetSADonationsFromDB((string)arg[1]));
                return ("OkObjectResult", responseMessage);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while getting pennmy donationsding: ", ex);
                return ("BadRequestObjectResult", "Error while getting my donations: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
