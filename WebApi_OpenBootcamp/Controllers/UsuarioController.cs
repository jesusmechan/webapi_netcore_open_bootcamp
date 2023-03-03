using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Controladora;
using DTO;

namespace WebApi_OpenBootcamp.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
