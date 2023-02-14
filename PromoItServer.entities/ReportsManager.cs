using PromoItServer.DataSql;
using System.Data;
using System.Threading.Tasks;
using Utilities_CS;

namespace PromoItServer.entities
{
    public class ReportsManager : BaseEntity
    {
        public ReportsManager (Log Logger) : base (Logger) { reportsQ = new ReportsQueries(Logger); }
        ReportsQueries reportsQ;
        public DataTable GetReportFromDB(string type)
        {
            Log.LogEvent("GetReportFromDB function was called");
            return reportsQ.GetReportQuery(type);
        }
    }
}
