using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Senac.Fecomercio.Common
{
    //Extension methods must be defined in a static class 
    public static class Extension
    {
        /// <summary>
        /// Este método aceita object, convertendo para bool, 'S', 'Y', 'N'
        /// Também checa DBNull.value
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool? ToBoolNullable(this object obj)
        {
            if (obj == DBNull.Value)
                return null;

            string temp = obj.ToString();
            bool result = false;
            if (!bool.TryParse(temp, out result))
            {
                result = temp.Equals("S") || temp.Equals("Y");
            }
            return result;
        }

        /// <summary>
        /// Este método aceita object, convertendo para bool, 'S', 'Y', 'N', 1 e 0
        /// Também checa DBNull.value
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBool(this object obj)
        {
            if (obj == DBNull.Value)
                return false;

            string temp = obj.ToString();
            bool result = false;
            if (!bool.TryParse(temp, out result))
            {
                result = temp.Equals("S") || temp.Equals("Y") || temp.Equals("1");
            }
            return result;
        }

        /// <summary>
        /// Este método aceita object, convertendo para datetime.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj)
        {
            string temp = obj.ToString();
            DateTime result;
            DateTime.TryParse(temp, out result);
            return result;
        }

        /// <summary>
        /// Este método aceita object, convertendo para datetime.
        /// Também checa DBNull.value
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeNullable(this object obj)
        {
            if (obj == DBNull.Value)
                return null;

            string temp = obj.ToString();
            DateTime result;
            if (!DateTime.TryParse(temp, out result))
                return null;

            return result;
        }

        /// <summary>
        /// Converte para inteiro, considerando DBNull.Value para nullable type.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? ToIntNullable(this object obj)
        {
            if (obj == DBNull.Value)
                return null;

            int temp;
            int.TryParse(obj.ToString(), out temp);

            return temp;
        }

        /// <summary>
        /// Converte para decimal, considerando DBNull.Value para nullable type.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal? ToDecimalNullable(this object obj)
        {
            if (obj == DBNull.Value)
                return null;

            decimal temp;
            decimal.TryParse(obj.ToString(), out temp);

            return temp;
        }

        /// <summary>
        /// Converte para inteiro
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this object obj)
        {
            int temp;
            int.TryParse(obj.ToString(), out temp);

            return temp;
        }

        /// <summary>
        /// Converte para decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj)
        {
            decimal temp;
            decimal.TryParse(obj.ToString(), out temp);

            return temp;
        }

        public static string ToStringCheckDBNull(this object obj)
        {
            if (obj == DBNull.Value)
                return "";

            return obj.ToString();
        }

        public static bool IsNullOrEmpty(this object obj)
        {
            if (obj == null)
                return true;

            if (string.IsNullOrEmpty(obj.ToString()))
                return true;

            return false;
        }

        /// <summary>
        /// Returns the first few characters of the string with a length
        /// specified by the given parameter. If the string's length is less than the 
        /// given length the complete string is returned. If length is zero or 
        /// less an empty string is returned
        /// </summary>
        /// <param name="s">the string to process</param>
        /// <param name="length">Number of characters to return</param>
        /// <returns></returns>
        public static string Left(this string s, int length)
        {
            if (length <= 0)
                return "";
            return s != null && s.Length > length ? s.Substring(0, length) : s;
        }

        /// <summary>
        /// Returns characters from right of specified length
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="length">Max number of charaters to return</param>
        /// <returns>Returns string from right</returns>
        public static string Right(this string value, int length)
        {
            return value != null && value.Length > length ? value.Substring(value.Length - length) : value;
        }

        public static bool IsNumeric(this string theValue)
        {
            long retNum;
            return long.TryParse(theValue, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out retNum);
        }

        public static bool IsDouble(this string theValue)
        {
            double retNum;
            return double.TryParse(theValue, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out retNum);
        }

        public static bool IsLetterOnly(this string theValue)
        {
            char[] letras = theValue.ToCharArray();
            bool retorno = true;

            for (int i = 0; i < letras.Length; i++)
            {
                retorno = char.IsLetter(letras[i]);
                if (!retorno)
                    break;
            }
            return retorno;
        }

        public static Stream ToStream(this string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string PrintProperties(this object obj)
        {
            return PrintProperties(obj, 0);
        }

        private static string PrintProperties(object obj, int indent)
        {
            string retorno = string.Empty;
            if (obj == null) return retorno;

            string linha = string.Empty;

            string indentString = new string(' ', indent);
            Type objType = obj.GetType();
            PropertyInfo[] properties = objType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object propValue = property.GetValue(obj, null);

                if (property.PropertyType.Assembly == objType.Assembly)
                {
                    linha = String.Format("{0}{1}:{2}", indentString, property.Name, Environment.NewLine);
                    Console.WriteLine(linha);
                    linha += PrintProperties(propValue, indent + 2);
                }
                else
                {
                    linha = String.Format("{0}{1}: {2}{3}", indentString, property.Name, propValue, Environment.NewLine);
                    Console.WriteLine(linha);
                }
                retorno += linha;
            }
            return retorno;
        }

        /// <summary>
        /// Converte para char, considerando DBNull.Value para nullable type.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static char? ToCharNullable(this object obj)
        {
            if (obj == DBNull.Value)
                return null;

            char temp;
            char.TryParse(obj.ToString(), out temp);

            return temp;
        }

        /// <summary>
        /// Converte para byte, considerando DBNull.Value para nullable type.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte? ToByteNullable(this object obj)
        {
            if (obj == DBNull.Value)
                return null;

            byte temp;
            byte.TryParse(obj.ToString(), out temp);

            return temp;
        }

        /// <summary>
        /// Converte para string com tamanho e caracteres específicos
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToString<T>(this T obj, int length)
        {
            if (typeof(T) == typeof(int) || typeof(T) == typeof(int?) || typeof(T) == typeof(decimal) || typeof(T) == typeof(decimal?) || typeof(T) == typeof(long) || typeof(T) == typeof(long?))
            {
                if (obj.IsNull())
                    return new String('0', length);
                else
                    return obj.ToString().PadLeft(length, '0').Substring(0, length);
            }
            else
            {
                if (obj.IsNull())
                    return new String(' ', length);
                else
                    return obj.ToString().PadRight(length, ' ').Substring(0, length);
            }
        }

        /// <summary>
        /// Converte para string com tamanho e caracteres específicos
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="length"></param>
        /// <param name="caracter"></param>
        /// <returns></returns>
        public static string ToString<T>(this T obj, int length, char caracter)
        {
            if (caracter.Equals('0'))
                return obj.ToString().PadLeft(length, caracter).Substring(0, length);
            else
                return obj.ToString().PadRight(length, caracter).Substring(0, length);
        }

        public static bool IsInt(this string theValue)
        {
            int retNum;
            return int.TryParse(theValue, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out retNum);
        }

        public static List<T> CopyList<T>(this List<T> list) where T : ICloneable
        {
            var newList = new List<T>(list.Count);

            foreach (T item in list)
            {
                newList.Add((T)item.Clone());
            }

            return newList;
        }

        public static string ToFormat(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        #region IsNull
        public static bool IsNull(this object obj)
        {
            if (obj == null)
                return true;
            else
                return false;
        }

        public static bool IsNull(this string obj)
        {
            return string.IsNullOrEmpty(obj);
        }

        public static bool IsNotNull(this object obj)
        {
            if (obj != null)
                return true;
            else
                return false;
        }

        public static bool IsNotNull(this string obj)
        {
            return !string.IsNullOrEmpty(obj);
        }

        public static object IfNullThenDBNull(this string obj)
        {
            if (String.IsNullOrEmpty(obj))
                return DBNull.Value;
            else
                return obj;
        }

        public static object IfNullThenDBNull(this bool obj)
        {
            if (obj == null)
                return DBNull.Value;
            else
                return obj;
        }

        public static bool IsDbNull(this object obj)
        {
            if (obj == DBNull.Value)
                return true;
            else
                return false;
        }
        #endregion

        public static string GetValueConfig(string chave, bool warnException = false)
        {
            string value = string.Empty;

            if (warnException && (ConfigurationManager.AppSettings[chave].IsNull() || (ConfigurationManager.AppSettings[chave].IsNotNull() && ConfigurationManager.AppSettings[chave].ToString().IsNull())))
            {
                throw new ConfigurationException("A chave '{0}' não foi encontrado no arquivo CONFIG".ToFormat(chave));
            }
            else if (ConfigurationManager.AppSettings[chave].IsNotNull() && ConfigurationManager.AppSettings[chave].ToString().IsNotNull())
            {
                value = ConfigurationManager.AppSettings[chave].ToString();
            }

            return value;
        }

        public static string[] GetValueConfig(string chave, char splitChar, bool warnException = false)
        {
            string[] value = null;

            string valueChave = GetValueConfig(chave, warnException);

            if (valueChave.IsNotNull())
            {
                if (valueChave.Contains(splitChar))
                {
                    value = valueChave.Split(splitChar);
                }
                else
                {
                    value = new string[1];
                    value[0] = valueChave;
                }
            }

            return value;
        }

        #region CSV
        public static string CriarCSV(this DataTable data, bool appendHeader = true)
        {
            StringBuilder sb = new StringBuilder();

            if (appendHeader)
            {
                IEnumerable<string> columnNames = data.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                sb.AppendLine(string.Join(";", columnNames));
            }

            foreach (DataRow row in data.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString().StringToCSVCell());
                sb.AppendLine(string.Join(";", fields));
            }

            return sb.ToString();
        }

        public static string CriarCSV<T>(this List<T> data)
        {

            var props = typeof(T).GetProperties();
            var result = new StringBuilder();

            foreach (var row in data)
            {
                var values = props.Select(p => p.GetValue(row, null))
                                  .Select(v => Convert.ToString(v).StringToCSVCell());
                var line = string.Join(";", values);
                result.AppendLine(line);
            }

            return result.ToString();
        }

        public static string StringToCSVCell(this string str)
        {
            bool mustQuote = (str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n"));
            if (mustQuote)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\"");
                foreach (char nextChar in str)
                {
                    sb.Append(nextChar);
                    if (nextChar == '"')
                        sb.Append("\"");
                }
                sb.Append("\"");
                return sb.ToString();
            }

            return str;
        }
        #endregion

        #region DataReader
        public static T ObterValor<T>(this IDataReader dr, string fieldName)
        {
            try
            {
                return dr.IsNotNull() && dr[fieldName].IsNotNull() && dr[fieldName] != DBNull.Value ? (T)dr[fieldName] : default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static bool IsEmpty<T>(this ICollection<T> value)
        {
            try
            {
                if (value.IsNull() || value.Count == 0)
                    return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        public static string[] GetPropertiesFromType(this object atype)
        {
            if (atype == null) return new string[] { };
            Type t = atype.GetType();
            PropertyInfo[] props = t.GetProperties();
            List<string> propNames = new List<string>();
            foreach (PropertyInfo prp in props)
            {
                propNames.Add(prp.Name);
            }
            return propNames.ToArray();
        }

        public static PropertyInfo[] GetProperties(this object obj)
        {
            if (obj == null) return new PropertyInfo[] { };
            Type t = obj.GetType();
            PropertyInfo[] props = t.GetProperties();

            return props;
        }

        public static string GetDisplayNameFromEnum(this Enum value)
        {
            Type enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            MemberInfo member = enumType.GetMember(enumValue)[0];

            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            var outString = ((DisplayAttribute)attrs[0]).Name;

            if (((DisplayAttribute)attrs[0]).ResourceType != null)
            {
                outString = ((DisplayAttribute)attrs[0]).GetName();
            }

            return outString;
        }

        public static string GetAllErrorDetail(this Exception ex)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Error: '{0}' - Source: '{1}' - StackTrace: '{2}' - TargetSite: '{3}'{4}", ex.Message, ex.Source, ex.StackTrace, ex.TargetSite.IsNotNull() ? ex.TargetSite.ToString() : "", Environment.NewLine);

            if (ex.InnerException != null)
            {
                sb.AppendLine(ex.InnerException.GetAllErrorDetail());
            }

            return sb.ToString();
        }

        public static string GetPlatform()
        {
            if (IntPtr.Size == 8)
                return "x64";
            else
                return "x86";
        }

        public static string SubSTR(this string value, int startIndex, int length)
        {
            string ret = string.Empty;

            if ((value.Length - startIndex) >= length)
            {
                ret = value.Substring(startIndex, length);
            }
            else if (value.Length > startIndex)
            {
                ret = ret = value.Substring(startIndex, (value.Length - startIndex));
            }

            return ret;
        }

        public static string GetDumpObject(this object value)
        {
            string retorno = string.Empty;
            try
            {
                retorno = JsonConvert.SerializeObject(value);
            }
            catch { }

            return retorno;
        }

        public static bool IsDateTime(this string value, CultureInfo cultura = null)
        {
            bool ret = false;

            CultureInfo cultureInfo = new CultureInfo("pt-BR");

            if (cultura.IsNotNull())
            {
                cultureInfo = cultura;
            }

            try
            {
                DateTime dtTeste = Convert.ToDateTime(value, cultureInfo);
                ret = true;
            }
            catch
            {
                ret = false;
            }

            return ret;
        }

        public static bool IsCpf(this string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public static bool IsCnpj(this string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static bool DataTableExisteColuna(this DataTable table, string columnName)
        {
            bool ret = false;
            DataColumnCollection columns = table.Columns;
            if (columns.Contains(columnName))
            {
                ret = true;
            }
            return ret;
        }

        public static T GetValueDataTable<T>(this object value)
        {
            T ret = default(T);
            Type tpValue = typeof(T);

            if (tpValue.IsGenericType && tpValue.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value == DBNull.Value)
                {
                    return default(T);
                }

                tpValue = Nullable.GetUnderlyingType(tpValue);
            }

            ret = (T)Convert.ChangeType(value, tpValue);

            return ret;
        }

        public static string GetDirectoryPath(this Assembly assembly)
        {
            string filePath = new Uri(assembly.CodeBase).LocalPath;
            return Path.GetDirectoryName(filePath);
        }

        public static bool IsXML(this string xml)
        {
            XmlDocument doc = null;
            bool ret = false;
            try
            {
                doc = new XmlDocument();
                doc.LoadXml(xml);
                ret = true;
            }
            catch { }
            finally
            {
                if (doc.IsNotNull())
                {
                    doc = null;
                }
            }
            return ret;
        }

        //Implemented based on interface, not part of algorithm
        public static string RemoveAllNamespaces(this string xmlDocument)
        {
            XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));

            return xmlDocumentWithoutNs.ToString();
        }

        //Core recursion function
        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    xElement.Add(attribute);

                return xElement;
            }
            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
        }

        public static string LimpaNumeroTelefone(this string value)
        {
            if (value.IsNotNull())
            {
                return value.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace("+", "").Replace(".", "");
            }
            else
            {
                return value;
            }
        }

        public static void DumpObjeto(this object value)
        {
            try
            {
                string isDump = Extension.GetValueConfig("GTEC_DUMP_OBJECT", false);

                if (isDump.IsNotNull() && isDump.Equals("1"))
                {
                    Logger.LogInfo("DumpObjeto({0})".ToFormat(Extension.GetDumpObject(value)));
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("DumpObjeto() > Error: {0}".ToFormat(ex.GetAllErrorDetail()), ex);
            }
        }

        public static string JsonSerializeObject(this object value)
        {
            string retorno = string.Empty;
            try
            {
                retorno = JsonConvert.SerializeObject(value);
            }
            catch { }

            return retorno;
        }

        public static string JsonSerializeObject(this object value, JsonSerializerSettings jsonSettings)
        {
            string retorno = string.Empty;
            try
            {
                retorno = JsonConvert.SerializeObject(value, jsonSettings);
            }
            catch { }

            return retorno;
        }

        public static T JsonDeserializeObject<T>(this string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch
            {
                throw;
            }
        }

        public static string ToStringValorAmericano(this decimal value)
        {
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
            return value.ToString("0.00", cultureInfo);
        }

        public static string ToStringHtmlEncode(this string value)
        {
            if (value.IsNotNull())
            {
                return System.Net.WebUtility.HtmlEncode(value);
            }
            else
            {
                return value;
            }
        }

        public static string FormataDiaHoraTempoAtendimento(int? tempo, string tipo)
        {
            string retorno = null;

            if (tempo.HasValue && (tipo.Equals("S") || tipo.Equals("N")))
            {
                retorno = tempo.ToString().PadLeft(2, '0');

                if (tipo.Equals("S") && tempo.Equals(1))
                {
                    retorno = "{0} hr".ToFormat(retorno);
                }
                else if (tipo.Equals("S") && tempo > 1)
                {
                    retorno = "{0} hrs".ToFormat(retorno);
                }
                else if (tipo.Equals("N") && tempo.Equals(1))
                {
                    retorno = "{0} dia".ToFormat(retorno);
                }
                else if (tipo.Equals("N") && tempo > 1)
                {
                    retorno = "{0} dias".ToFormat(retorno);
                }
            }

            return retorno;
        }

        public static string ObterItemMemoFileSextaLinha(this string memoFile, int idxItem)
        {
            string ret = string.Empty;

            if (memoFile.IsNotNull() && memoFile.Contains(Environment.NewLine))
            {
                string[] stringSeparators = new string[] { "\r\n" };
                string[] arLinha = memoFile.Split(stringSeparators, StringSplitOptions.None);

                if (arLinha.Length > 0)
                {
                    string ultimaLinha = arLinha[5];

                    if (ultimaLinha.IsNotNull())
                    {
                        string[] arCampos = ultimaLinha.Split(';');

                        if (arCampos.Length > 0)
                        {
                            try
                            {
                                ret = arCampos[idxItem].Trim();
                            }
                            catch { }
                        }
                    }
                }
            }

            return ret;
        }
    }
}