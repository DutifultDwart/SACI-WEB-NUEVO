using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Negocio
{
    public class RNExportaciones
    {

        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;

        #endregion

        public RNExportaciones()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }


        #region SELECCIONA LAS FECHAS DE IMPORTACIONES

        /// <summary>
        /// METODO PARA SELECCIONAR LAS FECHAS DE LAS IMPORTACIONES
        /// </summary>
        public DataTable SelectFechaExpo()
        {
            try
            {
                CadenaSQL = sp_Exportaciones.SACIWEB_MC_EXPO_AÑO_MES;
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



        #region MUESTRA LAS EXPORTACIONES POR LA FECHA SELECCIONADA


        /// <summary>
        /// METODO PARA SELECCIONAR LAS IMPORTACIONES POR FECHA
        /// </summary>
        public DataTable SelectExpoByFecha(int OPCION = 0, int YEAR = 0, int MONTH = 0, string PEDIMENTOARMADO = "", string FACTURA = "", string DESDE = "", string HASTA = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = sp_Exportaciones.SACIWEB_MC_EXPO_BUSCAR + string.Format("{0}, {1}, '{2}', '{3}', {4}, '{5}', '{6}', '{7}', '{8}'", YEAR, MONTH, PEDIMENTOARMADO, FACTURA, OPCION, DESDE, HASTA, USUARIO, PLANTAS);
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



        #region INSERTA EXPORTACIONES DATOS

        /// <summary>
        /// METODO PARA INSERTAR LOS DATOS DE LAS EXPORTACIONES ENCABEZADO
        /// </summary>
        public DataTable InsetExportaciones(int OPCIONS = 0, int SKEY = 0, string TIPOOPER = "", string ADUANA = "", string PATENTE = "", string CVE_PEDIMENTO = "", string DOCUMENTO = "", string FECHA = "", string CLIENTE = "",
                                        string PAISD = "", decimal FACTORME = 0, decimal TC = 0, decimal DTA = 0, string PAISC = "", int ALMACENKEY = 0, decimal PREV = 0, decimal CNT = 0, string PED_OR = "",
                                        string DESCARGA = "", decimal PESOB = 0, string IDENTIFICADOR = "", string OBSERVACIONES = "", string TIPODESC = "")
        {
            try
            {
                CadenaSQL = sp_Exportaciones.SACIWEB_MC_EXPO_DATOS + string.Format("{0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', {13}, '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', {22}", SKEY, TIPOOPER, ADUANA, PATENTE, CVE_PEDIMENTO, DOCUMENTO, FECHA, CLIENTE, PAISD, FACTORME, TC, DTA, PAISC, ALMACENKEY, PREV, CNT, PED_OR, DESCARGA, PESOB, IDENTIFICADOR, OBSERVACIONES, TIPODESC, OPCIONS);
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


        #region TOTALES EXPORTACIONES

        /// <summary>
        /// METODO PARA SELECCIONAR LOS TOTALES DE EXPO
        /// </summary>
        public DataTable SelectTotalExpo(int SLINK)
        {
            try
            {
                CadenaSQL = sp_Exportaciones.SACIWEB_MC_EXPO_TOTALES + string.Format("{0}", SLINK);
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

        #region INSERTA EXPORTACIONES PARTIDAS

        /// <summary>
        /// METODO PARA INSERTAR LOS DATOS DE LAS IEXPORTACIONES ENCABEZADO
        /// </summary>
        public DataTable InsetExpoPartidas(int OPCIONPS = 0, int PSKEY = 0, int SLINK = 0, string CLAVE = "", string DESC = "", string FRACCION = "", decimal CANTIDAD = 0, string UNIDAD = "", decimal VAL_PESOS = 0, decimal VAL_DOLARES = 0, decimal VAL_ME = 0, string FACTURA = "",
                                        string FECHAFACTURA = "", int PARTIDA = 0, string CLIENTE = "", decimal VAL_COMERCIAL = 0, decimal VAL_AGREGADO = 0, decimal CANTIDADT = 0, string UNIDADT = "", string COVE = "", string PAISC = "",
                                        string PAISD = "", int BLOQUEADO = 0, string LOTE = "", int ALMACEN = 0, string OBSERVACIONES = "", decimal CANTIDAD_PED = 0, string UNIDAD_PED = "",
                                        string NICO = "", string MARCA = "", string MODELO = "", string SERIE = "")
        {
            try
            {
                CadenaSQL = sp_Exportaciones.SACIWEB_MC_EXPO_PARTIDAS + string.Format("{0}, {1}, '{2}', '{3}', '{4}', {5}, '{6}', {7}, {8}, {9}, '{10}', '{11}', {12}, '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', {21}, '{22}', {23}, '{24}', {25}, '{26}', {27}, '{28}'" +
                ",'{29}','{30}','{31}'", PSKEY, SLINK, CLAVE, DESC, FRACCION, CANTIDAD, UNIDAD, VAL_PESOS, VAL_DOLARES, VAL_ME, FACTURA, FECHAFACTURA, PARTIDA, CLIENTE, VAL_COMERCIAL, VAL_AGREGADO, CANTIDADT, UNIDADT, COVE, PAISC, PAISD, BLOQUEADO, LOTE, ALMACEN, OBSERVACIONES, CANTIDAD_PED, UNIDAD_PED, OPCIONPS, NICO,
                MARCA, MODELO, SERIE);
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
