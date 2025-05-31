using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sistemaControl.dataBase;
using sistemaControl.Model;

namespace sistemaControl.Models
{
    public class UsuarioRepository
    {
        private readonly Conexion conexion = new Conexion();

        public int? ValidarLogin(string usuario, string contrasena)
        {
            int? idUsuario = null;

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                string query = @"SELECT idUsuario FROM usuarios 
                                 WHERE usuario = @usuario AND contrasena = @contrasena AND vigente = 1";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@contrasena", contrasena);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    idUsuario = Convert.ToInt32(result);
                    ActualizarUltimoLogin(idUsuario);

                }
            }
            return idUsuario;
        }


        public void ActualizarUltimoLogin(int? idUsuario)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string updateQuery = @"UPDATE usuarios SET ultimoLogin = GETDATE() WHERE idUsuario = @idUsuario";
                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                updateCmd.ExecuteNonQuery();
            }
        }


        public Usuario ObtenerPorId(int idUsuario)
        {
            Usuario user = null;

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                string query = @"SELECT * FROM usuarios WHERE idUsuario = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", idUsuario);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new Usuario
                    {
                        IdUsuario = (int)reader["idUsuario"],
                        Nombre = reader["nombre"].ToString(),
                        Apellido = reader["apellido"].ToString(),
                        UsuarioNombre = reader["usuario"].ToString(),
                        Contrasena = reader["contrasena"].ToString(),
                        UltimoLogin = reader["ultimoLogin"] == DBNull.Value ? null : (DateTime?)reader["ultimoLogin"],
                        Nivel = (int)reader["nivel"],
                        Vigente = (bool)reader["vigente"]
                    };
                }
            }

            return user;
        }

    }
}
