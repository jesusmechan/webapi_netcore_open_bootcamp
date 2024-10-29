using Microsoft.AspNetCore.Mvc;
using Controladora;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace WebApi_OpenBootcamp.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Authorize]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        CtrEspecialidad ctr = null;
        ClaseResultado<DtoEspecialidad> resultado = null;
        public PersonaController() 
        {
            ctr = new CtrEspecialidad();
            resultado = new ClaseResultado<DtoEspecialidad>();
        }

        [HttpGet]
        public ClaseResultado<DtoEspecialidad> Especialidad_Listar()
        {
            try
            {
                resultado = ctr.Especialidad_Listar();
                return resultado;
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
