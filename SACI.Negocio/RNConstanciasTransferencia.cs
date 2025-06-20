using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SACI.Datos;
using System.Data;

namespace SACI.Negocio
{
    public class RNConstanciasTransferencia
    {
        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;

        #endregion

        public RNConstanciasTransferencia()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }

        #region SELECCIONA LAS FECHAS DE IMPORTACIONES
        public DataTable SelectPeriodoCT()
        {
            try
            {
                CadenaSQL = sp_ConstanciasTransferencia.SACIWEB_MC_CT_AÑO_MES;
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

        #region MUESTRA LAS IMPORTACIONES POR LA FECHA SELECCIONADA

        public DataTable SelectCTByFecha(int OPCION = 0, int YEAR = 0, int MONTH = 0, string DOCUMENTO = "", string DESDE = "", string HASTA = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = sp_ConstanciasTransferencia.SACIWEB_MC_CT_BUSCAR + string.Format("{0}, {1}, '{2}', '{3}', '{4}', {5}, '{6}', '{7}'", YEAR, MONTH, DOCUMENTO, DESDE, HASTA, OPCION, USUARIO, PLANTAS);
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

        #region PROCESA CONSTANCIAS DE TRANSFERENCIA (ABC)

        public DataTable ProcesaConstTranferencia(int OPCION = 0, int SALIDAKEY = 0, string DOCUMENTO = "", string FECHA = "", string FECHADESCARGA = "", string CVE_CLIENTE = "", int ALMACENKEY = 0, string OBSERVACIONES = "", string DESCARGA = "", string TIPO_DESCARGA="")
        {
            try
            {
                CadenaSQL = sp_ConstanciasTransferencia.SACIWEB_MC_CT_DATOS + string.Format("{0}, '{1}', '{2}', '{3}', '{4}', {5}, '{6}', '{7}', {8}, '{9}'", SALIDAKEY, DOCUMENTO, FECHA, FECHADESCARGA, CVE_CLIENTE, ALMACENKEY, OBSERVACIONES, DESCARGA, OPCION,TIPO_DESCARGA); 
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

        #region PROCESA PARTIDAS DE CONSTANCIAS DE TRANSFERENCIA (ABC)

        /// <summary>
        /// METODO PARA INSERTAR LOS DATOS DE LAS IMPORTACIONES ENCABEZADO
        /// </summary>
        /// 
        public DataTable ProcesaPartidasCT(int OPCION = 0, int PSKEY = 0, int SALIDAKEY = 0, string CLAVE = "", string DESCRIPCION = "", string FRACCION = "", decimal CANTIDAD = 0,
            string UNIDAD = "", string ANEXO = "", int BLOQUEADO = 0, string LOTE = "", string OBSERVACIONES = "")
        {
            try
            {
                CadenaSQL = sp_ConstanciasTransferencia.SACIWEB_MC_CT_PARTIDAS + string.Format("{0}, {1}, '{2}', '{3}', '{4}', {5}, '{6}', '{7}', {8}, '{9}', '{10}', {11}",
                    PSKEY, SALIDAKEY, CLAVE, DESCRIPCION, FRACCION, CANTIDAD, UNIDAD, ANEXO, BLOQUEADO, LOTE, OBSERVACIONES, OPCION);
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
