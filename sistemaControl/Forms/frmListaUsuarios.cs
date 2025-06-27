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
    public partial class frmListaUsuarios : Form
    {
        private UsuarioRepository usuarioRepository = new UsuarioRepository();
        public Usuario UsuarioSeleccionado { get; private set; }

        public frmListaUsuarios()
        {
            InitializeComponent();
        }

        private void frmListaUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            try
            {
                List<Usuario> listaDeUsuarios = usuarioRepository.ObtenerTodosLosUsuarios();
                lsbUsuarios.DataSource = null;
                lsbUsuarios.DataSource = listaDeUsuarios;
                lsbUsuarios.DisplayMember = "NombreCompleto";
                lsbUsuarios.ValueMember = "IdUsuario";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lsbUsuarios_DoubleClick_1(object sender, EventArgs e)
        {
            if (lsbUsuarios.SelectedItem != null)
            {
                this.UsuarioSeleccionado = (Usuario)lsbUsuarios.SelectedItem;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
