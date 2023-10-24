using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class CtrMenu
    {
        DaoMenu _dao = null;
        ClaseResultado<DtoMenu> _resultado = null;
        public CtrMenu()
        {
            _dao = new DaoMenu();
            _resultado = new ClaseResultado<DtoMenu>();
        }

        public List<DtoMenu> Menu_Listar(DtoMenu entidad)
        {
            return _dao.Menu_Listar(entidad);
        }

        public List<DtoMenu> Menu_Listar_Tabla()
        {
            return _dao.Menu_Listar_Tabla();
        }

        public ClaseResultado<DtoMenu> Menu_Insertar_Actualizar(DtoMenu _entidad)
        {
            try
            {
                _resultado = _dao.Menu_Insertar_Actualizar(_entidad);
            }
            catch (Exception ex)
            {
                _resultado.HuboError = true;
                _resultado.Mensaje = ex.Message.ToString();

            }
            return _resultado;
        }

        public ClaseResultado<DtoMenu> Menu_Activar_Inactivar(DtoMenu _entidad)
        {
            try
            {
                _resultado = _dao.Menu_Activar_Inactivar(_entidad);
            }
            catch (Exception ex)
            {
                _resultado.HuboError = true;
                _resultado.Mensaje = ex.Message.ToString();
            }
            return _resultado;
        }
    }
}
