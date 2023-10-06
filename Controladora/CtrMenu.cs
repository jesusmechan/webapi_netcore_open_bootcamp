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
        public CtrMenu()
        {
            _dao = new DaoMenu();
        }

        public List<DtoMenu> Menu_Listar(DtoMenu entidad)
        {
            return _dao.Menu_Listar(entidad);
        }
    }
}
