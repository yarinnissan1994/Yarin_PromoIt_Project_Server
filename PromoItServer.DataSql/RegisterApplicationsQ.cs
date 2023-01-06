using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItServer.DataSql
{
    public class RegisterApplicationsQ
    {
        public bool? getPenddingQuery(string email)
        {
            string isAprovedQuery = "select Is_Aproved from Register_Applications where Email like '" + email + "'";
            bool? isPendding = (bool?) PromoItServer.DAL.SqlFunctions.ReadScalarFromDB(isAprovedQuery);
            return isPendding;
        }
    }
}
