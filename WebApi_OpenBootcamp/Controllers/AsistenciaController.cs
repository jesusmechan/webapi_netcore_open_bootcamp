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
    public class AsistenciaController : ControllerBase
    {
        CtrAsistencia ctr = null;
        ClaseResultado<DtoAsistencia> resultado = null;
        public AsistenciaController()
        {
            ctr = new CtrAsistencia();
            resultado = new ClaseResultado<DtoAsistencia>();
        }

        [HttpPost]
        public ClaseResultado<DtoAsistencia> MNT_Asistencia(DtoAsistencia _entidad)
        {
            try
            {
                resultado = ctr.MNT_Asistencia(_entidad);
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
