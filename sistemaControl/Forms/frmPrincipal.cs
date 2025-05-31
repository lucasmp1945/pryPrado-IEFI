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
    public partial class frmPrincipal : Form
    {
        private Usuario usuarioActual;
        private DateTime horaInicio;
        private Timer timer;
        private int idAuditoria;
        private Usuario usuario;


        public frmPrincipal(int idUsuario)
        {
            InitializeComponent();
            usuarioActual = BuscarUsuario(idUsuario);
            horaInicio = DateTime.Now;

            var AuditoriaRepository = new AuditoriaRepository();
            idAuditoria = AuditoriaRepository.RegistrarInicioAuditoria(idUsuario);

            IniciarTimer();

        }

        private void IniciarTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan tiempo = DateTime.Now - horaInicio;
            lblTiempo.Text = $"Tiempo conectado: {tiempo.Hours:D2}:{tiempo.Minutes:D2}:{tiempo.Seconds:D2}";
        }

        private Usuario BuscarUsuario(int idUsuario)
        {
            var repo = new UsuarioRepository();
            Usuario user = repo.ObtenerPorId(idUsuario);
            lblMensajeFondo.Text = "Bienvenido " + user.Nombre;
            lblMensajeFondo.Left = (this.ClientSize.Width - lblMensajeFondo.Width) / 2;
            lblMensajeFondo.Top = (this.ClientSize.Height - lblMensajeFondo.Height) / 2;
            return user;
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var usu = new frmUsuario();
            usu.Show();
        }

        private void auditoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var auditoria = new frmAuditoria();
            auditoria.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
            TimeSpan duracion = DateTime.Now - horaInicio;
            var AuditoriaRepository = new AuditoriaRepository();
            AuditoriaRepository.RegistrarTiempoUso(idAuditoria, duracion);
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
        }
    }
}
