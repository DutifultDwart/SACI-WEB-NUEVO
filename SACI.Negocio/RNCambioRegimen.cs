using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Negocio
{
    public class RNCambioRegimen
    {

        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;

        #endregion

        public RNCambioRegimen()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }

        #region SELECCIONA LAS FECHAS DE IMPORTACIONES

        /// <summary>
        /// METODO PARA SELECCIONAR LAS FECHAS DE LAS IMPORTACIONES
        /// </summary>
        public DataTable SelectFechaCReg()
        {
            try
            {
                CadenaSQL = sp_CambioRegimen.SACIWEB_MC_CR_AÑO_MES;
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


        public DataTable SelectFechaCRegularizaciones()
        {
            try
            {
                CadenaSQL = sp_CambioRegimen.SACIWEB_MC_CREG_AÑO_MES;
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
        public DataTable SelectCRegByFecha(int OPCION = 0, int YEAR = 0, int MONTH = 0, string PEDIMENTOARMADO = "", string ADUANA = "", string PATENTE = "", string DESDE = "", string HASTA = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = sp_CambioRegimen.SACIWEB_MC_CR_BUSCAR + string.Format("{0}, {1}, '{2}', '{3}', '{4}', {5}, '{6}', '{7}', '{8}', '{9}'", YEAR, MONTH, PEDIMENTOARMADO, ADUANA, PATENTE, OPCION, DESDE, HASTA, USUARIO, PLANTAS);
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


        public DataTable SelectCRegByFechaRegularizaciones(int OPCION = 0, int YEAR = 0, int MONTH = 0, string PEDIMENTOARMADO = "", string FACTURA = "", string DESDE = "", string HASTA = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = sp_CambioRegimen.SACIWEB_MC_CREG_BUSCAR + string.Format("{0}, {1}, '{2}', '{3}', {4}, '{5}', '{6}', '{7}', '{8}'", YEAR, MONTH, PEDIMENTOARMADO, FACTURA, OPCION, DESDE, HASTA, USUARIO, PLANTAS);
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

        #region INSERTA CABIO REGIMEN DATOS

        /// <summary>
        /// METODO PARA INSERTAR LOS DATOS DE LAS EXPORTACIONES ENCABEZADO
        /// </summary>
        public DataTable InsetCambioRegimen(int OPCIONS = 0, int SKEY = 0, string TIPOOPER = "", string ADUANA = "", string PATENTE = "", string CVE_PEDIMENTO = "", string DOCUMENTO = "", string FECHA = "", string PROVEEDOR = "",
                                        string PAISD = "", decimal FACTORME = 0, decimal TC = 0, decimal DTA = 0, string PAISC = "", int ALMACENKEY = 0, decimal PREV = 0, decimal CNT = 0, decimal MULTAS = 0, decimal RECARGOS = 0, decimal IVA = 0, decimal IGIE = 0, string PED_OR = "",
                                        string DESCARGA = "", decimal PESOB = 0, string IDENTIFICADOR = "", string OBSERVACIONES = "", string TIPODESC = "")
        {
            try
            {
                CadenaSQL = sp_CambioRegimen.SACIWEB_MC_CR_DATOS + string.Format("{0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', {13}, '{14}', '{15}', {16}, {17}, {18}, {19} , '{20}', '{21}', '{22}', '{23}', '{24}', '{25}', {26}", SKEY, TIPOOPER, ADUANA, PATENTE, CVE_PEDIMENTO, DOCUMENTO, FECHA, PROVEEDOR, PAISD, FACTORME, TC, DTA, PAISC, ALMACENKEY, PREV, CNT, MULTAS, RECARGOS, IVA, IGIE, PED_OR, DESCARGA, PESOB, IDENTIFICADOR, OBSERVACIONES, TIPODESC, OPCIONS);
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

        public DataTable InsetRegularizaciones(int OPCIONS = 0, int SKEY = 0, string TIPOOPER = "", string ADUANA = "", string PATENTE = "", string CVE_PEDIMENTO = "", string DOCUMENTO = "", string FECHA = "", string PROVEEDOR = "",
                                    string PAISD = "", decimal FACTORME = 0, decimal TC = 0, decimal DTA = 0, string PAISC = "", int ALMACENKEY = 0, decimal PREV = 0, decimal CNT = 0, decimal MULTAS = 0, decimal RECARGOS = 0, decimal IVA = 0, decimal IGIE = 0, string PED_OR = "",
                                    string DESCARGA = "", decimal PESOB = 0, string IDENTIFICADOR = "", string OBSERVACIONES = "", string TIPODESC = "", string CVE_A31 = "")
        {
            try
            {
                CadenaSQL = sp_CambioRegimen.SACIWEB_MC_CREG_DATOS + string.Format("{0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', {13}, '{14}', '{15}', {16}, {17}, {18}, {19} , '{20}', '{21}', '{22}', '{23}', '{24}', '{25}', {26},'{27}'",
                    SKEY, TIPOOPER, ADUANA, PATENTE, CVE_PEDIMENTO, DOCUMENTO, FECHA, PROVEEDOR, PAISD, FACTORME, TC, DTA, PAISC, ALMACENKEY, PREV, CNT, MULTAS, RECARGOS, IVA, IGIE, PED_OR, DESCARGA, PESOB, IDENTIFICADOR, OBSERVACIONES, TIPODESC, OPCIONS, CVE_A31);
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


        #region INSERTA CAMBIO REGIMEN PARTIDAS

        /// <summary>
        /// METODO PARA INSERTAR LOS DATOS DE LOS CAMBIOS DE REGIMEN PARTIDAS
        /// </summary>
        public DataTable InsetCambioRegimenPartidas(int OPCIONPS = 0, int PSKEY = 0, int SLINK = 0, string CLAVE = "", string DESC = "", string FRACCION = "", decimal CANTIDAD = 0, string UNIDAD = "", decimal VAL_PESOS = 0, decimal VAL_DOLARES = 0, decimal VAL_ME = 0,
                                     decimal VAL_ADUANA = 0, decimal CANTIDADT = 0, string UNIDADT = "", string PAISC = "", string PAISD = "", string FACTURA = "", string FECHAFACTURA = "", decimal MONTOIGIE = 0, decimal FPIGIE = 0, decimal MONTOIVA = 0, decimal FPIVA = 0,
                                     decimal TASACC = 0, decimal MONTOCC = 0, decimal FPCC = 0, decimal MULTA = 0, string COVE = "", decimal SEGUROS = 0, decimal FLETES = 0, decimal EMBALAJES = 0, decimal OTROS = 0, string INCOTERM = "", int PARTIDA = 0, string LOTE = "",
                                     int ALMACEN = 0, string DIRIGIDO = "", string OBSERVACIONES = "", string NICO = "", string MARCA = "", string MODELO = "", string SERIE = "")
        {
            try
            {
                CadenaSQL = sp_CambioRegimen.SACIWEB_MC_CR_PARTIDAS + string.Format("{0}, {1}, '{2}', '{3}', '{4}', {5}, '{6}', {7}, {8}, {9}, {10}, {11}, '{12}', '{13}', '{14}', '{15}', '{16}', {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, '{25}', {26}, {27}, {28}, {29}, '{30}', {31}, '{32}', {33}, '{34}', '{35}', {36}, '{37}'" +
                ",'{38}','{39}','{40}'", PSKEY, SLINK, CLAVE, DESC, FRACCION, CANTIDAD, UNIDAD, VAL_PESOS, VAL_DOLARES, VAL_ME, VAL_ADUANA, CANTIDADT, UNIDADT, PAISC, PAISD, FACTURA, FECHAFACTURA, MONTOIGIE, FPIGIE, MONTOIVA, FPIVA, TASACC, MONTOCC, FPCC, MULTA, COVE, SEGUROS, FLETES, EMBALAJES, OTROS, INCOTERM, PARTIDA, LOTE, ALMACEN, DIRIGIDO, OBSERVACIONES, OPCIONPS, NICO,
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


        public DataTable InsetRegularizacionesPartidas(int OPCIONPS = 0, int PSKEY = 0, int SLINK = 0, string CLAVE = "", string DESC = "", string FRACCION = "", decimal CANTIDAD = 0, string UNIDAD = "", decimal VAL_PESOS = 0, decimal VAL_DOLARES = 0, decimal VAL_ME = 0,
                                     decimal VAL_ADUANA = 0, decimal CANTIDADT = 0, string UNIDADT = "", string PAISC = "", string PAISD = "", string FACTURA = "", string FECHAFACTURA = "", decimal MONTOIGIE = 0, decimal FPIGIE = 0, decimal MONTOIVA = 0, decimal FPIVA = 0,
                                     decimal TASACC = 0, decimal MONTOCC = 0, decimal FPCC = 0, decimal MULTA = 0, string COVE = "", decimal SEGUROS = 0, decimal FLETES = 0, decimal EMBALAJES = 0, decimal OTROS = 0, string INCOTERM = "", int PARTIDA = 0, string LOTE = "",
                                     int ALMACEN = 0, string DIRIGIDO = "", string OBSERVACIONES = "", string NICO = "", string MARCA = "", string MODELO = "", string SERIE = "")
        {
            try
            {
                CadenaSQL = sp_CambioRegimen.SACIWEB_MC_CREG_PARTIDAS + string.Format("{0}, {1}, '{2}', '{3}', '{4}', {5}, '{6}', {7}, {8}, {9}, {10}, {11}, '{12}', '{13}', '{14}', '{15}', '{16}', {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, '{25}', {26}, {27}, {28}, {29}, '{30}', {31}, '{32}', {33}, '{34}', '{35}', {36}," +
                "'{37}', '{38}', '{39}', '{40}'", PSKEY, SLINK, CLAVE, DESC, FRACCION, CANTIDAD, UNIDAD, VAL_PESOS, VAL_DOLARES, VAL_ME, VAL_ADUANA, CANTIDADT, UNIDADT, PAISC, PAISD, FACTURA, FECHAFACTURA, MONTOIGIE, FPIGIE, MONTOIVA, FPIVA, TASACC, MONTOCC, FPCC, MULTA, COVE, SEGUROS, FLETES, EMBALAJES, OTROS, INCOTERM, PARTIDA, LOTE, ALMACEN, DIRIGIDO, OBSERVACIONES, OPCIONPS,
                NICO, MARCA, MODELO, SERIE);
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
        public DataTable SelectTotalCR(int SLINK)
        {
            try
            {
                CadenaSQL = sp_CambioRegimen.SACIWEB_MC_CR_TOTALES + string.Format("{0}", SLINK);
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



        public DataTable SelectTotalRegularizaciones(int SLINK)
        {
            try
            {
                CadenaSQL = sp_CambioRegimen.SACIWEB_MC_CREG_TOTALES + string.Format("{0}", SLINK);
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
