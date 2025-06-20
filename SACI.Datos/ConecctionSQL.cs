using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace SACI.Datos
{
    public class ConecctionSQL
    {
        d_OLEDb _obetoBd = new d_OLEDb();
        string Nombd;


        public static string constrSolomon /// ..0.2
        {
            get
            {
                if (HttpContext.Current.Session["idBase"].ToString() == "Bd1")
                {
                    return ConfigurationManager.ConnectionStrings["conn1"].ConnectionString;
                }
                else if (HttpContext.Current.Session["idBase"].ToString() == "Bd2")
                {
                    return ConfigurationManager.ConnectionStrings["conn2"].ConnectionString;
                }
                else if (HttpContext.Current.Session["idBase"].ToString() == "Bd3")
                {
                    return ConfigurationManager.ConnectionStrings["conn3"].ConnectionString;
                }
                else if (HttpContext.Current.Session["idBase"].ToString() == "Bd4")
                {
                    return ConfigurationManager.ConnectionStrings["conn4"].ConnectionString;
                }
                else if (HttpContext.Current.Session["idBase"].ToString() == "Bd5")
                {
                    return ConfigurationManager.ConnectionStrings["conn5"].ConnectionString;
                }
                else if (HttpContext.Current.Session["idBase"].ToString() == "Bd6")
                {
                    return ConfigurationManager.ConnectionStrings["conn6"].ConnectionString;
                }
                else if (HttpContext.Current.Session["idBase"].ToString() == "Bd7")
                {
                    return ConfigurationManager.ConnectionStrings["conn7"].ConnectionString;
                }
                else if (HttpContext.Current.Session["idBase"].ToString() == "Bd8")
                {
                    return ConfigurationManager.ConnectionStrings["conn8"].ConnectionString;
                }
                else if (HttpContext.Current.Session["idBase"].ToString() == "Bd9")
                {
                    return ConfigurationManager.ConnectionStrings["conn9"].ConnectionString;
                }
                else
                {
                    return "Sin base de datos";
                }

            }
        }


        public ConecctionSQL()
        {

        }


        // ABRIR CONECCION
        public DataTable Conectar(string Query)
        {
            try
            {

                _obetoBd = new d_OLEDb();
                _obetoBd.ConectionString = constrSolomon;
                _obetoBd.OpenConection();
                return _obetoBd.ExecuteSQLDT(Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _obetoBd.CloseConnection();
            }
        }


        // ABRIR CONECCION
        public DataTable ConectarConsultaCount(string Query)
        {
            try
            {

                _obetoBd = new d_OLEDb();
                _obetoBd.ConectionString = constrSolomon;
                _obetoBd.OpenConection();
                return _obetoBd.ExecuteConsultasCountSQLDT(Query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _obetoBd.CloseConnection();
            }
        }

        // ABRIR CONECCION
        public DataTable ConectarConsultabloques(string Query, int inicio, int fin)
        {
            try
            {

                _obetoBd = new d_OLEDb();
                _obetoBd.ConectionString = constrSolomon;
                _obetoBd.OpenConection();
                if (HttpContext.Current.Session["Empresa"].ToString().Trim().ToUpper().Contains("MAHLE"))
                {
                    return _obetoBd.ExecuteConsultasBloquesSQLDT_Orig(constrSolomon, Query, inicio, fin);
                }
                else
                {
                    return _obetoBd.ExecuteConsultasBloquesSQLDT(constrSolomon, Query, inicio, fin);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _obetoBd.CloseConnection();
            }
        }


        // ABRIR CONECCION
        public DataTable ConectarEstruct(string Query, DataTable Import)
        {
            {
                try
                {
                    _obetoBd = new d_OLEDb();
                    _obetoBd.ConectionString = constrSolomon;
                    _obetoBd.OpenConection();
                    return _obetoBd.ExecuteSQLDTEstruct(Query, Import);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _obetoBd.CloseConnection();
                }
            }
        }

        // ABRIR CONECCION
        public DataTable ConectarUpdRegistro(string SP, int OPCION, string DENOMINACION, string RFC, string REGISTROSACI)
        {
            try
            {
                _obetoBd = new d_OLEDb();
                _obetoBd.ConectionString = constrSolomon;//CadenaSQL.Conn;
                _obetoBd.OpenConection();
                return _obetoBd.ExecuteSPInfoRegistro(SP, OPCION, DENOMINACION, RFC, REGISTROSACI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _obetoBd.CloseConnection();
            }
        }

        // INVENTARIO FINAL 	
        public DataTable ConectarEstructInvFianl(string Query, int Opcion, DataTable Import)
        {
            {
                try
                {
                    _obetoBd = new d_OLEDb();
                    _obetoBd.ConectionString = constrSolomon;
                    _obetoBd.OpenConection();
                    return _obetoBd.ExecuteSQLDTEstructInvFianl(Query, Opcion, Import);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _obetoBd.CloseConnection();
                }
            }
        }

        // INVENTARIO FINAL 
        public DataTable ConectarEstructInvIni(string Query, int Opcion, DataTable Import)
        {
            {
                try
                {
                    _obetoBd = new d_OLEDb();
                    _obetoBd.ConectionString = constrSolomon;
                    _obetoBd.OpenConection();
                    return _obetoBd.ExecuteSQLDTEstructInvInicial(Query, Opcion, Import);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _obetoBd.CloseConnection();
                }
            }
        }



        // INVENTARIO FINAL 
        public DataTable ConectarEstructConsultas(string Query, int OPCION = 0, int CONSULTAKEY = 0, string NOMBRE = "", string CONSULTA = "", string DESCRIPCION = "", bool PARAMETROS = false)
        {
            {
                try
                {
                    _obetoBd = new d_OLEDb();
                    _obetoBd.ConectionString = constrSolomon;
                    _obetoBd.OpenConection();
                    return _obetoBd.ExecuteSQLDTEstructConsultas(Query, OPCION, CONSULTAKEY, NOMBRE, CONSULTA, DESCRIPCION, PARAMETROS);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _obetoBd.CloseConnection();
                }
            }
        }


        // ABRIR CONECCION
        public DataTable ConectarEstructAcdes(string Query, DataTable Acdes)
        {
            {
                try
                {
                    _obetoBd = new d_OLEDb();
                    _obetoBd.ConectionString = constrSolomon;
                    _obetoBd.OpenConection();
                    return _obetoBd.ExecuteSQLDTEstructAcdes(Query, Acdes);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _obetoBd.CloseConnection();
                }
            }
        }

        // ABRIR CONECCION
        public DataTable ConectarEstructFactura(string Query, DataTable Acdes)
        {
            {
                try
                {
                    _obetoBd = new d_OLEDb();
                    _obetoBd.ConectionString = constrSolomon;
                    _obetoBd.OpenConection();
                    return _obetoBd.ExecuteSQLDTEstructFactura(Query, Acdes);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _obetoBd.CloseConnection();
                }
            }
        }


        //ARCHIVOS DE DESCARGA
        public DataTable ConectarUplArchivosDescarga(string SP, string FILENAME, byte[] file)
        {
            try
            {
                _obetoBd = new d_OLEDb();
                _obetoBd.ConectionString = constrSolomon;
                _obetoBd.OpenConection();
                return _obetoBd.ExecuteUplArchivoDescarga(SP, FILENAME, file);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _obetoBd.CloseConnection();
            }
        }


        // ABRIR CONECCION FACTURA SERVICIOS
        public DataTable ConectarEstructFacturaServicios(string Query, DataTable Acdes)
        {
            {
                try
                {
                    _obetoBd = new d_OLEDb();
                    _obetoBd.ConectionString = constrSolomon;
                    _obetoBd.OpenConection();
                    return _obetoBd.ExecuteSQLDTEstructFacturaServicios(Query, Acdes);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _obetoBd.CloseConnection();
                }
            }
        }

        //INTERFACE PEDIMENTOS
        public DataTable ConectarEstructPedimentos(string Query, DataTable dtPedimentos)
        {
            {
                try
                {
                    _obetoBd = new d_OLEDb();
                    _obetoBd.ConectionString = constrSolomon;
                    _obetoBd.OpenConection();
                    return _obetoBd.ExecuteSQLDTEstructPedimentos(Query, dtPedimentos);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _obetoBd.CloseConnection();
                }
            }
        }



        //INFORME AJUSTES CON 3 SELECTS
        public void ConectarRptAjustes(string sp, int opcion, string desde, string hasta,string anio, ref DataTable dt1, ref DataTable dt2, ref DataTable dt3)
        {
            {
                try
                {
                    _obetoBd = new d_OLEDb();
                    _obetoBd.ConectionString = constrSolomon;
                    _obetoBd.OpenConection();
                    _obetoBd.ExecuteSQLRptAjustes(sp, opcion, desde, hasta,anio, ref dt1, ref dt2, ref dt3);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _obetoBd.CloseConnection();
                }
            }
        }

        //ARCHIVOS INTERFACE PROCESOS
        public DataTable UplArchivosProceso(string SP, int OPCION = 0, Guid UUID = default(Guid), string FILES = "", string NOMBRE = "", int ESTATUS = 0, string USUARIO_ID = "", int size = 0)
        {
            try
            {
                _obetoBd = new d_OLEDb();
                _obetoBd.ConectionString = constrSolomon;
                _obetoBd.OpenConection();
                return _obetoBd.ExecuteUplArchivosProc(SP, OPCION, UUID, FILES, NOMBRE, ESTATUS, USUARIO_ID, size);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _obetoBd.CloseConnection();
            }
        }

        // ABRIR CONECCION DIRIGIDOS
        public DataTable ConectarEstructDirigidos(string Query, DataTable dtD)
        {
            {
                try
                {
                    _obetoBd = new d_OLEDb();
                    _obetoBd.ConectionString = constrSolomon;
                    _obetoBd.OpenConection();
                    return _obetoBd.ExecuteSQLDTEstructDirigidos(Query, dtD);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _obetoBd.CloseConnection();
                }
            }
        }

        public DataTable ConectarEstructBOM(string Query, DataTable dtDatos, string nombre_archivo)
        {
            {
                try
                {
                    _obetoBd = new d_OLEDb();
                    _obetoBd.ConectionString = constrSolomon;
                    _obetoBd.OpenConection();
                    return _obetoBd.ExecuteSQLCargaBOMS(Query, dtDatos, nombre_archivo);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _obetoBd.CloseConnection();
                }
            }
        }

        // ABRIR CONECCION ANALISIS ESTRUCTURAS
        public DataTable ConectarAnalisisEstruct(string Query, int opcion, string desde, string hasta)
        {
            {
                try
                {
                    _obetoBd = new d_OLEDb();
                    _obetoBd.ConectionString = constrSolomon;
                    _obetoBd.OpenConection();
                    return _obetoBd.ExecuteSQLDTAnalisisEstruct(Query, opcion, desde, hasta);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    _obetoBd.CloseConnection();
                }
            }
        }

    }
}
