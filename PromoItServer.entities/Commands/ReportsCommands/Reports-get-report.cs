using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities.Commands
{
    public class Reports_get_report : ICommand
    {
        public (string, object) Run(params object[] arg)
        {
            MainManager.Instance.Logger.LogEvent("Reports_get_report Command Was called");
            try
            {
                string responseMessage = JsonConvert.SerializeObject(MainManager.Instance.ReportsM.GetReportFromDB((string) arg[1]));
                return ("OkObjectResult", responseMessage);
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.LogException("Error while getting report: ", ex);
                return ("BadRequestObjectResult", "Error while getting report: " + ex.Message);
            }
        }
        public void Init() { }
    }
}
