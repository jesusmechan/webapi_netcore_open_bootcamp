using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public  class ClaseBase
    {
        public int C_USUARIO_REGISTRO { get; set; } 
        public string C_FECHA_REGISTRO { get; set; } = string.Empty;
        public int C_USUARIO_MODIFICACION { get; set; }
        public string C_FECHA_MODIFICACION { get; set; } = string.Empty; 
    }
}
