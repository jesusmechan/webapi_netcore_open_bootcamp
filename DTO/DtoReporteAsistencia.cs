using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DtoReporteAsistencia
    {
        public int idEstudiante { get; set; }
        public string codigoEstudiante { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string apellidoPaterno { get; set; } = string.Empty;
        public string apellidoMaterno { get; set; } = string.Empty;
        public string especialidad { get; set; } = string.Empty;
        public string fechaIngreso { get; set; } = string.Empty;
        public string fechaIngresoFormat { get; set; } = string.Empty;
        public string horaIngreso { get; set; } = string.Empty;
    }

    public class filtrosReporteAsistencia
    {
        public string idEspecialidad { get; set; } = string.Empty;
        public string fechaInicio { get; set; } = string.Empty;
        public string fechaFin { get; set; } = string.Empty;
    }

    public class DtoListReporteAsistencia
    {
        public List<DtoReporteAsistencia> lista  { get; set; }
    }
}
