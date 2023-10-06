using Microsoft.AspNetCore.Mvc;
using Controladora;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace WebApi_OpenBootcamp.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]/[action]")]
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

        //[HttpPost]
        //////public DtoUsuario InicioSesion(DtoUsuario entidad)
        //public DtoUsuario InicioSesion (string usuario, string password) 
        //{
        //    DtoUsuario data = new DtoUsuario();
        //    DtoUsuario entidad = new DtoUsuario();
        //    entidad.C_DNI = usuario;
        //    entidad.C_PASSWORD = password;
        //    data = ctr.InicioSesion(entidad);
        //    return data;
        //}
    }
}
