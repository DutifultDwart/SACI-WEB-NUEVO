using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Datos
{
    public class d_OLEDb
    {
        protected SqlConnection _sqlConn = null;
        protected string _sConnStr = null;
        protected string _sConnectionSql = null;
        protected bool _bConexion = false;
        protected SqlCommand _sqlCmd = null;
        protected string sSentencia;


        public d_OLEDb()
        {
            _sqlCmd = new SqlCommand();
        }




        public string ConectionString
        {
            get { return _sConnStr; }
            set { _sConnStr = value; }
        }

        public string ConectionStringSQL
        {
            get { return _sConnectionSql; }
            set { _sConnectionSql = value; }
        }

        public bool OpenConection()
        {
            if ((_sqlConn == null) || (_sqlConn.State != ConnectionState.Open))
            {
                _sqlConn = new SqlConnection(_sConnStr);
                _sqlConn.Open();
                _sqlCmd.Connection = _sqlConn;
                _bConexion = true;
            }
            return _bConexion;
        }


        public DataTable ExecuteSQLDT(string strSQL)
        {
            _sqlCmd.CommandText = strSQL;
            DataTable table = new DataTable();
            string sDNS = string.Empty;

            try
            {
                //_sqlCmd.CommandTimeout = 180;
                _sqlCmd.CommandTimeout = 0; //480; //8min
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


        public DataTable ExecuteConsultasCountSQLDT(string strSQL)
        {
            _sqlCmd.CommandText = strSQL;
            DataTable table = new DataTable();
            string sDNS = string.Empty;

            try
            {
                _sqlCmd.CommandTimeout = 0;
                using (SqlDataReader reader = _sqlCmd.ExecuteReader())
                {
                    int countRows = 0;
                    while (reader.Read())
                    {
                        countRows = countRows + 1;
                    }

                    table.Columns.Add("total", typeof(String));
                    DataRow dtRow;
                    dtRow = table.NewRow();
                    dtRow[0] = countRows - 1;
                    table.Rows.Add(dtRow);
                }
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

        public DataTable ExecuteConsultasBloquesSQLDT_Orig(string cadena, string strSQL, int inicio, int fin)
        {
            _sqlCmd.CommandText = strSQL;
            DataTable table = new DataTable();
            string sDNS = string.Empty;
            var datos = new List<string>();
            int paso = 0;
            try
            {

                //using (SqlConnection con = new SqlConnection(cadena))
                //{
                //    using (SqlCommand cmd = new SqlCommand(strSQL, con))
                //    {
                //        cmd.CommandType = CommandType.Text;
                //        cmd.CommandTimeout = 0;
                //        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                //        {
                //            sda.Fill(table);
                //        }
                //    }
                //}




                _sqlCmd.CommandTimeout = 0;
                using (SqlDataReader reader = _sqlCmd.ExecuteReader())
                {
                    int columns = reader.FieldCount - 1;

                    Int64 countRows = 0;
                    while (reader.Read())
                    {
                        if (countRows >= inicio && countRows < fin)
                        {
                            DataRow dtRow;
                            dtRow = table.NewRow();

                            for (int i = 0; i <= columns; i++)
                            {
                                if (paso.Equals(0))
                                    table.Columns.Add(reader.GetName(i), typeof(string));

                                dtRow[i] = reader.GetValue(i).ToString(); //reader.GetString(i);
                                //datos.Add(reader.GetName(i));
                            }

                            table.Rows.Add(dtRow);
                            paso = paso + 1;
                        }

                        countRows = countRows + 1;
                    }
                }

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


        public DataTable ExecuteConsultasBloquesSQLDT(string cadena, string strSQL, int inicio, int fin)
        {
            strSQL = "SELECT 1 AS X;" + strSQL;
            _sqlCmd.CommandText = strSQL;
            DataTable dtTemp = new DataTable();

            DataTable table = new DataTable();
            string sDNS = string.Empty;
            var datos = new List<string>();
            try
            {
                _sqlCmd.CommandTimeout = 0;
                using (SqlDataReader reader = _sqlCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        dtTemp.Load(reader);
                        table.Load(reader);

                    }

                }

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

        public DataTable ExecuteSQLDTEstruct(string strSQL, DataTable Impor)
        {
            _sqlCmd.CommandText = strSQL;
            DataTable table = new DataTable();
            string sDNS = string.Empty;
            try
            {
                //_sqlCmd.CommandTimeout = 180;
                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@T_CTM", SqlDbType.Structured).Value = Impor;
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

        public DataTable ExecuteSPInfoRegistro(string SP, int OPCION, string DENOMINACION, string RFC, string REGISTROSACI)
        {
            _sqlCmd.CommandText = SP;
            DataTable table = new DataTable();
            string sDNS = string.Empty;
            try
            {
                //_sqlCmd.CommandTimeout = 180;
                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@DENOMINACION", SqlDbType.Char).Value = DENOMINACION;
                _sqlCmd.Parameters.Add("@RFC", SqlDbType.Char).Value = RFC;
                _sqlCmd.Parameters.Add("@REGISTROSACI", SqlDbType.NVarChar).Value = REGISTROSACI;
                _sqlCmd.Parameters.Add("@OPCION", SqlDbType.Int).Value = OPCION;
                using (SqlDataReader reader = _sqlCmd.ExecuteReader())
                {
                    table.Load(reader);
                }
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

        public DataTable ExecuteSQLRegistri(string strSQL, DataTable Impor)
        {
            _sqlCmd.CommandText = strSQL;
            DataTable table = new DataTable();
            string sDNS = string.Empty;
            try
            {
                //_sqlCmd.CommandTimeout = 180;
                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@T_CTM", SqlDbType.Structured).Value = Impor;
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

        /// <summary>	
        /// inventario final 	
        /// </summary>                	
        public DataTable ExecuteSQLDTEstructInvFianl(string strSQL, int Opcion, DataTable Impor)
        {
            _sqlCmd.CommandText = strSQL;
            DataTable table = new DataTable();
            string sDNS = string.Empty;
            try
            {
                //_sqlCmd.CommandTimeout = 180;	
                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@OPCION", SqlDbType.Int).Value = Opcion;
                _sqlCmd.Parameters.Add("@TB_INV_FIN", SqlDbType.Structured).Value = Impor;
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

        /// <summary>
        /// INVENTARIO INICIAL 
        /// </summary>                
        public DataTable ExecuteSQLDTEstructInvInicial(string strSQL, int Opcion, DataTable Impor)
        {
            _sqlCmd.CommandText = strSQL;
            DataTable table = new DataTable();
            string sDNS = string.Empty;
            try
            {
                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@OPCION", SqlDbType.Int).Value = Opcion;
                _sqlCmd.Parameters.Add("@TB_INV_INI", SqlDbType.Structured).Value = Impor;
                using (SqlDataReader reader = _sqlCmd.ExecuteReader())
                {

                    table.Load(reader);
                }
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

        public virtual void CloseConnection()
        {
            if (_sqlConn != null)
            {
                if (_sqlConn.State == ConnectionState.Open)
                    _sqlConn.Close();
                _sqlCmd = null;
            }
        }



        /// <summary>	
        /// inventario final 	
        /// </summary>                	
        public DataTable ExecuteSQLDTEstructConsultas(string strSQL, int OPCION = 0, int CONSULTAKEY = 0, string NOMBRE = "", string CONSULTA = "", string DESCRIPCION = "", bool PARAMETROS = false)
        {
            _sqlCmd.CommandText = strSQL.Trim();
            DataTable table = new DataTable();
            string sDNS = string.Empty;
            try
            {
                //_sqlCmd.CommandTimeout = 180;	
                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@OPCION", SqlDbType.Int).Value = OPCION;
                _sqlCmd.Parameters.Add("@CONSULTAKEY", SqlDbType.BigInt).Value = CONSULTAKEY;
                _sqlCmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = NOMBRE;
                _sqlCmd.Parameters.Add("@CONSULTA", SqlDbType.VarChar).Value = CONSULTA;
                _sqlCmd.Parameters.Add("@DESCRIPCION", SqlDbType.VarChar).Value = DESCRIPCION;
                _sqlCmd.Parameters.Add("@PARAMETROS", SqlDbType.Bit).Value = PARAMETROS;

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

        public DataTable ExecuteSQLDTEstructAcdes(string strSQL, DataTable ACDes)
        {
            _sqlCmd.CommandText = strSQL;
            DataTable table = new DataTable();
            string sDNS = string.Empty;
            try
            {
                //_sqlCmd.CommandTimeout = 180;
                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@T_ACDES", SqlDbType.Structured).Value = ACDes;
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

        public DataTable ExecuteSQLDTEstructFactura(string strSQL, DataTable ACDes)
        {
            _sqlCmd.CommandText = strSQL;
            DataTable table = new DataTable();
            string sDNS = string.Empty;
            try
            {
                //_sqlCmd.CommandTimeout = 180;
                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@T_FACTURA", SqlDbType.Structured).Value = ACDes;
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



        //ARCHIVOS DE LA DESCARGA
        public DataTable ExecuteUplArchivoDescarga(string SP, string FILENAME, byte[] file)
        {
            _sqlCmd.CommandText = SP;
            DataTable table = new DataTable();
            try
            {
                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@OPCION", SqlDbType.Int).Value = 2;
                _sqlCmd.Parameters.Add("@NOMBRE_ARCHIVO", SqlDbType.VarChar).Value = FILENAME;
                _sqlCmd.Parameters.Add("@FILE", SqlDbType.VarBinary).Value = file;

                using (SqlDataReader reader = _sqlCmd.ExecuteReader())
                {
                    table.Load(reader);
                }
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


        public DataTable ExecuteSQLDTEstructFacturaServicios(string strSQL, DataTable ACDes)
        {
            _sqlCmd.CommandText = strSQL;
            DataTable table = new DataTable();
            string sDNS = string.Empty;
            try
            {
                //_sqlCmd.CommandTimeout = 180;
                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@T_FACTURA_SERVICIOS", SqlDbType.Structured).Value = ACDes;
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

        //INTERFACE PEDIMENTOS
        public DataTable ExecuteSQLDTEstructPedimentos(string strSQL, DataTable dtPedimentos)
        {
            _sqlCmd.CommandText = strSQL;
            DataTable table = new DataTable();
            string sDNS = string.Empty;
            try
            {
                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@T_PEDIMENTOS", SqlDbType.Structured).Value = dtPedimentos;
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


        //INFORME AJUSTES CON 3 SELECTS
        public void ExecuteSQLRptAjustes(string sp, int opcion, string desde, string hasta, string anio, ref DataTable dt1, ref DataTable dt2, ref DataTable dt3)
        {
            _sqlCmd.CommandText = sp;
            DataTable table = new DataTable();
            string sDNS = string.Empty;
            try
            {
                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@OPCION", SqlDbType.Int).Value = opcion;
                _sqlCmd.Parameters.Add("@DESDE", SqlDbType.VarChar).Value = desde;
                _sqlCmd.Parameters.Add("@HASTA", SqlDbType.VarChar).Value = hasta;
                _sqlCmd.Parameters.Add("@ANIO", SqlDbType.VarChar).Value = anio;
                using (SqlDataReader reader = _sqlCmd.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        dt1.Load(reader);
                        dt2.Load(reader);
                        dt3.Load(reader);
                    }
                    else
                    {
                        dt1.Load(reader);
                        dt2.Load(reader);
                        dt3.Load(reader);
                    }


                    //if (reader.Read())
                    //{
                    //    dt1.Load(reader);

                    //    if (reader.NextResult() && reader.Read())
                    //    {
                    //        dt2.Load(reader);
                    //    }

                    //    if (reader.NextResult() && reader.Read())
                    //    {
                    //        dt3.Load(reader);
                    //    }
                    //}
                    //else
                    //{
                    //    dt1.Load(reader);
                    //    dt2.Load(reader);
                    //    dt3.Load(reader);
                    //}
                }
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
        }

        /// CARGA INTERFACE ARCHIVOS PROCESOS
        //ARCHIVOS DE LA DESCARGA
        public DataTable ExecuteUplArchivosProc(string SP = "", int OPCION = 0, Guid UUID = default(Guid), string FILES = "", string NOMBRE = "", int ESTATUS = 0, string USUARIO_ID = "", int size = 0)
        {
            _sqlCmd.CommandText = SP;
            DataTable table = new DataTable();
            try
            {
                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@OPCION", SqlDbType.Int).Value = OPCION;
                _sqlCmd.Parameters.Add("@UUID", SqlDbType.UniqueIdentifier).Value = UUID;
                _sqlCmd.Parameters.Add("@FILES", SqlDbType.VarChar, size).Value = FILES;
                _sqlCmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = NOMBRE;
                _sqlCmd.Parameters.Add("@ESTATUS", SqlDbType.Int).Value = ESTATUS;
                _sqlCmd.Parameters.Add("@USUARIO_ID", SqlDbType.VarChar).Value = USUARIO_ID;

                using (SqlDataReader reader = _sqlCmd.ExecuteReader())
                {
                    table.Load(reader);
                }
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

        //INTERFACE DIRIGIDOS
        public DataTable ExecuteSQLDTEstructDirigidos(string strSQL, DataTable ACDes)
        {
            _sqlCmd.CommandText = strSQL;
            DataTable table = new DataTable();
            string sDNS = string.Empty;
            try
            {
                //_sqlCmd.CommandTimeout = 180;
                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@T_DIRIGIDO", SqlDbType.Structured).Value = ACDes;
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

        //INTERFACE CARGA BOMS
        public DataTable ExecuteSQLCargaBOMS(string strSQL, DataTable dtDatos,string nombre_archivo)
        {
            _sqlCmd.CommandText = strSQL;
            DataTable table = new DataTable();
            string sDNS = string.Empty;
            try
            {
                //_sqlCmd.CommandTimeout = 180;
                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@T_BOM", SqlDbType.Structured).Value = dtDatos;
                _sqlCmd.Parameters.Add("@NOMBRE_ARCHIVO", SqlDbType.VarChar).Value = nombre_archivo;
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

        //INTERFACE ANALISIS ESTRUCTURAS
        public DataTable ExecuteSQLDTAnalisisEstruct(string strSQL, int opcion, string desde, string hasta)
        {
            _sqlCmd = new SqlCommand();
            _sqlCmd.CommandText = strSQL;

            DataTable table = new DataTable();
            string sDNS = string.Empty;
            try
            {
                _sqlCmd.Connection = _sqlConn;

                _sqlCmd.CommandTimeout = 0;
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Add("@OPCION", SqlDbType.Int).Value = opcion;
                _sqlCmd.Parameters.Add("@DESDE", SqlDbType.VarChar).Value = desde;
                _sqlCmd.Parameters.Add("@HASTA", SqlDbType.VarChar).Value = hasta;
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

    }
}
