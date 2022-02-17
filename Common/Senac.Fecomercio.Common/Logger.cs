using log4net;
using log4net.Config;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Threading;

[assembly: XmlConfigurator(Watch = true)]
namespace Senac.Fecomercio.Common
{
    public class Logger
    {
        #region Propriedades
        private readonly static Type ThisDeclaringType = typeof(Logger);

        private static readonly ILogger defaultLogger;
        private static DateTime? dataUltimoExpurgo { get; set; }
        public static List<Thread> listaThread { get; set; }
        #endregion

        #region Construtor
        static Logger()
        {
            GlobalContext.Properties["LogName"] = Thread.CurrentThread.ManagedThreadId;
            defaultLogger = LoggerManager.GetLogger(Assembly.GetCallingAssembly(), "");
            ExpurgoLogs();
        }
        #endregion

        #region Metodos
        public static void LogInfo(string message)
        {
            if (defaultLogger.IsEnabledFor(Level.Info))
            {
                ExpurgoLogs();
                if (message.Contains(Environment.NewLine))
                {
                    string[] linhas = message.Split(Environment.NewLine.ToCharArray());
                    foreach (string t in linhas)
                    {
                        if (!string.IsNullOrEmpty(t))
                            defaultLogger.Log(typeof(Logger), Level.Info, t, null);
                    }
                }
                else
                    defaultLogger.Log(typeof(Logger), Level.Info, message, null);
            }
        }

        public static void LogInfo(string message, params object[] parametros)
        {
            if (defaultLogger.IsEnabledFor(Level.Info))
            {
                ExpurgoLogs();
                defaultLogger.Log(typeof(Logger), Level.Info, String.Format(message, parametros), null);
            }
        }


        public static void LogWarn(string message)
        {
            if (defaultLogger.IsEnabledFor(Level.Warn))
            {
                ExpurgoLogs();
                if (message.Contains(Environment.NewLine))
                {
                    string[] linhas = message.Split(Environment.NewLine.ToCharArray());
                    foreach (string t in linhas)
                    {
                        if (!string.IsNullOrEmpty(t))
                            defaultLogger.Log(typeof(Logger), Level.Warn, t, null);
                    }
                }
                else
                    defaultLogger.Log(typeof(Logger), Level.Warn, message, null);
            }
        }

        public static void LogWarn(string message, params object[] parametros)
        {
            if (defaultLogger.IsEnabledFor(Level.Warn))
            {
                ExpurgoLogs();
                defaultLogger.Log(typeof(Logger), Level.Warn, String.Format(message, parametros), null);
            }
        }



        public static void LogDebug(string message)
        {
            if (defaultLogger.IsEnabledFor(Level.Debug))
            {
                ExpurgoLogs();
                defaultLogger.Log(typeof(Logger), Level.Debug, message, null);
            }
        }

        public static void LogDebug(string message, params object[] parametros)
        {
            if (defaultLogger.IsEnabledFor(Level.Debug))
            {
                ExpurgoLogs();
                defaultLogger.Log(typeof(Logger), Level.Debug, String.Format(message, parametros), null);
            }
        }

        public static void LogDebug(bool visible, string message, params object[] parametros)
        {
            if (visible == true)
            {
                ExpurgoLogs();
                if (defaultLogger.IsEnabledFor(Level.Debug))
                {
                    defaultLogger.Log(typeof(Logger), Level.Debug, String.Format(message, parametros), null);
                }
            }
        }

        public static void LogError(string message)
        {
            if (defaultLogger.IsEnabledFor(Level.Error))
            {
                ExpurgoLogs();
                if (message.Contains(Environment.NewLine))
                {
                    string[] linhas = message.Split(Environment.NewLine.ToCharArray());
                    foreach (string t in linhas)
                    {
                        if (!string.IsNullOrEmpty(t))
                            defaultLogger.Log(typeof(Logger), Level.Error, t, null);
                    }
                }
                else
                    defaultLogger.Log(typeof(Logger), Level.Error, message, null);
            }
        }

