using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sistemaControl.dataBase;

namespace sistemaControl.Repositories
{
    public class AuditoriaRepository
    {

        private readonly Conexion conexion = new Conexion();
        public int RegistrarInicioAuditoria(int idUsuario)
        {
            int idAuditoria = 0;

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"INSERT INTO auditoriaUsuarios (idUsuario, fecha)
                         OUTPUT INSERTED.idAuditoria
                         VALUES (@idUsuario, GETDATE())";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                idAuditoria = (int)cmd.ExecuteScalar();
            }

            return idAuditoria;
        }

        public void RegistrarTiempoUso(int idAuditoria, TimeSpan duracion)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"UPDATE auditoriaUsuarios SET tiempoUso = @tiempo WHERE idAuditoria = @idAuditoria";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tiempo", (int)duracion.TotalMinutes); 
                cmd.Parameters.AddWithValue("@idAuditoria", idAuditoria);

                cmd.ExecuteNonQuery();
            }
        }


    }
}
