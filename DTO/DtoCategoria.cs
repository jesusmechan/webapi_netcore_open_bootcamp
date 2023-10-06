using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DtoCategoria
    {
        public int C_ID_CATEGORIA { get; set; }
        public string C_NOMBRE_CATEGORIA { get; set; } = string.Empty;
        public bool C_ESTADO { get; set; }
        public int C_USUARIO_REGISTRO { get; set; }
        public string C_FECHA_REGSITRO { get; set; } = string.Empty;
        public int C_USUARIO_MODIFICACION { get; set; }
        public string C_FECHA_MODIFICACION { get; set; } = string.Empty;
        public string RutaImagen { get; set; } = string.Empty;
        public string NombreImagen { get; set; } = string.Empty;
        public string Base64 { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public string C_ACCION { get; set; } = string.Empty;

    }
}

