using Senac.Fecomercio.Common;
using System;
using System.Configuration;
using System.IO;
using System.Web.UI;
using UT = Senac.Fecomercio.BLL.Utilities.SenacAspUtil;

namespace Senac.Fecomercio.Web
{
    public class BasePage : Page, IDisposable
    {
        #region Propriedades
        protected string SessionAcessoRestrito
        {
            get { return (string)Session["ACESSO_RESTRITO"]; }
            set { Session["ACESSO_RESTRITO"] = value; }
        }

        protected string SessionUsuario
        {
            get { return (string)Session["USERNAME"]; }
            set { Session["USERNAME"] = value; }
        }

        protected string SessionDominio
        {
            get { return (string)Session["DOMAIN"]; }
            set { Session["DOMAIN"] = value; }
        }

        protected int PerfilUsuario
        {
            get { return (int)Session["PERFIL_USUARIO"]; }
            set { Session["PERFIL_USUARIO"] = value; }
        }
        #endregion

        #region EventosOverrides
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath).ToLower();

            if (!pageName.Equals("autenticaracesso"))
            {
                    SessionUsuario = "usuario";
                    SessionDominio = "dominio";
            }
        }
        #endregion

        #region Metodos
        protected void ExibeMensagem(string mensagem)
        {
        }
        #endregion

        #region IDisposable Members
        void IDisposable.Dispose()
        {

        }
        #endregion
    }
}