using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controladora
{
    public class CtrMarca
    {
        DaoMarca _dao = null;
        ClaseResultado<DtoMarca> _resultado = null;
        public CtrMarca() 
        {
            _dao = new DaoMarca();
            _resultado = new ClaseResultado<DtoMarca>();
        }

        public ClaseResultado<DtoMarca> Marca_Listar()
        {
            try
            {
                _resultado = _dao.Marca_Listar();
            }
            catch (Exception ex)
            {
                _resultado.HuboError = true;
                _resultado.Mensaje = ex.Message.ToString();
            }
            return _resultado;
        }

        public ClaseResultado<DtoMarca> Marca_Insertar_Actualizar(DtoMarca _entidad)
        {
            try
            {
                _resultado = _dao.Marca_Insertar_Actualizar(_entidad);
            }
            catch (Exception ex)
            {
                _resultado.HuboError = true;
                _resultado.Mensaje = ex.Message.ToString(); 
            }
            return _resultado;
        }

        public ClaseResultado<DtoMarca> Marca_Activar_Inactivar(DtoMarca _entidad)
        {
            try
            {
                _resultado = _dao.Marca_Activar_Inactivar(_entidad);
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
