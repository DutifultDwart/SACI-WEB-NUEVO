using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace SACI.Negocio
{
    public class RNAnexos31
    {
        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;

        #endregion


        public RNAnexos31()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }


        #region TRAE DESTINO ADUANERO

        /// <summary>
        /// TRAE DESTINO ADUANERO
        /// </summary>
        public DataTable Trae_DestinoAduanero()
        {
            try
            {
                CadenaSQL = Informes_Anexo_31.SACIWEB_CAT_DESTINATARIO_A31;
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


        #region TRAE CLAVE

        /// <summary>
        /// TRAE CLAVE
        /// </summary>
        public DataTable Trae_Clave()
        {
            try
            {
                CadenaSQL = Informes_Anexo_31.SACIWEB_CAT_PERIODO_A31;
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


        #region Generar Informe

        /// <summary>
        /// GENERAR INFORME
        /// </summary>
        public DataTable Generar_Informe(string ANIO, string PERIODO, string SUSTITUYE, string CLAVE, int GENERAHOJA, int HOJA, string CONEXION, out string RESPUESTA)
        {
            DataTable dt = new DataTable();
            string sp = "SACIWEB_INFORME_A31";

            try
            {
                using (SqlConnection con = new SqlConnection(CONEXION))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sp, con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ANIO", SqlDbType.VarChar).Value = ANIO;
                        cmd.Parameters.Add("@PERIODO", SqlDbType.VarChar).Value = PERIODO;
                        cmd.Parameters.Add("@SUSTITUYE", SqlDbType.VarChar).Value = SUSTITUYE;
                        cmd.Parameters.Add("@CLAVE", SqlDbType.VarChar).Value = CLAVE;
                        cmd.Parameters.Add("@GENERARHOJA", SqlDbType.Int).Value = GENERAHOJA;
                        cmd.Parameters.Add("@HOJA", SqlDbType.Int).Value = HOJA;
                        cmd.Parameters.Add("@MsgRes", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                                dt = null;
                            else
                                dt.Load(reader);
                        }
                        RESPUESTA = cmd.Parameters["@MsgRes"].Value.ToString();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                RESPUESTA = ex.Message;

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
            return dt;
        }

        public DataTable Generar_Informe_Consolidado(string ANIO, string PERIODO, string SUSTITUYE, string CLAVE, int GENERAHOJA, int HOJA, string CONEXION, out string RESPUESTA)
        {
            DataTable dt = new DataTable();
            string sp = "SACIWEB_INFORME_A31_CONSOLIDADO";

            try
            {
                using (SqlConnection con = new SqlConnection(CONEXION))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sp, con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ANIO", SqlDbType.VarChar).Value = ANIO;
                        cmd.Parameters.Add("@PERIODO", SqlDbType.VarChar).Value = PERIODO;
                        cmd.Parameters.Add("@SUSTITUYE", SqlDbType.VarChar).Value = SUSTITUYE;
                        cmd.Parameters.Add("@CLAVE", SqlDbType.VarChar).Value = CLAVE;
                        cmd.Parameters.Add("@GENERARHOJA", SqlDbType.Int).Value = GENERAHOJA;
                        cmd.Parameters.Add("@HOJA", SqlDbType.Int).Value = HOJA;
                        cmd.Parameters.Add("@MsgRes", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                                dt = null;
                            else
                                dt.Load(reader);
                        }
                        RESPUESTA = cmd.Parameters["@MsgRes"].Value.ToString();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                RESPUESTA = ex.Message;

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
            return dt;
        }

        /// <summary>
        /// GENERAR INFORME
        /// </summary>
        //public DataTable Genrar_Informe(string ANIO, string PERIODO, string SUSTITUYE, string CLAVE, int GENERAHOJA, int HOJA, out string RESPUESTA)
        //{
        //    try
        //    {
        //        CadenaSQL = Informes_Anexo_31.SACIWEB_INFORME_A31 + string.Format("'{0}', '{1}', '{2}', '{3}', {4}, {5}, '{6}' OUTPUT", ANIO, PERIODO, SUSTITUYE, CLAVE, GENERAHOJA, HOJA);

        //        return _ObjetoDBSQL.Conectar(CadenaSQL);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Substring(0, 1).ToString() == "1")
        //        {
        //            throw ex;
        //        }
        //        else
        //        {
        //            throw new ApplicationException(string.Format("Error: {0}. {1}. {2}. {3}. {4}", ex.Message.Replace("\r\n", "|").Replace("'", "´"), "|Metodo: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
        //            "|Recurso: " + ex.Source,
        //            "|Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"),
        //            "|Sp: " + CadenaSQL.Replace("\r\n", "|").Replace("'", "´")), ex);
        //        }

        //    }
        //    finally
        //    {
        //        _ObjetoDB = null;
        //    }
        //}


        #endregion


        #region TRAE ARCHIVO TXT

        /// <summary>
        /// TRAE ARCHIVO TXT POR NOMBRE
        /// </summary>
        public DataTable Trae_Txt(string NOMBRE = "")
        {
            try
            {
                CadenaSQL = Informes_Anexo_31.SACIWEB_GUARDA_A31 + string.Format("'{0}'", NOMBRE);
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

        public DataTable Trae_Txt_Consolidado(string NOMBRE = "")
        {
            try
            {
                CadenaSQL = Informes_Anexo_31.SACIWEB_GUARDA_A31_CONSOLIDADO + string.Format("'{0}'", NOMBRE);
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




        /// <summary>
        /// GENERAR INFORME
        /// </summary>
        public DataTable Generar_InformeAnex31(string DESDE, string HASTA)
        {
            try
            {
                CadenaSQL = Informes_Anexo_31.SPWEB_A30_InformeEntradas + string.Format("'{0}', '{1}'", DESDE, HASTA);
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

        /// <summary>
        /// GENERAR INFORME
        /// </summary>
        public DataTable SelectPeriodos()
        {
            try
            {
                CadenaSQL = Informes_Anexo_31.SACIWEB_PERIODOS;
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

        /// <summary>
        /// GENERAR INFORME
        /// </summary>
        public DataTable SelectDetallePeriodoEjercio(string PERIODO = "", int EJERCICIO = 0)
        {
            try
            {
                CadenaSQL = Informes_Anexo_31.SACIWEB_SEL_CVEPEDIMENTO + string.Format("'{0}', {1}", PERIODO, EJERCICIO); ;
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


        /// <summary>
        /// GENERAR INFORME
        /// </summary>
        public DataTable InsertaDS()
        {
            try
            {
                CadenaSQL = Informes_Anexo_31.INSERTAFALTANTESA31;
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



        /// <summary>
        /// DESCARGAR
        /// </summary>
        public DataTable Descargar()
        {
            try
            {
                CadenaSQL = Informes_Anexo_31.DESCARGAS_A31;
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



        /// <summary>
        /// GENERAR INFORME
        /// </summary>
        public DataTable SelectDatosPedimentos(int OPCION = 0, string PERIODO = "", int EJERCICIO = 0, string CVEPEDIMENTO = "", string ARCHVIO = "")
        {
            try
            {
                CadenaSQL = Informes_Anexo_31.SACIWEB_SEL_DATOS_PEDIMENTOS + string.Format("{0}, '{1}', {2}, '{3}', '{4}'", OPCION, PERIODO, EJERCICIO, CVEPEDIMENTO, ARCHVIO);
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



        /// <summary>
        /// GENERAR INFORME
        /// </summary>
        public DataTable VerDescarga(int OPCION = 0)
        {
            try
            {
                CadenaSQL = Informes_Anexo_31.SACIWEB_SEL_DESCARGA + string.Format("{0}", OPCION);
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

        /// <summary>
        /// GENERAR INFORME
        /// </summary>
        public DataTable AnalisisDescarga(int OPCION = 0)
        {
            try
            {
                CadenaSQL = Informes_Anexo_31.SACIWEB_ANALISIS_DESCARGA + string.Format("{0}", OPCION);
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

        /// <summary>
        /// GENERAR INFORME
        /// </summary>
        public DataTable DiferenciaDescarga(int OPCION = 0)
        {
            try
            {
                CadenaSQL = Informes_Anexo_31.SACIWEB_COMPARATIVA_DIFERENCIAS + string.Format("{0}", OPCION);
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

        /// <summary>
        /// GENERAR INFORME
        /// </summary>
        public DataTable AnalisisPeriodo(int OPCION = 0, string PERIODO = "", string EJERCICIO = "")
        {
            try
            {
                CadenaSQL = Informes_Anexo_31.SACIWEB_ANALISISPERIODO + string.Format("{0}, '{1}', '{2}'", OPCION, PERIODO, EJERCICIO);
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



        /// <summary>
        /// GENERAR INFORME
        /// </summary>
        public DataTable Vencimientos(int OPCION = 0)
        {
            try
            {
                CadenaSQL = Informes_Anexo_31.SACIWEB_VENCIMIENTOS + string.Format("{0}", OPCION);
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


        #region Inventario Inicial y archivos de descarga

        public DataTable procArchivosDescarga(int opcion = 0, string Nombre_Archivo = "", string Folio = "", Guid Id_File = default(Guid), byte[] file = null)
        {
            try
            {
                if (file == null)
                {
                    CadenaSQL = sp_Archivos.SACIWEB_PROC_ARCHIVOS_DESCARGA + string.Format("{0},'{1}','{2}','{3}'", opcion, Nombre_Archivo, Folio, Id_File);
                    return _ObjetoDBSQL.Conectar(CadenaSQL);
                }
                else
                {
                    CadenaSQL = sp_Archivos.SACIWEB_PROC1_ARCHIVOS_DESCARGA;
                    return _ObjetoDBSQL.ConectarUplArchivosDescarga(CadenaSQL, Nombre_Archivo, file);
                }

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

        public DataTable procesaFileDescarga(Guid FILEKEY = default(Guid))
        {
            try
            {
                CadenaSQL = sp_Archivos.SACIWEB_PROCESA_ARCHIVO_DESCARGA + string.Format("'{0}'", FILEKEY);
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
