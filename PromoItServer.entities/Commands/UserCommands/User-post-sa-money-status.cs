using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class User_post_sa_money_status : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("User_post_sa_money_status Command Was called");
            try
            {
                object data = System.Text.Json.JsonSerializer.Deserialize<decimal>((string)arg[3]);
                MainManager.Instance.UsersM.SendMoneyStatusToDB((decimal)data, int.Parse((string)arg[1]));
                return ("success", null);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while posting sa money status: ", ex);
                return ("BadRequestObjectResult", "Error while posting sa money status: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
