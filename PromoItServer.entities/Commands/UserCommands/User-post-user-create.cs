using PromoItServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class User_post_user_create : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("User_post_user_create Command Was called");
            try
            {
                if (((string)arg[1]) == "NPO")
                {
                    object data = new NonProfitOrganization();
                    data = System.Text.Json.JsonSerializer.Deserialize<NonProfitOrganization>((string)arg[3]);
                    MainManager.Instance.UsersM.SendUserToDB((NonProfitOrganization)data);
                }
                else if (((string)arg[1]) == "BC")
                {
                    object data = new BuisnessCompany();
                    data = System.Text.Json.JsonSerializer.Deserialize<BuisnessCompany>((string)arg[3]);
                    MainManager.Instance.UsersM.SendUserToDB((BuisnessCompany)data);
                }
                else
                {
                    object data = new SocialActivist();
                    data = System.Text.Json.JsonSerializer.Deserialize<SocialActivist>((string)arg[3]);
                    MainManager.Instance.UsersM.SendUserToDB((SocialActivist)data);
                }
                return ("success", null);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while posting user create: ", ex);
                return ("BadRequestObjectResult", "Error while posting user create: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
