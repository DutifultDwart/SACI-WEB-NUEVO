using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Datos
{
    public static class d_Static_OLEDb
    {
        static SqlConnection _sqlConn = null;
        static string _sConnStr = null;
        static string _sConnectionSql = null;
        static bool _bConexion = false;
        static SqlCommand _sqlCmd = new SqlCommand();
        static string sSentencia;
      

        public static string ConectionString
        {
            get { return _sConnStr; }
            set { _sConnStr = value; }
        }

        public static string ConectionStringSQL
        {
            get { return _sConnectionSql; }
            set { _sConnectionSql = value; }
        }

        public static bool OpenConection()
        {
            _sqlCmd = new SqlCommand();
            if ((_sqlConn == null) || (_sqlConn.State != ConnectionState.Open))
            {
                _sqlConn = new SqlConnection(_sConnStr);
                _sqlConn.Open();
                _sqlCmd.Connection = _sqlConn;
                _bConexion = true;
            }
            return _bConexion;
        }


        public static DataTable ExecuteSQLDT(string strSQL)
        {
            _sqlCmd.CommandText = strSQL;
            DataTable table = new DataTable();
            string sDNS = string.Empty;
            try
            {
                _sqlCmd.CommandTimeout = 180;
                // Este objeto se destruye en la clase de implementación a través de System.GC.Collect(); 
                // NOTA ---> Si se destruye a este nivel a través del finally, perderia los valores antes de que lleguen a la capa superior <--- 
                //IDataReader oRd = _sqlCmd.ExecuteReader(CommandBehavior.SingleResult);

                //int count = oRd.FieldCount - 1;
                //for (int i = 0; i <= count; i++)
                //    table.Columns.Add(new DataColumn(oRd.GetName(i), oRd.GetFieldType(i)));

                //DataRow dtRow;
                //while (oRd.Read())
                //{
                //    dtRow = table.NewRow();
                //    for (int i = 0; i <= count; i++)
                //        dtRow[i] = oRd.GetValue(i);

                //    table.Rows.Add(dtRow);
                //}
                using (SqlDataReader reader = _sqlCmd.ExecuteReader())
                {
                    table.Load(reader);
                }
                //table.AcceptChanges();
            }
            catch (SystemException se)
            {
                table = null;
                sSentencia = se.Message.ToString();
                SystemException oSystemException = new SystemException(sSentencia.ToString(), se.InnerException);
                throw oSystemException;
            }
            finally
            {
                if (_sqlConn.State == ConnectionState.Open)
                    _sqlConn.Close();
                _sqlCmd = null;
            }
            return table;
        }


        public static void CloseConnection()
        {
            if (_sqlConn != null)
            {
                if (_sqlConn.State == ConnectionState.Open)
                    _sqlConn.Close();
                _sqlCmd = null;
            }
        }

    }
}
