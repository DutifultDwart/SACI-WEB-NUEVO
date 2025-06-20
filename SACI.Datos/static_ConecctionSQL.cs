using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Datos
{
    public static class static_ConecctionSQL
    {                
        public static string constrSolomon /// ..0.2
        {
            get { return ConfigurationManager.ConnectionStrings["conn"].ConnectionString; }
        }



        // ABRIR CONECCION
        public static DataTable Conectar(string Query)
        {
            try
            {
                d_Static_OLEDb.ConectionString = constrSolomon;
                d_Static_OLEDb.OpenConection();
                return d_Static_OLEDb.ExecuteSQLDT(Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                d_Static_OLEDb.CloseConnection();
            }
        }

      
    }
}
