using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DtoEstudiante : ClaseBase
    {
        public int idEstudiante { get; set; }
        public int idEspecialidad { get; set; }
        public int idtipo { get; set; }
        public string codigoEstudiante { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string apellidoPaterno { get; set; } = string.Empty;
        public string apellidoMaterno { get; set; } = string.Empty;
        public bool estado { get; set; }
        public string especialidad { get; set; } = string.Empty;
    }

    public class filtrosBusqueda
    {
        public int idEspcialidad  { get; set; }
    }

}
