
using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Negocio
{
    public class RNCatalogos
    {
        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;

        #endregion


        public RNCatalogos()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }


        #region CATALOGO ACTIVIDADES



        /// <summary>
        /// INSERTA EVENTO EN ACTIVIDADES
        /// </summary>
        public DataTable CatActividades(int OPCION = 0, int PK_ID_ACTIVIDAD = 0, string CVE_ACTIVIDAD = "", string NOM_ACTIVIDAD = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_ACTIVIDADES + string.Format("{0}, {1}, '{2}', '{3}'", OPCION, PK_ID_ACTIVIDAD, CVE_ACTIVIDAD, NOM_ACTIVIDAD);
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




        #region CATALOGO PERFILES

        /// <summary>
        /// INSERTA EVENTO EN ACTIVIDADES
        /// </summary>
        public DataTable CatPerfiles(int OPCION = 0, int PK_ID_PERFIL = 0, string CVE_PERFIL = "", string NOM_PERFIL = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_PERFILES + string.Format("{0}, {1}, '{2}', '{3}'", OPCION, PK_ID_PERFIL, CVE_PERFIL, NOM_PERFIL);
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








        #region CATALOGO AF



        /// <summary>
        /// CONSULTA EVENTO EN AF
        /// </summary>
        public DataTable CatAF(int OPCION = 0, Int64 MATERIALKEY = 0, string CLAVE = "", string DESCRIPCION = "", string FRACCION = "", string UNIDAD = "", string UNIDADT = "", string TIPOMATERIAL = "",
                               decimal FACTORUM = 0, Int64 IGIE = 0, string CERTNAFTA = "", string CLAVEPROVEEDOR = "", Int64 ALMACENKEY = 0, string TIPOM = "", string FAMILIA = "", string NUMEROSERIE = "",
                               string MARCA = "", string MODELO = "", string NICO = "", string PLANTA = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_AF + string.Format("{0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, {8}, '{9}', '{10}', {11}, '{12}', '{13}', '{14}', '{15}', '{16}', {17}, '{18}', '{19}', '{20}', '{21}'",
                            MATERIALKEY, CLAVE, DESCRIPCION, FRACCION, UNIDAD, UNIDADT, TIPOMATERIAL, FACTORUM, IGIE, CERTNAFTA, CLAVEPROVEEDOR, ALMACENKEY, TIPOM, FAMILIA, NUMEROSERIE, MARCA, MODELO, OPCION, NICO, PLANTA, USUARIO, PLANTAS);
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


        #region CATALOGO SUBMAQUILA



        /// <summary>
        /// CONSULTA EVENTO EN SUBMAQUILA
        /// </summary>
        public DataTable CatSubmaquila(int OPCION = 0, Int64 TRANSKEY = 0, string CLAVE = "", string NOMBRE = "", string DOMICILIO = "", string NUMOFICIO = "", DateTime? FECHA = null, string Proceso = "")
        {
            try
            {
                #region FORMATO FECHAS

                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;
                string v_fecha = string.Empty;

                if (FECHA != null)
                {
                    dia = FECHA.Value.Day.ToString().Length == 1 ? "0" + FECHA.Value.Day.ToString() : FECHA.Value.Day.ToString();
                    mes = FECHA.Value.Month.ToString().Length == 1 ? "0" + FECHA.Value.Month.ToString() : FECHA.Value.Month.ToString();
                    anio = FECHA.Value.Year.ToString();
                    v_fecha = dia + "/" + mes + "/" + anio;
                }

                #endregion

                CadenaSQL = sp_Catalogos.SP_CAT_SUBMAQUILA + string.Format("{0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, '{7}'", TRANSKEY, CLAVE, NOMBRE, DOMICILIO, NUMOFICIO, FECHA == null ? Convert.DBNull : v_fecha, OPCION, Proceso);
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


        #region CATALOGO CATEGORIAS



        /// <summary>
        /// CONSULTA EVENTO EN CATEGORIAS
        /// </summary>
        public DataTable CatCategiras(int OPCION = 0, Int64 CATKEY = 0, string CATEGORIA = "", string DESCRIPCION = "", int MESES = 0)
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_CATEGORIAS + string.Format("{0}, '{1}', '{2}', {3}, {4}", CATKEY, CATEGORIA, DESCRIPCION, MESES, OPCION);
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


        #region CATALOGO DIVISION Y ALMACENES



        /// <summary>
        /// CONSULTA EVENTO EN DIVISION Y ALMACENES
        /// </summary>
        public DataTable CatDivisionYAlmacen(int OPCION = 0, Int64 AKEY = 0, string ALMACEN = "", string DESCRIPCION = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_DIVISIONYALMACENES + string.Format("{0}, '{1}', '{2}', {3}", AKEY, ALMACEN, DESCRIPCION, OPCION);
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


        #region CATALOGO UNIDADES



        /// <summary>
        /// CONSULTA EVENTO EN UNIDADES
        /// </summary>
        public DataTable CatUnidades(int OPCION = 0, Int64 UKEY = 0, string CVE_UNIDAD = "", string NOMBRE = "", string ALIAS = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_UNIDADES + string.Format("{0}, '{1}', '{2}', '{3}', {4}", UKEY, CVE_UNIDAD, NOMBRE, ALIAS, OPCION);
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


        #region CATALOGO PERMISOS



        /// <summary>
        /// CONSULTA EVENTO EN PERMISOS
        /// </summary>
        public DataTable CatPermisos(int OPCION = 0, Int64 PERMISOKEY = 0, string NUMPERMISO = "", DateTime? FECHAEXP = null, string NUMSOLICITUD = "", string NUMFORMATO = "", DateTime? VALDESDE = null, DateTime? VALHASTA = null)
        {
            try
            {
                #region FORMATO FECHAS

                string dia = string.Empty;
                string mes = string.Empty;
                string anio = string.Empty;

                string v_fechaExp = string.Empty;
                string v_desde = string.Empty;
                string v_hasta = string.Empty;

                if (FECHAEXP != null)
                {
                    dia = FECHAEXP.Value.Day.ToString().Length == 1 ? "0" + FECHAEXP.Value.Day.ToString() : FECHAEXP.Value.Day.ToString();
                    mes = FECHAEXP.Value.Month.ToString().Length == 1 ? "0" + FECHAEXP.Value.Month.ToString() : FECHAEXP.Value.Month.ToString();
                    anio = FECHAEXP.Value.Year.ToString();
                    v_fechaExp = dia + "/" + mes + "/" + anio;
                }

                if (VALDESDE != null)
                {
                    dia = VALDESDE.Value.Day.ToString().Length == 1 ? "0" + VALDESDE.Value.Day.ToString() : VALDESDE.Value.Day.ToString();
                    mes = VALDESDE.Value.Month.ToString().Length == 1 ? "0" + VALDESDE.Value.Month.ToString() : VALDESDE.Value.Month.ToString();
                    anio = VALDESDE.Value.Year.ToString();
                    v_desde = dia + "/" + mes + "/" + anio;
                }

                if (VALHASTA != null)
                {
                    dia = VALHASTA.Value.Day.ToString().Length == 1 ? "0" + VALHASTA.Value.Day.ToString() : VALHASTA.Value.Day.ToString();
                    mes = VALHASTA.Value.Month.ToString().Length == 1 ? "0" + VALHASTA.Value.Month.ToString() : VALHASTA.Value.Month.ToString();
                    anio = VALHASTA.Value.Year.ToString();
                    v_hasta = dia + "/" + mes + "/" + anio;
                }

                #endregion

                CadenaSQL = sp_Catalogos.SP_CAT_PERMISOS + string.Format("{0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}", PERMISOKEY, NUMPERMISO, FECHAEXP == null ? Convert.DBNull : v_fechaExp, NUMSOLICITUD, NUMFORMATO, VALDESDE == null ? Convert.DBNull : v_desde, VALHASTA == null ? Convert.DBNull : v_hasta, OPCION);
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




        #region CATALOGO MATERIALES
        public DataTable catMateriales(int MATERIALKEY = 0, string CLAVE = "", string DESCRIPCION = "", string FRACCION = "", string UNIDAD = "",
                                       string UNIDADT = "", string TIPOMATERIAL = "", decimal FACTORUM = 0, Int64 IGIE = 0, string CERTNAFTA = "",
                                       string CLAVEPROVEEDOR = "", Int64 ALMACENKEY = 0, string TIPOM = "", string FAMILIA = "", string NUMEROSERIE = "",
                                       string MARCA = "", string MODELO = "", int OPCION = 0, string NICO = "", string USUARIO = "", string PLANTAS = "",
                                       string UM_AMERICANA = "", decimal FACTOR_UM_AMERICANA = 0)
        {

            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_MATERIALES + string.Format(" {0}, '{1}', '{2}', '{3}','{4}','{5}','{6}',{7},{8},'{9}','{10}',{11},'{12}','{13}','{14}','{15}'," +
                "'{16}',{17}, '{18}', '{19}', '{20}','{21}','{22}',{23}", MATERIALKEY, CLAVE, DESCRIPCION, FRACCION, UNIDAD,
                UNIDADT, TIPOMATERIAL, FACTORUM, IGIE, CERTNAFTA,
                CLAVEPROVEEDOR, ALMACENKEY, TIPOM, FAMILIA, NUMEROSERIE,
                MARCA, MODELO, OPCION, NICO, string.Empty, USUARIO, PLANTAS, UM_AMERICANA, FACTOR_UM_AMERICANA);

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

        #region CATALOGO ALMACEN
        public DataTable catAlamcen(int OPCION = 0, int ALMACENKEY = 0, string ALMACEN = "", string DESCRIPCION = "")
        {

            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_ALMACENES + string.Format(" {0}, {1}, '{2}','{3}' ", OPCION, ALMACENKEY, ALMACEN, DESCRIPCION);

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

        #region CATALOGO UNIDADES
        public DataTable catUnidad(int OPCION = 0, Int64 UKEY = 0, string CVE_UNIDAD = "", string NOMBRE = "", string ALIAS = "")
        {

            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_UNIDADES + string.Format(" {0}, '{1}', '{2}','{3}',{4}", UKEY, CVE_UNIDAD, NOMBRE, ALIAS, OPCION);

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



        #region TIPO MATERIALES

        public DataTable TipoDeMateriales(int OPCION = 0, int TMKey = 0, string TIPOMATERIAL = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_TIPOMATERIAL + string.Format("{0}, {1}, '{2}'", OPCION, TMKey, TIPOMATERIAL);
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




        #region CATALOGO DATOS GENERALES

        public DataTable CatDatosGenerales(int OPCION = 0, int DGKey = 0, string DENOMINACION = "", string RFC = "", string REGISTROPITEX = "", string REGISTROMAQUILA = "", string REGISTROECEX = "",
            string REGISTRORECIME = "", string REGISTROPROSEC = "", string CALLENUMERO = "", string CODIGOPOSTAL = "", string COLONIA = "", string ENTIDAD = "", string TELEFONO = "", string FAX = "",
            string CORREO = "", string ACTIVIDAD = "", string REGISTROALTEX = "", string REGISTROIMMEX = "", string PAIS = "", string MUNICIPIO = "", string LOCALIDAD = "", string CALLENUMEROINTERIOR = "",
            string OFICIOCERTIFICACION = "", string FECHAVIGENCIA = "", string TIPOCERT_EMPRESA = "", string CERT_OEA = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_DATOSGENERALES + string.Format("{0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '{24}','{25}',{26}",
                    DGKey, DENOMINACION, RFC, REGISTROPITEX, REGISTROMAQUILA, REGISTROECEX, REGISTRORECIME, REGISTROPROSEC, CALLENUMERO, CODIGOPOSTAL, COLONIA, ENTIDAD, TELEFONO, FAX, CORREO, ACTIVIDAD, REGISTROALTEX, REGISTROIMMEX, PAIS, MUNICIPIO, LOCALIDAD, CALLENUMEROINTERIOR, OFICIOCERTIFICACION, FECHAVIGENCIA, TIPOCERT_EMPRESA, CERT_OEA, OPCION);
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



        #region CATALOGO PRODUCTOS



        /// <summary>
        /// INSERTA EVENTO EN PRODUCTOS
        /// </summary>
        public DataTable CatalogosProductos(int OPCION = 0, int PRODUCTOKEY = 0, string CVE_PRODUCTO = "", string NOMBRE = "", string UNIDAD = "", string FRACCION = "", string CVE_PRODUCTO_CLIENTE = "", decimal ALMACENKEY = 0, string AUXILIAR = "", string TIPO = "", string UNIDADT = "", string NICO = "", string USUARIO = "", string PLANTAS = "",
                                             string UM_AMERICANA = "", decimal FACTOR_UM_AMERICANA = 0, decimal VALOR_TRANSACCION = 0, string MONEDA = "", string USO = "", string CLIENTE = "", string PAIS = "", decimal COSTO_NETO = 0)
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_PRODUCTOS + string.Format("{0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, '{7}', '{8}', '{9}', {10}, '{11}', '{12}', '{13}', '{14}'" +
                    ",'{15}',{16}, {17}, '{18}', '{19}', '{20}', '{21}', {22}",
                    PRODUCTOKEY, CVE_PRODUCTO, NOMBRE, UNIDAD, FRACCION, CVE_PRODUCTO_CLIENTE, ALMACENKEY, AUXILIAR, TIPO, UNIDADT, OPCION, NICO, string.Empty, USUARIO, PLANTAS,
                    UM_AMERICANA, FACTOR_UM_AMERICANA, VALOR_TRANSACCION, MONEDA, USO, CLIENTE, PAIS, COSTO_NETO);
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



        #region CATALOGO PROVEEDORES


        /// <summary>
        /// CATALOGO PROVEEDORES
        /// </summary>
        public DataTable CatalogosProveedores(int OPCION = 0, int PKEY = 0, string CLAVE = "", string NOMBRE = "", string IDFISCAL = "", string TIPONE = "", string PROGRAMA = "", string CALLENUMERO = "", string CODIGO = "", string COLONIA = "", string ENTIDAD = "", string PAIS = "", string TELEFONO = "", string CORREO = "", string FAX = "", int ALMACENKEY = 0, string APATERNO = "", string AMATERNO = "", string CALLE = "", int CALLENUMINT = 0, string LOCALIDAD = "", string REFERENCIA = "", string MUNICIPIO = "", string TIPOIDENTIFICADOR = "", string CP = "", string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_PROVEEDORES + string.Format("{0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', {14}, '{15}', '{16}', '{17}', {18}, '{19}', '{20}', '{21}', '{22}', '{23}', {24}, '{25}', '{26}', '{27}'", PKEY, CLAVE, NOMBRE, IDFISCAL, TIPONE, PROGRAMA, CALLENUMERO, CODIGO, COLONIA, ENTIDAD, PAIS, TELEFONO, CORREO, FAX, ALMACENKEY, APATERNO, AMATERNO, CALLE, CALLENUMINT, LOCALIDAD, REFERENCIA, MUNICIPIO, TIPOIDENTIFICADOR, CP, OPCION, string.Empty, USUARIO, PLANTAS);
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


        #region CATALOGO CLIENTES


        /// <summary>
        /// CATALOGO CLIENTES
        /// </summary>
        public DataTable CatalogosClientes(int OPCION = 0, int CKEY = 0, string CLAVE = "", string NOMBRE = "", string IDFISCAL = "", string TIPONE = "", string PROGRAMA = "", string CALLENUMERO = "", string CODIGO = "", string COLONIA = "", string ENTIDAD = "", string PAIS = "", string TELEFONO = "", string CORREO = "", string FAX = "", string APATERNO = "", string AMATERNO = "", string CALLE = "", int CALLENUMINT = 0, string LOCALIDAD = "", string REFERENCIA = "", string MUNICIPIO = "", string TIPOIDENTIFICADOR = "", string CP = "", decimal almacenkey = 0, string USUARIO = "", string PLANTAS = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_CLIENTES + string.Format("{0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', {17}, '{18}', '{19}', '{20}', '{21}', '{22}', {23}, {24}, '{25}', '{26}', '{27}'", CKEY, CLAVE, NOMBRE, IDFISCAL, TIPONE, PROGRAMA, CALLENUMERO, CODIGO, COLONIA, ENTIDAD, PAIS, TELEFONO, CORREO, FAX, APATERNO, AMATERNO, CALLE, CALLENUMINT, LOCALIDAD, REFERENCIA, MUNICIPIO, TIPOIDENTIFICADOR, CP, OPCION, almacenkey, string.Empty, USUARIO, PLANTAS);
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



        #region CATALOGO AGENTES


        /// <summary>
        /// CATALOGO CLIENTES
        /// </summary>
        public DataTable CatalogoAgentes(int OPCION = 0, int AGKEY = 0, string CLAVE = "", string NOMBRE = "", string DOMICILIO = "", string DOMICILIO2 = "", string PATENTE = "", string RFC = "", string CURP = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_AGENTES + string.Format("{0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}'", AGKEY, CLAVE, NOMBRE, DOMICILIO, DOMICILIO2, PATENTE, RFC, OPCION, CURP);
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



        #region CATALOGO REL ACTIVIDADES PERFILES


        /// <summary>
        /// CATALOGO REL ACTIVIDADES PERFILES
        /// </summary>
        public DataTable CatalogoRelActividades(int OPCION = 0, int PERFIL_ID = 0, int ACTIVIDAD_ID = 0)
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_REL_ACTIVIDAD_PERFIL + string.Format("{0}, {1}, {2}", OPCION, PERFIL_ID, ACTIVIDAD_ID);
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

        public DataTable CatalogoRelActividades2(int OPCION = 0, int PERFIL_ID = 0, int ACTIVIDAD_ID = 0)
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_REL_ACTIVIDAD_PERFIL2 + string.Format("{0}, {1}, {2}", OPCION, PERFIL_ID, ACTIVIDAD_ID);
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



        #region CATALOGO TIPO NACIONAL, IMPORTACION
        public DataTable catTipoNac_Imp()
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_CAT_TIPO_NAC_IMP;

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




        #region TIPO MATERIALES RELACION UNIDAD

        public DataTable RelMaterialUnidad(int OPCIONCR = 0, int FMPKey = 0, int MATERIALLINK = 0, string CLAVECR = "", string UNIDADORG = "", string UNIDADCR = "", decimal FACTOR = 0)
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_CR_MATERIAL + string.Format("{0}, {1}, {2}, '{3}', '{4}', '{5}', {6}", OPCIONCR, FMPKey, MATERIALLINK, CLAVECR, UNIDADORG, UNIDADCR, FACTOR);
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



        #region REL PRODUCTO ESTRUCTURA

        public DataTable RelProdEstruc(int PRODUCTO_KEY = 0)
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_CR_PROD_ESTRUCTURA + string.Format("{0}", PRODUCTO_KEY);
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




        #region ESTRUCTURAS

        public DataTable CatEstructuras(int OPCION = 0, int ESTRUCTURA_KEY = 0, string FEC_ESTRUCTURA = "", int PRODUCTO_KEY = 0, string ORDEN = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_ESTRUCTURAS + string.Format("{0}, {1}, '{2}', {3}, '{4}'", OPCION, ESTRUCTURA_KEY, FEC_ESTRUCTURA, PRODUCTO_KEY, ORDEN);
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



        #region ESTRUCTURAS Y MATERIALES

        public DataTable RelEstructMat(int OPCION = 0, int PRODC_KEY = 0, string CVE_MAT = "", decimal CANT_MATERIAL = 0, decimal CANT_MERMA = 0, decimal CANT_DESPR = 0, int PRODUCTO_LINK = 0, int ESTRUCT_KEY = 0, string PEDIMENTO = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_CR_PRODUCTOMATERIAL + string.Format("{0}, {1}, '{2}', {3}, {4}, {5}, {6}, {7}, '{8}'", OPCION, PRODC_KEY, CVE_MAT, CANT_MATERIAL, CANT_MERMA, CANT_DESPR, PRODUCTO_LINK, ESTRUCT_KEY, PEDIMENTO);
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



        #region MATERIALES Y ALTERNATIVOS

        public DataTable RelMatAlternativo(int OPCION = 0, Int64 ALTERNATIV_KEY = 0, string CVE_MAT = "", Int64 PRODUCTO_LINK = 0, Int64 PRODMAT_KEY = 0)
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_CR_MATE_ALTERNA + string.Format("{0}, {1}, '{2}', {3}, {4}", OPCION, ALTERNATIV_KEY, CVE_MAT, PRODUCTO_LINK, PRODMAT_KEY);
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


        #region RELACION PERMISOS

        public DataTable RelPermisoDet(int Opcion = 1, int DetPermisoKey = 0, int PermisoKey = 0, int Linea = 1, decimal Cant = 0, string Unidad = "", string Descripcion = "", string Fraccion = "", decimal PrecioUnitUSD = 0, decimal Total = 0, string NumeroParte = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_CR_PERMISO_DETALLE + string.Format("{0}, {1}, {2}, {3}, {4}, '{5}', '{6}', '{7}', {8}, {9}, '{10}'", Opcion, DetPermisoKey, PermisoKey, Linea, Cant, Unidad, Descripcion, Fraccion, PrecioUnitUSD, Total, NumeroParte);
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


        #region REL FACTOR PRODUCTO

        public DataTable RelFactorProd(int OPCIONCR = 0, int FMPKey = 0, int PRODUCTOLINK = 0, string CLAVECR = "", string UNIDADORG = "", string UNIDADCR = "", decimal FACTOR = 0)
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_CR_PROD_FACTOR + string.Format("{0}, {1}, {2}, '{3}', '{4}', '{5}', '{6}'", OPCIONCR, FMPKey, PRODUCTOLINK, CLAVECR, UNIDADORG, UNIDADCR, FACTOR);
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



        #region PEDIMENTOS


        /// <summary>
        /// SELECCIONA LOS PEDIMENTOS
        /// </summary>
        public DataTable SelectPedimentos()
        {
            try
            {
                CadenaSQL = sp_Importaciones.SACEWEB_SEL_PEDIMENTOS;
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



        #region PEDIMENTOS EXPO


        /// <summary>
        /// SELECCIONA LOS PEDIMENTOS
        /// </summary>
        public DataTable SelectPedimentosExpo()
        {
            try
            {
                CadenaSQL = sp_Exportaciones.SACIWEB_SEL_PEDIMENTOS_EXPO;
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



        #region CATALOGO PLANTAS


        /// <summary>
        /// CATALOGO CLIENTES
        /// </summary>
        public DataTable CatalogoPlantas(int OPCION = 0, int PLANTAKEY = 0, string PLANTAID = "", string NOMBRE = "", string UBICACION = "", int FOLIO = 0, string DIR1 = "", string DIR2 = "", string DIR3 = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_CAT_PLANTAS + string.Format("{0}, '{1}', '{2}', '{3}', {4}, '{5}', '{6}', '{7}', {8}", PLANTAKEY, PLANTAID, NOMBRE, UBICACION, FOLIO, DIR1, DIR2, DIR3, OPCION);
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




        #region CATALOGO USUARIOS


        ///// <summary>
        ///// CATALOGO USUARIOS
        ///// </summary>
        //public DataTable CatalogoUsuarios(int OPCION = 0, int PK_ID_USER = 0, string CVE_USER = "", string PWD_USER = "", string NOM_USER = "", string DES_MAIL_USER = "", bool STA_USUARIO = false, int FK_ID_PERFIL_USER = 0, string TEL = "", int SMS = 0, int BLOQUEO = 0, string CHK_MAIL = "")
        //{
        //    try
        //    {
        //        CadenaSQL = sp_Catalogos.SP_CAT_USUARIOS + string.Format("{0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', {9}, {10}, '{11}'", OPCION, PK_ID_USER, CVE_USER, PWD_USER, NOM_USER, DES_MAIL_USER, STA_USUARIO, FK_ID_PERFIL_USER, TEL, SMS, BLOQUEO, CHK_MAIL);
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

        /// <summary>
        /// CATALOGO USUARIOS
        /// </summary>

        //public DataTable CatalogoUsuarios(int OPCION = 0, int PK_ID_USER = 0, string CVE_USER = "", string PWD_USER = "", string NOM_USER = "", string DES_MAIL_USER = "", bool STA_USUARIO = false, int FK_ID_PERFIL_USER = 0, string TEL = "", int SMS = 0, int BLOQUEO = 0, string CHK_MAIL = "", string PLANTAS = "")
        public DataTable CatalogoUsuarios(int OPCION = 0, int PK_ID_USER = 0, string CVE_USER = "", string PWD_USER = "", string NOM_USER = "", string DES_MAIL_USER = "", bool STA_USUARIO = false, int FK_ID_PERFIL_USER = 0, string TEL = "", int SMS = 0, int BLOQUEO = 0, string CHK_MAIL = "", string PLANTAS = "", int VIGENCIA = 0)
        {
            try
            {
                //CadenaSQL = sp_Catalogos.SP_CAT_USUARIOS + string.Format("{0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', {9}, {10}, '{11}', '{12}'", OPCION, PK_ID_USER, CVE_USER, PWD_USER, NOM_USER, DES_MAIL_USER, STA_USUARIO, FK_ID_PERFIL_USER, TEL, SMS, BLOQUEO, CHK_MAIL, PLANTAS);
                CadenaSQL = sp_Catalogos.SP_CAT_USUARIOS + string.Format("{0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', {9}, {10}, '{11}', '{12}', {13}", OPCION, PK_ID_USER, CVE_USER, PWD_USER, NOM_USER, DES_MAIL_USER, STA_USUARIO, FK_ID_PERFIL_USER, TEL, SMS, BLOQUEO, CHK_MAIL, PLANTAS, VIGENCIA);
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

        #region ACCESOS USUARIOS


        /// <summary>
        /// ACCESOS USUARIOS
        /// </summary>
        public DataTable ACCESOS_USUARIOS(int ID_PEFIL = 0, string CVE_PANTALLA = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_ACCESO_USUARIO + string.Format("{0}, '{1}'", ID_PEFIL, CVE_PANTALLA);
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

        #region CATALOGO PAISES
        public DataTable catPaises()
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_PAISES;
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

        #region CATALOGO INCOTEMR
        public DataTable CatIncoterm()
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_CAT_INCOTERM;
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

        #region CATALOGO FORMA PAGO
        public DataTable CatFormaPago()
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_CAT_FORMAS_PAGO;
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

        #region INFORMACION INICIAL VENCIMIENTOS/MENSAJES
        public DataTable MENSAJES_INICIO(int OPCION = 0)
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_WARNINGS + string.Format("{0}", OPCION);
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

        public DataTable DETALLE_WARNINGS(int OPCION = 0)
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_DETALLE_WARNINGS;
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

        #region REGISTRO SACI
        public DataTable InfoRegistro(int OPCION = 0, string DENOMINACION = "", string RFC = "", string REGISTROSACI = "")
        {
            try
            {
                return _ObjetoDBSQL.ConectarUpdRegistro(sp_Catalogos.SACIWEB_REGISTRO, OPCION, DENOMINACION, RFC, REGISTROSACI);

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

        #region CONSULTAS

        /// <summary>
        /// CONSULTAS
        /// </summary>
        public DataTable TraerConsultas(int OPCION = 0)
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_CAT_CONSULTAS + string.Format("{0}", OPCION);
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

        public DataTable ObtenerDatosConsultasCount(string strQuery)
        {
            try
            {
                CadenaSQL = strQuery;
                return _ObjetoDBSQL.ConectarConsultaCount(CadenaSQL);
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

        public DataTable ObtenerDatosConsultasBloques(string strQuery, int inicio, int fin)
        {
            try
            {
                CadenaSQL = strQuery;
                return _ObjetoDBSQL.ConectarConsultabloques(CadenaSQL, inicio, fin);
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



        #region CATALOGO CRUD CONSLTAS


        /// <summary>
        /// CATALOGO CLIENTES
        /// </summary>
        public DataTable CatalogoConsultasCrud(int OPCION = 0, int CONSULTAKEY = 0, string NOMBRE = "", string CONSULTA = "", string DESCRIPCION = "", bool PARAMETROS = false)
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_CAT_CONSULTAS_CRUD;
                return _ObjetoDBSQL.ConectarEstructConsultas(CadenaSQL, OPCION, CONSULTAKEY, NOMBRE, CONSULTA, DESCRIPCION, PARAMETROS); ;
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

        #region CATALOGO CERTIFICACIONES (DATOS GENERALES)
        public DataTable CertificacionesDG(int OPCION = 0, Int64 Certificacion_Key = 0, string rfc = "", string clave = "", string descripcion = "", string inicio = "", string fin = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_CAT_DG_CERTIFICACIONES + string.Format("{0},{1},'{2}','{3}','{4}','{5}','{6}'", OPCION, Certificacion_Key, rfc, clave, descripcion, inicio, fin);
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

        #region MAIL


        /// <summary>
        /// MAIL
        /// </summary>
        public DataTable TraerMail()
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_MAILFROM;
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

        #region BITACORA MOVIMIENTOS


        /// <summary>
        /// BITACORA MOVIMIENTOS
        /// </summary>
        public DataTable ValidarBitacora(int opcion = 0, Int64 idBitacora = 0, Int64 idUsuario = 0, string modulo = "", string accion = "", string detalle = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_BITACORA_MOVIMIENTOS + string.Format("{0},{1}, {2} ,'{3}','{4}','{5}'", opcion, idBitacora, idUsuario, modulo, accion, detalle);
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

        #region CATALOGO ESTATUS


        /// <summary>
        /// CATALOGO ESTATUS
        /// </summary>
        public DataTable CatalogoEstatus()
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_ESTATUS_ARCHI;
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

        #region CATALOGO AF



        /// <summary>
        /// CONSULTA PLANTA USUARIO
        /// </summary>
        public DataTable TraePlantasPorUsuario(string USUARIO = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_PLANTA_USUARIO + string.Format("'{0}'", USUARIO);
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

        #region CATALOGO TRATADOS


        /// <summary>
        /// CATALOGO TRATADOS
        /// </summary>
        public DataTable CatalogosTratados(int OPCION = 0, Int64 CKEY = 0, string CLAVE = "", string DESC = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_TRATADO + string.Format("{0}, {1}, '{2}', '{3}'", OPCION, CKEY, CLAVE, DESC);
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


        #region CATALOGO CAPITULOS


        /// <summary>
        /// CATALOGO TRATADOS
        /// </summary>
        public DataTable CatalogosCapitulos(int OPCION = 0, Int64 CKEY = 0, string CAPITULO = "", string DESC = "", string TRATADO = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_CAPITULO + string.Format("{0}, {1}, '{2}', '{3}', '{4}'", OPCION, CKEY, CAPITULO, DESC, TRATADO);
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


        #region CATALOGO REGLAS DE NEGOCIO
        public DataTable CatalogoReglaOrigen(int OPCION = 0, Int64 REGLA_KEY = 0, string TRATADO = "", string CAPITULO = "0", string CONDICION = "", string REGLA = "", string NOTA_ID = "0", string TRADADO_BUSQUEDA = "", string REGLA_BUSQUEDA = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_REGLA_ORIGEN + string.Format("{0}, {1}, '{2}', {3},'{4}','{5}',{6},'{7}','{8}'", OPCION, REGLA_KEY, TRATADO, CAPITULO, CONDICION, REGLA, NOTA_ID, TRADADO_BUSQUEDA, REGLA_BUSQUEDA);
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

        #region CATALOGO REGION
        public DataTable CatalogoRegion(int OPCION = 0, Int64 REGION_KEY = 0, string TRATADO = "", string REGION = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_REGION + string.Format("{0}, {1},'{2}','{3}'", OPCION, REGION_KEY, TRATADO, REGION);

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
                    throw new ApplicationException(string.Format("Error: {0}. {1}. {2}. {3}.", ex.Message.Replace("\r\n", "|").Replace("'", "´"), "|Metodo: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
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

        #region CATALOGO NOTAS CAPITULO


        /// <summary>
        /// CATALOGO NOTAS CAPITULO
        /// </summary>
        public DataTable CatalogosNotasCapitulos(int OPCION = 0, Int64 KEY = 0, int CAPITULO_ID = 0, string TEXTO = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_NOTAS_CAPITULO + string.Format("{0}, {1}, {2}, '{3}'", OPCION, KEY, CAPITULO_ID, TEXTO);
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

        #region NOTA
        public DataTable CatalogoNota(int OPCION = 0, Int64 Notas_Key = 0, string TRATADO = "", Int64 Nota_Id = 0, string Nota_Texto = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_Notas + string.Format("{0}, {1}, '{2}', {3},'{4}'", OPCION, Notas_Key, TRATADO, Nota_Id, Nota_Texto);
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

        #region CATALOGO CASOS DE REGLA
        public DataTable CatalogoCasosRegla(int OPCION = 0, Int64 CASO_KEY = 0, decimal REGLA_LINK = 0, string CONDICION_CASO = "", string TEXTO_CASO = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SP_CAT_CASOS_REGLA + string.Format("{0}, {1}, '{2}', '{3}',{4}", CASO_KEY, REGLA_LINK, CONDICION_CASO, TEXTO_CASO, OPCION);
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



