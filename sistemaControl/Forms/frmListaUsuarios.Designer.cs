namespace sistemaControl.Forms
{
    partial class frmListaUsuarios
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
            this.lsbUsuarios = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 387);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Doble click para seleccionar usuario";
            // 
            // lsbUsuarios
            // 
            this.lsbUsuarios.FormattingEnabled = true;
            this.lsbUsuarios.Location = new System.Drawing.Point(12, 12);
            this.lsbUsuarios.Name = "lsbUsuarios";
            this.lsbUsuarios.Size = new System.Drawing.Size(349, 355);
            this.lsbUsuarios.TabIndex = 1;
            this.lsbUsuarios.DoubleClick += new System.EventHandler(this.lsbUsuarios_DoubleClick_1);
            // 
            // frmListaUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 413);
            this.Controls.Add(this.lsbUsuarios);
            this.Controls.Add(this.label1);
            this.Name = "frmListaUsuarios";
            this.Text = "Usuarios";
            this.Load += new System.EventHandler(this.frmListaUsuarios_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lsbUsuarios;
    }
}