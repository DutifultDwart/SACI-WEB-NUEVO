using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SACI.Negocio
{
    public class RNReportes
    {
        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;

        #endregion


        public RNReportes()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }




        #region REPORTE SALDOS

        /// <summary>
        /// REPORTE SALDOS 
        /// </summary>
        public DataTable RptSaldos(string MATERIAL = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = Informes.SACIWEB_MS_SALDOS + string.Format("'{0}', '{1}', '{2}'", MATERIAL, USUARIO, PLANTAS);
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




        #region REPORTE SALDOS FECHA

        /// <summary>
        /// REPORTE SALDOS 
        /// </summary>
        public DataTable RptSaldosFecha(string Fecha, string MATERIAL = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = Informes.SACIWEB_MS_SALDOS_FECHA + string.Format("'{0}', '{1}', '{2}', '{3}'", Fecha, MATERIAL, USUARIO, PLANTAS);
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


        #region REPORTE IMPORTACIONES



        /// <summary>
        /// REPORTE IMPORTACIONES
        /// </summary>
        public DataTable RptImportaciones(DateTime? DESDE = null, DateTime? HASTA = null, string documento = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                #region FORMATO FECHAS

                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    v_fecha_desde = anio + mes + dia;
                }

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    v_fecha_hasta = anio + mes + dia;
                }

                #endregion

                CadenaSQL = Informes.SACIWEB_MS_IMPORTACIONES + string.Format("'{0}', '{1}', '{2}', '{3}', '{4}'", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta, documento, USUARIO, PLANTAS);
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

        public DataTable RptImportaciones_Paginado(DateTime? DESDE = null, DateTime? HASTA = null, string documento = "", int pagina = 1)
        {
            try
            {
                #region FORMATO FECHAS

                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    v_fecha_desde = anio + mes + dia;
                }

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    v_fecha_hasta = anio + mes + dia;
                }

                #endregion

                CadenaSQL = Informes.SACIWEB_MS_IMPORTACIONES_PAGINADO + string.Format("'{0}', '{1}', '{2}', {3}", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta, documento, pagina);
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

        public DataTable RptIFCF4CTMA(string DESDE, string HASTA)
        {
            try
            {
                #region FORMATO FECHAS

                //string dia = string.Empty;
                //string mes = string.Empty;
                //string anio = string.Empty;
                //string v_fecha_desde = string.Empty;
                //string v_fecha_hasta = string.Empty;

                //if (DESDE != null)
                //{
                //    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                //    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                //    anio = DESDE.Value.Year.ToString();
                //    v_fecha_desde = dia + "/" + mes + "/" + anio;
                //}

                //if (HASTA != null)
                //{
                //    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                //    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                //    anio = HASTA.Value.Year.ToString();
                //    v_fecha_hasta = dia + "/" + mes + "/" + anio;
                //}

                #endregion

                //CadenaSQL = Informes.SACIWEB_CONSOLIDA_F4CTMA + string.Format("'{0}', '{1}'", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta);
                CadenaSQL = Informes.SACIWEB_CONSOLIDA_F4CTMA + string.Format("'{0}', '{1}'", DESDE, HASTA);
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

        public DataTable RptIFCCTM()
        {
            try
            {
                CadenaSQL = Informes.SACIWEB_CTMFACTURA_CONSOLIDADO;
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

        public DataTable RptIFCHDE()
        {
            try
            {
                CadenaSQL = Informes.SACIWEB_CONSOLIDA_HDE;
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

        public DataTable RptIFCSAL()
        {
            try
            {
                CadenaSQL = Informes.SACIWEB_CONSOLIDA_SALDOS;
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

        public DataTable RptIFCMAT(string DESDE, string HASTA)
        {
            try
            {
                #region FORMATO FECHAS

                //string dia = string.Empty;
                //string mes = string.Empty;
                //string anio = string.Empty;
                //string v_fecha_desde = string.Empty;
                //string v_fecha_hasta = string.Empty;

                //if (DESDE != null)
                //{
                //    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                //    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                //    anio = DESDE.Value.Year.ToString();
                //    v_fecha_desde = dia + "/" + mes + "/" + anio;
                //}

                //if (HASTA != null)
                //{
                //    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                //    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                //    anio = HASTA.Value.Year.ToString();
                //    v_fecha_hasta = dia + "/" + mes + "/" + anio;
                //}

                #endregion

                //CadenaSQL = Informes.SACIWEB_CONSOLIDA_MATERIAL + string.Format("'{0}', '{1}'", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta);
                CadenaSQL = Informes.SACIWEB_CONSOLIDA_MATERIAL + string.Format("'{0}', '{1}'", DESDE, HASTA);
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

        public DataTable RptIFCPROD(string DESDE, string HASTA)
        {
            try
            {
                #region FORMATO FECHAS

                //string dia = string.Empty;
                //string mes = string.Empty;
                //string anio = string.Empty;
                //string v_fecha_desde = string.Empty;
                //string v_fecha_hasta = string.Empty;

                //if (DESDE != null)
                //{
                //    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                //    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                //    anio = DESDE.Value.Year.ToString();
                //    v_fecha_desde = dia + "/" + mes + "/" + anio;
                //}

                //if (HASTA != null)
                //{
                //    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                //    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                //    anio = HASTA.Value.Year.ToString();
                //    v_fecha_hasta = dia + "/" + mes + "/" + anio;
                //}

                #endregion

                //CadenaSQL = Informes.SACIWEB_CONSOLIDA_PRODUCTOS + string.Format("'{0}', '{1}'", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta);
                CadenaSQL = Informes.SACIWEB_CONSOLIDA_PRODUCTOS + string.Format("'{0}', '{1}'", DESDE, HASTA);
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

        public DataTable RptIFCESTR(string DESDE, string HASTA)
        {
            try
            {
                #region FORMATO FECHAS

                //string dia = string.Empty;
                //string mes = string.Empty;
                //string anio = string.Empty;
                //string v_fecha_desde = string.Empty;
                //string v_fecha_hasta = string.Empty;

                //if (DESDE != null)
                //{
                //    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                //    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                //    anio = DESDE.Value.Year.ToString();
                //    v_fecha_desde = dia + "/" + mes + "/" + anio;
                //}

                //if (HASTA != null)
                //{
                //    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                //    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                //    anio = HASTA.Value.Year.ToString();
                //    v_fecha_hasta = dia + "/" + mes + "/" + anio;
                //}

                #endregion

                //CadenaSQL = Informes.SACIWEB_CONSOLIDA_ESTRUCTURAS + string.Format("'{0}', '{1}'", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta);
                CadenaSQL = Informes.SACIWEB_CONSOLIDA_ESTRUCTURAS + string.Format("'{0}', '{1}'", DESDE, HASTA);
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


        #region REPORTE EXPORTACIONES



        /// <summary>
        /// REPORTE EXPORTACIONES
        /// </summary>
        public DataTable RptExportaciones(DateTime? DESDE = null, DateTime? HASTA = null, string pedimento = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                #region FORMATO FECHAS

                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    v_fecha_desde = anio + mes + dia;
                }

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    v_fecha_hasta = anio + mes + dia;
                }

                #endregion

                CadenaSQL = Informes.SACIWEB_MS_EXPORTACIONES + string.Format("'{0}', '{1}', '{2}', '{3}', '{4}'", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta, pedimento, USUARIO, PLANTAS);
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

        public DataTable RptExportaciones_Paginado(DateTime? DESDE = null, DateTime? HASTA = null, string pedimento = "", int pagina = 1)
        {
            try
            {
                #region FORMATO FECHAS

                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    v_fecha_desde = anio + mes + dia;
                }

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    v_fecha_hasta = anio + mes + dia;
                }

                #endregion

                CadenaSQL = Informes.SACIWEB_MS_EXPORTACIONES_PAGINADO + string.Format("'{0}', '{1}', '{2}', {3}", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta, pedimento, pagina);
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





        #region REPORTE HISTORIA DE DESCARGOS POR IMPORTACION



        /// <summary>
        /// REPORTE HISTORIA DE DESCARGOS POR IMPORTACION
        /// </summary>
        public DataTable RptHistoriaImportacion(DateTime? DESDE = null, DateTime? HASTA = null, string CLAVE = null, string DOCUMENTO = null, int repetirFilas = 0, string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                #region FORMATO FECHAS

                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    v_fecha_desde = anio + mes + dia;
                }

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    v_fecha_hasta = anio + mes + dia;
                }

                #endregion

                CadenaSQL = Informes.SACIWEB_MS_HISTORIADESCARGAE + string.Format("'{0}', '{1}', '{2}', '{3}', {4}, '{5}', '{6}'", DESDE == null ? Convert.DBNull : v_fecha_desde,
                    HASTA == null ? Convert.DBNull : v_fecha_hasta, CLAVE == null ? Convert.DBNull : CLAVE, DOCUMENTO == null ? Convert.DBNull : DOCUMENTO,
                    repetirFilas == null ? Convert.DBNull : repetirFilas, USUARIO, PLANTAS);
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

        public DataTable RptHistoriaImportacion_Paginado(DateTime? DESDE = null, DateTime? HASTA = null, string CLAVE = null, string DOCUMENTO = null, int repetirFilas = 0, int pagina = 1)
        {
            try
            {
                #region FORMATO FECHAS

                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    v_fecha_desde = anio + mes + dia;
                }

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    v_fecha_hasta = anio + mes + dia;
                }

                #endregion

                CadenaSQL = Informes.SACIWEB_MS_HISTORIADESCARGAE_PAGINADO + string.Format("'{0}', '{1}', '{2}', '{3}', {4}, {5}", DESDE == null ? Convert.DBNull : v_fecha_desde,
                    HASTA == null ? Convert.DBNull : v_fecha_hasta, CLAVE == null ? Convert.DBNull : CLAVE, DOCUMENTO == null ? Convert.DBNull : DOCUMENTO,
                    repetirFilas == null ? Convert.DBNull : repetirFilas, pagina);
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

        #region REPORTE HISTORIA DE DESCARGOS POR EXPORTACION


        /// <summary>
        /// REPORTE HISTORIA DE DESCARGOS POR EXPORTACION
        /// </summary>
        public DataTable RptHistoriaExportaciones(DateTime? DESDE = null, DateTime? HASTA = null, string PROD = null, string CLAVE = null, string DOCUMENTO = null, int OPCION = 0, string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                #region FORMATO FECHAS

                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    v_fecha_desde = anio + mes + dia;
                }

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    v_fecha_hasta = anio + mes + dia;
                }

                #endregion

                CadenaSQL = Informes.SACIWEB_MS_HISTORIADESCARGAS + string.Format("'{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}', '{7}'", DESDE == null ? Convert.DBNull : v_fecha_desde,
                    HASTA == null ? Convert.DBNull : v_fecha_hasta, PROD == null ? Convert.DBNull : PROD, CLAVE == null ? Convert.DBNull : CLAVE, DOCUMENTO == null ? Convert.DBNull : DOCUMENTO, OPCION, USUARIO, PLANTAS);
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

        public DataTable RptHistoriaExportaciones_Paginado(DateTime? DESDE = null, DateTime? HASTA = null, string PROD = null, string CLAVE = null, string DOCUMENTO = null, int OPCION = 0, int pagina = 1)
        {
            try
            {
                #region FORMATO FECHAS

                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    v_fecha_desde = anio + mes + dia;
                }

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    v_fecha_hasta = anio + mes + dia;
                }

                #endregion

                CadenaSQL = Informes.SACIWEB_MS_HISTORIADESCARGAS_PAGINADO + string.Format("'{0}', '{1}', '{2}', '{3}', '{4}', {5}, {6}", DESDE == null ? Convert.DBNull : v_fecha_desde,
                    HASTA == null ? Convert.DBNull : v_fecha_hasta, PROD == null ? Convert.DBNull : PROD, CLAVE == null ? Convert.DBNull : CLAVE, DOCUMENTO == null ? Convert.DBNull : DOCUMENTO, OPCION, pagina);
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



        #region REPORTE DE ANALISIS DE LA DESCARGA

        /// <summary>
        /// REPORTE DE ANALISIS DE LA DESCARGA
        /// </summary>
        public DataTable RptAnalisisDescarga(DateTime? DESDE = null, DateTime? HASTA = null, string PROD = null, string MATERIAL = null, string PEDIMENTO = null)
        {
            try
            {
                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd"); //myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                DateTime dtPrueba = DateTime.Parse(sqlFormattedDate);

                #region FORMATO FECHAS

                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;
                //DateTime? fechaDesde = null;
                //DateTime? fechaHasta = null;                

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    //v_fecha_desde = dia + "-" + mes + "-" + anio;
                    v_fecha_desde = anio + "-" + mes + "-" + dia;
                    //fechaDesde = DateTime.Parse(DateTime.Parse(v_fecha_desde).ToShortDateString());
                }


                //fechaDesde = DateTime. fechaDesde.ToShortDateString();

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    //v_fecha_hasta = dia + "-" + mes + "-" + anio;
                    v_fecha_hasta = anio + "-" + mes + "-" + dia;
                    //fechaHasta = DateTime.Parse(DateTime.Parse(v_fecha_hasta).ToShortDateString());
                }

                //var fff = fechaHasta.Value.Date;
                //fechaDesde != null ? fechaDesde.Value.Date: null
                #endregion

                CadenaSQL = Informes.SACIWEB_ANALISIS_DESCARGA + string.Format(" '{0}', '{1}', '{2}', '{3}', '{4}'", DESDE == null ? Convert.DBNull : v_fecha_desde,
                    HASTA == null ? Convert.DBNull : v_fecha_hasta, PROD == null ? Convert.DBNull : PROD, MATERIAL == null ? Convert.DBNull : MATERIAL, PEDIMENTO == null ? Convert.DBNull : PEDIMENTO);
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


        #region REPORTE VENCIMIENTOS


        /// <summary>
        /// REPORTE VENCIMIENTOS
        /// </summary>
        public DataTable RptVencimientos(string USUARIO = "", string PLANTAS = "")
        {
            try
            {

                CadenaSQL = Informes.SACIWEB_RPT_VENCIMIENTOS + string.Format(" '{0}', '{1}'", USUARIO, PLANTAS);
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



        #region REPORTE ESTRUCTURAS


        /// <summary>
        /// REPORTE VENCIMIENTOS
        /// </summary>
        public DataTable RptEstructuras(DateTime? DESDE = null, DateTime? HASTA = null, string PRODUCTO = "", string MATERIAL = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                #region FORMATO FECHAS
                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    //v_fecha_desde = dia + "/" + mes + "/" + anio;
                    v_fecha_desde = anio + mes + dia;
                }

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    //v_fecha_hasta = dia + "/" + mes + "/" + anio;
                    v_fecha_hasta = anio + mes + dia;
                }
                #endregion

                CadenaSQL = Informes.SACIWEB_INFORME_ESTRUCTURAS + string.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}'", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta, PRODUCTO, MATERIAL, USUARIO, PLANTAS);
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

        public DataTable RptEstructuras_Paginado(DateTime? DESDE = null, DateTime? HASTA = null, string PRODUCTO = "", string MATERIAL = "", int PAGINA = 1)
        {
            try
            {
                #region FORMATO FECHAS
                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    //v_fecha_desde = dia + "/" + mes + "/" + anio;
                    v_fecha_desde = anio + mes + dia;
                }

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    //v_fecha_hasta = dia + "/" + mes + "/" + anio;
                    v_fecha_hasta = anio + mes + dia;
                }
                #endregion

                CadenaSQL = Informes.SACIWEB_INFORME_ESTRUCTURAS_PAGINADO + string.Format("'{0}', '{1}', '{2}', '{3}', {4}", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta, PRODUCTO, MATERIAL, PAGINA);
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


        #region REPORTE COMPULSA

        /// <summary>
        /// REPORTE COMPULSA
        /// </summary>
        public DataTable RptCompulsa(DateTime? DESDE = null, DateTime? HASTA = null, string PEDIMENTO = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                #region FORMATO FECHAS

                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    //v_fecha_desde = dia + "/" + mes + "/" + anio;
                    v_fecha_desde = anio + mes + dia;
                }

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    //v_fecha_hasta = dia + "/" + mes + "/" + anio;
                    v_fecha_hasta = anio + mes + dia;
                }

                #endregion

                //CadenaSQL = Informes.SACIWEB_MS_COMPULSA + string.Format("'{0}', '{1}'", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta,PEDIMENTO);
                CadenaSQL = Informes.SACIWEB_MS_COMPULSA + string.Format("'{0}', '{1}', '{2}', '{3}', '{4}'", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta, PEDIMENTO, USUARIO, PLANTAS);
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

        #region REPORTE F4/CTMA

        /// <summary>
        /// REPORTE COMPULSA
        /// </summary>
        public DataTable RptF4CTMA(DateTime? DESDE = null, DateTime? HASTA = null, string PEDIMENTO = "", int REPROCESAR = 0, string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                #region FORMATO FECHAS
                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    //v_fecha_desde = dia + "/" + mes + "/" + anio;
                    v_fecha_desde = anio + mes + dia;
                }

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    //v_fecha_hasta = dia + "/" + mes + "/" + anio;
                    v_fecha_hasta = anio + mes + dia;
                }
                #endregion

                CadenaSQL = Informes.SACIWEB_REP_F4CTMA + string.Format("'{0}', '{1}', '{2}',{3}, '{4}', '{5}'", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta, PEDIMENTO, REPROCESAR, USUARIO, PLANTAS);
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

        #region REPORTE F4/DESPERDICIOS

        /// <summary>
        /// REPORTE COMPULSA
        /// </summary>
        public DataTable RptF4_Desperdicios(DateTime? DESDE = null, DateTime? HASTA = null, string PEDIMENTO = "", int REPROCESAR = 0, string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                #region FORMATO FECHAS
                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    //v_fecha_desde = dia + "/" + mes + "/" + anio;
                    v_fecha_desde = anio + mes + dia;
                }

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    //v_fecha_hasta = dia + "/" + mes + "/" + anio;
                    v_fecha_hasta = anio + mes + dia;
                }
                #endregion

                CadenaSQL = Informes.SACIWEB_REP_F4DESPERDICIOS + string.Format("'{0}', '{1}', '{2}',{3}, '{4}', '{5}'", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta, PEDIMENTO, REPROCESAR, USUARIO, PLANTAS);
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

        #region REPORTE DIRIGIDOS

        /// <summary>
        /// REPORTE DIRIGIDOS
        /// </summary>
        public DataTable RptDirigidos(DateTime? DESDE = null, DateTime? HASTA = null, string EXPO = "", string IMPO = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                #region FORMATO FECHAS

                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    //v_fecha_desde = dia + "/" + mes + "/" + anio;
                    v_fecha_desde = anio + mes + dia;
                }

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    //v_fecha_hasta = dia + "/" + mes + "/" + anio;
                    v_fecha_hasta = anio + mes + dia;
                }

                #endregion

                CadenaSQL = Informes.SACIWEB_INFORME_DIRIGIDOS + string.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}'", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta, EXPO, IMPO, USUARIO, PLANTAS);
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

        #region REPORTE DESCARGAS SCRAP
        public DataTable Rpt_DescargasSCRAP(string DESDE = "", string HASTA = "", string PEDIMENTO_EXPO = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = Informes.SACIWEB_INFORME_DESCARGAS_SCRAP + string.Format("'{0}','{1}','{2}', '{3}', '{4}'", DESDE, HASTA, PEDIMENTO_EXPO, USUARIO, PLANTAS); ;
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

        //[MBA][19/01/2021][nuevo repote de informe de permisos]
        #region REPORTE PERMISOS

        /// <summary>
        /// REPORTE SALDOS 
        /// </summary>
        public DataTable RptPermisos(string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = Informes.SACIWEB_INFORME_PERMISOS + string.Format("'{0}', '{1}'", USUARIO, PLANTAS);
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

        //[MBA][22/03/2021][nuevo repote de informe de ajustes]
        #region INFORME AJUSTES

        /// <summary>
        /// INFORME AJUSTES 
        /// </summary>
        public DataTable RptAjustes(int OPCION = 0, DateTime? DESDE = null, DateTime? HASTA = null)
        {
            try
            {
                CadenaSQL = Informes.SACIWEB_INFORME_AJUSTES + string.Format("{0}, '{1}', '{2}'", OPCION, DESDE == null ? Convert.DBNull : DESDE, HASTA == null ? Convert.DBNull : HASTA);
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

        //[MBA][02/05/2023][nuevo cambio al dar clic en generar ajuste traera 3 select]
        #region INFORME AJUSTES CON 3 SELECTS

        /// <summary>
        /// INFORME AJUSTES CON 3 SELECTS
        /// </summary>
        public void RptAjustesTRES(int opcion, string desde, string hasta, string anio, ref DataTable dt1, ref DataTable dt2, ref DataTable dt3)
        {
            try
            {
                CadenaSQL = Informes.SP_SACIWEB_INFORME_AJUSTES;
                _ObjetoDBSQL.ConectarRptAjustes(CadenaSQL, opcion, desde, hasta, anio, ref dt1, ref dt2, ref dt3);
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

        //[MBA][09/08/2022][nuevos repotes de informes]
        #region REPORTE RECTIFICACIONES

        /// <summary>
        /// RECTIFICACIONES
        /// </summary>
        public DataTable RptRectificaciones(string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = Informes.SACIWEB_MS_CADENA_RECTIFICACION + string.Format("'{0}', '{1}'", USUARIO, PLANTAS);
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

        #region REPORTE EXPORTACIONES DESCARGOS



        /// <summary>
        /// REPORTE EXPORTACIONES DESCARGOS
        /// </summary>
        public DataTable RptExportacionesDescargos(string DESDE = "", string HASTA = "", string USUARIO = "", string PLANTAS = "", string PEDIMENTO = "")
        {
            try
            {
                #region FORMATO FECHAS

                #endregion

                CadenaSQL = Informes.SACIWEB_MS_EXPORTAR_INFORME_DESCARGOS + string.Format("'{0}', '{1}', '{2}', '{3}','{4}'", DESDE, HASTA, USUARIO, PLANTAS, PEDIMENTO);
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

        #region REPORTE COMPLEMENTARIO (T-MEC antes TLCAN)
        public DataTable RptComplementarioTMEC(string DESDE = "", string HASTA = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {

                CadenaSQL = Informes.SACIWEB_MS_TLCUE + string.Format("'{0}', '{1}', '{2}', '{3}'", DESDE, HASTA, USUARIO, PLANTAS);
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


        #region REPORTE MATERIALES UTILIZADOS

        /// <summary>
        /// REPORTE MATERIALES UTILIZADOS
        /// </summary>
        public DataTable RptMaterial_Utilizados(DateTime? DESDE = null, DateTime? HASTA = null, string agrupar = "")
        {
            try
            {
                #region FORMATO FECHAS

                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha_desde = string.Empty;
                string v_fecha_hasta = string.Empty;

                if (DESDE != null)
                {
                    dia = DESDE.Value.Day.ToString().Length == 1 ? "0" + DESDE.Value.Day.ToString() : DESDE.Value.Day.ToString();
                    mes = DESDE.Value.Month.ToString().Length == 1 ? "0" + DESDE.Value.Month.ToString() : DESDE.Value.Month.ToString();
                    anio = DESDE.Value.Year.ToString();
                    v_fecha_desde = anio + mes + dia;
                }

                if (HASTA != null)
                {
                    dia = HASTA.Value.Day.ToString().Length == 1 ? "0" + HASTA.Value.Day.ToString() : HASTA.Value.Day.ToString();
                    mes = HASTA.Value.Month.ToString().Length == 1 ? "0" + HASTA.Value.Month.ToString() : HASTA.Value.Month.ToString();
                    anio = HASTA.Value.Year.ToString();
                    v_fecha_hasta = anio + mes + dia;
                }

                #endregion

                CadenaSQL = Informes.INFORME_MATERIALES_UTILIZADOS + string.Format("'{0}', '{1}', '{2}'", DESDE == null ? Convert.DBNull : v_fecha_desde, HASTA == null ? Convert.DBNull : v_fecha_hasta, agrupar);
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

        #region REPORTE DE MODULO DE ACTIVO FIJO
        public DataTable RptDeActivoFijo()
        {
            try
            {
                CadenaSQL = Informes.SACIWEB_INFORME_DE_MODULO_DE_ACTIVO_FIJO;
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
