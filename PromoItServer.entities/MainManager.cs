using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities
{
    public class MainManager
    {
        private MainManager() 
        {
            DataSql.GeneralQueries.ConnectionInit();
        }
        private static readonly MainManager insance = new MainManager();
        public static MainManager Instance { get { return insance; } }

        public UsersManager UsersM = new UsersManager();
        public CampaignsManager CampaignsM = new CampaignsManager();
    }
}
