using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_CS;

namespace PromoItServer.DataSql
{
    public class BaseDataSql
    {
        public BaseDataSql(Log log) { Log = log; }
        public static Log Log { get; set; }
    }
}
