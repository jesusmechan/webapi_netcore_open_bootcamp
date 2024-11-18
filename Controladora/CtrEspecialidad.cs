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
    public class CtrEspecialidad 
    { 

        DaoEspecialidad _dao = null;
        ClaseResultado<DtoEspecialidad> _resultado = null;

        public CtrEspecialidad()
        {
            _dao = new DaoEspecialidad();
            _resultado = new ClaseResultado<DtoEspecialidad>();
            
        }

        public ClaseResultado<DtoEspecialidad>  Especialidad_Listar ()
        {
            try
            {
                _resultado = _dao.Listar_Especialidad();
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
