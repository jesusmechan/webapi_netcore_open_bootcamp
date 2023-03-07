using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;

namespace Controladora
{
    public class CtrUsuario
    {
        DaoUsuario _dao = null;
        ClaseResultado<DtoUsuario> _resultado = null;
        public CtrUsuario()
        {
            _dao = new DaoUsuario();
            _resultado = new ClaseResultado<DtoUsuario>();
        }

        public List<DtoUsuario> Usuario_Listar()
        {
            try
            {
                return _dao.Usuario_Listar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DtoUsuario InicioSesion(DtoUsuario entidad)
        {
            try
            {
                return _dao.InicioSesion(entidad);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
