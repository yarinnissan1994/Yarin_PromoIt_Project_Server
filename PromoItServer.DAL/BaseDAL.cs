using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_CS;

namespace PromoItServer.DAL
{
    public class BaseDAL
    {
        public BaseDAL(Log log) { Log = log; }
        public static Log Log { get; set; }
    }
}
