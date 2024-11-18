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
    public class CtrEstudiante
    {
        DaoEstudiante _dao = null;
        ClaseResultado<DtoEstudiante> _resultado = null;

        public CtrEstudiante()
        {
            _dao = new DaoEstudiante();
            _resultado = new ClaseResultado<DtoEstudiante>();
        }

        public ClaseResultado<DtoEstudiante> listarEstudiante(filtrosBusqueda entidad)
        {
            try
            {
                return _dao.listarEstudiante(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ClaseResultado<DtoEstudiante> MNT_Estudiante(DtoEstudiante _entidad)
        {
            return _dao.MNT_Estudiante(_entidad);
        }
    }
}