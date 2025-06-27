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
using sistemaControl.Repositories;

namespace sistemaControl.Forms
{
    public partial class frmAuditoria : Form
    {
        private Usuario usuarioActual;
        private bool esAdmin;

        public frmAuditoria(Usuario usuario, bool esAdministrador)
        {
            InitializeComponent();
            usuarioActual = usuario;
            esAdmin = esAdministrador;
        }

        private void frmAuditoria_Load(object sender, EventArgs e)
        {
            var repo = new UsuarioRepository();

            if (esAdmin)
            {
               
                var usuarios = repo.ObtenerTodosLosUsuarios().Where(u => u.Vigente).ToList();
                cmbUsuarios.DataSource = usuarios;
                cmbUsuarios.DisplayMember = "NombreCompleto";
                cmbUsuarios.ValueMember = "IdUsuario";
                cmbUsuarios.SelectedIndex = -1;
                ckbTodos.Enabled = true;
                cmbUsuarios.Enabled = false;
                ckbTodos.Checked = true;
            }
            else
            {

                cmbUsuarios.DataSource = new List<Usuario> { usuarioActual };
                cmbUsuarios.DisplayMember = "NombreCompleto";
                cmbUsuarios.ValueMember = "IdUsuario";
                cmbUsuarios.SelectedIndex = 0;

                ckbTodos.Checked = false;
                ckbTodos.Enabled = false;
                cmbUsuarios.Enabled = false;
            }

            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today;
        }

        private void CargarUsuarios()
        {
            var usuarioRepository = new UsuarioRepository();
            var usuarios = usuarioRepository.ObtenerTodosLosUsuarios().Where(u => u.Vigente).ToList();

            cmbUsuarios.DataSource = usuarios;
            cmbUsuarios.DisplayMember = "NombreCompleto";
            cmbUsuarios.ValueMember = "IdUsuario";
            cmbUsuarios.SelectedIndex = -1;
        }

        private void ckbTodos_CheckedChanged(object sender, EventArgs e)
        {
            cmbUsuarios.Enabled = !ckbTodos.Checked;
            if (ckbTodos.Checked)
                cmbUsuarios.SelectedIndex = -1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date.AddDays(1).AddTicks(-1);

            int? idUsuario = null;
            if (!ckbTodos.Checked && cmbUsuarios.SelectedValue != null)
            {
                idUsuario = (int)cmbUsuarios.SelectedValue;
            }

            var auditoriaRepository = new AuditoriaRepository();
            var lista = auditoriaRepository.FiltrarAuditoria(desde, hasta, idUsuario);

            dgvAuditoria.DataSource = lista;
        }
    }
}
