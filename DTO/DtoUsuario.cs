using System.Text.Json.Serialization;

namespace DTO
{
    public class DtoUsuario : ClaseBase
    {
        public int IDUSUARIO { get; set; }
        public int IDROL { get; set; }
        public string NUMERODOCUMENTO { get; set; } = string.Empty;
        public int IDTIENDA { get; set; }
        public String NOMBRE { get; set; } = string.Empty;
        public String APELLIDOPATERNO { get; set; } = string.Empty;
        public String APELLIDOMATERNO { get; set; } = string.Empty;

        public String LOGIN { get; set; } = string.Empty;

        public String PASSWORD { get; set; } = string.Empty;

        public String ConfirmPassword { get; set; } = string.Empty;

        public bool ESTADO { get; set; }

        public String CORREO { get; set; } = string.Empty;

        public String CFOTO { get; set; } = string.Empty;

        public String CCELULAR { get; set; } = string.Empty;

        public int CIDTIPOUSUARIO { get; set; }

        public DtoRol Rol { get; set; } = new DtoRol();

        //Para el cierre de la sesión
        public int CULTIMOID { get; set; }
        public string CTOKEN { get; set; } = string.Empty;
        public string FECHANACIMIENTO { get; set;} = string.Empty;
        public string SEXO { get; set; } = string.Empty;
    }
}