        public static void LogError(string message, Exception exception)
        {
            string messageDetail = "{0}. Detalhe do erro: {1}".ToFormat(message, exception.GetAllErrorDetail());

            if (defaultLogger.IsEnabledFor(Level.Error))
            {
                ExpurgoLogs();
                if (messageDetail.Contains(Environment.NewLine))
                {
                    string[] linhas = messageDetail.Split(Environment.NewLine.ToCharArray());
                    foreach (string t in linhas)
                    {
                        if (!string.IsNullOrEmpty(t))
                            defaultLogger.Log(typeof(Logger), Level.Error, t, exception);
                    }
                }
                else
                    defaultLogger.Log(typeof(Logger), Level.Error, messageDetail, exception);
            }
        }

        public static void LogFatal(string message)
        {
            if (defaultLogger.IsEnabledFor(Level.Fatal))
            {
                ExpurgoLogs();
                defaultLogger.Log(typeof(Logger), Level.Fatal, message, null);
            }
        }

        public static void LogFatal(string message, Exception exception)
        {
            if (defaultLogger.IsEnabledFor(Level.Fatal))
            {
                ExpurgoLogs();
                defaultLogger.Log(typeof(Logger), Level.Fatal, message, exception);
            }
        }

        #region Expurgo
        private static void ExpurgoLogs()
        {
            try
            {
                int totalDias = 0;
                if (defaultLogger != null && PodeExpurgar(out totalDias))
                {
                    dataUltimoExpurgo = DateTime.Now;

                    Thread threadExp = new Thread(delegate()
                    {
                        RealizarExpurgo(totalDias);
                    });
                    threadExp.Name = "Thread_Expurgo_Log";

                    if (listaThread.IsNull())
                    {
                        listaThread = new List<Thread>();
                    }

                    threadExp.Start();
                    listaThread.Add(threadExp);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static bool PodeExpurgar(out int totalDias)
        {
            bool ret = false;
            totalDias = 0;

            //Verifica se existe a tag no .config
            if (ConfigurationManager.AppSettings[Constantes.LOG4NET_EXPURGO_DIA].IsNotNull()
                && ConfigurationManager.AppSettings[Constantes.LOG4NET_EXPURGO_DIA].ToString().IsNotNull()
                && ConfigurationManager.AppSettings[Constantes.LOG4NET_EXPURGO_DIA].ToString().IsInt()
                && ConfigurationManager.AppSettings[Constantes.LOG4NET_EXPURGO_DIA].ToString().ToInt() > 0)
            {
                totalDias = ConfigurationManager.AppSettings[Constantes.LOG4NET_EXPURGO_DIA].ToString().ToInt();

                if (dataUltimoExpurgo.IsNotNull())
                {
                    //Verifica se hoje já foi realizado expurgo
                    if (DateTime.Now.Date > dataUltimoExpurgo.Value.Date)
                    {
                        if (GetIndexTreadExpurgo() == -1)
                        {
                            ret = true;
                        }
                    }
                }
                else
                {
                    if (GetIndexTreadExpurgo() == -1)
                    {
                        ret = true;
                    }
                }
            }

            return ret;
        }

        private static void RealizarExpurgo(int totalDias)
        {
            LoggerPurgeFile purge = null;
            try
            {
                totalDias = totalDias * -1;
                DateTime dataExpurgo = DateTime.Now.AddDays(totalDias);

                purge = new LoggerPurgeFile();
                purge.CleanUp(dataExpurgo);
            }
            catch (Exception ex)
            {
                Logger.LogError("RealizarExpurgo > Erro ao relizar o expurgo dos logs. {0}".ToFormat(ex.Message), ex);
            }
            finally
            {
                int idxTread = GetIndexTreadExpurgo();

                if (idxTread != -1)
                {
                    listaThread.RemoveAt(idxTread);
                }

                purge = null;
            }
        }

        private static int GetIndexTreadExpurgo()
        {
            int ret = -1;
            if (listaThread.IsNotNull() && listaThread.Count > 0)
            {
                ret = listaThread.FindIndex(x => x.Name.Equals("Thread_Expurgo_Log"));
            }

            return ret;
        }
        #endregion
        #endregion
    }
}