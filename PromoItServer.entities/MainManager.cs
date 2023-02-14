using PromoItServer.DataSql;
using PromoItServer.model;
using System.Threading.Tasks;
using Utilities_CS;

namespace PromoItServer.entities
{
    public class MainManager
    {
        private MainManager()
        {
            init();
            Logger.LogEvent("Program Started!");
        }

        private static readonly MainManager insance = new MainManager();
        public static MainManager Instance { get { return insance; } }

        public void init() 
        { 
            Logger = new Log("File");
            Config = new ConfigClass();
            CommmandM = new CommandManager(Logger);
            GeneralQ = new GeneralQueries(Logger, Config);
            CampaignsM = new CampaignsManager(Logger);
            ProductsM = new ProductsManager(Logger);
            ReportsM = new ReportsManager(Logger);
            TwitterM = new TwitterManager(Logger);
            UsersM = new UsersManager(Logger);
            GeneralQ.LogInit();
            GeneralQ.ConnectionInit();
            GeneralQ.ConfigInit();
            TwitterM.StartTwitterUpdaterTask();
        }
        public Log Logger;
        public ConfigClass Config;
        public GeneralQueries GeneralQ;
        public CommandManager CommmandM;
        public CampaignsManager CampaignsM;
        public ProductsManager ProductsM;
        public ReportsManager ReportsM;
        public TwitterManager TwitterM;
        public UsersManager UsersM;
        
        
    }
}
