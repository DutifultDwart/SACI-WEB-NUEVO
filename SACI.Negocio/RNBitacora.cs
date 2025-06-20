using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Negocio
{
    public class RNBitacora
    {
        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;

        #endregion


        public RNBitacora()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }


        /// <summary>
        /// INSERTA EVENTO EN BITACORA
        /// </summary>
        public DataTable InsertBitacora(string DesEvento, string FecEvento, string CveUsuario, string NomPantalla, string NomEquipo)
        {
            try
            {
                CadenaSQL = sp_Catalogos.SpBitacora + "'" + DesEvento + "', '" + FecEvento + "', '" + CveUsuario + "', '" + NomPantalla + "', '" + NomEquipo + "'";
                return _ObjetoDBSQL.Conectar(CadenaSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _ObjetoDB = null;
            }
        }


    }
}
