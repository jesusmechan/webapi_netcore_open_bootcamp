using Microsoft.AspNetCore.Mvc;
using Controladora;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace WebApi_OpenBootcamp.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class MenuController : ControllerBase
    {
        CtrMenu ctr = null;
        ClaseResultado<DtoMenu> resultado = null;
        public MenuController()
        {
            ctr = new CtrMenu();
            resultado = new ClaseResultado<DtoMenu>();
        }

        [HttpPost]
        public List<DtoMenu> Menu_Listar(DtoMenu entidad)
        {
            List<DtoMenu> _lista = new List<DtoMenu>();
            _lista = ctr.Menu_Listar(entidad);
            return _lista;
        }

        [HttpGet]
        public List<DtoMenu> Menu_Listar_Tabla()
        {
            return ctr.Menu_Listar_Tabla();
        }


        [HttpPost]
        public ClaseResultado<DtoMenu> Menu_Insertar_Actualizar(DtoMenu _entidad)
        {
            try
            {
                resultado = ctr.Menu_Insertar_Actualizar(_entidad);
            }
            catch (Exception ex)
            {
                resultado.HuboError = true;
                resultado.UltimoId = 0;
                resultado.Mensaje = ex.Message.ToString();
            }
            return resultado;
        }

        [HttpPost]
        public ClaseResultado<DtoMenu> Menu_Activar_Inactivar(DtoMenu _entidad)
        {
            resultado = ctr.Menu_Activar_Inactivar(_entidad);
            return resultado;
        }

    }
}
