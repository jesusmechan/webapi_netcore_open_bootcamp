using Microsoft.AspNetCore.Mvc;
using Controladora;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace WebApi_OpenBootcamp.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]/[action]")]
    //[Authorize]
    [ApiController]
    public class MenuController : ControllerBase
    {
        CtrMenu ctr = null;
        public MenuController()
        {
            ctr = new CtrMenu();
        }

        [HttpPost]
        public List<DtoMenu> Menu_Listar(DtoMenu entidad)
        {
            List<DtoMenu> _lista = new List<DtoMenu>();
            _lista = ctr.Menu_Listar(entidad);
            return _lista;
        }
    }
}
