namespace EncriptaDecripta
{
    partial class EncriptaDecripta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtTextoEntrada = new System.Windows.Forms.TextBox();
            this.cmdEncripta = new System.Windows.Forms.Button();
            this.cmdDecripta = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTextoSaida = new System.Windows.Forms.TextBox();
            this.cmdColar = new System.Windows.Forms.Button();
            this.cmdCopiar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optCriptoDotnet = new System.Windows.Forms.RadioButton();
            this.optCriptoAsp = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Texto de Entrada";
            // 
            // txtTextoEntrada
            // 
            this.txtTextoEntrada.Location = new System.Drawing.Point(25, 48);
            this.txtTextoEntrada.Multiline = true;
            this.txtTextoEntrada.Name = "txtTextoEntrada";
            this.txtTextoEntrada.Size = new System.Drawing.Size(402, 57);
            this.txtTextoEntrada.TabIndex = 1;
            this.txtTextoEntrada.TextChanged += new System.EventHandler(this.txtTextoEntrada_TextChanged);
            this.txtTextoEntrada.Enter += new System.EventHandler(this.txtTextoEntrada_Enter);
            // 
            // cmdEncripta
            // 
            this.cmdEncripta.Location = new System.Drawing.Point(25, 144);
            this.cmdEncripta.Name = "cmdEncripta";
            this.cmdEncripta.Size = new System.Drawing.Size(146, 40);
            this.cmdEncripta.TabIndex = 3;
            this.cmdEncripta.Text = "Encripta";
            this.cmdEncripta.UseVisualStyleBackColor = true;
            this.cmdEncripta.Click += new System.EventHandler(this.cmdEncripta_Click);
            // 
            // cmdDecripta
            // 
            this.cmdDecripta.Location = new System.Drawing.Point(281, 144);
            this.cmdDecripta.Name = "cmdDecripta";
            this.cmdDecripta.Size = new System.Drawing.Size(146, 40);
            this.cmdDecripta.TabIndex = 4;
            this.cmdDecripta.Text = "Decripta";
            this.cmdDecripta.UseVisualStyleBackColor = true;
            this.cmdDecripta.Click += new System.EventHandler(this.cmdDecripta_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 227);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Texto de Saída";
            // 
            // txtTextoSaida
            // 
            this.txtTextoSaida.Location = new System.Drawing.Point(25, 243);
            this.txtTextoSaida.Multiline = true;
            this.txtTextoSaida.Name = "txtTextoSaida";
            this.txtTextoSaida.ReadOnly = true;
            this.txtTextoSaida.Size = new System.Drawing.Size(402, 57);
            this.txtTextoSaida.TabIndex = 6;
            // 
            // cmdColar
            // 
            this.cmdColar.Location = new System.Drawing.Point(352, 22);
            this.cmdColar.Name = "cmdColar";
            this.cmdColar.Size = new System.Drawing.Size(75, 23);
            this.cmdColar.TabIndex = 2;
            this.cmdColar.Text = "Colar";
            this.cmdColar.UseVisualStyleBackColor = true;
            this.cmdColar.Click += new System.EventHandler(this.cmdColar_Click);
            // 
            // cmdCopiar
            // 
            this.cmdCopiar.Location = new System.Drawing.Point(352, 217);
            this.cmdCopiar.Name = "cmdCopiar";
            this.cmdCopiar.Size = new System.Drawing.Size(75, 23);
            this.cmdCopiar.TabIndex = 7;
            this.cmdCopiar.Text = "Copiar";
            this.cmdCopiar.UseVisualStyleBackColor = true;
            this.cmdCopiar.Click += new System.EventHandler(this.cmdCopiar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optCriptoAsp);
            this.groupBox1.Controls.Add(this.optCriptoDotnet);
            this.groupBox1.Location = new System.Drawing.Point(444, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 129);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox1.Visible = false;
            // 
            // optCriptoDotnet
            // 
            this.optCriptoDotnet.AutoSize = true;
            this.optCriptoDotnet.Checked = true;
            this.optCriptoDotnet.Location = new System.Drawing.Point(20, 35);
            this.optCriptoDotnet.Name = "optCriptoDotnet";
            this.optCriptoDotnet.Size = new System.Drawing.Size(59, 17);
            this.optCriptoDotnet.TabIndex = 0;
            this.optCriptoDotnet.TabStop = true;
            this.optCriptoDotnet.Text = "DotNet";
            this.optCriptoDotnet.UseVisualStyleBackColor = true;
            // 
            // optCriptoAsp
            // 
            this.optCriptoAsp.AutoSize = true;
            this.optCriptoAsp.Location = new System.Drawing.Point(21, 64);
            this.optCriptoAsp.Name = "optCriptoAsp";
            this.optCriptoAsp.Size = new System.Drawing.Size(43, 17);
            this.optCriptoAsp.TabIndex = 1;
            this.optCriptoAsp.Text = "Asp";
            this.optCriptoAsp.UseVisualStyleBackColor = true;
            // 
            // EncriptaDecripta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 331);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdCopiar);
            this.Controls.Add(this.cmdColar);
            this.Controls.Add(this.txtTextoSaida);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdDecripta);
            this.Controls.Add(this.cmdEncripta);
            this.Controls.Add(this.txtTextoEntrada);
            this.Controls.Add(this.label1);
            this.Name = "EncriptaDecripta";
            this.Text = "Encripta Decripta .Net";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTextoEntrada;
        private System.Windows.Forms.Button cmdEncripta;
        private System.Windows.Forms.Button cmdDecripta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTextoSaida;
        private System.Windows.Forms.Button cmdColar;
        private System.Windows.Forms.Button cmdCopiar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optCriptoAsp;
        private System.Windows.Forms.RadioButton optCriptoDotnet;
    }
}

