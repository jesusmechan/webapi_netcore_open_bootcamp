using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DtoAsistencia : ClaseBase
    {
        public int idAsistencia { get; set; }
        public string codigoEstudiante { get; set; } = string.Empty;
        public DateTime fechaIngreso { get; set; }
    }
}
