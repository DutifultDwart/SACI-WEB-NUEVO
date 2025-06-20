using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SACI.Negocio
{
    public class RNFacturacion
    {
        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;

        #endregion


        public RNFacturacion()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }


        #region SELECCIONA LAS FECHAS DE ACTAS DE FACTURACION
        public DataTable SelectPeriodoFact()
        {
            try
            {
                CadenaSQL = sp_Facturacion.SACIWEB_MC_FACTURACION_ANIO_MES;
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



        #region MUESTRA LAS FACTURAS POR LA FECHA SELECCIONADA
        public DataTable ConsultarFacturas_Fecha(int OPCION = 0, int YEAR = 0, int MONTH = 0, string DOCUMENTO = "", string DESDE = "", string HASTA = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = sp_Facturacion.SACIWEB_MC_FACTURAS_BUSCAR + string.Format("{0}, {1}, {2}, '{3}', '{4}', '{5}', '{6}', '{7}'", OPCION, YEAR, MONTH, DOCUMENTO, DESDE, HASTA, USUARIO, PLANTAS);
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



        #region PROCESA FACTURAS (ABC)

        public DataTable ProcesaFacturas(int OPCION = 0, int SALIDAKEY = 0, string DOCUMENTO = "", string FECHA = "", int ALMACENKEY = 0, string OBSERVACIONES = "", string DESCARGA = "", string TIPODESCARGA = "", string CFDI = "")
        {
            try
            {
                CadenaSQL = sp_Facturacion.SACIWEB_MC_FACT_DATOS + string.Format("{0}, '{1}', '{2}', {3}, '{4}', '{5}', '{6}', {7}, '{8}'", SALIDAKEY, DOCUMENTO, FECHA, ALMACENKEY, OBSERVACIONES, DESCARGA, TIPODESCARGA, OPCION, CFDI);
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


        #region PROCESA PARTIDAS DE LAS FACTURAS (ABC)

        public DataTable ProcesaPartidasFactura(int OPCION = 0, int PSKEY = 0, int SALIDAKEY = 0, string CLAVE = "", string DESCRIPCION = "", string FRACCION = "", decimal CANTIDAD = 0,
            string UNIDAD = "", int BLOQUEADO = 0, string OBSERVACIONES = "", int PARTIDA = 0, string LOTE = "")
        {
            try
            {
                CadenaSQL = sp_Facturacion.SACIWEB_MC_FACT_PARTIDAS + string.Format("{0}, {1}, '{2}', '{3}', '{4}', {5}, '{6}', '{7}', '{8}', {9}, '{10}', {11}",
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
        public DataTable ImportaFactura(DataTable dtAcdes)
        {
            try
            {
                return _ObjetoDBSQL.ConectarEstructFactura(sp_Interfaces.SACIWEB_IMPORTA_FACTURAS, dtAcdes);
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


        #region PROCESA FACTURAS

        public DataTable Guardar_FACT(string UNIQUE = "")
        {
            try
            {
                CadenaSQL = sp_Interfaces.SACIWEB_INTERFACE_CARGA_FACTURAS + string.Format("'{0}'", UNIQUE);
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



        #region IMPOPRTA EL ARCHIVO SERVICIOS

        /// <summary>
        /// METODO PARA IMPORTAR EL ARCHIVO DE ACTAS DE DESTRUCCION SERVICIOS
        /// </summary>
        public DataTable ImportaFacturaServicios(DataTable dtAcdes)
        {
            try
            {
                return _ObjetoDBSQL.ConectarEstructFacturaServicios(sp_Interfaces.SACIWEB_IMPORTA_FACTURAS_SERVICIOS, dtAcdes);
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

        #region PROCESA FACTURAS SERVICIOS

        public DataTable Guardar_FACT_SERV(string UNIQUE = "")
        {
            try
            {
                CadenaSQL = sp_Interfaces.SACIWEB_INTERFACE_CARGA_FACTURAS_SERVICIOS + string.Format("'{0}'", UNIQUE);
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


        #region SELECCIONA LAS FECHAS DE FACTURACION SERVICIOS
        public DataTable SelectPeriodoFact_Servicios()
        {
            try
            {
                CadenaSQL = sp_Facturacion.SACIWEB_MC_FACTURASERV_ANIO_MES;
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

        #region MUESTRA LAS FACTURAS DE SERVICIOS POR LA FECHA SELECCIONADA
        public DataTable ConsultarFacturasServ_Fecha(int OPCION = 0, int YEAR = 0, int MONTH = 0, string DOCUMENTO = "", string DESDE = "", string HASTA = "")
        {
            try
            {
                CadenaSQL = sp_Facturacion.SACIWEB_MC_FACTURASERV_BUSCAR + string.Format("{0}, {1}, {2}, '{3}', '{4}', '{5}' ", OPCION, YEAR, MONTH, DOCUMENTO, DESDE, HASTA); ;
                //
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

        #region PROCESA FACTURAS SERVICIOS (ABC)

        public DataTable ProcesaFacturas_Serv(int OPCION = 0, int SALIDAKEY = 0, string DOCUMENTO = "", string FECHA = "", int ALMACENKEY = 0, string OBSERVACIONES = "", string DESCARGA = "", string TIPODESCARGA = "",
            string CLIENTE = "", string PAIS_COMPRADOR = "", string PAIS_DESTINO = "", decimal TIPO_CAMBIO = 0, string VIGENTE = "", string SERIE = "",
            string FOLIO = "", decimal SUBTOTAL = 0, decimal TOTAL = 0, decimal DESCUENTO = 0, decimal IVA = 0, decimal TASA = 0)
        {
            try
            {
                CadenaSQL = sp_Facturacion.SACIWEB_MC_FACTURASERV_DATOS + string.Format("{0}, '{1}', '{2}', {3}, '{4}', '{5}', '{6}', {7}, " +
                "'{8}','{9}','{10}',{11},'{12}','{13}','{14}',{15},{16},{17},{18},{19}", SALIDAKEY, DOCUMENTO, FECHA, ALMACENKEY, OBSERVACIONES, DESCARGA, TIPODESCARGA, OPCION,
               CLIENTE, PAIS_COMPRADOR, PAIS_DESTINO, TIPO_CAMBIO, VIGENTE, SERIE, FOLIO, SUBTOTAL, TOTAL, DESCUENTO, IVA, TASA);
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

        #region PROCESA PARTIDAS DE LAS FACTURAS SERVICIOS (ABC)

        public DataTable ProcesaPartidasFactura_Serv(int OPCION = 0, int PSKEY = 0, int SALIDAKEY = 0, string CLAVE = "", string DESCRIPCION = "", string FRACCION = "", decimal CANTIDAD = 0,
            string UNIDAD = "", int BLOQUEADO = 0, string OBSERVACIONES = "", int PARTIDA = 0, string LOTE = "", decimal VALOR_PESOS = 0, decimal VALOR_DOLARES = 9)
        {
            try
            {
                CadenaSQL = sp_Facturacion.SACIWEB_MC_FACTSERV_PARTIDAS + string.Format("{0}, {1}, '{2}', '{3}', '{4}', {5}, '{6}', '{7}', '{8}', {9}, '{10}', {11}, {12}, {13}",
                    PSKEY, SALIDAKEY, CLAVE, DESCRIPCION, FRACCION, CANTIDAD, UNIDAD, BLOQUEADO, OBSERVACIONES, PARTIDA, LOTE, OPCION, VALOR_PESOS, VALOR_DOLARES);
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
