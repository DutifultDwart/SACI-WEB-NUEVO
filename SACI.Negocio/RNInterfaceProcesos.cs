using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;



namespace SACI.Negocio
{
    public class RNInterfaceProcesos
    {
        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;

        #endregion

        public RNInterfaceProcesos()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }


        #region INTERFACE PROCESOS 

        public DataTable procArchivosCarga(int OPCION = 0, Guid UUID = default(Guid), string FILES = "", string NOMBRE = "", int ESTATUS = 0,  string USUARIO_ID = "", int size = 0)
        {
            try
            {
                CadenaSQL = sp_Archivos.SACIWEB_INTERFACE_PROCESOS;
                return _ObjetoDBSQL.UplArchivosProceso(CadenaSQL, OPCION, UUID, FILES, NOMBRE, ESTATUS, USUARIO_ID, size);
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    throw ex;
                }
                else
                {
                    throw new ApplicationException(string.Format("Error: {0}. {1}. {2}. {3}. {4}", ex.Message.Replace("\r\n", "|").Replace("'", "´"), "|Metodo: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    "|Recurso: " + ex.Source,
                    "|Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"),
                    "|Sp: " + CadenaSQL.Replace("\r\n", "|").Replace("'", "´")), ex);
                }

            }
            finally
            {
                _ObjetoDB = null;
            }
        }

        public DataTable procesaFileDescarga(int OPCION = 0, Guid UUID = default(Guid))
        {
            try
            {
                CadenaSQL = sp_Archivos.SACIWEB_SELECT_INTERFACE_ARCHIVO + string.Format("{0}, '{1}'", OPCION, UUID);
                return _ObjetoDBSQL.Conectar(CadenaSQL);
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    throw ex;
                }
                else
                {
                    throw new ApplicationException(string.Format("Error: {0}. {1}. {2}. {3}. {4}", ex.Message.Replace("\r\n", "|").Replace("'", "´"), "|Metodo: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    "|Recurso: " + ex.Source,
                    "|Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"),
                    "|Sp: " + CadenaSQL.Replace("\r\n", "|").Replace("'", "´")), ex);
                }

            }
            finally
            {
                _ObjetoDB = null;
            }
        }




        public DataTable procesaFile(string tipoArchivo = "", Guid UUID = default(Guid))
        {
            try
            {
                if (tipoArchivo == "b")
                {
                    CadenaSQL = sp_Archivos.SP_CARGA_BOM + string.Format("'{0}'", UUID);
                }
                else if (tipoArchivo == "rm")
                {
                    CadenaSQL = sp_Archivos.SP_CARGA_TXT_RM + string.Format("'{0}'", UUID);
                }
                else if (tipoArchivo == "fg")
                {
                    CadenaSQL = sp_Archivos.SP_CARGA_TXT_FG + string.Format("'{0}'", UUID);
                }
                else if (tipoArchivo == "E")
                {
                    CadenaSQL = sp_Archivos.SP_CARGA_EXPO + string.Format("'{0}'", UUID);
                }
                else if (tipoArchivo == "")
                {
                    CadenaSQL = sp_Archivos.SP_CARGA_IMPO + string.Format("'{0}'", UUID);
                }
                return _ObjetoDBSQL.Conectar(CadenaSQL);
            }
            catch (Exception ex)
            {
                if (ex.Message.Substring(0, 1).ToString() == "1")
                {
                    throw ex;
                }
                else
                {
                    throw new ApplicationException(string.Format("Error: {0}. {1}. {2}. {3}. {4}", ex.Message.Replace("\r\n", "|").Replace("'", "´"), "|Metodo: " ,
                    "|Recurso: " + ex.Source,
                    "|Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"),
                    "|Sp: " + CadenaSQL.Replace("\r\n", "|").Replace("'", "´")), ex);
                }

            }
            finally
            {
                _ObjetoDB = null;
            }
        }


        #endregion

    }
}
