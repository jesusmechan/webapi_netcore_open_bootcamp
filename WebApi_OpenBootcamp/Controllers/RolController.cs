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
    public class RolController : ControllerBase
    {
        CtrRol ctr = null;
        ClaseResultado<DtoRol> resultado = null;
        public RolController()
        {
            ctr = new CtrRol();
            resultado = new ClaseResultado<DtoRol>();
        }

        [HttpGet]
        public List<DtoRol> Rol_Listar()
        {
            List<DtoRol> _lista = new List<DtoRol>();
            _lista = ctr.Rol_Listar();
            return _lista;
        }

        [HttpPost]
        public ClaseResultado<DtoRol> Rol_Insertar_Actualizar(DtoRol _entidad)
        {
            try
            {
                resultado = ctr.Rol_Insertar_Actualizar(_entidad);
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
        public ClaseResultado<DtoRol> Rol_Activar_Inactivar(DtoRol _entidad)
        {

            try
            {
                resultado = ctr.Rol_Activar_Inactivar(_entidad);
            }
            catch (Exception ex)
            {
                resultado.HuboError = true;
                resultado.UltimoId = 0;
                resultado.Mensaje = ex.Message.ToString();
            }
            return resultado;
        }

    }
}
