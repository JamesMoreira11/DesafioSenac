using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UT = Senac.Fecomercio.BLL.Utilities.SenacAspUtil;

public partial class Controles_DataHora : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public bool ApresentarHora
    {
        get
        {
            return drpHora.Visible;
        }
        set
        {
            drpHora.Visible = value;
            drpMinuto.Visible = value;
            lblDoisPontos.Visible = value;
        }
    }
    
    public System.Drawing.Color ForeColor
    {
        get
        {
            return lblData.ForeColor;
        }
        set
        {
            lblData.ForeColor = value;
        }
    }

    public bool ModoEdicao
    {
        get
        {
            return pnlEdicao.Visible;
        }
        set
        {
            DateTime? d = Value;

            pnlEdicao.Visible = value;
            lblData.Visible = !value;

            Value = d;
        }
    }

    public string Text
    {
        get
        {
            if (Value == null)
            {
                // Esta linha impediria a validação no client
                //return "";
                return GetTextEdit();
            }

            if (ApresentarHora)
                return ((DateTime)Value).ToString("dd/MM/yyyy HH:mm");
            else
                return ((DateTime)Value).ToString("dd/MM/yyyy");
        }
        set
        {
            //if (value.Trim().Length == 0)
            //    Value = null;
            //else
            //    Value = Convert.ToDateTime(value);
            
            if (UT.IsDate(value))
                Value = Convert.ToDateTime(value);
            else
            {
                Value = null;
                if (ModoEdicao)
                {
                    txtData.Text = value;
                }
                else
                    lblData.Text = value;
            }
        }
    }

    private string GetTextEdit()
    {
        string s;
        s = txtData.Text.Trim();

        if (s.Length > 0 && drpHora.Visible)
        {
            s += " " + drpHora.SelectedValue;

            if (drpMinuto.Visible)
                s += ":" + drpMinuto.SelectedValue;
            else
                s += ":00";
        }
        return s;
    }
    
    public DateTime? Value
    {
        get
        {
            string s = "";
            if (ModoEdicao)
                s = GetTextEdit();
            else
                s = lblData.Text;

            DateTime? d = null;
            try
            {
                d = Convert.ToDateTime(s);
            }
            catch (Exception)
            {
                d = null;
            }
            return d;
        }
        set
        {
            if (ModoEdicao)
            {
                txtData.Text = value == null ? "" : ((DateTime)value).ToString("dd/MM/yyyy");

                if (drpHora.Visible)
                    drpHora.SelectedValue = value == null ? "00" : ((DateTime)value).Hour.ToString("00");

                if (drpMinuto.Visible)
                    drpMinuto.SelectedValue = value == null ? "00" : ((DateTime)value).Minute.ToString("00");
            }
            else
                lblData.Text = value == null ? "" : ((DateTime)value).ToString("dd/MM/yyyy HH:mm:ss");
                //lblData.Text = !UT.IsDate(value) ? "" : ((DateTime)value).ToString("dd/MM/yyyy HH:mm:ss");
        }
    }

    public int Period
    {
        set
        {
            DateTime NowDay = DateTime.Today;
            DateTime lastDay = DateTime.Today.AddMonths(-value);

            txtData_CalendarExtender.StartDate = lastDay;
            txtData_CalendarExtender.EndDate = NowDay;
        }
    }
}
