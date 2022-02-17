using Senac.Fecomercio.Common;
using Senac.Fecomercio.Data;
using Senac.Fecomercio.Web;
using System;
using System.Data;
using System.Web.UI.WebControls;
using Senac.Fecomercio.BLL.Dao.Cadastro;
using Senac.Fecomercio.BLL.Utilities;
using System.Configuration;
using UT = Senac.Fecomercio.BLL.Utilities.SenacAspUtil;

public partial class Cadastros_RegistroPonto_PesquisaFolhaPonto : BasePage
{
    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Sempre que a sessão é perdida, o GTEC realiza redirect para a página de login
        if (string.IsNullOrEmpty(this.SessionUsuario))
        {
            // Este parâmetro NÃO SERÁ fornecido quando a tela for chamada a partir da conclusão 
            //      de uma edição, nestas situações a SESSION deverá estar preenchida.

            // Somente realizar login SE !IsPostBack, senão outras variáveis session podem ser perdidas

            if (!IsPostBack && Request.QueryString["username"] != null)
            {
                SessionUsuario = Request.QueryString["username"];
                SessionDominio = Request.QueryString["domain"];
                //FormsAuthentication.RedirectFromLoginPage();
            }
            else if (!IsPostBack && ConfigurationManager.AppSettings["ambiente"] == "HOMOLOGACAO")
            {
                SessionUsuario = "usuario";
                SessionDominio = "dominio";

                //Session.Timeout = 1;
            }
            else
            {
                //Response.Redirect(Request.QueryString["RedirectToLogin"]);
                Response.Redirect(UT.RedirectToLogin());
            }

        }
        #endregion

        if (!IsPostBack)
        {
            ConsultaGrid();
        }

    }

    protected void grdItens_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdItens.PageIndex = e.NewPageIndex;
            ConsultaGrid();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void grdItens_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Zebrado
            if (((e.Row.RowIndex) % 2) == 0)
            {
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            }
            else
            {
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#e4e9ef");
            }
        }
    }

    protected void grdItens_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            grdItens.PageIndex = 0;
            ConsultaGrid();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        try
        {
            grdItens.PageIndex = 0;
            txtColaborador.Text = "";
            ConsultaGrid();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Metodos
    private void ConsultaGrid()
    {
        bool conversao = true;
        int codigo = 0;

        DataTable lista;

        codigo = 0;
        conversao = int.TryParse(txtColaborador.Text, out codigo);

        lista = PesquisaFolhaPontoDAO.ListaFolhaPonto(codigo);

        codigo = 0;
        conversao = int.TryParse(txtpaginacao.Text, out codigo);

        if (codigo <= 1)
        {
            codigo = 20;
            txtpaginacao.Text = "20";
        }

        lbltotal.Text = "";

        if (lista.Rows.Count > 0)
        {
            string var2 = txtpaginacao.Text;
            int paginas = 1;
            int resto = 0;
            if (lista.Rows.Count <= codigo)
            {
                var2 = lista.Rows.Count.ToString();
            }
            paginas = (lista.Rows.Count / codigo);
            resto = lista.Rows.Count - (paginas * codigo);
            if (resto >= 1)
            {
                paginas = paginas + 1;
            }
            lbltotal.Text = "1 - " + var2 + "  de  " + lista.Rows.Count.ToString() + "   registros em   " + paginas.ToString() + "  pagina(s).";
        }

        if (lista.Rows.Count == 0)
        {
            grdItens.Visible = false;
        }
        else
        {
            grdItens.Visible = true;
            grdItens.AllowPaging = true;
            grdItens.PageSize = codigo;
            grdItens.DataSource = lista;
            grdItens.DataBind();
        }
    }
    protected void OK_Click(object sender, EventArgs e)
    {
        bool conversao = true;
        int codigo = 0;

        DataTable lista;

        codigo = 0;
        conversao = int.TryParse(txtColaborador.Text, out codigo);

        lista = PesquisaFolhaPontoDAO.ListaFolhaPonto(codigo);

        codigo = 0;
        conversao = int.TryParse(txtpaginacao.Text, out codigo);

        if (codigo <= 1)
        {
            codigo = 20;
            txtpaginacao.Text = "20";
        }

        lbltotal.Text = "";

        if (lista.Rows.Count > 0)
        {
            string var2 = txtpaginacao.Text;
            int paginas = 1;
            int resto = 0;
            if (lista.Rows.Count <= codigo)
            {
                var2 = lista.Rows.Count.ToString();
            }
            paginas = (lista.Rows.Count / codigo);
            resto = lista.Rows.Count - (paginas * codigo);
            if (resto >= 1)
            {
                paginas = paginas + 1;
            }
            lbltotal.Text = "1 - " + var2 + "  de  " + lista.Rows.Count.ToString() + "   registros em   " + paginas.ToString() + "  pagina(s).";
        }

        if (lista.Rows.Count == 0)
        {
            grdItens.Visible = false;
        }
        else
        {
            grdItens.Visible = true;
            grdItens.AllowPaging = true;
            grdItens.PageSize = codigo;
            grdItens.DataSource = lista;
            grdItens.DataBind();
        }
    }

    #endregion
}
