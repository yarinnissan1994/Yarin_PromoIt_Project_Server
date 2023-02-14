using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class User_post_user_message : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("User_post_user_message Command Was called");
            try
            {
                object data = new UserMessage();
                data = System.Text.Json.JsonSerializer.Deserialize<UserMessage>((string)arg[3]);
                MainManager.Instance.UsersM.SendUserMessageToDB((UserMessage)data);
                return ("success", null);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while posting user message: ", ex);
                return ("BadRequestObjectResult", "Error while posting user message: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
