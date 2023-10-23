using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DtoMenu : ClaseBase
    {
        public int IDMENU { get; set; }
        public string NOMBREMENU { get; set; } = string.Empty;
        public string ICONO { get; set; } = string.Empty;
        public bool ESTADO { get; set; }
        public int NUMEROORDEN { get; set; }
        public int IDROL { get; set; }
        public int IDUSUARIO { get; set; }
        public DtoSubMenu dtoSubMenu { get; set; } = new DtoSubMenu();
    }
}
