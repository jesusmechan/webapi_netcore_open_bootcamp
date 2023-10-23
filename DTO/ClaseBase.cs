using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public  class ClaseBase
    {
        public string ACCION { get; set; } = string.Empty;
        public int IDUSUARIOCREACION { get; set; }
        public string FECHACREACION { get; set; } = string.Empty;
        public int IDUSUARIOMODIFICACION { get; set; }
        public string FECHAMODIFICACION { get; set; } = string.Empty;
    }
}
