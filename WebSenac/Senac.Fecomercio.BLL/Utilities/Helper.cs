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
using LogviewHelper;
using Senac.Fecomercio.BLL.Utilities.Exceptions;

namespace Senac.Fecomercio.BLL.Utilities
{
    public static class Helper
    {
        public static int TamanhoLoteProcessamento
        {
            get
            {
                int numLinhas = 0;
                int.TryParse(ConfigurationManager.AppSettings["TamanhoLoteProcessamento"], out numLinhas);
                return numLinhas;
            }
        }

        public static string GetNameSystem
        {
            get { return ConfigurationManager.AppSettings["NomeSistema"]; }
        }

        /// <summary>
        /// Gets the value of an Enum, based on it's Description Attribute or named value
        /// </summary>
        /// <param name="value">The Enum type</param>
        /// <param name="description">The description or name of the element</param>
        /// <returns>The value, or the passed in description, if it was not found</returns>
        public static object GetEnumValue(System.Type value, string description)
        {
            FieldInfo[] fis = value.GetFields();
            foreach (FieldInfo fi in fis)
            {
                DescriptionAttribute[] attributes =
                  (DescriptionAttribute[])fi.GetCustomAttributes(
                  typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    if (attributes[0].Description == description)
                    {
                        return fi.GetValue(fi.Name);
                    }
                }
                if (fi.Name == description)
                {
                    return fi.GetValue(fi.Name);
                }
            }
            return description;
        }

        //public static SqlConnection GetConnection(string dbName)
        //{
        //    return DBManager.GetConnection(dbName);
        //    //Dictionary<string, string> cnns = new Dictionary<string, string>();
        //    //cnns.Add(ConfigurationManager.ConnectionStrings[1].Name, ConfigurationManager.ConnectionStrings[1].ConnectionString);
        //    //cnns.Add(ConfigurationManager.ConnectionStrings[2].Name, ConfigurationManager.ConnectionStrings[2].ConnectionString);

        //    //return new SqlConnection(cnns[dbName]);
        //}

        public static int GetNumLinesStreamReader(StreamReader reader)
        {
            int numLines = 0;

            if (reader != null)
            {
                Regex regex = new Regex("\r\n");
                string line = reader.ReadToEnd();
                numLines = regex.Matches(line).Count;
                reader.Close();
                reader.Dispose();
                reader = null;
            }

            return numLines;
        }
        public static int NumeroPartesArquivo(int tamLista)
        {
            if (tamLista > TamanhoLoteProcessamento)
            {
                int res = tamLista / TamanhoLoteProcessamento;
                int resto = tamLista % TamanhoLoteProcessamento;
                if (resto > 0)
                    res += 1;
                return res;
            }
            else
                return 1;
        }

        //public static bool TemCaracterInvalido(string texto)
        //{
        //    bool hasInvalidChar = false;
        //    char[] invalidos = { '#', '@', '%', '¨', '&', '*', ';', '~', '"', '£', '¢', '¬', '§', '+', '=', '°', '>', '<' };
        //    if (!string.IsNullOrEmpty(texto))
        //    {
        //        if (texto.IndexOfAny(invalidos) != -1)
        //            hasInvalidChar = true;
        //    }
        //    return hasInvalidChar;
        //}

        public static bool DateTime_TryParse_BR(string s, out DateTime result)
        {
            //CultureInfo dateCulture = CultureInfo.CreateSpecificCulture("en-US");
            CultureInfo dateCulture = CultureInfo.GetCultureInfo("pt-BR");
            return DateTime.TryParse(s, dateCulture, DateTimeStyles.None, out result);

            //IFormatProvider provider = new CultureInfo("pt-BR", true).DateTimeFormat;
            //return DateTime.TryParse(s, new CultureInfo("pt-BR", true).DateTimeFormat, DateTimeStyles.None, out result)
        }

        public static DateTime Convert_ToDateTime_BR(string value)
        {
            //CultureInfo dateCulture = CultureInfo.CreateSpecificCulture("en-US");
            CultureInfo dateCulture = CultureInfo.GetCultureInfo("pt-BR");
            return Convert.ToDateTime(value, dateCulture);
        }

        public static string[] SplitTrim(string line)
        {
            return SplitTrim(line, ';');
        }

        public static string[] SplitTrim(string line, char separador)
        {
            string[] registro = line.Split(separador);
            for (int i = 0; i < registro.Length; i++)
                registro[i] = registro[i].Trim();
            return registro;
        }


        public static void RegistraLogSistema(Level level, string message)
        {
            try
            {
                LogClass logClass = new LogClass();
                logClass.log(level, Helper.GetNameSystem, message);
            }
            catch (Exception ex)
            {
                RegistraLogEvento(Level.ERROR, "Falha ao gravar em log de sistema. ERRO ATUAL: " + ex.Message + " - MENSAGEM ORIGINAL: [" + level.ToString() + "]" + message);
                throw;
            }
        }

        public static void RegistraLogEvento(Level level, string message)
        {
            //LogClass.EventLogWrite(level, Helper.GetNameSystem, message);

            LogClass logClass = new LogClass();
            logClass.EventLogWrite(level, Helper.GetNameSystem, message);
            logClass.log(level, Helper.GetNameSystem, message);
        }

        public static bool ValidarCEP(Int32 cep, out string msgErro)
        {
            try
            {
                msgErro = string.Empty;

                if (cep < 1 || cep > 99999999)
                    throw new BusinessException("CEP INVALIDO");

                return true;
            }
            catch (BusinessException businessException)
            {
                msgErro = businessException.Message;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static object GetDbParameterValue(object obj)
        {
            if (obj == null)
                return DBNull.Value;

            return obj;
        }

        /// <summary>
        /// Formatar o campo Estabelecimento
        /// </summary>        
        public static string formatarEstabelecimento(long estabelecimento)
        {
            return estabelecimento.ToString("0000000000");
        }




    }
}