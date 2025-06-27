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
    public partial class frmUsuario : Form
    {

        private bool esNuevo = false;
        private Usuario usuarioSeleccionado;
        private UsuarioRepository usuarioRepository = new UsuarioRepository();

        public frmUsuario()
        {
            InitializeComponent();
        }

        private void btnVerGrilla_Click(object sender, EventArgs e)
        {

            using (frmListaUsuarios selectorForm = new frmListaUsuarios())
            {
                DialogResult resultado = selectorForm.ShowDialog();
                if (resultado == DialogResult.OK)
                {
                    Usuario usuarioElegido = selectorForm.UsuarioSeleccionado;
                    if (usuarioElegido != null)
                    {
                        usuarioSeleccionado = usuarioElegido;
                        CargarDatosEnFormulario(usuarioSeleccionado);
                    }
                }
            }
        }

        private void CargarDatosEnFormulario(Usuario u)
        {
            txtNombre.Text = u.Nombre;
            txtApellido.Text = u.Apellido;
            txtNroDocumento.Text = u.NroDoc;
            dtmFechaNacimiento.Value = u.FecNacimiento;
            txtUsuario.Text = u.User;
            txtContrasenia.Text = u.Contrasena;
            cbActivo.Checked = u.Vigente;
            cmbNivel.SelectedIndex = u.Nivel - 1;
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            HabilitarControles(true);
            esNuevo = true;
            usuarioSeleccionado = null;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("Primero seleccione un usuario.");
                return;
            }

            esNuevo = false;
            HabilitarControles(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            Usuario usuario = CrearDesdeFormulario();

            bool resultado;

            if (esNuevo)
            {
                resultado = usuarioRepository.Insertar(usuario);
                if (resultado)
                    MessageBox.Show("Usuario guardado correctamente.");
            }
            else
            {
                usuario.IdUsuario = usuarioSeleccionado.IdUsuario;
                resultado = usuarioRepository.Actualizar(usuario);
                if (resultado)
                    MessageBox.Show("Usuario actualizado correctamente.");
            }

            if (resultado)
            {
                LimpiarFormulario();
                HabilitarControles(false);
                esNuevo = false;
                usuarioSeleccionado = null;
            }
        }


        private Usuario CrearDesdeFormulario()
        {
            return new Usuario
            {
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                NroDoc = txtNroDocumento.Text.Trim(),
                FecNacimiento = dtmFechaNacimiento.Value,
                User = txtUsuario.Text.Trim(),
                Contrasena = txtContrasenia.Text.Trim(),
                Vigente = cbActivo.Checked,
                Nivel = cmbNivel.SelectedIndex + 1
            };
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("Seleccione un usuario para eliminar.");
                return;
            }

            var confirm = MessageBox.Show("¿Seguro que desea eliminar?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                bool eliminado = usuarioRepository.Eliminar(usuarioSeleccionado.IdUsuario);
                if (eliminado)
                {
                    MessageBox.Show("Usuario eliminado.");
                    LimpiarFormulario();
                    usuarioSeleccionado = null;
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar.");
                }
            }
        }


        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtNroDocumento.Clear();
            dtmFechaNacimiento.Value = DateTime.Now;
            txtUsuario.Clear();
            txtContrasenia.Clear();
            cbActivo.Checked = true;
            cmbNivel.SelectedIndex = 0;
        }

        private void HabilitarControles(bool habilitar)
        {
            txtNombre.Enabled = habilitar;
            txtApellido.Enabled = habilitar;
            txtNroDocumento.Enabled = habilitar;
            dtmFechaNacimiento.Enabled = habilitar;
            txtUsuario.Enabled = habilitar;
            txtContrasenia.Enabled = habilitar;
            cbActivo.Enabled = habilitar;
            cmbNivel.Enabled = habilitar;
            btnGuardar.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtNroDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtContrasenia.Text))
            {
                MessageBox.Show("Complete todos los campos obligatorios.");
                return false;
            }
            return true;
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            var niveles = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Administrador"),
                new KeyValuePair<int, string>(2, "Usuario")
            };

            cmbNivel.DataSource = niveles;
            cmbNivel.DisplayMember = "Value";
            cmbNivel.ValueMember = "Key";
            cmbNivel.SelectedIndex = 0;
        }
    }
}
