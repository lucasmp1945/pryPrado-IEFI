using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sistemaControl.dataBase;
using sistemaControl.Models;

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


        public List<AuditoriaDTO> FiltrarAuditoria(DateTime desde, DateTime hasta, int? idUsuario)
        {
            var lista = new List<AuditoriaDTO>();

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                string query = @"SELECT a.fecha, a.tiempoUso, u.nombre, u.apellido
                         FROM auditoriaUsuarios a
                         INNER JOIN usuarios u ON a.idUsuario = u.idUsuario
                         WHERE a.fecha BETWEEN @desde AND @hasta";

                if (idUsuario.HasValue)
                    query += " AND a.idUsuario = @idUsuario";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@desde", desde);
                cmd.Parameters.AddWithValue("@hasta", hasta);

                if (idUsuario.HasValue)
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario.Value);

                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var item = new AuditoriaDTO
                    {
                        Fecha = reader["fecha"] != DBNull.Value ? (DateTime)reader["fecha"] : DateTime.MinValue,
                        TiempoUso = reader["tiempoUso"] != DBNull.Value ? Convert.ToInt32(reader["tiempoUso"]) : 0,
                        NombreUsuario = $"{reader["apellido"]}, {reader["nombre"]}"
                    };

                    lista.Add(item);
                }

            }

            return lista;
        }



    }
}
