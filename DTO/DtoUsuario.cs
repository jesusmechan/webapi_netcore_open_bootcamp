namespace DTO
{
    public class DtoUsuario : ClaseBase
    {
        public int ID_USUARIO { get; set; }
        public string DNI { get; set; } = string.Empty;
        public int ID_TIENDA { get; set; }
        public String NOMBRE { get; set; } = string.Empty;
        public String APELLIDO_PATERNO { get; set; } = string.Empty;
        public String APELLIDO_MATERNO { get; set; } = string.Empty;

        public String LOGIN { get; set; } = string.Empty;

        public String PASSWORD { get; set; } = string.Empty;

        public String ConfirmPassword { get; set; } = string.Empty;

        public bool ESTADO { get; set; }

        public String CORREO { get; set; } = string.Empty;

        public String C_FOTO { get; set; } = string.Empty;

        public String C_CELULAR { get; set; } = string.Empty;

        public int C_ID_TIPO_USUARIO { get; set; }


        public DtoRol Rol { get; set; } = new DtoRol();

        //Para el cierre de la sesión
        public int C_ULTIMO_ID { get; set; }
        public string C_TOKEN { get; set; } = string.Empty;
    }
}