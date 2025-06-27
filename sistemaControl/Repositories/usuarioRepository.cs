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

            using (SqlConnection connection = conexion.ObtenerConexion())
            {
                string query = @"SELECT * FROM usuarios WHERE idUsuario = @id";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", idUsuario);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new Usuario
                    {
                        IdUsuario = (int)reader["idUsuario"],
                        Nombre = reader["nombre"].ToString(),
                        Apellido = reader["apellido"].ToString(),
                        User = reader["usuario"].ToString(),
                        Contrasena = reader["contrasena"].ToString(),
                        UltimoLogin = reader["ultimoLogin"] == DBNull.Value ? null : (DateTime?)reader["ultimoLogin"],
                        Nivel = (int)reader["nivel"],
                        Vigente = (bool)reader["vigente"]
                    };
                }
            }

            return user;
        }




        public bool Insertar(Usuario usuario)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                string query = @"INSERT INTO usuarios (nombre, apellido, nroDoc, fecNacimiento, usuario, contrasena, nivel, vigente)
                         VALUES (@nombre, @apellido, @nroDoc, @fecNacimiento, @usuario, @contrasena, @nivel, @vigente)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@nroDoc", usuario.NroDoc);
                cmd.Parameters.AddWithValue("@fecNacimiento", usuario.FecNacimiento);
                cmd.Parameters.AddWithValue("@usuario", usuario.User);
                cmd.Parameters.AddWithValue("@contrasena", usuario.Contrasena);
                cmd.Parameters.AddWithValue("@nivel", usuario.Nivel);
                cmd.Parameters.AddWithValue("@vigente", usuario.Vigente);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public bool Actualizar(Usuario usuario)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                string query = @"UPDATE usuarios 
                         SET nombre = @nombre,
                             apellido = @apellido,
                             nroDoc = @nroDoc,
                             fecNacimiento = @fecNacimiento,
                             usuario = @usuario,
                             contrasena = @contrasena,
                             nivel = @nivel,
                             vigente = @vigente
                         WHERE idUsuario = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@nroDoc", usuario.NroDoc);
                cmd.Parameters.AddWithValue("@fecNacimiento", usuario.FecNacimiento);
                cmd.Parameters.AddWithValue("@usuario", usuario.User);
                cmd.Parameters.AddWithValue("@contrasena", usuario.Contrasena);
                cmd.Parameters.AddWithValue("@nivel", usuario.Nivel);
                cmd.Parameters.AddWithValue("@vigente", usuario.Vigente);
                cmd.Parameters.AddWithValue("@id", usuario.IdUsuario);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public bool Eliminar(int idUsuario)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                string query = @"UPDATE usuarios SET vigente = 0 WHERE idUsuario = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", idUsuario);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            var usuarios = new List<Usuario>();

            using (SqlConnection connection = conexion.ObtenerConexion())
            {
                string query = @"SELECT * FROM usuarios ORDER BY apellido, nombre;";

                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var usuario = new Usuario
                    {
                        IdUsuario = (int)reader["idUsuario"],
                        Nombre = reader["nombre"].ToString(),
                        Apellido = reader["apellido"].ToString(),
                        NroDoc = reader["nroDoc"].ToString(),
                        FecNacimiento = (DateTime)reader["fecNacimiento"],
                        User = reader["usuario"].ToString(),
                        Contrasena = reader["contrasena"].ToString(),
                        UltimoLogin = reader["ultimoLogin"] == DBNull.Value ? null : (DateTime?)reader["ultimoLogin"],
                        Nivel = (int)reader["nivel"],
                        Vigente = (bool)reader["vigente"]
                    };

                    usuarios.Add(usuario);
                }
            }

            return usuarios;
        }



    }
}
