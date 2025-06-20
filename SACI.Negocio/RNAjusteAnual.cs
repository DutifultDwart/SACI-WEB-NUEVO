using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Negocio
{
    public class RNAjusteAnual
    {
        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;

        #endregion

        public RNAjusteAnual()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }

        public DataTable selAñosCTM()
        {
            try
            {
                return _ObjetoDBSQL.Conectar(sp_Ajuste_Anual.SACIWEB_AANUAL_SELANIO_CTM);
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

        public DataTable CargarVentas(DataTable dtVentas)
        {
            try
            {
                return _ObjetoDBSQL.ConectarEstruct(sp_Ajuste_Anual.SACIWEB_AANUAL_IMPORTA_VENTAS, dtVentas);
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

        public DataTable procesaCTM(int opcion = 0, int año = 0)
        {
            try
            {
                CadenaSQL = sp_Ajuste_Anual.SACIWEB_AANUAL_CTM + string.Format("{0},{1}", opcion, año);
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

        public DataTable cargaCTM( int año = 0)
        {
            try
            {
                CadenaSQL = sp_Ajuste_Anual.SACIWEB_AANUAL_CARGA_CTM + string.Format("{0}", año);
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

        public DataTable cargaVentas(int opcion = 0)
        {
            try
            {
                CadenaSQL = sp_Ajuste_Anual.SACIWEB_AANUAL_VENTAS + string.Format("{0}", opcion);
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

        public DataTable LimpiarTodo()
        {
            try
            {
                return _ObjetoDBSQL.Conectar(sp_Ajuste_Anual.SACIWEB_AANUAL_LIMPIAR_TODO);
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

        /// <summary>
        /// inventario final
        /// </summary>        
        /// <returns></returns>
        public DataTable CargarInvFin(int Opcion, DataTable dtInvFinal)
        {
            try
            {
                return _ObjetoDBSQL.ConectarEstructInvFianl(sp_Ajuste_Anual.SACIWEB_AANUAL_INV_FINAL, Opcion, dtInvFinal);
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

        /// <summary>
        /// INVENTARIO INICIAL
        /// </summary>        
        /// <returns></returns>
        public DataTable CargarInvIni(int Opcion, DataTable dtInvInicial)
        {
            try
            {
                return _ObjetoDBSQL.ConectarEstructInvIni(sp_Ajuste_Anual.SACIWEB_AANUAL_INV_INICIAL, Opcion, dtInvInicial);
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

    }
}
