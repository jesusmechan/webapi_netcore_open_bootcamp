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
    public class CtrReporteAsistencia
    {
        DaoReportes _dao = null;
        ClaseResultado<DtoReporteAsistencia> _resultado = null;
        ClaseResultado<DtoReporteAsistenciaXMes> _resRepXMEs = null;

        public CtrReporteAsistencia()
        {
            _dao = new DaoReportes();
            _resultado = new ClaseResultado<DtoReporteAsistencia>();
            _resRepXMEs = new ClaseResultado<DtoReporteAsistenciaXMes>();
        }

        public ClaseResultado<DtoReporteAsistencia> listarReporteAsistencia(filtrosReporteAsistencia entidad)
        {
            try
            {
                return _resultado =  _dao.listarReporteAsistencia(entidad);
            }
            catch (Exception ex)
            {
                _resultado.HuboError = true;
                _resultado.Mensaje = ex.Message.ToString();
            }
            return _resultado;
        }

        public ClaseResultado<DtoReporteAsistenciaXMes> listarReporteAsistenciaXMes(filtrosRepXMes entidad)
        {
            try
            {
                return _resRepXMEs = _dao.listarReporteAsistenciaXMes(entidad);
            }
            catch (Exception ex)
            {
                _resultado.HuboError = true;
                _resultado.Mensaje = ex.Message.ToString();
            }
            return _resRepXMEs;
        }
        

    }
}
