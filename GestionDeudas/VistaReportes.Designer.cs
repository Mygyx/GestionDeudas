namespace GestionDeudas
{
    partial class VistaReportes
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
            this.rpCli = new System.Windows.Forms.RadioButton();
            this.pnl = new System.Windows.Forms.Panel();
            this.rpFecha = new System.Windows.Forms.RadioButton();
            this.prMon = new System.Windows.Forms.RadioButton();
            this.rpUsu = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rpCli
            // 
            this.rpCli.AutoSize = true;
            this.rpCli.Checked = true;
            this.rpCli.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rpCli.Location = new System.Drawing.Point(12, 1);
            this.rpCli.Name = "rpCli";
            this.rpCli.Size = new System.Drawing.Size(210, 33);
            this.rpCli.TabIndex = 0;
            this.rpCli.TabStop = true;
            this.rpCli.Text = "Reporte clientes";
            this.rpCli.UseVisualStyleBackColor = true;
            this.rpCli.CheckedChanged += new System.EventHandler(this.rpCli_CheckedChanged);
            // 
            // pnl
            // 
            this.pnl.Location = new System.Drawing.Point(-1, 40);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(1925, 1017);
            this.pnl.TabIndex = 1;
            // 
            // rpFecha
            // 
            this.rpFecha.AutoSize = true;
            this.rpFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rpFecha.Location = new System.Drawing.Point(259, 1);
            this.rpFecha.Name = "rpFecha";
            this.rpFecha.Size = new System.Drawing.Size(197, 33);
            this.rpFecha.TabIndex = 8;
            this.rpFecha.Text = "Reporte fechas";
            this.rpFecha.UseVisualStyleBackColor = true;
            this.rpFecha.CheckedChanged += new System.EventHandler(this.rpFecha_CheckedChanged);
            // 
            // prMon
            // 
            this.prMon.AutoSize = true;
            this.prMon.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prMon.Location = new System.Drawing.Point(495, 1);
            this.prMon.Name = "prMon";
            this.prMon.Size = new System.Drawing.Size(206, 33);
            this.prMon.TabIndex = 9;
            this.prMon.TabStop = true;
            this.prMon.Text = "Reporte montos";
            this.prMon.UseVisualStyleBackColor = true;
            this.prMon.CheckedChanged += new System.EventHandler(this.prMon_CheckedChanged);
            // 
            // rpUsu
            // 
            this.rpUsu.AutoSize = true;
            this.rpUsu.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rpUsu.Location = new System.Drawing.Point(742, 1);
            this.rpUsu.Name = "rpUsu";
            this.rpUsu.Size = new System.Drawing.Size(218, 33);
            this.rpUsu.TabIndex = 10;
            this.rpUsu.TabStop = true;
            this.rpUsu.Text = "Reporte usuarios";
            this.rpUsu.UseVisualStyleBackColor = true;
            this.rpUsu.CheckedChanged += new System.EventHandler(this.rpUsu_CheckedChanged);
            // 
            // VistaReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(245)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.rpUsu);
            this.Controls.Add(this.prMon);
            this.Controls.Add(this.rpFecha);
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.rpCli);
            this.Name = "VistaReportes";
            this.Text = "VistaReportes";
            this.Load += new System.EventHandler(this.VistaReportes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rpCli;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.RadioButton rpFecha;
        private System.Windows.Forms.RadioButton prMon;
        private System.Windows.Forms.RadioButton rpUsu;
    }
}