using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace LogviewHelper
{
    #region Enums
    public enum Level : int
    {
        DEBUG = 0,
        INFO = 1,
        WARN = 2,
        ERROR = 3
    }
    #endregion
    
    public class LogClass : IDisposable
    {

        private bool _logDebug              = false;
        private bool _logInfo               = false;
        private bool _logWarn               = false;
        private bool _logError              = true;
        private EventLog eventLog           = null;

        private static bool _trapInfo               = false;
        private static bool _trapWarn               = false;
        private static bool _trapError              = false;
        
        private static string _callerName           = String.Empty;
        private static string _pathLogFile          = String.Empty;
        private static string _trapTarget           = String.Empty;
        private static string _trapCommunity        = String.Empty;
        private static string _trapOID              = String.Empty;

        //private static FileStream _Stream;
        //private static StreamWriter _Writer;

        #region Property
        public bool isLogDebug
        {
            get { return _logDebug; }
            set { _logDebug = value; }
        }
        public bool isLogInfo
        {
            get { return _logInfo; }
            set { _logInfo = value; }
        }
        public bool isLogWarn
        {
            get { return _logWarn; }
            set { _logWarn = value; }
        }
        public bool isLogError
        {
            get { return _logError; }
            set { _logError = value; }
        }

        public bool isTrapError
        {
            get { return _trapError; }
            set { _trapError = value; }
        }
        public bool isTrapInfo
        {
            get { return _trapInfo; }
            set { _trapInfo = value; }
        }
        public bool isTrapWarn
        {
            get { return _trapWarn; }
            set { _trapWarn = value; }
        }

        public String Path
        {
            get { return _pathLogFile; }
            set 
            {
                if (System.IO.Path.IsPathRooted(value))
                    _pathLogFile = value;
                else
                    _pathLogFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, value);
            }
        }
        public String CallerName
        {
            get { return _callerName; }
            set { _callerName = value; }
        }
        #endregion

        public LogClass()
        {
            if (string.IsNullOrEmpty(this.Path))
                //this.Path = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["app.pathlog"];
                this.Path = GetStringSetting("app.pathlog");

            if (string.IsNullOrEmpty(this.CallerName))
                this.CallerName = GetStringSetting("NomeSistema");

            // Liga/Desliga a gravação no arquivo de log da aplicação
            this.isLogDebug = GetStringSetting("log.debug").Equals("1") ? true : false;
            this.isLogInfo = GetStringSetting("log.info").Equals("1") ? true : false;
            this.isLogWarn = GetStringSetting("log.warn").Equals("1") ? true : false;

            // Liga/Desliga o envio de TRAPs
            this.isTrapError = GetStringSetting("trap.error").Equals("1") ? true : false;
            this.isTrapInfo = GetStringSetting("trap.info").Equals("1") ? true : false;
            this.isTrapWarn = GetStringSetting("trap.warn").Equals("1") ? true : false;
        }

        public void log(Level paramLevel, String paramCallerName, String paramMessageContent)
        {
            _callerName = paramCallerName == null ? "" : paramCallerName;
            log(paramLevel, paramMessageContent);
        }

        public void log(Level paramLevel, String paramMessageContent)
        {
            try
            {
                printLog(paramLevel, _callerName, paramMessageContent);
                sendTrap(paramLevel, _callerName, paramMessageContent);
            }
            catch (Exception)
            {
            }
        }

        public void logSecao(Level paramLevel, String paramMessageContent)
        {
            try
            {
                String secao = null;
                log(paramLevel, string.Format("[{0}]{1}", secao, paramMessageContent));
            }
            catch (Exception)
            {
            }
        }

        public void logSecao(Level paramLevel, String secao, String paramMessageContent)
        {
            try
            {
                log(paramLevel, string.Format("[{0}]{1}", secao, paramMessageContent));
            }
            catch (Exception)
            {
            }
        }
        private void printLog(Level paramLevel, String paramCallerName, String paramMessageContent)
        {
            try
            {
                string nomeArquivo = gerarNomeArquivo();
                lock ("LOGVIEW")
                {
                    FileStream _Stream = new FileStream(nomeArquivo, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    StreamWriter _Writer = new StreamWriter(_Stream);
                    _Writer.AutoFlush = true;
                    _Writer.WriteLine(montarLinha(paramLevel, paramCallerName, paramMessageContent));
                    _Writer.Close();
                    _Stream.Close();
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("[printLog] error - Mensagem original: {0} - Exception: {1}", paramMessageContent, ex.Message);
                EventLogWrite(Level.ERROR, paramCallerName, message);
            }
        }

        private void sendTrap(Level paramLevel, String paramCallerName, String paramMessageContent)
        {
            try 
	        {	        
                // Envio de mensagem de monitoramento
                // SNMPClass.Send(_trapTarget, 200, paramMessageContent, GenericStatus.EnterpriseSpecific); 
	        }
	        catch (Exception ex)
	        {
                string message = string.Format("[sendTrap] error - Mensagem original: {0} - Exception: {1}", paramMessageContent, ex.Message);
                EventLogWrite(Level.ERROR, paramCallerName, message);
	        }
        }

        public void EventLogWrite(Level paramLevel, String paramCallerName, String paramMessageContent)
        {
            try
            {
                if (eventLog == null)
                    EventLogInit(paramCallerName);

                EventLogEntryType messageType;
                switch (paramLevel)
                {
                    case Level.DEBUG:
                        messageType = EventLogEntryType.SuccessAudit;
                        break;
                    case Level.INFO:
                        messageType = EventLogEntryType.Information;
                        break;
                    case Level.WARN:
                        messageType = EventLogEntryType.Warning;
                        break;
                    case Level.ERROR:
                        messageType = EventLogEntryType.Error;
                        break;
                    default:
                        messageType = EventLogEntryType.FailureAudit;
                        break;
                }

                string message = montarLinha(paramLevel, paramCallerName, paramMessageContent);

                eventLog.WriteEntry(message, messageType);
            }
            catch (Exception)
            {
            }

        }

        public void EventLogInit(String paramCallerName)
        {
            // Alguns usuários não possuem permissão para criação de SOURCE no log de eventos
            try
            {
                eventLog = new EventLog();

                string source = paramCallerName; // ConfigurationManager.AppSettings["EventLogSource"];
                string log = "Application";

                if (!EventLog.SourceExists(source))
                    EventLog.CreateEventSource(source, log);

                eventLog.Source = source;
                eventLog.Log = log;
            }
            catch (Exception)
            {
            }

        }

        public void EventLogClose()
        {
            try
            {
                if (eventLog != null)
                {
                    eventLog.Close();
                    eventLog = null;
                }
            }
            catch (Exception)
            {
            }

        }

		private static string montarLinha(Level paramLevel, String paramCallerName, String paramMessageContent)
        {
            string _return = String.Empty;
            _return += String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now) + " :";
            //_return += "[000000]";
            _return += "[" + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("000000") + "]";
            _return += "[" + paramCallerName + "]";
            _return += paramLevel == Level.DEBUG? Constants.CONST_LABEL_DEBUG : "";
            _return += paramLevel == Level.ERROR? Constants.CONST_LABEL_ERROR : "";
            _return += paramLevel == Level.INFO ? Constants.CONST_LABEL_INFO  : "";
            _return += paramLevel == Level.WARN ? Constants.CONST_LABEL_WARN  : "";
            _return += ": ";
            _return += paramMessageContent;
            return _return;
        }

        private string gerarNomeArquivo()
        {
            string _return = String.Empty;
            _return += System.IO.Path.Combine(_pathLogFile, _callerName);
            _return += String.Format("{0:yyyyMMddHH}", DateTime.Now);
            _return += Constants.CONST_SUFIXO_FILELOG;
            return _return;
        }

        /// <summary>
        /// Leitura do arquivo de configurações
        /// </summary>
        public static string GetStringSetting(string setting)
        {
            //string s = ConfigurationSettings.AppSettings[setting];
            string s = ConfigurationManager.AppSettings[setting];
            return s == null ? "" : s;
        }


        #region Implement IDisposable (Finalize and Dispose)
        ~LogClass()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (_Writer != null)
                //{
                //    _Writer.Close();
                //    _Writer.Dispose();
                //    _Writer = null;
                //}
                //if (_Stream != null)
                //{
                //    _Stream.Close();
                //    _Stream.Dispose();
                //    _Stream = null;
                //}

                EventLogClose();
            }
        }
        #endregion

    }

}
