using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controles_ComboLabel : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public System.Drawing.Color ForeColor
    {
        get
        {
            return lblGeral.ForeColor;
        }
        set
        {
            lblGeral.ForeColor = value;
        }
    }

    public bool ModoEdicao
    {
        get
        {
            return drpGeral.Visible;
        }
        set
        {
            drpGeral.Visible = value;
            lblGeral.Visible = !value;
        }
    }

    public string Text
    {
        get
        {
            return Value;
        }
        set
        {
            Value = value;
        }
    }

    public string Value
    {
        get
        {
            if (ModoEdicao)
                return drpGeral.SelectedValue;
            else
                return lblGeral.Text;
        }
        set
        {
            if (ModoEdicao)
                drpGeral.SelectedValue = value;
            else
                lblGeral.Text = value;
        }
    }

    //public string LiteralText
    //{
    //    set
    //    {
    //        ltrGeral.Visible = true;
    //        ltrGeral.Text = value;
    //    }
    //}

    public void DataBind()
    {
        drpGeral.DataBind();
    }

    public DropDownList Combo
    {
        get
        {
            return drpGeral;
        }
    }

    public ListItemCollection Items
    {
        get
        {
            return drpGeral.Items;
        }
    }
    
    public string DataTextField
    {
        get
        {
            return drpGeral.DataTextField;
        }
        set
        {
            drpGeral.DataTextField = value;
        }
    }

    public string DataValueField
    {
        get
        {
            return drpGeral.DataValueField;
        }
        set
        {
            drpGeral.DataValueField = value;
        }
    }

    public object DataSource
    {
        get
        {
            return drpGeral.DataSource;
        }
        set
        {
            drpGeral.DataSource = value;
        }
    }

    public string SelectedValue
    {
        get
        {
            return drpGeral.SelectedValue;
        }
        set
        {
            drpGeral.SelectedValue = value;
        }
    }
}
