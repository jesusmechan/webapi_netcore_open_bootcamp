using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DtoRol : ClaseBase
    {
        public int IDROL { get; set; }
        public string NOMBREROL { get; set; } = string.Empty;
        public bool ESTADO { get; set; }

    }
}
