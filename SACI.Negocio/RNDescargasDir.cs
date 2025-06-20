using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Negocio
{
    public class RNDescargasDir
    {
        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;

        #endregion


        public RNDescargasDir()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }



        #region SELECCIONA LOS DOCUMENTOS EXPORTACION

        /// <summary>
        /// METODO PARA SELECCIONAR LAS FECHAS DE LAS IMPORTACIONES
        /// </summary>
        public DataTable SelectDocExpo()
        {
            try
            {
                CadenaSQL = sp_DescargasDir.SACIWEB_SEL_DOC_EXPO;
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




        #region SELECCIONA LAS PARTIDAS DE LOS DOCUMENTOS

        /// <summary>
        /// METODO PARA SELECCIONAR LAS PARTIDAS DE LAS EXPOS
        /// </summary>
        public DataTable SelectPartidasExpo(int SALIDALINK, string USUARIO = "")
        {
            try
            {
                CadenaSQL = sp_DescargasDir.SACIWEB_SEL_PARTIDAS_EXPO + string.Format("{0},'{1}'", SALIDALINK, USUARIO);
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




        #region SELECCIONA LOS DOCUMENTOS DE IMPORTACION

        /// <summary>
        /// METODO PARA SELECCIONAR LOS DOCUMENTOS DE IMPORTACIONES
        /// </summary>
        public DataTable SelectDocImpo(int OPCION = 0, int IPEDIMENTO = 0)
        {
            try
            {
                CadenaSQL = sp_DescargasDir.SACIWEB_SEL_IMPORTACIONES + string.Format("{0}, {1}", OPCION, IPEDIMENTO);
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



        #region INSERTA LOS DIRIGIDOS

        /// <summary>
        /// METODO PARA SELECCIONAR LAS FECHAS DE LAS IMPORTACIONES
        /// </summary>
        public DataTable InsertDirigidos(int OPCION = 0, int DKEY = 0, string DOCUMENTO = "", string CLAVE = "", decimal INCORPORADO = 0, decimal DESPERDIIO = 0, decimal MERMA = 0, int SALIDAKEY = 0, int PSALIDAKEY = 0,
            string FACTURA = "")
        {
            try
            {
                CadenaSQL = sp_DescargasDir.SACIWEB_INSERTA_DIRIGIDO + string.Format("{0}, {1}, '{2}', '{3}', {4}, {5}, {6}, {7}, {8}, '{9}'", OPCION, DKEY, DOCUMENTO, CLAVE, INCORPORADO, DESPERDIIO, MERMA, SALIDAKEY, PSALIDAKEY, FACTURA);
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



        #region DESCARGA TODOS LOS DIRIGIDOS

        /// <summary>
        /// METODO PARA DESCARGAR TODOS LOS DIRIGIDOS
        /// </summary>
        public DataTable DescargaTodosDirigidos()
        {
            try
            {
                CadenaSQL = sp_DescargasDir.DESCARGATODOSDIRIGIDOS;
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





        #region INFORME DESCARGOS DIR

        /// <summary>
        /// METODO PARA DGENERAR EL INFORME DE DESCARGOS DIR
        /// </summary>
        public DataTable InformeDescargos()
        {
            try
            {
                CadenaSQL = sp_DescargasDir.SACIWEB_INFORME_DESCARGOS_DIRIGIDOS;
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
