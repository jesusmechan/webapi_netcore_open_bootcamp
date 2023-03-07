using Microsoft.AspNetCore.Mvc;
using Controladora;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace WebApi_OpenBootcamp.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        CtrUsuario ctr = null;
        public UsuarioController()
        {
            ctr = new CtrUsuario();
        }

        [HttpGet]
        public List<DtoUsuario> Listar_Usuario()
        {
            List<DtoUsuario> _lista = new List<DtoUsuario>();
            _lista = ctr.Usuario_Listar();
            return _lista;
        }

        
        //public DtoUsuario InicioSesion(DtoUsuario entidad)
        //{
        //    DtoUsuario data = new DtoUsuario();
        //    data = ctr.InicioSesion(entidad);
        //    return data;
        //}
    }
}
