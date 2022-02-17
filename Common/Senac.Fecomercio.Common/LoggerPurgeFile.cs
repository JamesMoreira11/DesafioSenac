using log4net;
using log4net.Appender;
using System;
using System.IO;
using System.Linq;

namespace Senac.Fecomercio.Common
{
    public class LoggerPurgeFile
    {
        #region Construtor
        public LoggerPurgeFile()
        {
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Cleans up. Auto configures the cleanup based on the log4net configuration
        /// </summary>
        /// <param name="date">Anything prior will not be kept.</param>
        public void CleanUp(DateTime date)
        {
            string directory = string.Empty;
            string fileExtension = string.Empty;

            var repo = LogManager.GetAllRepositories().FirstOrDefault();
            if (repo == null)
                throw new NotSupportedException("Log4Net não configurado ainda");

            var app = repo.GetAppenders().Where(x => x.GetType() == typeof(RollingFileAppender)).FirstOrDefault();
            if (app != null)
            {
                var appender = app as RollingFileAppender;

                directory = Path.GetDirectoryName(appender.File);
                fileExtension = Path.GetExtension(appender.File);

                CleanUp(directory, fileExtension, date);
            }
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        /// <param name="logDirectory">The log directory.</param>
        /// <param name="logPrefix">The log prefix. Example: logfile dont include the file extension.</param>
        /// <param name="date">Anything prior will not be kept.</param>
        private void CleanUp(string logDirectory, string extensionFile, DateTime date)
        {
            if (string.IsNullOrEmpty(logDirectory))
                throw new ArgumentException("logDirectory is missing");

            if (string.IsNullOrEmpty(extensionFile))
                throw new ArgumentException("Extension File is missing");

            var dirInfo = new DirectoryInfo(logDirectory);
            if (!dirInfo.Exists)
                return;

            var fileInfos = dirInfo.GetFiles("*{0}".ToFormat(extensionFile));
            if (fileInfos.Length == 0)
                return;

            foreach (var info in fileInfos)
            {
                if (info.LastWriteTime.Date <= date.Date)
                {
                    try
                    {
                        info.Delete();
                    }
                    catch (FieldAccessException filEx)
                    {
                        Logger.LogInfo("Não foi possível excluir o arquivo de log '{0}' devido não possuir permissão. Mensagem de erro '{1}'".ToFormat(info.Name, filEx.Message));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        #endregion
    }
}