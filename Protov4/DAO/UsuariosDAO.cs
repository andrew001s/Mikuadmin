﻿using Microsoft.Extensions.Configuration;
using Protov4.DTO;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;


namespace Protov4.DAO
{
    public class UsuariosDAO : DbConnection
    {

        public UsuariosDAO(IConfiguration configuration) : base(configuration)
        {
            // Constructor que llama al constructor de la clase base (DbConnection) pasando la configuración.
            // Esto asegura que el objeto de conexión se inicialice correctamente.
        }

        public ((int id_usuario, int id_rol_user), int id_cliente) ValidarUsuario(UsuariosDTO nuser)
        {
            int id_usuario = 0;
            int id_cliente = 0;
            int id_rol_user = 0;

            nuser.contrasena = ConvertirSha256(nuser.contrasena);
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("Login_ValidarUsuario", connection);
                cmd.Parameters.AddWithValue("@correo_elec", nuser.correo_elec);
                cmd.Parameters.AddWithValue("@contrasena", nuser.contrasena);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id_usuario = Convert.ToInt32(reader["id_usuario"]);
                        id_rol_user = Convert.ToInt32(reader["id_rol_user"]);
                        id_cliente = Convert.ToInt32(reader["id_cliente"]);

                    }

                    if (id_usuario == 0 && id_rol_user == 0)
                    {
                        id_usuario = 0;
                        id_cliente = 0;
                        id_rol_user = 0;
                    }

                }
                connection.Close();
                return ((id_usuario, id_rol_user), id_cliente);
            }
        }

        public bool Registrar(UsuariosDTO nuser, ClientesDTO nclient)
        {
            bool registrado = false;
            string mensaje = string.Empty;

            try
            {
                if (nuser.contrasena == nuser.confirmar_contrasena)
                {
                    nuser.contrasena = ConvertirSha256(nuser.contrasena);
                }
                else
                {
                    mensaje = "Las contraseñas no coinciden";
                    return registrado;
                }

                using (var connection = GetSqlConnection())
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("Login_RegistrarUsuario", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@correo_elec", SqlDbType.VarChar).Value = nuser.correo_elec;
                        cmd.Parameters.Add("@contrasena", SqlDbType.VarChar).Value = nuser.contrasena;
                        cmd.Parameters.Add("@nombre_cliente", SqlDbType.VarChar).Value = nclient.nombre_cliente;
                        cmd.Parameters.Add("@apellido_cliente", SqlDbType.VarChar).Value = nclient.apellido_cliente;
                        cmd.Parameters.Add("@telefono_cliente", SqlDbType.VarChar).Value = nclient.telefono_cliente;

                        cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                        mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                    }
                }
            }
            catch (Exception)
            {
                mensaje = "Error al registrar";
            }

            // Puedes manejar el mensaje en el controlador si es necesario.
            return registrado;
        }

        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

    }
}
