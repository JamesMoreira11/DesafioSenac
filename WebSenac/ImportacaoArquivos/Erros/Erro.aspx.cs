using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UT = Senac.Fecomercio.BLL.Utilities.SenacAspUtil;

public partial class Erros_Erro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblErro.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            if (!UT.IsNull(Session["DOMAIN"]))
                lblErro.Text += " " + Session["DOMAIN"].ToString();

            if (!UT.IsNull(Session["USERNAME"]))
                lblErro.Text += " \\ " + Session["USERNAME"].ToString();
        }
        catch (Exception)
        {
        }
    }
}
