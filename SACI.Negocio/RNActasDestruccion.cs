using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Negocio
{
    public class RNActasDestruccion
    {
        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;

        #endregion

        public RNActasDestruccion()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }
        #region SELECCIONA LAS FECHAS DE ACTAS DE DESTRUCCION
        public DataTable SelectPeriodoActas()
        {
            try
            {
                CadenaSQL = sp_ActasDestruccion.SACIWEB_MC_ACTASD_AÑO_MES;
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
        #endregion

        #region MUESTRA LAS ACTAS DE DESTRUCCION POR LA FECHA SELECCIONADA
        public DataTable ConsultarActas_Fecha(int OPCION = 0, int YEAR = 0, int MONTH = 0, string DOCUMENTO = "", string DESDE = "", string HASTA = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = sp_ActasDestruccion.SACIWEB_MC_ACTASD_BUSCAR + string.Format("{0}, {1}, {2}, '{3}', '{4}', '{5}', '{6}', '{7}'", OPCION, YEAR, MONTH, DOCUMENTO, DESDE, HASTA, USUARIO, PLANTAS);
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
        #endregion

        #region PROCESA ACTAS DE DESTRUCCION (ABC)

        public DataTable ProcesaActas(int OPCION = 0, int SALIDAKEY = 0, string DOCUMENTO = "", string FECHA = "", int ALMACENKEY = 0, string OBSERVACIONES = "", string DESCARGA = "", string TIPODESCARGA = "")
        {
            try
            {
                CadenaSQL = sp_ActasDestruccion.SACIWEB_MC_DESP_DATOS + string.Format("{0}, '{1}', '{2}', {3}, '{4}', '{5}', '{6}', {7}", SALIDAKEY, DOCUMENTO, FECHA, ALMACENKEY, OBSERVACIONES, DESCARGA, TIPODESCARGA, OPCION);
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

        #endregion

        #region PROCESA PARTIDAS DE ACTAS DE DESTRUCCION (ABC)

        public DataTable ProcesaPartidasActas(int OPCION = 0, int PSKEY = 0, int SALIDAKEY = 0, string CLAVE = "", string DESCRIPCION = "", string FRACCION = "", decimal CANTIDAD = 0,
            string UNIDAD = "", int BLOQUEADO = 0, string OBSERVACIONES = "", int PARTIDA = 0, string LOTE = "")
        {
            try
            {
                CadenaSQL = sp_ActasDestruccion.SACIWEB_MC_DESP_PARTIDAS + string.Format("{0}, {1}, '{2}', '{3}', '{4}', {5}, '{6}', '{7}', '{8}', {9}, '{10}', {11}",
                    PSKEY, SALIDAKEY, CLAVE, DESCRIPCION, FRACCION, CANTIDAD, UNIDAD, BLOQUEADO, OBSERVACIONES, PARTIDA, LOTE, OPCION);
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

        #endregion



        #region IMPPRTA EL ARCHIVO

        /// <summary>
        /// METODO PARA IMPORTAR EL ARCHIVO DE ACTAS DE DESTRUCCION
        /// </summary>
        public DataTable ImportaACDES(DataTable dtAcdes)
        {
            try
            {
                return _ObjetoDBSQL.ConectarEstructAcdes(sp_Interfaces.SACIWEB_IMPORTA_ACTAS_DESTRUCCION, dtAcdes);
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

        #endregion


        #region PROCESA ACTAS DE DESTRUCCION

        public DataTable Guardar_ACDES(string UNIQUE = "")
        {
            try
            {
                CadenaSQL = sp_Interfaces.SACIWEB_INTERFACE_CARGA_ACTAS_DESTRUCCION + string.Format("'{0}'", UNIQUE);
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

        #endregion


    }
}
