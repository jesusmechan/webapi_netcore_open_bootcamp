using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DtoRol
    {
        public int ID_ROL { get; set; }
        public string NOMBRE_ROL { get; set; } = string.Empty;
        public string FECHA_CREACION { get; set; } = string.Empty;
        public int USUARIO_CREACION { get; set; }
        public string FECHA_MODIFICACION { get; set; } = string.Empty;
    }
}
