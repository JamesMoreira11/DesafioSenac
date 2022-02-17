using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EncriptaDecripta
{
    public partial class EncriptaDecripta : Form
    {
        public EncriptaDecripta()
        {
            InitializeComponent();
        }

        private void txtTextoEntrada_Enter(object sender, EventArgs e)
        {
            try
            {
                txtTextoEntrada.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.Message);
            }
        }

        private void txtTextoEntrada_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtTextoSaida.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.Message);
            }
        }

        private void cmdColar_Click(object sender, EventArgs e)
        {
            try
            {
                txtTextoEntrada.Text = Clipboard.GetText();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.Message);
            }
        }

        private void cmdCopiar_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();
                Clipboard.SetText(txtTextoSaida.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.Message);
            }

        }

        private void cmdEncripta_Click(object sender, EventArgs e)
        {
            fEncriptaDecripta(true);
        }

        private void cmdDecripta_Click(object sender, EventArgs e)
        {
            fEncriptaDecripta(false);
        }

        private void fEncriptaDecripta(bool encripta)
        {
            try
            {
                string entrada = txtTextoEntrada.Text;

                txtTextoSaida.Text = "";
                string saida = "";

                if (optCriptoDotnet.Checked)
                {
                    var cripto = new CieloGtecDAL.Crypt.Crypt();
                    saida = encripta ? cripto.Encrypt(entrada) : cripto.Decrypt(entrada);
                    cripto = null;
                }
                else if (optCriptoAsp.Checked)
                {
                    Type visaCriptoType = Type.GetTypeFromProgID("Visa_Cripto.cripto");
                    object visaCripto = Activator.CreateInstance(visaCriptoType);
                    object[] arguments = {entrada};
                    string metodo = encripta ? "criptografar" : "descriptografar";
                    object v = visaCriptoType.InvokeMember(metodo, System.Reflection.BindingFlags.InvokeMethod, null, visaCripto, arguments);
                    saida = (string)v;
                    visaCripto = null;
                    visaCriptoType = null;
                }
                else
                    throw new Exception("Tipo de criptografia não foi definido");

                txtTextoSaida.Text = saida;
                txtTextoSaida.SelectAll();
                txtTextoSaida.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.Message);
            }
        }

    }
}
