using PromoItServer.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_CS;
using static System.Collections.Specialized.BitVector32;

namespace PromoItServer.entities
{
    public interface ICommand
    {
        (string, object) Run(params object[] arg);
        void Init();
    }
    public class CommandManager : BaseEntity
    {
        public CommandManager (Log Logger) : base (Logger) { }

        private Dictionary<string, ICommand> _CommandList;

        public Dictionary<string, ICommand> CommandList
        {
            get
            {
                if (_CommandList == null) Init();
                return _CommandList;
            }
        }
        void Init()
        {
            try
            {
                Log.LogEvent("Command List Initialization");

                _CommandList = new Dictionary<string, ICommand>
                {
                    {"get-role", new Commands.MicroService_get_role() },

                    {"get-campaigns", new Commands.Campaigns_get_campaigns()},
                    {"post-new-campaign", new Commands.Capmiagns_post_new_campaign()},
                    {"post-updated-campaign", new Commands.Campaigns_post_updated_campaign()},
                    {"post-campaign-is-active", new Commands.Campaigns_post_campaign_is_active()},

                    {"get-products", new Commands.Products_get_products()},
                    {"get-orders", new Commands.Products_get_orders()},
                    {"post-order-shipped", new Commands.Products_post_order_shipped()},
                    {"post-new-campaign-product", new Commands.Products_post_new_campaign_product()},
                    {"post-updated-campaign-product", new Commands.Products_post_updated_campaign_product()},
                    {"post-donate-details", new Commands.Products_post_donate_details()},

                    {"get-report", new Commands.Reports_get_report()},

                    {"post-donation-tweet", new Commands.Twitter_post_donation_tweet()},

                    {"post-user-create", new Commands.User_post_user_create()},
                    {"get-user-info", new Commands.User_get_user_info()},
                    {"get-pendding", new Commands.User_get_pendding()},
                    {"get-pendding-list", new Commands.User_get_pendding_list()},
                    {"get-my-donations", new Commands.User_get_my_donations()},
                    {"post-approve-user", new Commands.User_post_approve_user()},
                    {"post-user-message", new Commands.User_post_user_message()},
                    {"post-sa-money-status", new Commands.User_post_sa_money_status()},
                };

            }
            catch (Exception ex)
            {
                Log.LogException("Command List Initialization failed: ", ex);
            }

        }
    }
}