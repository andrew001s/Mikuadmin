using Protov4.DTO;
using System.Data.SqlClient;
using System.Data;

namespace Protov4.DAO
{
    public class AuditoriaDAO : DbConnection
    {
        public AuditoriaDAO(IConfiguration configuration) : base(configuration)
        {
            // Constructor que llama al constructor de la clase base (DbConnection) pasando la configuración.
            // Esto asegura que el objeto de conexión se inicialice correctamente.
        }
        public List<AuditoriaDTO> ListarAuditoria()
        {
            try
            {
                var Lista = new List<AuditoriaDTO>(); //Creamos una lista con los mismos campos del modelo MisPedidos

                using (var connection = GetSqlConnection())
                {
                    connection.Open(); // Abrir la conexión a la base de datos
                    SqlCommand cmd = new SqlCommand("ObtenerDatosAuditoriaClientesUsuarios", connection); // Llamar al procedimiento almacenado "ObtenerDatosPedido"
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new AuditoriaDTO     // Agregar detalles de los pedidos a la lista
                            {
                                id_auditoria = (int)dr["id_auditoria"],
                                id_usuario = (int)dr["id_usuario"],
                                nombre_cliente = dr["nombre_cliente"].ToString(),
                                apellido_cliente = dr["apellido_cliente"].ToString(),
                                correo_elec = dr["correo_elec"].ToString(),
                                fecha_inicio_sesion = ((DateTime)dr["fecha_inicio_sesion"]),
                                fecha_cierre_session = ((DateTime)dr["fecha_cierre_sesion"])

                            });
                        }
                    }
                }
                return Lista;
            }
            catch (Exception ex)
            {
                // Agregar manejo de errores aquí si es necesario
                Console.WriteLine("Error en RegistrarAuditoria: " + ex.Message);
                return new List<AuditoriaDTO>();
            }
        }
    }
}