using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SACI.Datos
{
    public class DatosComun
    {
        public static string constrSolomon /// ..0.2
        {
            get { return ConfigurationManager.ConnectionStrings["conn"].ConnectionString; }
        }


        public static string Provider
        {
            get
            {
                return ConfigurationManager.AppSettings["myProvider"];
            }
        }

        public static DbProviderFactory dpf
        {
            get
            {
                return DbProviderFactories.GetFactory(Provider);
            }
        }


        public DatosComun()
        {
        }




        private static int ExecuteNonQuery(string StoreProcedure, List<DbParameter> parametros, int Servidor)
        {
            int id = 0;
            string strConecction;
            try
            {
                using (DbConnection con = dpf.CreateConnection())
                {
                    strConecction = constrSolomon;
                    con.ConnectionString = strConecction;
                    using (DbCommand cmd = dpf.CreateCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = StoreProcedure;
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (DbParameter param in parametros)
                            cmd.Parameters.Add(param);
                        con.Open();
                        id = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return id;
        }




        /// <summary>
        /// PROCESOS USUARIOS
        /// </summary>
       

    }
}
