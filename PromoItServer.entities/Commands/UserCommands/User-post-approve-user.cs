using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class User_post_approve_user : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("User_post_approve_user Command Was called");
            try
            {
                MainManager.Instance.UsersM.ApproveUserInDB((string)arg[1]);
                return ("success", null);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while posting approve user: ", ex);
                return ("BadRequestObjectResult", "Error while posting approve user: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
