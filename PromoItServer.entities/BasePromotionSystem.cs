using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities_CS;

namespace PromoItServer.entities
{
    public class BasePromotionSystem
    {
        public BasePromotionSystem(Log log) { Log = log; }
        public static Log Log { get; set; }

    }
}
