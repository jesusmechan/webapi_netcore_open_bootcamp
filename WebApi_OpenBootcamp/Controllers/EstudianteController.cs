using Microsoft.AspNetCore.Mvc;
using Controladora;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.ConstrainedExecution;

namespace WebApi_OpenBootcamp.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Authorize]
    [ApiController]
    public class EstudianteController : ControllerBase
    {

        CtrEstudiante ctr = null;
        ClaseResultado<DtoEstudiante> resultado = null;
        public EstudianteController()
        {
            ctr = new CtrEstudiante();
            resultado = new ClaseResultado<DtoEstudiante>();
        }

        [HttpPost]
        public ClaseResultado<DtoEstudiante> listarEstudiante(filtrosBusqueda entidad)
        {
            try
            {
                resultado = ctr.listarEstudiante(entidad);
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

        [HttpPost]
        public ClaseResultado<DtoEstudiante> MNT_Estudiante(DtoEstudiante _entidad)
        {
            try
            {
                resultado = ctr.MNT_Estudiante(_entidad);
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
