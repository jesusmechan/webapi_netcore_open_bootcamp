namespace DTO
{
    public class DtoUsuario : ClaseBase
    {
        public int C_ID_USUARIO { get; set; }
        public string C_DNI { get; set; } = string.Empty;
        public int C_ID_TIENDA { get; set; }
        public string C_NOMBRE_TIENDA { get; set; } = string.Empty;
        //public DtoTipoUsuario TipoUsuario { get; set; } ;
        public String C_NOMBRECOMPLETO { get; set; } = string.Empty;
        public String C_NOMBRES { get; set; } = string.Empty;
        public String C_APELLIDO_PATERNO { get; set; } = string.Empty;
        public String C_APELLIDO_MATERNO { get; set; } = string.Empty;

        public String C_LOGIN { get; set; } = string.Empty;

        public String C_PASSWORD { get; set; } = string.Empty;

        public String ConfirmPassword { get; set; } = string.Empty;

        public bool C_ESTADO { get; set; }
        public string C_ESTADO_STRING { get; set; } = string.Empty;

        public String C_CORREO { get; set; } = string.Empty;

        public String C_FOTO { get; set; } = string.Empty;


        public String C_CELULAR { get; set; } = string.Empty;

        public int C_ID_TIPO_USUARIO { get; set; }




        //Para el cierre de la sesión
        public int C_ULTIMO_ID { get; set; }
    }
}