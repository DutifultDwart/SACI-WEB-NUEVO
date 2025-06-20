using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Negocio
{
    public class RNImportaciones
    {
        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;

        #endregion


        public RNImportaciones()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }



        #region SELECCIONA LAS FECHAS DE IMPORTACIONES

        /// <summary>
        /// METODO PARA SELECCIONAR LAS FECHAS DE LAS IMPORTACIONES
        /// </summary>
        public DataTable SelectFechaImpo()
        {
            try
            {
                CadenaSQL = sp_Importaciones.SACIWEB_MC_IMPO_AÑO_MES;
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


        /// <summary>
        /// METODO PARA SELECCIONAR LAS IMPORTACIONES POR FECHA
        /// </summary>
        public DataTable SelectImpoByFecha(int OPCION = 0, int YEAR = 0, int MONTH = 0, string PEDIMENTOARMADO = "", string FACTURA = "", string DESCRIPCION = "", string FRACCION = "", string DESDE = "", string HASTA = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = sp_Importaciones.SACIWEB_MC_IMPO_BUSCAR + string.Format("{0}, {1}, '{2}', '{3}', '{4}', '{5}', {6}, '{7}', '{8}', '{9}', '{10}'", YEAR, MONTH, PEDIMENTOARMADO, FACTURA, DESCRIPCION, FRACCION, OPCION, DESDE, HASTA, USUARIO, PLANTAS);
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



        #region SELECCIONA LAS CLAVES DE PEDIMENTO

        /// <summary>
        /// METODO PARA SELECCIONAR LAS FECHAS DE LAS IMPORTACIONES
        /// </summary>
        public DataTable SelectCvePedimento()
        {
            try
            {
                CadenaSQL = sp_Importaciones.SACIWEB_SEL_CVE_PEDIMIENTO;
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



        #region INSERTA IMPORTACIONES DATOS

        /// <summary>
        /// METODO PARA INSERTAR LOS DATOS DE LAS IMPORTACIONES ENCABEZADO
        /// </summary>
        public DataTable InsetImportaciones(int OPCIONI = 0, int IKEY = 0, string TIPOOPER = "", string CVE_PEDIMENTO = "", string ADUANA = "", string PATENTE = "", string NUMERO_PED = "", decimal TC = 0, decimal FME = 0, string FECHA = "",
                                        string CVE_PROVEEDOR = "", string TRATADO = "", decimal FACTOR_INC = 0, decimal IGI = 0, decimal DTA = 0, decimal IVA = 0, decimal PREV = 0, decimal CNT = 0, decimal MULTAS = 0,
                                        decimal RECARGOS = 0, string PED_ORG = "", string DESCARGA = "", string TRACKING = "", string TRANSPORTISTA = "", string FECHA_ENTRADA = "", string IDENT = "", string OBSERVACIONES = "",
                                        string LOTE = "", decimal PESOBRUTO = 0, int ALMACENKEY = 0, decimal IVA_PRE = 0,
                                        decimal FLETES = 0, decimal SEGUROS = 0, decimal EMBALAJES = 0, decimal INCREMENTABLES = 0)
        {
            try
            {
                CadenaSQL = sp_Importaciones.SACIWEB_MC_IMPO_DATOS + string.Format("{0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, '{8}', '{9}', '{10}', {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, '{19}', '{20}', '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', {28}, {29}, {30}" +
                    ",{31},{32},{33},{34}", IKEY, TIPOOPER, CVE_PEDIMENTO, ADUANA, PATENTE, NUMERO_PED, TC, FME, FECHA, CVE_PROVEEDOR, TRATADO, FACTOR_INC, IGI, DTA, IVA, PREV, CNT, MULTAS, RECARGOS, PED_ORG, DESCARGA, TRACKING, TRANSPORTISTA, FECHA_ENTRADA, IDENT, OBSERVACIONES, LOTE, PESOBRUTO, 
                    ALMACENKEY, OPCIONI, IVA_PRE,
                    FLETES, SEGUROS, EMBALAJES, INCREMENTABLES);
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




        #region INSERTA IMPORTACIONES PARTIDAS

        /// <summary>
        /// METODO PARA INSERTAR LOS DATOS DE LAS IMPORTACIONES ENCABEZADO
        /// </summary>
        public DataTable InsetImpoPartidas(int OPCIONP = 0, int PKEY = 0, int ILINK = 0, string CLAVE = "", string DESC = "", string FRACCION = "", decimal CANTIDAD = 0, string UNIDAD = "", decimal CANTIDADT = 0, string UNIDADT = "",
                                     string PORIGEN = "", string PVENDEDOR = "", string CATEGORIA = "", string PROVEEDOR = "", decimal VAL_DOLARES = 0, decimal VAL_COMERCIAL = 0, decimal VAL_ADUANAL = 0, decimal VAL_ME = 0,
                                        decimal ARANCEL = 0, decimal MONTOIGI = 0, decimal FPIGI = 0, string TTIGI = "", decimal TASAIVA = 0, decimal MONTOIVA = 0, decimal FPIVA = 0, string PERMISO = "", string FRACCIONPERMISO = "",
                                        decimal TASACC = 0, decimal MONTOCC = 0, decimal FPCC = 0, string COVE = "", decimal CANTIDADPEDIMENTO = 0, string UNIDADPEDIMENTO = "", string MARCA = "", string MODELO = "",
                                        string SERIE = "", string ESACTIVO = "", string LOTE = "", string PIDENT = "", string FACTURA = "", string FECHAFACTURA = "", int ALMACEN = 0, int PARTIDA = 0, string INCOTERM = "",
                                        string OBSERVACIONES = "", string C1 = "", string C2 = "", string C3 = "", string NICO = "", string FECHA_REMESA = "")
        {
            try
            {
                CadenaSQL = sp_Importaciones.SACIWEB_MC_IMPO_PARTIDAS + string.Format("{0}, {1}, '{2}', '{3}', '{4}', {5}, '{6}', {7}, '{8}', '{9}', '{10}', '{11}', '{12}', {13}, {14}, {15}, {16}, {17}, {18}, {19}, '{20}', {21}, {22}, {23}, '{24}', '{25}', {26}, {27}, {28}, '{29}', {30}, '{31}', '{32}', '{33}', '{34}', '{35}', '{36}', '{37}', '{38}', '{39}', {40}, {41}, '{42}', '{43}', '{44}', '{45}', '{46}', {47},'{48}','{49}'", 
                    PKEY, ILINK, CLAVE, DESC, FRACCION, CANTIDAD, UNIDAD, CANTIDADT, UNIDADT, PORIGEN, PVENDEDOR, CATEGORIA, PROVEEDOR, VAL_DOLARES, VAL_COMERCIAL, VAL_ADUANAL, VAL_ME, ARANCEL, MONTOIGI, FPIGI, TTIGI, TASAIVA, MONTOIVA, FPIVA, PERMISO, FRACCIONPERMISO, TASACC, MONTOCC, FPCC, COVE, CANTIDADPEDIMENTO, UNIDADPEDIMENTO, MARCA, MODELO, SERIE, ESACTIVO, LOTE, PIDENT, FACTURA, FECHAFACTURA, ALMACEN, PARTIDA, INCOTERM, OBSERVACIONES, C1, C2, C3, OPCIONP, NICO, FECHA_REMESA);
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



        #region TOTALES IMPORTACIONES

        /// <summary>
        /// METODO PARA SELECCIONAR LAS IMPORTACIONES
        /// </summary>
        public DataTable SelectTotalImpo(int ILINK)
        {
            try
            {
                CadenaSQL = sp_Importaciones.SACIWEB_MC_IMPO_TOTALES + string.Format("{0}", ILINK);
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



        /////// ACTIVO FIJO 



        #region SELECCIONA LAS CLAVES DE PEDIMENTO

        /// <summary>
        /// METODO PARA SELECCIONAR LAS FECHAS DE LAS IMPORTACIONES
        /// </summary>
        public DataTable SelectCvePedimentoAF()
        {
            try
            {
                CadenaSQL = sp_ActivoFijo.SACIWEB_SEL_CVE_PEDIMIENTO;
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



        #region SELECCIONA LAS FECHAS DE IMPORTACIONES

        /// <summary>
        /// METODO PARA SELECCIONAR LAS FECHAS DE LAS IMPORTACIONES
        /// </summary>
        public DataTable SelectFechaImpoAS()
        {
            try
            {
                CadenaSQL = sp_ActivoFijo.SACIWEB_MC_AF_AÑO_MES;
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


        /// <summary>
        /// METODO PARA SELECCIONAR LAS IMPORTACIONES POR FECHA
        /// </summary>
        public DataTable SelectImpoByFechaAF(int OPCION = 0, int YEAR = 0, int MONTH = 0, string PEDIMENTOARMADO = "", string DESDE = "", string HASTA = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = sp_ActivoFijo.SACIWEB_MC_AF_BUSCAR + string.Format("{0}, {1}, '{2}', {3},'{4}','{5}','{6}','{7}'", YEAR, MONTH, PEDIMENTOARMADO, OPCION, DESDE, HASTA, USUARIO, PLANTAS);
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





        #region INSERTA IMPORTACIONES DATOS AF

        /// <summary>
        /// METODO PARA INSERTAR LOS DATOS DE LAS IMPORTACIONES ENCABEZADO
        /// </summary>
        public DataTable InsetImportacionesAF(int OPCIONI = 0, int IKEY = 0, string TIPOOPER = "", string CVE_PEDIMENTO = "", string ADUANA = "", string PATENTE = "", string NUMERO_PED = "", decimal TC = 0, decimal FME = 0, string FECHA = "",
                                        string CVE_PROVEEDOR = "", string TRATADO = "", decimal FACTOR_INC = 0, decimal IGI = 0, decimal DTA = 0, decimal IVA = 0, decimal PREV = 0, decimal CNT = 0, string PED_ORG = "", string DESCARGA = "",
                                        string TRACKING = "", string TRANSPORTISTA = "", string FECHA_ENTRADA = "", string PERMISO = "", string IDENT = "", string OBSERVACIONES = "",
                                        string LOTE = "", int ALMACENKEY = 0, decimal IVA_PRE = 0)
        {
            try
            {
                CadenaSQL = sp_ActivoFijo.SACIWEB_MC_AF_DATOS + string.Format("{0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, '{8}', '{9}', '{10}', {11}, {12}, {13}, {14}, {15}, {16}, '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '{24}', '{25}', {26}, {27}, {28}", IKEY, TIPOOPER, CVE_PEDIMENTO, ADUANA, PATENTE, NUMERO_PED, TC, FME, FECHA, CVE_PROVEEDOR, TRATADO, FACTOR_INC, IGI, DTA, IVA, PREV, CNT, PED_ORG, DESCARGA, TRACKING, TRANSPORTISTA, FECHA_ENTRADA, PERMISO, IDENT, OBSERVACIONES, LOTE, ALMACENKEY, OPCIONI, IVA_PRE);
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





        #region TOTALES IMPORTACIONES AF

        /// <summary>
        /// METODO PARA SELECCIONAR LAS IMPORTACIONES
        /// </summary>
        public DataTable SelectTotalImpoAF(int ILINK)
        {
            try
            {
                CadenaSQL = sp_ActivoFijo.SACIWEB_MC_AF_TOTALES + string.Format("{0}", ILINK);
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




        #region INSERTA IMPORTACIONES PARTIDAS AF

        /// <summary>
        /// METODO PARA INSERTAR LOS DATOS DE LAS IMPORTACIONES ENCABEZADO
        /// </summary>
        public DataTable InsetImpoPartidasAF(int OPCIONP = 0, int PKEY = 0, int ILINK = 0, string CLAVE = "", string DESC = "", string FRACCION = "", decimal CANTIDAD = 0, string UNIDAD = "", decimal CANTIDADT = 0, string UNIDADT = "",
                                     string PORIGEN = "", string PROVEEDOR = "", string CATEGORIA = "", decimal VAL_DOLARES = 0, decimal VAL_COMERCIAL = 0, decimal VAL_ADUANAL = 0, decimal VAL_ME = 0,
                                        decimal ARANCEL = 0, decimal MONTOIGI = 0, decimal FPIGI = 0, string TTIGI = "", string PVENDEDOR = "", decimal TASAIVA = 0, decimal MONTOIVA = 0, decimal FPIVA = 0, decimal TASACC = 0, decimal MONTOCC = 0,
                                        decimal FPCC = 0, decimal RECARGOS = 0, string MARCA = "", string MODELO = "", string SERIE = "", string ESACTIVO = "", string LOTE = "", string PIDENT = "", string FACTURA = "",
                                        string FECHAFACTURA = "", int ALMACEN = 0, string COVE = "", int PARTIDA = 0, string OBSERVACIONES = "", string PERMISO = "", string FRACCIONPERMISO = "", string COMPLEMENTO1 = "", string COMPLEMENTO2 = "", string COMPLEMENTO3 = "", string NICO = "")
        {
            try
            {
                CadenaSQL = sp_ActivoFijo.SACIWEB_MC_AF_PARTIDAS + string.Format("{0}, {1}, '{2}', '{3}', '{4}', {5}, '{6}', {7}, '{8}', '{9}', '{10}', '{11}', {12}, {13}, {14}, {15}, {16}, {17}, {18}, '{19}', '{20}', {21}, {22}, {23}, {24}, {25}, {26}, {27}, '{28}', '{29}', '{30}', '{31}', '{32}', '{33}', '{34}', '{35}', {36}, '{37}', {38}, '{39}', {40}, '{41}', '{42}', '{43}', '{44}', '{45}', '{46}'", PKEY, ILINK, CLAVE, DESC, FRACCION, CANTIDAD, UNIDAD, CANTIDADT, UNIDADT, PORIGEN, PVENDEDOR, CATEGORIA, VAL_DOLARES, VAL_COMERCIAL, VAL_ADUANAL, VAL_ME,
                    ARANCEL, MONTOIGI, FPIGI, TTIGI, PROVEEDOR, TASAIVA, MONTOIVA, FPIVA, TASACC, MONTOCC, FPCC, RECARGOS, MARCA, MODELO, SERIE, ESACTIVO, LOTE, PIDENT, FACTURA, FECHAFACTURA, ALMACEN, COVE, PARTIDA, OBSERVACIONES, OPCIONP, PERMISO, FRACCIONPERMISO, COMPLEMENTO1, COMPLEMENTO2, COMPLEMENTO3, NICO);
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


        #region INTERFACE PEDIMENTO - IMPORTAR ARCHIVO PEDIMENTOS

        /// <summary>
        /// METODO PARA IMPORTAR EL ARCHIVO DE PEDIMENTOS
        /// </summary>
        public DataTable ImportaPedimentos(DataTable dtPedimentos)
        {
            try
            {
                return _ObjetoDBSQL.ConectarEstructPedimentos(sp_Interfaces.SACIWEB_IMPORTA_PEDIMENTOS, dtPedimentos);
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

        #region INTERFACE PEDIMENTO - CARGAR PEDIMENTOS

        public DataTable Guardar_Pedimentos()
        {
            try
            {
                CadenaSQL = sp_Interfaces.SACIWEB_CARGAPEDIMENTOS;
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
