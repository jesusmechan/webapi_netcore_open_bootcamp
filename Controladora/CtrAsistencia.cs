using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;


namespace Controladora
{
    public class CtrAsistencia
    {
        DaoAsistencia _dao = null;
        ClaseResultado<DtoAsistencia> _resultado = null;

        public CtrAsistencia()
        {
            _dao = new DaoAsistencia();
            _resultado = new ClaseResultado<DtoAsistencia>();
        }

        public ClaseResultado<DtoAsistencia> MNT_Asistencia(DtoAsistencia _entidad)
        {
            return _dao.MNT_Asistencia(_entidad);
        }
    }
}
