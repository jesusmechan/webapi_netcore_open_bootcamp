using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class CtrImportacion
    {
        DaoImportacion dao = null;
        public CtrImportacion() 
        {
            dao = new DaoImportacion();
        }
        public bool InsertDataIntoDatabase(List<DtoImportacionVisitante> dataList)
        {
            return dao.InsertarDatosGA(dataList);
        }

        public bool InsertDataIntoDatabase2(List<DtoImportacionAsitencia> dataList)
        {
            return dao.InsertarDatosGA2(dataList);
        }
    }
}
