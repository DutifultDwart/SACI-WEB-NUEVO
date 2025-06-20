using SACI.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SACI.Negocio
{
    public class RNRegistroSaci
    {
        #region Propiedades Globales

        d_OLEDb _ObjetoDB;
        ConecctionSQL _ObjetoDBSQL;
        string CadenaSQL = string.Empty;
        RNCatalogos RNCat = null;
        #endregion

        [DllImport("DelphiLibrary.dll", EntryPoint = "descifrar",
                     CallingConvention = CallingConvention.StdCall,
                     CharSet = CharSet.Ansi)]
        public static extern bool
            descifrar(int Key, string inputString, int outputStringBufferSize, ref string outputStringBuffer);

        public RNRegistroSaci()
        {
            _ObjetoDBSQL = new ConecctionSQL();
        }

        public static string NumberEncode(string str)
        {
            string Result = "";
            int charVal;
            int Longitud;
            char c;

            Longitud = str.Length;

            for (int x = 0; x < Longitud; x++)
            {
                c = str[x];
                charVal = (int)c;
                Result = Result + "|" + Convert.ToString(charVal);
            }

            return Result;
        }

        public string ValidaRegistro(int opcion)
        {
            RNCat = new RNCatalogos();
            DataTable dtDatosGrales = new DataTable();
            string rfcRegistro, fecha, RESULTADO = "";
            try
            {
                const int stringBufferSizeD = 1024;
                var outputStringBufferD = new String('\x00', stringBufferSizeD);
                var inputstringD = new String('\x00', 1024);

                string rfc, cadena,dia, año, mes;
                string error = string.Empty;
                int largorfc;
              
                int difDias = 0;
                //DateTime? fechaAcceso = (DateTime?)null;
                DateTime fechaAcceso ;
                TimeSpan ts;

                dtDatosGrales = RNCat.InfoRegistro(1);
                rfc = dtDatosGrales.Rows[0]["Rfc"].ToString().Trim();
                cadena = dtDatosGrales.Rows[0]["REGISTROSACI"].ToString().Trim();
                //cadena = "想ܸ�ྸ�មӜ᷼▵땺ⰼ鱬㐟哽㪁솫䉤嚙";

                if (cadena.Trim() != string.Empty)
                {
                    inputstringD = NumberEncode(cadena);

                    descifrar(132002, inputstringD, stringBufferSizeD, ref outputStringBufferD);

                    //Inicio Funcion Registrar
                    if (outputStringBufferD != "")
                    {
                        if (opcion.Equals(1))
                        {
                            if (rfc == "" || rfc == string.Empty)
                            {
                                error = error + " EL RFC NO PUEDE ESTAR VACIO";
                            }

                            largorfc = rfc.Length;

                            rfcRegistro = outputStringBufferD.Substring(0, largorfc);

                            if (rfc != rfcRegistro)
                            {
                                string caracter1;
                                string caracter2;
                                int coincidencias=0;
                                double porcentaje;

                                for (int i = 0; i < largorfc; i++)
                                {
                                    caracter1 = rfc.Substring(i, 1);
                                    caracter2 = rfcRegistro.Substring(i, 1);
                                    if (caracter1==caracter2)
                                    {
                                        coincidencias = coincidencias + 1;
                                    }
                                }

                                porcentaje = (double.Parse(coincidencias.ToString())/double.Parse(largorfc.ToString()))*100;

                                if (porcentaje < 50)
                                {
                                    error = error + " EL RFC NO ESTA EN LA CADENA DE REGISTRO";    
                                }
                                
                            }

                            //Valido la fecha

                            dia = outputStringBufferD.Substring(largorfc, 2).ToString();
                            año = outputStringBufferD.Substring(largorfc + 2, 4).ToString();
                            mes = outputStringBufferD.Substring(largorfc + 6, 2).ToString();

                            dia = dia.Trim().Length.Equals(1) ? "0" + dia : dia;
                            mes = mes.Trim().Length.Equals(1) ? "0" + mes : mes; 

                            fecha = dia + "/" + mes + "/" + año;                            

                            // Trata de convertir la fecha
                            try
                            {
                                //fechaAcceso = DateTime.Parse(fecha);
                                fechaAcceso = DateTime.ParseExact(fecha, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            catch (FormatException)
                            {
                                error = error + " ERROR AL CONVERTIR LA FECHA";
                            }

                            if (error != "")
                            {
                                RESULTADO = "NO ERRORES:" + error;
                            }
                            else
                            {
                                //ts = DateTime.Parse(fecha) - DateTime.Today;
                                //difDias = (fechaAcceso - fechaActual).Days;
                                ts = DateTime.ParseExact(fecha, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) - DateTime.Today;
                                difDias = ts.Days;

                                if ((difDias >= 0) && (DateTime.ParseExact(fecha, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.Today))
                                {
                                    RESULTADO = "SI DIAS RESTANTES: " + difDias;
                                }
                                else
                                {
                                    RESULTADO = "NO DIAS VENCIDO: " + difDias;
                                }
                            }

                            return RESULTADO;
                        }
                        else //Valida solo la fecha
                        {
                            largorfc = rfc.Length;

                            //Valido la fecha

                            dia = outputStringBufferD.Substring(largorfc, 2).ToString();
                            año = outputStringBufferD.Substring(largorfc + 2, 4).ToString();
                            mes = outputStringBufferD.Substring(largorfc + 6, 2).ToString();

                            dia = dia.Trim().Length.Equals(1) ? "0" + dia : dia;
                            mes = mes.Trim().Length.Equals(1) ? "0" + mes : mes; 

                            fecha = dia + "/" + mes + "/" + año;
                            //fechaConvert = año.ToString() + mes.ToString() + dia.ToString();

                            // Trata de convertir la fecha
                            try
                            {
                                fechaAcceso = DateTime.ParseExact(fecha, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            catch (FormatException)
                            {
                                error = error + " ERROR AL CONVERTIR LA FECHA " + fecha;
                            }

                            if (error != "")
                            {
                                RESULTADO = "ERROR:" + error;
                            }
                            else
                            {
                                RESULTADO = "Registro válido hasta " + fecha;
                            }

                            return RESULTADO;
                        }
                        
                    }
                    else
                    {
                        return "NO HAY REGISTRO";
                    }
                    //Fin Funcion Registrar
                }
                else
                {
                    return "NO HAY REGISTRO";
                }

                //return outputStringBufferD;
            }
            catch (Exception ex)
            {
                //return RESULTADO;
                throw ex;
                //if (ex.Message.Substring(0, 1).ToString() == "1")
                //{
                //    throw ex;
                //}
                //else
                //{
                //    throw new ApplicationException(string.Format("Error: {0}. {1}. {2}. {3}. {4}", ex.Message.Replace("\r\n", "|").Replace("'", "´"), "|Metodo: " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                //    "|Recurso: " + ex.Source,
                //    "|Detalle: " + ex.StackTrace.Replace("\\", "/").Replace("\r\n", "|"),
                //    "|Sp: " + CadenaSQL.Replace("\r\n", "|").Replace("'", "´")), ex);
                //}

            }
            finally
            {
                _ObjetoDB = null;
            }
        }

        public string PipesStringToChine(string s)
        {

            string[] words = s.Split('|');
            string res;
            int i;
            res = "";

            foreach (var word in words)
            {
                if (word != "")
                {
                    i = Convert.ToInt32(word);
                    res = res + Convert.ToChar(i);
                }
            }

            return res;
        }
    }
}
