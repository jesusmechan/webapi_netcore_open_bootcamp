using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DtoMenu
    {
        public int ID_MENU { get; set; }
        public string NOMBRE_MENU { get; set; } = string.Empty;
        public string ICONO { get; set; } = string.Empty;
        public bool ESTADO { get; set; }
        public int NUMERO_ORDEN { get; set; }
        public string FECHA_CREACION { get; set; } = string.Empty;
        public int USUARIO_CREACION { get; set; }
        public string FECHA_MODIFICACION { get; set; } = string.Empty;
        public int USUARIO_MODIFICACION { get; set; }
        public int ID_ROL { get; set; }
        public int ID_USUARIO { get; set; }
        public DtoSubMenu dtoSubMenu { get; set; } = new DtoSubMenu();
    }
}
