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
        public void InsertDataIntoDatabase(List<DtoImportacionVisitante> dataList)
        {
            //dao.InsertDataIntoDatabase(dataList);
            //dao.BulkInsertData(dataList);
            dao.InsertarDatosGA(dataList);
        }
    }
}
