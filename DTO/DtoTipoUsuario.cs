using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DtoTipoUsuario
    {
        #region Atributos
        public int C_ID_TIPO_USUARIO { get; set; }
        public String C_NOMBRE_TIPO_USUARIO { get; set; } = string.Empty;
        [Required]
        public String C_DESCRIPCION_TIPO_USUARIO { get; set; } = string.Empty;
        public String C_FECHA_REGISTRO_TIPO_USUARIO { get; set; } = string.Empty;

        //public List<DtoTipoUsuario> LstRoles { get; set; }
        #endregion
    }
}
