using PromoItServer.DataSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.entities
{
    public class ReportsManager
    {
        public DataTable GetReportFromDB(string type)
        {
            ReportsQueries reportsQ = new ReportsQueries();
            return reportsQ.GetReportQuery(type);
        }
    }
}
