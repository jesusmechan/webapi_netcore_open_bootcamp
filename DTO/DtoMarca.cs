using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DtoMarca : ClaseBase
    {
        public int IDMARCA { get; set; }
        public string NOMBREMARCA { get; set; } = string.Empty;
        public bool ESTADO { get; set; }
    }
}
