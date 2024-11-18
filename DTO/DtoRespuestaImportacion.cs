using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DtoRespuestaImportacion
    {
        public string accion { get; set; } = string.Empty;
        public int cantFilas { get; set; }
    }
}
