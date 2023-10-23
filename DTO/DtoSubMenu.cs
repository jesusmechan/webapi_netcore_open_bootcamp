using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DtoSubMenu : ClaseBase
    {
        public int IDSUBMENU { get; set; }
        public int IDMENU { get; set; }
        public string NOMBRESUBMENU { get; set; } = string.Empty;
        public string RUTA { get; set; } = string.Empty;
        public string ICONO { get; set; } = string.Empty;
        public bool ESTADO { get; set; }
        public int NUMERO_ORDEN { get; set; }
    }
}
