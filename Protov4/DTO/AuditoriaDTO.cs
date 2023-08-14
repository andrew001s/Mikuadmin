namespace Protov4.DTO
{
    public class AuditoriaDTO
    {
        public int id_auditoria { get; set; }
        public int id_usuario { get; set; }
        public string nombre_cliente { get; set; }
        public string apellido_cliente { get; set; }
        public string correo_elec { get; set; }
        public DateTime fecha_inicio_sesion { get; set; }
        public DateTime fecha_cierre_session { get; set; }

    }
}