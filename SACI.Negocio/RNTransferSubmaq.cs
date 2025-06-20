using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SACI.Negocio
{
    public class RNTransferSubmaq
    {

        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;

        #endregion

        public RNTransferSubmaq()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }


        #region SELECCIONA LAS FECHAS DE TRANSFERENCIA SUBMAQUILA
        public DataTable SelectPeriodoTrnasSub()
        {
            try
            {
                CadenaSQL = sp_TransferenciaSubmaqila.SACIWEB_MC_TRANSUBMAQ_AÑO_MES;
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


        #region MUESTRA LAS TRANSFERENCIAS DE SUBMAQUILA POR LA FECHA SELECCIONADA
        public DataTable ConsultarTransSubmaq_Fecha(int OPCION = 0, int YEAR = 0, int MONTH = 0, string DOCUMENTO = "", string DESDE = "", string HASTA = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = sp_TransferenciaSubmaqila.SACIWEB_MC_TRANSUBMAQ_BUSCAR + string.Format("{0}, {1}, {2}, '{3}', '{4}', '{5}', '{6}', '{7}'", OPCION, YEAR, MONTH, DOCUMENTO, DESDE, HASTA, USUARIO, PLANTAS);
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

        #region PROCESA TRANSFERENCIA SUBMAQUILA (ABC)

        public DataTable ProcesaTransSubMaq(int OPCION = 0, int SALIDAKEY = 0, string DOCUMENTO = "", string FECHA = "", int ALMACENKEY = 0, string OBSERVACIONES = "", string DESCARGA = "", string TIPODESCARGA = "", string Codsubmaquila = "", string NombreSubMaquila = "")
        {
            try
            {
                CadenaSQL = sp_TransferenciaSubmaqila.SACIWEB_MC_TRANSUBMAQ_DATOS + string.Format("{0}, '{1}', '{2}', {3}, '{4}', '{5}', '{6}', {7}, '{8}', '{9}'", SALIDAKEY, DOCUMENTO, FECHA, ALMACENKEY, OBSERVACIONES, DESCARGA, TIPODESCARGA, OPCION, Codsubmaquila, NombreSubMaquila); 
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


        #region PROCESA PARTIDAS DE TRANSFERENCIAS DE SUBMAQUILA (ABC)

        public DataTable ProcesaPartidasTransSubmaq(int OPCION = 0, int PSKEY = 0, int SALIDAKEY = 0, string CLAVE = "", string DESCRIPCION = "", string FRACCION = "", decimal CANTIDAD = 0,
            string UNIDAD = "", int BLOQUEADO = 0, string OBSERVACIONES = "", int PARTIDA = 0, string LOTE = "", string DIRIGIDO = "")
        {
            try
            {
                CadenaSQL = sp_TransferenciaSubmaqila.SACIWEB_MC_TRANSUBMAQ_PARTIDAS + string.Format("{0}, {1}, '{2}', '{3}', '{4}', {5}, '{6}', '{7}', '{8}', {9}, '{10}', {11}, '{12}'",
                    PSKEY, SALIDAKEY, CLAVE, DESCRIPCION, FRACCION, CANTIDAD, UNIDAD, BLOQUEADO, OBSERVACIONES, PARTIDA, LOTE, OPCION, DIRIGIDO);
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
