using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using CieloGtecDAL.DAL;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Data;
using System.Collections.Specialized;


namespace Senac.Fecomercio.BLL.Utilities
{
    public static class SenacAspUtil
    {
        #region Localização do site ASP do GTEC
        // Retorna o caminho absoluto da página em questão no site ASP do Gtec
        public static string SiteGtecASP(string page)
        {
            string gtecBase = ConfigurationManager.AppSettings["SiteGtecASP"].ToString();
            if (gtecBase.Length == 0 || gtecBase.Substring(gtecBase.Length - 1, 1) != "/")
                gtecBase += "/";
            return gtecBase + page;
        }

        public static string RedirectToLogin()
        {
            return SiteGtecASP("login.asp");
        }

        #endregion

        #region Codificar e decodificar objetos para querystring
        public static string CodificarObjeto(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("Nenhum parâmetro fornecido");
            }

            BinaryFormatter formatter = new BinaryFormatter();
            byte[] dataBytes;

            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, data);
                dataBytes = stream.ToArray();
            }

            //return HttpServerUtility.UrlTokenEncode(dataBytes);
            // [Base64] parece não funcionar bem em querystring
            //return Convert.ToBase64String(dataBytes);

            return byte2hex(dataBytes);
        }

        public static object DecodificarObjeto(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Nenhum parâmetro fornecido");
            }

            //byte[] decoded = HttpServerUtility.UrlTokenDecode(value);
            //byte[] decoded = Convert.FromBase64String(value);
            byte[] decoded = hex2byte(value);

            BinaryFormatter formatter = new BinaryFormatter();
            object deserialized;

            using (MemoryStream stream = new MemoryStream(decoded))
            {
                deserialized = formatter.Deserialize(stream);
            }

            return deserialized;
        }

        public static string byte2hex(byte[] buffer)
        {
            string ret = "";

            foreach (byte b in buffer)
                ret += b.ToString("X").PadLeft(2, '0');

            return ret;
        }

        public static byte[] hex2byte(string hex)
        {
            List<byte> buffer = new List<byte>();

            for (int i = 0; i < hex.Length; i += 2)
                buffer.Add(Byte.Parse(hex.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));

            return buffer.ToArray();
        }

        #endregion

        #region Métodos utilizados na versão ASP do GTEC - métodos copiados do código ASP, traduzidos para C#
        public static string FormatHrs2DaysUteis(string strHrs2DaysHHHMMSS, int intHrsPerDay)
        {
            string vFormatHrs2DaysUteis = null;
            string vFormatHrs2DaysUteis_d = null;
            string vFormatHrs2DaysUteis_h = null;
            string vFormatHrs2DaysUteis_m = null;
            string vFormatHrs2DaysUteis_s = null;

            //If Not IsNull (strHrs2DaysHHHMMSS) And Len (strHrs2DaysHHHMMSS) > 0 Then
            if (IsNull(strHrs2DaysHHHMMSS) || strHrs2DaysHHHMMSS.Length == 0)
            {
                vFormatHrs2DaysUteis = "";
            }
            else
            {
                strHrs2DaysHHHMMSS = strHrs2DaysHHHMMSS.Replace(".000", "");
                if (strHrs2DaysHHHMMSS.Length > 0 & strHrs2DaysHHHMMSS != "00:00")
                {
                    string[] arrHrs2DaysHHHMMSS = strHrs2DaysHHHMMSS.Split(':');

                    if (intHrsPerDay == 0) //(String.Len(intHrsPerDay) == 0)
                        intHrsPerDay = 10;

                    if (arrHrs2DaysHHHMMSS.Length == 3)
                    {
                        vFormatHrs2DaysUteis_d = Math.Floor(Convert.ToDecimal(arrHrs2DaysHHHMMSS[0]) / intHrsPerDay).ToString("0");
                        vFormatHrs2DaysUteis_h = ZerosEsquerda(2, Convert.ToInt32(arrHrs2DaysHHHMMSS[0]) % intHrsPerDay);

                        vFormatHrs2DaysUteis_m = (arrHrs2DaysHHHMMSS[1]);
                        vFormatHrs2DaysUteis_s = (arrHrs2DaysHHHMMSS[2]);
                        string vFormatHrs2DaysUteis_hhmmss = vFormatHrs2DaysUteis_h + ":" + vFormatHrs2DaysUteis_m + ":" + vFormatHrs2DaysUteis_s;
                        //	If vFormatHrs2DaysUteis_hhmmss = "00:00:00" Then vFormatHrs2DaysUteis_hhmmss = ""
                        if (vFormatHrs2DaysUteis_hhmmss == "00:00:00")
                        {
                            vFormatHrs2DaysUteis_hhmmss = "";
                            //	ElseIf Right (vFormatHrs2DaysUteis_hhmmss,Len (vFormatHrs2DaysUteis_hhmmss)-2) = "00:00" Then
                        }
                        else if (Right(vFormatHrs2DaysUteis_hhmmss, 5) == "00:00")
                        {
                            vFormatHrs2DaysUteis_hhmmss = vFormatHrs2DaysUteis_h;
                            vFormatHrs2DaysUteis_hhmmss = vFormatHrs2DaysUteis_h + "hs";
                            //	ElseIf Right (vFormatHrs2DaysUteis_hhmmss,Len (vFormatHrs2DaysUteis_hhmmss)-6) = "00" Then
                        }
                        else if (Right(vFormatHrs2DaysUteis_hhmmss, 2) == "00")
                        {
                            vFormatHrs2DaysUteis_hhmmss = vFormatHrs2DaysUteis_h + ":" + vFormatHrs2DaysUteis_m;
                            vFormatHrs2DaysUteis_hhmmss = vFormatHrs2DaysUteis_h + "hs " + vFormatHrs2DaysUteis_m + "min";
                        }
                        else
                        {
                            vFormatHrs2DaysUteis_hhmmss = vFormatHrs2DaysUteis_hhmmss.Substring(0, vFormatHrs2DaysUteis_hhmmss.Length - 3);
                            vFormatHrs2DaysUteis_hhmmss = vFormatHrs2DaysUteis_h + "hs " + vFormatHrs2DaysUteis_m + "min";
                        }
                        vFormatHrs2DaysUteis = vFormatHrs2DaysUteis_d + "d " + vFormatHrs2DaysUteis_hhmmss;
                    }
                    else
                    {
                        vFormatHrs2DaysUteis = strHrs2DaysHHHMMSS;
                    }
                }
                else
                {
                    vFormatHrs2DaysUteis = "";
                }
            }
            return vFormatHrs2DaysUteis;
        }

        public static string FormataDataHora(object campoDataHora)
        {
            if (campoDataHora == null || campoDataHora == DBNull.Value)
                return "";

            return FormataDataHora(Convert.ToDateTime(campoDataHora));
        }

        public static string FormataDataHora(string dataHora)
        {
            return FormataDataHora(Convert.ToDateTime(dataHora.Trim()));
        }

        public static string FormataDataHora(DateTime dataHora)
        {
            return dataHora.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static string FormataData(string data)
        {
            return FormataData(Convert.ToDateTime(data.Trim()));
        }

        public static string FormataData(DateTime data)
        {
            return data.ToString("dd/MM/yyyy");
        }

        //public static string FormataHora(object campoDataHora)
        //{
        //    if (campoDataHora == null || campoDataHora == DBNull.Value)
        //        return "";

        //    return FormataHora(Convert.ToDateTime(campoDataHora));
        //}

        //public static string FormataHora(string dataHora)
        //{
        //    return FormataHora(Convert.ToDateTime(dataHora.Trim()));
        //}

        public static string FormataHora(DateTime dataHora)
        {
            return dataHora.ToString("HH:mm:ss");
        }

        public static string FormataHora_AtendTempoReal(string dataHora)
        {
            if (IsNull(dataHora))
                return "";

            // Na realidade, a stored procedure já elimina este trecho
            int i = dataHora.IndexOf('.');
            if (i >= 0)
                dataHora = dataHora.Substring(0, i);

            return dataHora;
        }

        public static string FormataDiaHora(int? tempo, string tipo)
        {
            string retorno = null;

            if (tempo != null && (tipo.Equals("S") || tipo.Equals("N")))
            {
                if (tipo == "S" && tempo.Equals(1))
                {
                    retorno = IsNull(tempo) ? string.Empty : ZerosEsquerda(2, tempo) + " hr";
                }
                else if (tipo == "S" && tempo > 1)
                {
                    retorno = IsNull(tempo) ? string.Empty : ZerosEsquerda(2, tempo) + " hrs";
                }
                else if (tipo == "N" && tempo.Equals(1))
                {
                    retorno = IsNull(tempo) ? string.Empty : ZerosEsquerda(2, tempo) + " dia";
                }
                else if (tipo == "N" && tempo > 1)
                {
                    retorno = IsNull(tempo) ? string.Empty : ZerosEsquerda(2, tempo) + " dias";
                }
            }

            return retorno;
        }

        public static string FormataDataHoraSQL(object data)
        {
            if (IsNull(data))
                return "";

            return FormataDataHoraSQL(Convert.ToDateTime(data));
        }

        public static string FormataDataHoraSQL(DateTime data)
        {
            if (IsNull(data))
                return "";

            return data.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ZerosEsquerda(int length, object campo)
        {
            if (campo == null)
                return "";

            return campo.ToString().PadLeft(length, '0');
        }

        public static string FormataParaNumero(object campo)
        {
            return FormataParaNumero(campo.ToString());
        }

        public static string FormataParaNumero(string entrada)
        {
            try
            {
                string s = entrada
                    .Replace("-", "")
                    .Replace(".", "")
                    .Replace("/", "")
                    .Replace(",", "")
                    .Replace("+", "")
                    .Replace(" ", "")
                    //.Replace ("/","")
                    ;

                double d = Convert.ToDouble(s);
                return d.ToString("0");
            }
            catch (Exception)
            {
            }
            return "";
        }

        public static string FormataFone(object campo)
        {
            return FormataFone(campo.ToString());
        }

        public static string FormataFone(string entrada)
        {
            try
            {
                string s = FormataParaNumero(entrada);
                if (s.Length >= 6)
                    return Left(s, s.Length - 4) + "-" + Right(s, 4);
            }
            catch (Exception)
            {
            }
            return entrada;
        }

        public static string FormataCEP(object campo)
        {
            return FormataCEP(campo.ToString());
        }

        public static string FormataCEP(string entrada)
        {
            string s = FormataParaNumero(entrada);
            if (s.Length == 0)
                return "";
            s = ZerosEsquerda(8, s);
            s = Left(s, s.Length - 3) + "-" + Right(s, 3);
            s = ZerosEsquerda(9, s);
            return s;
        }

        public static string Get_DC_SOLUCAO_DEF(string strCD_SOLUCAO_DEF)
        {
            string vGet_DC_SOLUCAO_DEF = "";
            if (strCD_SOLUCAO_DEF.Length > 0)
            {
                switch (strCD_SOLUCAO_DEF.ToUpper())
                {
                    case "L":
                        //	vGet_DC_SOLUCAO_DEF = "LYNX"
                        vGet_DC_SOLUCAO_DEF = "SCOREPOS";
                        break;
                    case "E":
                        //	vGet_DC_SOLUCAO_DEF = "GTeC - LYNX COM ERRO"
                        vGet_DC_SOLUCAO_DEF = "GTeC - SCOREPOS C/ERRO";
                        break;
                    case "C":
                        vGet_DC_SOLUCAO_DEF = "CENTRAL DE ATENDIMENTO";
                        break;
                    case "T":
                        //	vGet_DC_SOLUCAO_DEF = "GTeC - LYNX TIMEOUT"
                        vGet_DC_SOLUCAO_DEF = "GTeC - TIMEOUT SCOREPOS";
                        break;
                    case "I":
                        //	vGet_DC_SOLUCAO_DEF = "GTeC - LYNX BYPASS"
                        vGet_DC_SOLUCAO_DEF = "GTeC - SCOREPOS IGNORADO";
                        break;
                    case "Z":
                        //	vGet_DC_SOLUCAO_DEF = "GTeC - LYNX LISTA ZERADA"
                        vGet_DC_SOLUCAO_DEF = "GTeC - SCOREPOS C/LISTA ZERADA";
                        break;
                    case "M":
                        //	vGet_DC_SOLUCAO_DEF = "GTeC - LYNX LISTA ZERADA"
                        vGet_DC_SOLUCAO_DEF = "GTeC - TROCA DE MANUTENÇÃO";
                        break;
                    case "B":
                        //	vGet_DC_SOLUCAO_DEF = "GTeC - LYNX LISTA ZERADA"
                        vGet_DC_SOLUCAO_DEF = "GTeC - BYPASS SCOREPOS (EVENTO/DEPEND.)";
                        break;
                    default:
                        vGet_DC_SOLUCAO_DEF = "N/A";
                        break;
                }
            }
            else
            {
                vGet_DC_SOLUCAO_DEF = "N/A";
            }
            return vGet_DC_SOLUCAO_DEF;
        }

        // Nova implementação
        public static bool EhPar(int numero)
        {
            return numero % 2 == 0;
        }
        // Nova implementação
        public static int VerificacaoParImpar(int numero)
        {
            // 0=par  1=impar
            return numero % 2 == 1 ? 1 : 0;
        }

        public static string BuildXMLSintax(string strProcess, string strXMLLabel, string strXMLValue)
        {
            string vBuildXMLSintax = "";
            switch (strProcess.ToUpper())
            {
                case "TO":
                case "IB":
                case "ED":
                case "PR":
                case "2N":
                case "TR":
                case "DR":
                case "WS":
                    //	vBuildXMLSintax = vBuildXMLSintax & "<update><XMLChamado"
                    string[] arrstrXMLLabel = strXMLLabel.Split('|');
                    string[] arrstrXMLValue = strXMLValue.Split('|');
                    int UBstrXMLLabel = arrstrXMLLabel.GetUpperBound(0);
                    for (var iBXMLS = 0; iBXMLS <= UBstrXMLLabel; iBXMLS++)
                    {
                        vBuildXMLSintax = vBuildXMLSintax + " " + arrstrXMLLabel[iBXMLS] + "=\"" + arrstrXMLValue[iBXMLS].Trim() + "\"";
                    }
                    //	vBuildXMLSintax = vBuildXMLSintax & "></XMLChamado>"
                    //	vBuildXMLSintax = vBuildXMLSintax & "</update>"
                    break;
                default:
                    //	N2D
                    break;
            }
            return vBuildXMLSintax;
        }

        public static string CapturaStatusSolucionamento(object parTServ)
        {
            int intTServ = Convert.ToInt32(parTServ);
            string vCapturaStatusSolucionamento = null;
            switch (intTServ)
            {
                case 1:
                    vCapturaStatusSolucionamento = "ISO";
                    break;
                case 2:
                    vCapturaStatusSolucionamento = "MSO";
                    break;
                case 3:
                    vCapturaStatusSolucionamento = "DSO";
                    break;
                case 4:
                    vCapturaStatusSolucionamento = "TSO";
                    break;
                case 5:
                    vCapturaStatusSolucionamento = "FSO";
                    break;
                default:
                    vCapturaStatusSolucionamento = "";
                    break;
            }
            return vCapturaStatusSolucionamento;
        }

        #endregion

        #region Métodos criados para facilitar a conversão de ASP para ASP.NET
        public static bool IsDate(string data)
        {
            try
            {
                DateTime d = Convert.ToDateTime(data.Trim());
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        public static bool IsNull(string valor)
        {
            if (valor == null)
                return true;

            return valor.Trim().Length == 0;
        }

        public static bool IsNull(object campo)
        {
            if (campo == null || campo == DBNull.Value || campo.ToString().Trim().Length == 0)
                return true;

            return false;
        }

        public static object DefaultIfNull(object campo, object defaultValue)
        {
            if (campo == null || campo == DBNull.Value || campo.ToString().Trim().Length == 0)
                return defaultValue;

            return campo;
        }

        public static string Right(string param, int length)
        {
            try
            {
                return param.Substring(param.Length - length, length);
            }
            catch (Exception)
            {
            }
            return param;
        }

        public static string Left(string param, int length)
        {
            try
            {
                return param.Substring(0, length);
            }
            catch (Exception)
            {
                if (length > param.Length)
                    return param;

                throw;
            }
        }

        public static string Mid(string param, int startIndex, int length)
        {
            try
            {
                if (startIndex < 0)
                    startIndex = 0;
                return param.Substring(startIndex, length);
            }
            catch (Exception)
            {
            }
            return param;
        }

        #endregion

        #region Auxiliar para tratamento de item complementar

        // Adaptação da rotina original em ASP [CapturaPar]
        // Este método somente retorna o item complementar se atender as regras de cancelamento
        //public static int? RetornaSequencialDoItemComplementar              (int seqOriginal          , int servicoTipo, bool cancelamento, bool geraNuLogico)
        public static int? RetornaSequencialDoItemComplementar(int sequencialSelecionado, int servicoTipo, bool cancelamento, bool geraNuLogico)
        {
            // TROCA DE TECNOLOGIA
            if (servicoTipo == 4)
            {
                if (geraNuLogico == false) // SEQ=0, PAR
                    return sequencialSelecionado + 1;
                else
                    return sequencialSelecionado - 1;
            }


            // FEIRAS E EVENTOS - Somente retornará par complementar quando receber como parâmetro o item de INSTALAÇÃO
            if (servicoTipo == 5 && cancelamento)
            {
                if (geraNuLogico == true) // SEQ=0, PAR
                    return sequencialSelecionado + 1;
            }

            return null;
        }

        public static int? RetornaSequencialDoItemComplementar(int sequencialSelecionado, int servicoTipo, bool cancelamento)
        {
            bool par = sequencialSelecionado % 2 == 0;

            // TROCA DE TECNOLOGIA
            if (servicoTipo == 4)
            {
                if (par) // IN_GERA_NU_LOGICO==0,  SEQ=0, PAR
                    return sequencialSelecionado + 1;
                else
                    return sequencialSelecionado - 1;
            }


            // FEIRAS E EVENTOS
            //      Somente retornará par complementar quando receber como parâmetro o item de INSTALAÇÃO na operação
            //      de cancelamento, pois em feiras e eventos somente a operação de cancelamento poderá baixar o item 
            //      complementar automaticamente, e somente quando selecionado o item de INSTALAÇÃO.
            if (servicoTipo == 5 && cancelamento)
            {
                if (par)   //  IN_GERA_NU_LOGICO==1, SEQ=0, PAR
                    return sequencialSelecionado + 1;
            }

            return null;
        }

        #endregion

        #region Utilizados para POSTAR dados de uma página para outra, usando método POST
        //NameValueCollection data = new NameValueCollection();
        //data.Add("v1", "val1");
        //data.Add("v2", "val2");
        //RedirectAndPOST(this.Page, "http://DestUrl/Default.aspx", data);

        /// <summary>
        /// POST data and Redirect to the specified url using the specified page.
        /// </summary>
        /// <param name="page">The page which will be the referrer page.</param>
        /// <param name="destinationUrl">The destination Url to which
        /// the post and redirection is occuring.</param>
        /// <param name="data">The data should be posted.</param>
        /// <Author>Samer Abu Rabie</Author>

        public static void RedirectAndPOST(System.Web.UI.Page page, string destinationUrl, NameValueCollection data)
        {
            //Prepare the Posting form
            string strForm = PreparePOSTForm(destinationUrl, data);
            //Add a literal control the specified page holding 
            //the Post Form, this is to submit the Posting form with the request.
            page.Controls.Add(new System.Web.UI.LiteralControl(strForm));
        }

        /// <summary>
        /// This method prepares an Html form which holds all data
        /// in hidden field in the addetion to form submitting script.
        /// </summary>
        /// <param name="url">The destination Url to which the post and redirection
        /// will occur, the Url can be in the same App or ouside the App.</param>
        /// <param name="data">A collection of data that
        /// will be posted to the destination Url.</param>
        /// <returns>Returns a string representation of the Posting form.</returns>
        /// <Author>Samer Abu Rabie</Author>

        private static String PreparePOSTForm(string url, NameValueCollection data)
        {
            //Set a name for the form
            string formID = "PostForm";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + url +
                           "\" method=\"POST\">");

            foreach (string key in data)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + key +
                               "\" value=\"" + data[key] + "\">");
            }

            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language=\"javascript\">");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }
        #endregion
    }
}
