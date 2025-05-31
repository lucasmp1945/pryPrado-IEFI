using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistemaControl.Model;
using sistemaControl.Models;

namespace sistemaControl.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;

            var repo = new UsuarioRepository();
            int? id = repo.ValidarLogin(usuario, contrasena);

            if (id != null)
            {
                var frm = new frmPrincipal(id.Value);
                frm.Show();

            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }
    }
}
