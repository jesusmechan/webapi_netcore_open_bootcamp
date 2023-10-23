using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;
using Newtonsoft.Json;

namespace Controladora
{
    public class CtrRol
    {
        DaoRol _dao = null;
        ClaseResultado<DtoRol> _resultado = null;
        public CtrRol()
        {
            _dao = new DaoRol();
            _resultado = new ClaseResultado<DtoRol>();
        }

        public List<DtoRol> Rol_Listar()
        {
            try
            {
                return _dao.Rol_Listar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ClaseResultado<DtoRol> Rol_Insertar_Actualizar(DtoRol _entidad)
        {
            try
            {
                _resultado = _dao.Rol_Insertar_Actualizar(_entidad);
            }
            catch (Exception ex)
            {
                _resultado.HuboError = true;
                _resultado.Mensaje = ex.Message.ToString();
            }
            return _resultado;
        }
        public ClaseResultado<DtoRol> Rol_Activar_Inactivar(DtoRol entidad)
        {
            try
            {
                _resultado = _dao.Rol_Activar_Inactivar(entidad);
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
