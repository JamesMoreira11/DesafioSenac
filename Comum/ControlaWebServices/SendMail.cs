using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace Visanet.GTeC.WebService
{
    class SendMail
    {
        public static void fnEnviaEmail(Exception oException, string sException)
        {
            try
            {
                //envio de email

                SmtpClient client = new SmtpClient(this.smtpServer);
                MailMessage msg = new MailMessage();

                msg.From = (new MailAddress(emailFrom));

                foreach (string sMail in emailsTo.Split(','))
                {
                    msg.To.Add(new MailAddress(sMail));
                }

                msg.Subject = "GTeC - Controle de Envio de Chamados";

                if (sException.ToString() == "")
                {
                    msg.Body = "Controle de Envio de Chamados, finalizado com erro." + oException.ToString();
                }
                else
                {
                    msg.Body = sException;
                    sException = "";
                }
                //Envia a mensagem 
                client.Send(msg);

                eventLog.WriteEntry("Controle de Envio de Chamados finalizado com erro, enviado e-mail de alerta com sucesso.", System.Diagnostics.EventLogEntryType.Warning);
            }
            catch (Exception oErro)
            {
                eventLog.WriteEntry("Controle de Envio de Chamados finalizado com erro, não foi possivel enviar e-mail com ocorrência." + oErro.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }

        }

    }
}
