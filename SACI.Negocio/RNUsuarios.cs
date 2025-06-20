using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Negocio
{
    public class RNUsuarios
    {
        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;

        #endregion


        public RNUsuarios()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }



        /// <summary>
        /// INSERTA EVENTO EN BITACORA
        /// </summary>
        //public DataTable CatUsuarios(int OPCION = 0, int PK_ID_USER = 0, string CVE_USER = "", string PWD_USER = "", string NOM_USER = "", string DES_MAIL_USER = "", bool STA_USUARIO = false, int FK_ID_PERFIL_USER = 0)
        public DataTable CatUsuarios(int OPCION = 0, int PK_ID_USER = 0, string CVE_USER = "", string PWD_USER = "", string NOM_USER = "", string DES_MAIL_USER = "", bool STA_USUARIO = false, int FK_ID_PERFIL_USER = 0, string TEL = "", int SMS = 0, int BLOQUEO = 0, string CHK_MAIL = "", string PLANTAS = "", int VIGENCIA = 0)
        {
            try
            {
                //CadenaSQL = sp_Catalogos.SP_CAT_USUARIOS + string.Format("{0}, {1}, '{2}', '{3}' ,'{4}', '{5}', '{6}', {7}", OPCION, PK_ID_USER, CVE_USER, PWD_USER, NOM_USER, DES_MAIL_USER, STA_USUARIO, FK_ID_PERFIL_USER);
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

        public DataTable Acceso(int OPCION = 0, string USER = "", string ACCION = "", string DETALLE = "", string SISTEMA = "", string FECHA1 = "", string FECHA2 = "")
        {
            try
            {
                CadenaSQL = sp_Catalogos.SACIWEB_ACCESO + string.Format("{0}, '{1}', '{2}', '{3}' ,'{4}', '{5}', '{6}'", OPCION, USER, ACCION, DETALLE, SISTEMA, FECHA1, FECHA2);
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


    }
}
