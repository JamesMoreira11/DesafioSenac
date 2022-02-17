using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Senac.Fecomercio.Common
{
    public class MailClient
    {
        #region Propriedades
        private SmtpClient mailClient = null;
        private string emailFrom { get; set; }
        #endregion

        #region Construtor
        public MailClient(string emailFrom, string smpt, string senha, int porta, bool enableSSL = false)
        {
            mailClient = new SmtpClient();
            mailClient.Port = porta;
            NetworkCredential basicAuthenticationInfo = new NetworkCredential(emailFrom, senha);
            mailClient.Credentials = basicAuthenticationInfo;
            mailClient.EnableSsl = enableSSL;
            mailClient.Host = smpt;
            this.emailFrom = emailFrom;
        }
        #endregion

        #region Metodos
        public void Send(MailMessage email)
        {
            mailClient.Send(email);
            email.Dispose();
        }

        public void Send(string[] emailsTO, string assunto, string mensagem)
        {
            try
            {
                MailMessage messageEmail = new MailMessage();

                if (emailsTO.IsNotNull() && emailsTO.Count() > 0)
                {
                    for (int i = 0; i < emailsTO.Count(); i++)
                    {
                        messageEmail.To.Add(emailsTO[i]);
                    }
                }

                messageEmail.From = new MailAddress(this.emailFrom, "GTEC - Informativo");
                messageEmail.Subject = assunto;
                messageEmail.Body = mensagem;
                mailClient.Send(messageEmail);
                messageEmail.Dispose();
                messageEmail = null;
            }
            catch (Exception ex)
            {
                Logger.LogError("Erro ao tentar enviar e-mail. Erro: '{0}'".ToFormat(ex.Message), ex);
            }
        }
        #endregion
    }
}