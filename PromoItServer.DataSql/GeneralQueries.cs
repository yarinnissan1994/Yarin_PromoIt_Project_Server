using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.DataSql
{
    public class GeneralQueries
    {
        public static void ConnectionInit()
        {
            PromoItServer.DAL.SqlFunctions.ConnectionStringInit("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=PromoIt;Data Source=localhost\\sqlexpress");
        }
    }
}
